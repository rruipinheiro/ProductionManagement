using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            base.OnModelCreating(modelBuilder);

            var hasher = new PasswordHasher<User>();

            modelBuilder.Entity<User>().ToTable("User").Property(p => p.Id).HasColumnName("UserId");

            modelBuilder.Entity<User>().HasData(new User() {
                Id = "1",
                UserName = "rui@gmail.com",
                NormalizedUserName = "RUI@GMAIL.COM",
                Email = "rui@gmail.com",
                NormalizedEmail = "RUI@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "123456"),
                SecurityStamp = string.Empty,
                OperadorId = 1
            });

            modelBuilder.Entity<OrdemProducao>().HasData(new OrdemProducao() {
                Id = 1,
                Tamanho = 40,
                Quantidade = 300,
                Data = new DateTime(),
                SolaId = 1,
                MaquinaId = 1,
                OperadorId = 1
            });

            modelBuilder.Entity<Sola>().HasData(new Sola() {
                Id = 1,
                Nome = "Sola Trade",
            });

            modelBuilder.Entity<Producao>().HasData(new Producao()
            {
                Id = 1,
                Inicio = new DateTime(),
                Fim = new DateTime(),
                OrdemProducaoId = 1,
                DefeitoId = 1,
                EstadoId = 1,
            });

            modelBuilder.Entity<Defeito>().HasData(new Defeito()
            {
                Id = 1,
                Nome = "Sola incompleta",
                Descricao = "Sola incompleta"
            });

            modelBuilder.Entity<Estado>().HasData(new Estado()
            {
                Id = 1,
            });

            modelBuilder.Entity<Maquina>().HasData(new Maquina()
            {
                Id = 1,
                Nome = "Maquina"
            });

            modelBuilder.Entity<Operador>().HasData(new Operador()
            {
                Id = 1,
                Nome = "Manuel Silva",
                Numero = 123456,
                UserId = 1
            });

            modelBuilder.Entity<Pausa>().HasData(new Pausa()
            {
                Id = 1,
                Inicio = new DateTime(),
                Fim = new DateTime(),
                OperadorId = 1,
            });

            modelBuilder.Entity<Registo>().HasData(new Registo()
            {
                Id = 1,
                Valor = 12,
                Data = new DateTime(),
                SensorId = 1,
            });

            modelBuilder.Entity<Sensor>().HasData(new Sensor()
            {
                Id = 1,
                Nome = "Temperatura",
                MaquinaId = 1,
            });
    }

    }
}
