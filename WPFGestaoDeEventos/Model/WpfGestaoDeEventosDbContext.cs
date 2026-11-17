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

            //Relação 1:N entre Organizador e Evento
            modelBuilder.Entity<Organizador>()
                .HasMany(o => o.Eventos)
                .WithOne(e => e.Organizador)
                .HasForeignKey(e => e.OrganizadorId);

            //Relação 1:N entre Evento e Inscricao
            modelBuilder.Entity<Evento>()
                .HasMany(e => e.Inscricoes)
                .WithOne(i => i.Evento)
                .HasForeignKey(i => i.EventoId);

            //Relação 1:N entre Participante e Inscricao
            modelBuilder.Entity<Participante>()
                .HasMany(p => p.Inscricoes)
                .WithOne(i => i.Participante)
                .HasForeignKey(i => i.ParticipanteId);

            //Relação 1:1 entre Certificado e Inscricao
            modelBuilder.Entity<Certificado>()
                .HasOne(c => c.Inscricao)
                .WithOne(i => i.Certificado)
                .HasForeignKey<Certificado>(c => c.InscricaoId);

            //Relação 1:N entre Local e Evento
            modelBuilder.Entity<Local>()
                .HasMany(l => l.Eventos)
                .WithOne(e => e.Local)
                .HasForeignKey(e => e.LocalId);

            //Relação 1:N entre Conferencia e Palestra
            modelBuilder.Entity<Conferencia>()
                .HasMany(c => c.Palestras)
                .WithOne(p => p.Conferencia)
                .HasForeignKey(p => p.ConferenciaId);

            //Relação 1:1 entre Workshop e Palestra
            modelBuilder.Entity<Workshop>()
                .HasOne(w => w.Palestra)
                .WithOne(p => p.Workshop)
                .HasForeignKey<Palestra>(p => p.WorkshopId);

            //Relação 1:N entre Palestrante e Palestra
            modelBuilder.Entity<Palestrante>()
                .HasMany(palestrante => palestrante.Palestras)
                .WithOne(palestra => palestra.Palestrante)
                .HasForeignKey(palestra => palestra.PalestranteId);

        }
    }
}
