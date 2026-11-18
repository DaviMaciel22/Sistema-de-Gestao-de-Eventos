using System.Net.Mail;
using WPFGestaoDeEventos.Model;

namespace WPFGestaoDeEventos.Controller
{
    public class ParticipanteController
    {
        // Perfil e Meus Ingressos: participante de teste (o primeiro do seed)
        public Participante ObterAtual()
        {
            using var contexto = new WpfGestaoDeEventosDbContext();

            return contexto.Participantes.OrderBy(p => p.ParticipanteId).FirstOrDefault()
                ?? throw new InvalidOperationException("Nenhum participante cadastrado.");
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

            using var contexto = new WpfGestaoDeEventosDbContext();

            var participante = contexto.Participantes.OrderBy(p => p.ParticipanteId).First();

            bool emailEmUso = contexto.Participantes.Any(p =>
                p.Email == email && p.ParticipanteId != participante.ParticipanteId);
            if (emailEmUso)
                throw new InvalidOperationException("Este e-mail já está em uso.");

            participante.Nome = nome.Trim();
            participante.Email = email;
            contexto.SaveChanges();
        }

        // Perfil: "Alterar senha"
        public void AlterarSenha(string atual, string nova, string confirmacao)
        {
            if (string.IsNullOrEmpty(atual) || string.IsNullOrEmpty(nova) || string.IsNullOrEmpty(confirmacao))
                throw new InvalidOperationException("Preencha todos os campos de senha.");
            if (nova.Length < 6)
                throw new InvalidOperationException("A nova senha deve ter pelo menos 6 caracteres.");
            if (nova != confirmacao)
                throw new InvalidOperationException("A confirmação não confere com a nova senha.");

            using var contexto = new WpfGestaoDeEventosDbContext();

            var participante = contexto.Participantes.OrderBy(p => p.ParticipanteId).First();

            if (!participante.SenhaConfere(atual))
                throw new InvalidOperationException("A senha atual está incorreta.");

            participante.DefinirSenha(nova);
            contexto.SaveChanges();
        }

        // Perfil: "Excluir conta" (recusa se houver inscrição confirmada)
        public void ExcluirConta()
        {
            using var contexto = new WpfGestaoDeEventosDbContext();

            var participante = contexto.Participantes.OrderBy(p => p.ParticipanteId).First();

            bool temConfirmada = contexto.Inscricoes.Any(i =>
                i.ParticipanteId == participante.ParticipanteId && i.Status == "Confirmado");
            if (temConfirmada)
                throw new InvalidOperationException(
                    "Não é possível excluir a conta com ingresso confirmado. Cancele os ingressos primeiro.");

            contexto.Participantes.Remove(participante);
            contexto.SaveChanges();
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
