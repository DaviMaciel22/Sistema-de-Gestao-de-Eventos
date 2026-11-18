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
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

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
            // Composição: ao excluir a conferência, as palestras dela são excluídas junto (Cascade)
            modelBuilder.Entity<Conferencia>()
                .HasMany(c => c.Palestras)
                .WithOne(p => p.Conferencia)
                .HasForeignKey(p => p.ConferenciaId)
                .OnDelete(DeleteBehavior.Cascade);

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


            modelBuilder.Entity<Empresa>().HasData(
                   new Empresa
                   {
                       OrganizadorId = 1,
                       Numero = "27999998888",
                       Email = "contato@uvveventos.com.br",
                       RazaoSocial = "UVV",
                       CNPJ = "12.345.678/0001-90"
                   }
            );

            modelBuilder.Entity<PessoaFisica>().HasData(
                new PessoaFisica
                {
                    OrganizadorId = 2,
                    Numero = "27999998888",
                    Email = "contato@uvveventos.com.br",
                    Nome = "Júnior da Costa",
                    CPF = "123.456.789-00"
                }
            );

            modelBuilder.Entity<Local>().HasData(
                new Local
                {
                    LocalId = 1,
                    Nome = "Auditório UVV",
                    Capacidade = 100,
                    Rua = "Rua da UVV",
                    Numero = 123,
                    Bairro = "Centro",
                    Cidade = "Vila Velha",
                    Estado = "ES",
                    Pais = "Brasil"
                },
                new Local
                {
                    LocalId = 2,
                    Nome = "Centro de Convenções",
                    Capacidade = 300,
                    Rua = "Av. Vitória",
                    Numero = 456,
                    Bairro = "Praia do Canto",
                    Cidade = "Vitória",
                    Estado = "ES",
                    Pais = "Brasil"
                },
                new Local
                {
                    LocalId = 3,
                    Nome = "Lab 3 UVV",
                    Capacidade = 30,
                    Rua = "Rua da UVV",
                    Numero = 123,
                    Bairro = "Centro",
                    Cidade = "Vila Velha",
                    Estado = "ES",
                    Pais = "Brasil"
                }
            );

            modelBuilder.Entity<Workshop>().HasData(
                new Workshop
                {
                    EventoId = 1,
                    Titulo = "Workshop de C#",
                    Descricao = "Desenvolvimento desktop prático com WPF e Entity Framework Core.",
                    DataEHora = new DateTime(2026, 11, 20, 19, 0, 0),
                    QntInscricoes = 25,
                    VagasRestantes = 15,
                    Valor = 50.00,
                    Duracao = "3h",
                    Status = "Disponível",
                    OrganizadorId = 1,
                    LocalId = 1,
                    PalestraId = 1
                },
                new Workshop
                {
                    EventoId = 3,
                    Titulo = "Workshop de UX",
                    Descricao = "Prototipação de interfaces e experiência do usuário.",
                    DataEHora = new DateTime(2026, 12, 2, 14, 0, 0),
                    QntInscricoes = 30,
                    VagasRestantes = 0,
                    Valor = 0.00,
                    Duracao = "2h",
                    Status = "Disponível",
                    OrganizadorId = 1,
                    LocalId = 3
                }
            );

            modelBuilder.Entity<Conferencia>().HasData(
                new Conferencia
                {
                    EventoId = 2,
                    Titulo = "Conferência Tech",
                    Descricao = "Palestras sobre arquitetura de software, cloud e inteligência artificial.",
                    DataEHora = new DateTime(2026, 11, 28, 9, 0, 0),
                    QntInscricoes = 112,
                    VagasRestantes = 188,
                    Valor = 120.00,
                    Duracao = "8h",
                    Status = "Disponível",
                    OrganizadorId = 1,
                    LocalId = 2
                }
            );

            modelBuilder.Entity<Palestra>().HasData(
                new Palestra
                {
                    PalestraId = 1,
                    Duracao = 3.0,
                    Nome = "Fundamentos de C#",
                    ConferenciaId = 2,
                    WorkshopId = null,
                    PalestranteId = 1
                },
                new Palestra
                {
                    PalestraId = 2,
                    Duracao = 2.0,
                    Nome = "EF Core na Prática",
                    ConferenciaId = 2,
                    WorkshopId = null,
                    PalestranteId = 2
                }
            );

            modelBuilder.Entity<Palestrante>().HasData(
                new Palestrante
                {
                    PalestranteId = 1,
                    Nome = "Ana Lima",
                    CPF = "123.456.789-00",
                    Email = "analima@email.com"
                },
                new Palestrante
                {
                    PalestranteId = 2,
                    Nome = "Bruno Reis",
                    CPF = "987.654.321-00",
                    Email = "brunoreis@email.com"
                }
            );

            modelBuilder.Entity<Participante>().HasData(
                new Participante
                {
                    ParticipanteId = 1,
                    Nome = "Ana Souza",
                    CPF = "123.456.789-00",
                    Email = "anasouza@email.com",
                    // senha de teste: 123456
                    SenhaHash = "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92"
                }
            );

            modelBuilder.Entity<Inscricao>().HasData(
                new Inscricao
                {
                    InscricaoId = 1,
                    EventoId = 1,
                    ParticipanteId = 1
                },
                new Inscricao
                {
                    InscricaoId = 2,
                    EventoId = 2,
                    ParticipanteId = 1
                }
            );



        }

    }
}
