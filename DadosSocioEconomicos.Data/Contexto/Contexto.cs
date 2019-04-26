using DadosSocioEconomicos.Entidades.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DadosSocioEconomicos.Data.Contexto
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> opcoes) : base(opcoes) { }

        public DbSet<Nacao> Nacoes { get; set; }

        public DbSet<Indicador> Indicadores { get; set; }

        public DbSet<Comparativo> Comparativos { get; set; }
    }
}
