using System.Net.Mail;
using WPFGestaoDeEventos.Data;
using WPFGestaoDeEventos.Model;

namespace WPFGestaoDeEventos.Controller
{
    public class ParticipanteController
    {
        // Não há login: o app usa o primeiro participante do seed (Ana Souza) como usuário atual.
        private static Participante BuscarAtual(AppDbContext contexto)
        {
            return contexto.Participantes
                .OrderBy(p => p.IdParticipante)
                .FirstOrDefault()
                ?? throw new InvalidOperationException("Nenhum participante cadastrado.");
        }

        // Perfil: ao abrir a tela
        public Participante ObterAtual()
        {
            using var contexto = new AppDbContext();
            return BuscarAtual(contexto);
        }

        // Perfil: botão "Salvar alterações" (valida campos vazios e e-mail)
        public void AtualizarDados(string nome, string email)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("Informe o nome.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Informe o e-mail.");
            }

            email = email.Trim();

            if (!EmailValido(email))
            {
                throw new ArgumentException("E-mail inválido.");
            }

            using var contexto = new AppDbContext();
            var participante = BuscarAtual(contexto);

            bool emailEmUso = contexto.Participantes.Any(p =>
                p.Email == email && p.IdParticipante != participante.IdParticipante);

            if (emailEmUso)
            {
                throw new InvalidOperationException("Este e-mail já está em uso.");
            }

            participante.Nome = nome.Trim();
            participante.Email = email;
            contexto.SaveChanges();
        }

        // Perfil: botão "Alterar senha"
        public void AlterarSenha(string atual, string nova, string confirmacao)
        {
            if (string.IsNullOrEmpty(atual) || string.IsNullOrEmpty(nova) || string.IsNullOrEmpty(confirmacao))
            {
                throw new ArgumentException("Preencha todos os campos de senha.");
            }

            if (nova.Length < 6)
            {
                throw new ArgumentException("A nova senha deve ter pelo menos 6 caracteres.");
            }

            if (nova != confirmacao)
            {
                throw new ArgumentException("A confirmação não confere com a nova senha.");
            }

            using var contexto = new AppDbContext();
            var participante = BuscarAtual(contexto);

            if (!participante.SenhaConfere(atual))
            {
                throw new InvalidOperationException("A senha atual está incorreta.");
            }

            participante.DefinirSenha(nova);
            contexto.SaveChanges();
        }

        // Perfil: botão "Excluir conta" (recusa se houver inscrição confirmada)
        public void ExcluirConta()
        {
            using var contexto = new AppDbContext();
            var participante = BuscarAtual(contexto);

            bool temConfirmada = contexto.Inscricoes.Any(i =>
                i.ParticipanteId == participante.IdParticipante && i.Status == StatusInscricao.Confirmado);

            if (temConfirmada)
            {
                throw new InvalidOperationException(
                    "Não é possível excluir a conta com ingressos confirmados. Cancele-os antes.");
            }

            contexto.Participantes.Remove(participante);
            contexto.SaveChanges();
        }

        private static bool EmailValido(string email)
        {
            return MailAddress.TryCreate(email, out var endereco) && endereco.Address == email;
        }
    }
}
