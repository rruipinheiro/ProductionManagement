using System;
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
                Data = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                SolaId = 1,
                MaquinaId = 1,
                OperadorId = 1
            });

            modelBuilder.Entity<OrdemProducao>().HasData(new OrdemProducao() {
                Id = 2,
                Data = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                SolaId = 1,
                MaquinaId = 2,
                OperadorId = 1
            });

            modelBuilder.Entity<Sola>().HasData(new Sola() {
                Id = 1,
                Nome = "Sola Trade",
            });

            modelBuilder.Entity<Producao>().HasData(new Producao() {
                Id = 1,
                Tamanho = 40,
                Quantidade = 300,
                Inicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                Fim = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                OrdemProducaoId = 1,
                DefeitoId = 1,
                EstadoId = 1,
            });

            modelBuilder.Entity<Producao>().HasData(new Producao() {
                Id = 2,
                Tamanho = 20,
                Quantidade = 300,
                Inicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                Fim = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                OrdemProducaoId = 1,
                DefeitoId = 2,
                EstadoId = 1,
            });

            modelBuilder.Entity<Defeito>().HasData(new Defeito()
            {
                Id = 1,
                Nome = "Sola incompleta"
            });

            modelBuilder.Entity<Defeito>().HasData(new Defeito()
            {
                Id = 2,
                Nome = "Sola chupada"
            });

            modelBuilder.Entity<Defeito>().HasData(new Defeito()
            {
                Id = 3,
                Nome = "Sola mal fechada"
            });

            modelBuilder.Entity<Defeito>().HasData(new Defeito()
            {
                Id = 4,
                Nome = "Sola com rebarba"
            });

            modelBuilder.Entity<Defeito>().HasData(new Defeito()
            {
                Id = 5,
                Nome = "Sola com marca no ponto de injeção"
            });

            modelBuilder.Entity<Defeito>().HasData(new Defeito()
            {
                Id = 6,
                Nome = "Sola com riscos"
            });


            modelBuilder.Entity<Defeito>().HasData(new Defeito()
            {
                Id = 7,
                Nome = "Solas com arrastamento"
            });


            modelBuilder.Entity<Defeito>().HasData(new Defeito()
            {
                Id = 8,
                Nome = "Solas delaminadas"
            });


            modelBuilder.Entity<Defeito>().HasData(new Defeito()
            {
                Id = 9,
                Nome = "Solas cor incorreta"
            });


            modelBuilder.Entity<Defeito>().HasData(new Defeito()
            {
                Id = 10,
                Nome = "Solas com bolhas de ar/gás"
            });


            modelBuilder.Entity<Defeito>().HasData(new Defeito()
            {
                Id = 11,
                Nome = "Solas com contaminações"
            });


            modelBuilder.Entity<Defeito>().HasData(new Defeito()
            {
                Id = 12,
                Nome = "Sola deformada"
            });

            modelBuilder.Entity<Estado>().HasData(new Estado()
            {
                Id = 1,
                Nome = "A Produzir"
            });

            modelBuilder.Entity<Estado>().HasData(new Estado()
            {
                Id = 2,
                Nome = "Produzido"
            });

            modelBuilder.Entity<Maquina>().HasData(new Maquina()
            {
                Id = 1,
                Nome = "Maquina 1"
            });

            modelBuilder.Entity<Maquina>().HasData(new Maquina()
            {
                Id = 2,
                Nome = "Maquina 2"
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
                Inicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                Fim = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                OperadorId = 1,
            });

            modelBuilder.Entity<Sensor>().HasData(new Sensor()
            {
                Id = 1,
                Nome = "Temperatura",
                MaquinaId = 1,
            });

            modelBuilder.Entity<Sensor>().HasData(new Sensor()
            {
                Id = 2,
                Nome = "Pressão",
                MaquinaId = 1,
            });


            modelBuilder.Entity<Sensor>().HasData(new Sensor()
            {
                Id = 3,
                Nome = "Velocidade de Injeção",
                MaquinaId = 1,
            });


            modelBuilder.Entity<Sensor>().HasData(new Sensor()
            {
                Id = 4,
                Nome = "Velocidade de Carregamento",
                MaquinaId = 1,
            });

            modelBuilder.Entity<Registo>().HasData(new Registo()
            {
                Id = 1,
                Valor = 200,
                Data = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                SensorId = 1,
            });

            modelBuilder.Entity<Registo>().HasData(new Registo()
            {
                Id = 2,
                Valor = 50,
                Data = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                SensorId = 2,
            });

            modelBuilder.Entity<Registo>().HasData(new Registo()
            {
                Id = 3,
                Valor = 40,
                Data = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                SensorId = 3,
            });

            modelBuilder.Entity<Registo>().HasData(new Registo()
            {
                Id = 4,
                Valor = 20,
                Data = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                SensorId = 4,
            });

        }

    }
}
