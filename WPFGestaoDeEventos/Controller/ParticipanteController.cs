using System.Net.Mail;
using WPFGestaoDeEventos.Model;
using WPFGestaoDeEventos.Repository;

namespace WPFGestaoDeEventos.Controller
{
    // MVC: o Controller valida os dados do Perfil; o banco fica no repositório
    // SOLID (SRP): aqui só ficam as regras do participante
    public class ParticipanteController
    {
        // Sem tela de login: o app usa sempre o participante de teste criado no seed
        private const int ParticipanteAtualId = 1;
        public const int TamanhoMinimoSenha = 6;

        // SOLID (DIP): depende da interface; a tela usa o construtor vazio, que cria o repositório real
        private readonly IParticipanteRepository _repositorio;

        public ParticipanteController() : this(new ParticipanteRepository()) { }

        public ParticipanteController(IParticipanteRepository repositorio)
        {
            _repositorio = repositorio;
        }

        // Perfil, Home e Meus Ingressos: participante que está usando o app
        public Participante ObterAtual()
        {
            return _repositorio.ObterPorId(ParticipanteAtualId)
                ?? throw new InvalidOperationException("Participante não encontrado.");
        }

        // Perfil: "Salvar alterações" (valida campos vazios e e-mail)
        public void AtualizarDados(string nome, string email)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new InvalidOperationException("Informe o nome.");
            if (string.IsNullOrWhiteSpace(email))
                throw new InvalidOperationException("Informe o e-mail.");

            email = email.Trim();
            if (!EmailValido(email))
                throw new InvalidOperationException("E-mail inválido.");

            var participante = ObterAtual();

            if (_repositorio.EmailEmUso(email, participante.ParticipanteId))
                throw new InvalidOperationException("Este e-mail já está em uso.");

            participante.Nome = nome.Trim();
            participante.Email = email;
            _repositorio.Salvar();
        }

        // Perfil: "Alterar senha"
        public void AlterarSenha(string atual, string nova, string confirmacao)
        {
            if (string.IsNullOrEmpty(atual) || string.IsNullOrEmpty(nova) || string.IsNullOrEmpty(confirmacao))
                throw new InvalidOperationException("Preencha todos os campos de senha.");
            if (nova.Length < TamanhoMinimoSenha)
                throw new InvalidOperationException($"A nova senha precisa ter no mínimo {TamanhoMinimoSenha} caracteres.");
            if (nova != confirmacao)
                throw new InvalidOperationException("A confirmação não confere com a nova senha.");

            var participante = ObterAtual();

            if (!participante.SenhaConfere(atual))
                throw new InvalidOperationException("A senha atual está incorreta.");

            participante.DefinirSenha(nova);
            _repositorio.Salvar();
        }

        // Perfil: "Excluir conta" (recusa se houver inscrição confirmada)
        public void ExcluirConta()
        {
            var participante = ObterAtual();

            if (_repositorio.TemInscricaoConfirmada(participante.ParticipanteId))
                throw new InvalidOperationException(
                    "Não é possível excluir a conta com ingresso confirmado. Cancele os ingressos primeiro.");

            _repositorio.Remover(participante);
            _repositorio.Salvar();
        }

        private static bool EmailValido(string email)
        {
            try
            {
                var endereco = new MailAddress(email);
                return endereco.Address == email;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
