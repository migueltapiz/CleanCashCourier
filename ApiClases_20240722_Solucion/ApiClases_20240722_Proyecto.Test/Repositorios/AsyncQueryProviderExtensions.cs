using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

// El archivo AsyncQueryProviderExtensions.cs contiene una extensión
// para crear un Mock de DbSet<T> que soporte operaciones asincrónicas.
// Esto es útil para pruebas unitarias cuando se trabaja con Entity Framework Core,
// ya que permite simular consultas de base de datos de manera asincrónica.
public static class AsyncQueryProviderExtensions
{
    /// <summary>
    /// Este método de extensión toma una colección de elementos 
    /// y crea un Mock de DbSet<T> que puede ser usado en pruebas unitarias. 
    /// Configura el Mock para que soporte operaciones de consulta asincrónicas.
    /// 1. Convertir a IQueryable: Convierte la colección de elementos a IQueryable<T>.
    /// 2. Crear Mock de DbSet: Crea un Mock de DbSet<T>.
    /// 3. Configurar IQueryable: Configura el Mock para que implemente IQueryable<T>, incluyendo el proveedor de consultas, la expresión, el tipo de elementos y el enumerador.
    /// 4. Configurar IAsyncEnumerable: Configura el Mock para que implemente IAsyncEnumerable<T>, incluyendo el enumerador asincrónico.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="elements"></param>
    /// <returns></returns>
    public static Mock<DbSet<T>> CreateDbSetMock<T>(this IEnumerable<T> elements) where T : class
    {
        var queryable = elements.AsQueryable();

        var dbSetMock = new Mock<DbSet<T>>();

        dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<T>(queryable.Provider));
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

        dbSetMock.As<IAsyncEnumerable<T>>()
                .Setup(m => m.GetAsyncEnumerator(default))
                .Returns(new TestAsyncEnumerator<T>(queryable.GetEnumerator()));

        return dbSetMock;
    }
}

/// <summary>
/// Esta clase implementa IAsyncQueryProvider 
/// y delega las operaciones de consulta al proveedor de consultas interno (_inner). 
/// También proporciona métodos para crear consultas asincrónicas.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
{
    private readonly IQueryProvider _inner;

    /// <summary>
    /// Constructor: Inicializa el proveedor interno.
    /// </summary>
    /// <param name="inner"></param>
    public TestAsyncQueryProvider(IQueryProvider inner)
    {
        _inner = inner;
    }

    /// <summary>
    /// Crea una consulta a partir de la expresión especificada.
    /// </summary>
    /// <param name="expression">La expresión que representa la consulta.</param>
    /// <returns>Un objeto IQueryable que representa la consulta.</returns>
    public IQueryable CreateQuery(Expression expression)
    {
        return new TestAsyncEnumerable<TEntity>(expression);
    }

    /// <summary>
    /// Crea una consulta a partir de la expresión especificada.
    /// </summary>
    /// <typeparam name="TElement">El tipo de los elementos en la consulta.</typeparam>
    /// <param name="expression">La expresión que representa la consulta.</param>
    /// <returns>Un objeto IQueryable que representa la consulta.</returns>
    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
    {
        return new TestAsyncEnumerable<TElement>(expression);
    }

    /// <summary>
    /// Ejecuta la expresión especificada.
    /// </summary>
    /// <param name="expression">La expresión a ejecutar.</param>
    /// <returns>El resultado de la ejecución.</returns>
    public object Execute(Expression expression)
    {
        return _inner.Execute(expression);
    }

    /// <summary>
    /// Ejecuta la expresión especificada.
    /// </summary>
    /// <typeparam name="TResult">El tipo del resultado.</typeparam>
    /// <param name="expression">La expresión a ejecutar.</param>
    /// <returns>El resultado de la ejecución.</returns>
    public TResult Execute<TResult>(Expression expression)
    {
        return _inner.Execute<TResult>(expression);
    }

    /// <summary>
    /// Ejecuta la expresión especificada de manera asincrónica.
    /// </summary>
    /// <typeparam name="TResult">El tipo del resultado.</typeparam>
    /// <param name="expression">La expresión a ejecutar.</param>
    /// <returns>Un enumerable asincrónico de tipo TResult.</returns>
    public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
    {
        return new TestAsyncEnumerable<TResult>(expression);
    }

    /// <summary>
    /// Ejecuta la expresión especificada de manera asincrónica.
    /// </summary>
    /// <typeparam name="TResult">El tipo del resultado.</typeparam>
    /// <param name="expression">La expresión a ejecutar.</param>
    /// <param name="cancellationToken">El token de cancelación.</param>
    /// <returns>El resultado de la ejecución.</returns>
    public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
    {
        return _inner.Execute<TResult>(expression);
    }
}

/// <summary>
/// Representa una implementación de prueba de un enumerable asincrónico 
/// que puede ser utilizado en pruebas unitarias.
/// </summary>
/// <typeparam name="T">El tipo de los elementos en el enumerable.</typeparam>
public class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="TestAsyncEnumerable{T}"/>.
    /// </summary>
    /// <param name="enumerable">El enumerable que se usará 
    /// como fuente de elementos.</param>
    public TestAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="TestAsyncEnumerable{T}"/>.
    /// </summary>
    /// <param name="expression">La expresión que representa la consulta.</param>
    public TestAsyncEnumerable(Expression expression) : base(expression)
    {
    }

    /// <summary>
    /// Obtiene un enumerador asincrónico que se puede usar para iterar a través de la colección.
    /// </summary>
    /// <param name="cancellationToken">El token de cancelación.</param>
    /// <returns>Un enumerador asincrónico sobre la colección.</returns>
    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    }

    /// <summary>
    /// Obtiene el proveedor de consultas que está asociado con esta fuente de datos.
    /// </summary>
    IQueryProvider IQueryable.Provider => new TestAsyncQueryProvider<T>(this);
}

/// <summary>
/// Representa un enumerador asincrónico que puede ser utilizado en pruebas unitarias.
/// </summary>
/// <typeparam name="T">El tipo de los elementos en el enumerador.</typeparam>
public class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
{
    private readonly IEnumerator<T> _inner;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="TestAsyncEnumerator{T}"/>.
    /// </summary>
    /// <param name="inner">El enumerador interno.</param>
    public TestAsyncEnumerator(IEnumerator<T> inner)
    {
        _inner = inner;
    }

    /// <summary>
    /// Libera los recursos utilizados por el enumerador de manera asincrónica.
    /// </summary>
    /// <returns>Una tarea que representa la operación asincrónica.</returns>
    public ValueTask DisposeAsync()
    {
        _inner.Dispose();
        return ValueTask.CompletedTask;
    }

    /// <summary>
    /// Mueve el enumerador asincrónicamente al siguiente elemento de la colección.
    /// </summary>
    /// <returns>Un <see cref="ValueTask{TResult}"/> que representa 
    /// la operación asincrónica. El resultado de la tarea contiene 
    /// un valor <see cref="bool"/> que indica 
    /// si el enumerador se movió con éxito al siguiente elemento.</returns>
    public ValueTask<bool> MoveNextAsync()
    {
        return new ValueTask<bool>(_inner.MoveNext());
    }

    /// <summary>
    /// Obtiene el elemento actual en el enumerador.
    /// </summary>
    public T Current => _inner.Current;
}