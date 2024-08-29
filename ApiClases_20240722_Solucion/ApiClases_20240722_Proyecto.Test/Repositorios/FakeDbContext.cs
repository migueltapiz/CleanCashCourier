using ApiClases_20270722_Proyecto.ContextoCarpeta;
using ApiClases_20270722_Proyecto.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClases_20240722_Proyecto.Test.Repositorios
{
    public class FakeDbContext : Contexto
    {
        public FakeDbContext(DbContextOptions<Contexto> options)
            : base(options)
        {
        }

        public DbSet<Transaccion> Transacciones { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaccion>().HasKey(t => t.Id);
            modelBuilder.Ignore<ConteoResult>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
