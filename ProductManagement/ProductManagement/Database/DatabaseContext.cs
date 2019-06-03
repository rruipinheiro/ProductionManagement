using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Database.Models;

namespace ProductManagement.Database {
    public class DatabaseContext : IdentityDbContext {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

            public DbSet<Operador> Operador { get; set; }
            public DbSet<OrdemProducao> OrdemProducao { get; set; }
            public DbSet<Producao> Producao { get; set; }
            public DbSet<Defeito> Defeito { get; set; }
            public DbSet<Estado> Estado { get; set; }
            public DbSet<Maquina> Maquina { get; set; }
            public DbSet<Pausa> Pausa { get; set; }
            public DbSet<Registo> Registo { get; set; }
            public DbSet<Sensor> Sensor { get; set; }
            public DbSet<Sola> Sola { get; set; }

    }
}
