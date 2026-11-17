using Microsoft.EntityFrameworkCore;
using WPFGestaoDeEventos.Model;

namespace WPFGestaoDeEventos.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Evento> Eventos { get; set; } = null!;
        public DbSet<Organizador> Organizadores { get; set; } = null!;
        public DbSet<Local> Locais { get; set; } = null!;
        public DbSet<Palestra> Palestras { get; set; } = null!;
        public DbSet<Palestrante> Palestrantes { get; set; } = null!;
        public DbSet<Participante> Participantes { get; set; } = null!;
        public DbSet<Inscricao> Inscricoes { get; set; } = null!;
        public DbSet<Certificado> Certificados { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // SQL Server LocalDB (vem com o Visual Studio), como descrito no README
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=GestaoEventos;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Herança: Evento -> Conferencia / Workshop (uma tabela, coluna "Tipo")
            modelBuilder.Entity<Evento>(evento =>
            {
                evento.HasKey(e => e.IdEvento);

                evento.HasDiscriminator<string>("Tipo")
                    .HasValue<Conferencia>("Conferencia")
                    .HasValue<Workshop>("Workshop");

                // Agregação: o Local continua existindo se o evento for removido
                evento.HasOne(e => e.Local)
                    .WithMany(l => l.Eventos)
                    .HasForeignKey(e => e.LocalId)
                    .OnDelete(DeleteBehavior.Restrict);

                // 1xN: Organizador -> Evento
                evento.HasOne(e => e.Organizador)
                    .WithMany(o => o.Eventos)
                    .HasForeignKey(e => e.OrganizadorId)
                    .OnDelete(DeleteBehavior.Restrict);

                // 1xN: Evento -> Inscricao
                evento.HasMany(e => e.Inscricoes)
                    .WithOne(i => i.Evento)
                    .HasForeignKey(i => i.EventoId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Composição: apagar o evento apaga as palestras
                evento.HasMany(e => e.Palestras)
                    .WithOne(p => p.Evento)
                    .HasForeignKey(p => p.EventoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Herança: Organizador -> Empresa / PessoaFisica
            modelBuilder.Entity<Organizador>(organizador =>
            {
                organizador.HasKey(o => o.IdOrganizador);

                organizador.HasDiscriminator<string>("TipoOrganizador")
                    .HasValue<Empresa>("Empresa")
                    .HasValue<PessoaFisica>("PessoaFisica");
            });

            modelBuilder.Entity<Local>().HasKey(l => l.IdLocal);

            modelBuilder.Entity<Palestra>(palestra =>
            {
                palestra.HasKey(p => p.IdPalestra);

                // 1xN: Palestrante -> Palestra
                palestra.HasOne(p => p.Palestrante)
                    .WithMany(p => p.Palestras)
                    .HasForeignKey(p => p.PalestranteId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Palestrante>().HasKey(p => p.IdPalestrante);

            modelBuilder.Entity<Participante>(participante =>
            {
                participante.HasKey(p => p.IdParticipante);
                participante.Property(p => p.Email).HasMaxLength(200);
                participante.HasIndex(p => p.Email).IsUnique();
            });

            // NxN: Participante <-> Evento, resolvido pela classe de associação Inscricao
            modelBuilder.Entity<Inscricao>(inscricao =>
            {
                inscricao.HasKey(i => i.IdInscricao);

                // O enum fica legível no banco (Confirmado, Cancelado, Utilizado)
                inscricao.Property(i => i.Status).HasConversion<string>();

                // 1xN: Participante -> Inscricao
                inscricao.HasOne(i => i.Participante)
                    .WithMany(p => p.Inscricoes)
                    .HasForeignKey(i => i.ParticipanteId)
                    .OnDelete(DeleteBehavior.Cascade);

                // 1x1: Inscricao <-> Certificado
                inscricao.HasOne(i => i.Certificado)
                    .WithOne(c => c.Inscricao)
                    .HasForeignKey<Certificado>(c => c.InscricaoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Certificado>().HasKey(c => c.IdCertificado);
        }

        // Aplica as migrations e cria os dados de teste na primeira execução
        public void Inicializar()
        {
            Database.Migrate();

            if (Participantes.Any())
            {
                return;
            }

            CriarDadosDeTeste();
        }

        private void CriarDadosDeTeste()
        {
            var uvv = new Empresa
            {
                Nome = "UVV",
                Numero = "(27) 3000-0000",
                Email = "eventos@uvv.br",
                CNPJ = "00.000.000/0001-00",
                RazaoSocial = "Universidade Vila Velha"
            };

            var auditorio = new Local
            {
                Nome = "Auditório UVV",
                Capacidade = 300,
                Cidade = "Vila Velha",
                Estado = "ES",
                Pais = "Brasil"
            };

            var centro = new Local
            {
                Nome = "Centro de Convenções",
                Capacidade = 1000,
                Cidade = "Vitória",
                Estado = "ES",
                Pais = "Brasil"
            };

            var lab3 = new Local
            {
                Nome = "Lab 3 UVV",
                Capacidade = 40,
                Cidade = "Vila Velha",
                Estado = "ES",
                Pais = "Brasil"
            };

            var workshopCSharp = new Workshop
            {
                Titulo = "Workshop de C#",
                Descricao = "Introdução a C# e .NET com exercícios práticos.",
                DataEHora = new DateTime(2026, 11, 20, 19, 0, 0),
                Capacidade = 16,
                Valor = 50,
                Duracao = "3h",
                Local = auditorio
            };

            var conferenciaTech = new Conferencia
            {
                Titulo = "Conferência Tech",
                Descricao = "Um dia de palestras sobre tecnologia e mercado.",
                DataEHora = new DateTime(2026, 11, 28, 9, 0, 0),
                Capacidade = 189,
                Valor = 120,
                Duracao = "8h",
                Local = centro
            };

            var workshopUx = new Workshop
            {
                Titulo = "Workshop de UX",
                Descricao = "Fundamentos de experiência do usuário.",
                DataEHora = new DateTime(2026, 12, 2, 14, 0, 0),
                Capacidade = 1,
                Valor = 0,
                Duracao = "2h",
                Local = lab3
            };

            var semanaTi = new Conferencia
            {
                Titulo = "Semana de TI",
                Descricao = "Semana de atividades da área de tecnologia.",
                DataEHora = new DateTime(2026, 11, 5, 18, 0, 0),
                Capacidade = 100,
                Valor = 0,
                Duracao = "5h",
                Status = "Encerrado",
                Local = auditorio
            };

            uvv.CriarEvento(workshopCSharp);
            uvv.CriarEvento(conferenciaTech);
            uvv.CriarEvento(workshopUx);
            uvv.CriarEvento(semanaTi);

            var anaLima = new Palestrante { Nome = "Ana Lima", CPF = "333.333.333-33", Email = "ana.lima@email.com" };
            var brunoReis = new Palestrante { Nome = "Bruno Reis", CPF = "444.444.444-44", Email = "bruno.reis@email.com" };

            var fundamentos = new Palestra
            {
                Titulo = "Fundamentos de C#",
                DataEHora = new DateTime(2026, 11, 20, 19, 0, 0),
                Duracao = 1.5,
                Evento = workshopCSharp,
                Palestrante = anaLima
            };

            var efCore = new Palestra
            {
                Titulo = "EF Core na prática",
                DataEHora = new DateTime(2026, 11, 20, 20, 30, 0),
                Duracao = 1.5,
                Evento = workshopCSharp,
                Palestrante = brunoReis
            };

            var ia = new Palestra
            {
                Titulo = "IA aplicada ao desenvolvimento",
                DataEHora = new DateTime(2026, 11, 28, 10, 0, 0),
                Duracao = 2,
                Evento = conferenciaTech,
                Palestrante = anaLima
            };

            var ana = new Participante { Nome = "Ana Souza", CPF = "111.111.111-11", Email = "ana.souza@email.com" };
            ana.DefinirSenha("123456");

            var carlos = new Participante { Nome = "Carlos Lima", CPF = "222.222.222-22", Email = "carlos.lima@email.com" };
            carlos.DefinirSenha("123456");

            var inscricaoCSharp = new Inscricao
            {
                Participante = ana,
                Evento = workshopCSharp
            };

            var inscricaoConferencia = new Inscricao
            {
                Participante = ana,
                Evento = conferenciaTech
            };

            var inscricaoSemanaTi = new Inscricao
            {
                Participante = ana,
                Evento = semanaTi,
                Status = StatusInscricao.Utilizado
            };

            // Deixa o Workshop de UX esgotado (1 vaga, 1 inscrição)
            var inscricaoUx = new Inscricao
            {
                Participante = carlos,
                Evento = workshopUx
            };

            Organizadores.Add(uvv);
            Palestras.AddRange(fundamentos, efCore, ia);
            Inscricoes.AddRange(inscricaoCSharp, inscricaoConferencia, inscricaoSemanaTi, inscricaoUx);

            SaveChanges();
        }
    }
}
