using WPFGestaoDeEventos.Model;

namespace WPFGestaoDeEventos.Controller
{
    public class ParticipanteController
    {
        // Perfil: ao abrir a tela (participante de teste via seed)
        public Participante ObterAtual()
        {
            throw new NotImplementedException();
        }

        // Perfil: "Salvar alterações" (validar campos vazios e e-mail)
        public void AtualizarDados(string nome, string email)
        {
            throw new NotImplementedException();
        }

        // Perfil: "Alterar senha"
        public void AlterarSenha(string atual, string nova, string confirmacao)
        {
            throw new NotImplementedException();
        }

        // Perfil: "Excluir conta" (recusa se houver inscrição confirmada)
        public void ExcluirConta()
        {
            throw new NotImplementedException();
        }
    }
}