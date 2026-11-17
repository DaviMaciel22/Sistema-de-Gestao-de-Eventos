using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WPFGestaoDeEventos.Model
{
    internal class WpfGestaoDeEventosDbContext : DbContext
    {
        public DbSet<Certificado> Certificados { get; set; } = null!;
        public DbSet<Evento> Eventos { get; set; } = null!;
        public DbSet<Conferencia> Conferencias { get; set; } = null!;
        public DbSet<Workshop> Workshops { get; set; } = null!;
        public DbSet<Organizador> Organizadores { get; set; } = null!;
        public DbSet<Empresa> Empresas { get; set; } = null!;
        public DbSet<PessoaFisica> PessoaFisicas { get; set; } = null!;
        public DbSet<Inscricao> Inscricoes { get; set; } = null!;
        public DbSet<Local> Locais { get; set; } = null!;
        public DbSet<Palestra> Palestras { get; set; } = null!;
        public DbSet<Palestrante> Palestrantes { get; set; } = null!;
        public DbSet<Participante> Participantes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Organizador>()
                .HasMany(o => o.Eventos)
                .WithOne(e => e.Organizador)
                .HasForeignKey(e => e.OrganizadorId);

            modelBuilder.Entity<Evento>()
                .HasMany(e => e.Inscricoes)
                .WithOne(i => i.Evento)
                .HasForeignKey(i => i.EventoId);

            modelBuilder.Entity<Participante>()
                .HasMany(p => p.Inscricoes)
                .WithOne(i => i.Participante)
                .HasForeignKey(i => i.ParticipanteId);

            modelBuilder.Entity<Participante>()
                .HasMany(p => p.Inscricoes)
                .WithOne(i => i.Participante)
                .HasForeignKey(i => i.ParticipanteId);

        }
    }
}
