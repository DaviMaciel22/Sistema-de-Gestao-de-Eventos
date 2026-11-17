using System.Security.Cryptography;
using System.Text;

namespace WPFGestaoDeEventos.Model
{
    public class Participante
    {
        public int IdParticipante { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;

        // 1xN: um participante tem várias inscrições
        public ICollection<Inscricao> Inscricoes { get; set; } = new List<Inscricao>();

        public static string GerarHash(string senha)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(senha));
            return Convert.ToHexString(bytes);
        }

        public void DefinirSenha(string senha)
        {
            SenhaHash = GerarHash(senha);
        }

        public bool SenhaConfere(string senha)
        {
            return SenhaHash == GerarHash(senha);
        }
    }
}
