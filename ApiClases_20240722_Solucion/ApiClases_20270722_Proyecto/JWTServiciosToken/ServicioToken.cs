namespace ApiClases_20270722_Proyecto.JWTServiciosToken;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
    public class ServicioToken : IServicioToken
    {

        private readonly IConfiguration _configuration;

        public ServicioToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

    public string GenerateJwtToken(UsuarioAplicacion user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName),
            // Agregar otros claims según sea necesario
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
