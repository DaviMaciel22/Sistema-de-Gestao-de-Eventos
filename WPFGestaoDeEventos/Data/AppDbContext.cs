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
        public DbSet<Pagamento> Pagamentos { get; set; } = null!;
        public DbSet<Certificado> Certificados { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Banco SQLite em arquivo, criado na pasta de execução do app
            optionsBuilder.UseSqlite("Data Source=gestaoeventos.db");
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
                    .WithMany()
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

            // NxN: Palestra <-> Palestrante (tabela de junção PalestraPalestrante)
            modelBuilder.Entity<Palestra>(palestra =>
            {
                palestra.HasKey(p => p.IdPalestra);

                palestra.HasMany(p => p.Palestrantes)
                    .WithMany(p => p.Palestras)
                    .UsingEntity(juncao => juncao.ToTable("PalestraPalestrante"));
            });

            modelBuilder.Entity<Palestrante>().HasKey(p => p.IdPalestrante);

            modelBuilder.Entity<Participante>(participante =>
            {
                participante.HasKey(p => p.IdParticipante);
                participante.HasIndex(p => p.Email).IsUnique();
            });

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

                // 1x1: Inscricao <-> Pagamento
                inscricao.HasOne(i => i.Pagamento)
                    .WithOne(p => p.Inscricao)
                    .HasForeignKey<Pagamento>(p => p.InscricaoId)
                    .OnDelete(DeleteBehavior.Cascade);

                // 1x1: Inscricao <-> Certificado
                inscricao.HasOne(i => i.Certificado)
                    .WithOne(c => c.Inscricao)
                    .HasForeignKey<Certificado>(c => c.InscricaoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Herança: Pagamento -> Pix / Cartao / Boleto
            modelBuilder.Entity<Pagamento>().HasKey(p => p.IdPagamento);
            modelBuilder.Entity<PagamentoPix>();
            modelBuilder.Entity<PagamentoCartao>();
            modelBuilder.Entity<PagamentoBoleto>();

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
                Cnpj = "00.000.000/0001-00"
            };

            var auditorio = new Local { Nome = "Auditório UVV", Cidade = "Vila Velha", Estado = "ES" };
            var centro = new Local { Nome = "Centro de Convenções", Cidade = "Vitória", Estado = "ES" };
            var lab3 = new Local { Nome = "Lab 3 UVV", Cidade = "Vila Velha", Estado = "ES" };

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

            var anaLima = new Palestrante { Nome = "Ana Lima" };
            var brunoReis = new Palestrante { Nome = "Bruno Reis" };

            var fundamentos = new Palestra
            {
                Titulo = "Fundamentos de C#",
                Horario = new DateTime(2026, 11, 20, 19, 0, 0),
                Evento = workshopCSharp,
                Palestrantes = { anaLima }
            };

            var efCore = new Palestra
            {
                Titulo = "EF Core na prática",
                Horario = new DateTime(2026, 11, 20, 20, 30, 0),
                Evento = workshopCSharp,
                Palestrantes = { brunoReis }
            };

            // Palestra com dois palestrantes (exemplo do NxN)
            var ia = new Palestra
            {
                Titulo = "IA aplicada ao desenvolvimento",
                Horario = new DateTime(2026, 11, 28, 10, 0, 0),
                Evento = conferenciaTech,
                Palestrantes = { anaLima, brunoReis }
            };

            var ana = new Participante { Nome = "Ana Souza", Email = "ana.souza@email.com" };
            ana.DefinirSenha("123456");

            var carlos = new Participante { Nome = "Carlos Lima", Email = "carlos.lima@email.com" };
            carlos.DefinirSenha("123456");

            var inscricaoCSharp = new Inscricao
            {
                Participante = ana,
                Evento = workshopCSharp,
                Pagamento = new PagamentoPix { Valor = workshopCSharp.Valor }
            };
            inscricaoCSharp.Pagamento.Pagar();

            var inscricaoConferencia = new Inscricao
            {
                Participante = ana,
                Evento = conferenciaTech,
                Pagamento = new PagamentoCartao { Valor = conferenciaTech.Valor }
            };
            inscricaoConferencia.Pagamento.Pagar();

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
