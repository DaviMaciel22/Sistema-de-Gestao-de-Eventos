using System;

namespace WPFGestaoDeEventos.Model;

public class Participante
{
    public int ParticipanteId { get; set; }
    public string Nome { get; set; } = null!;
    public string CPF { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string SenhaHash { get; set; } = null!;
    public ICollection<Inscricao> Inscricoes { get; set; } = new List<Inscricao>();

    // Segurança: a senha nunca é guardada; só o hash SHA-256 dela
    public static string GerarHash(string senha)
    {
        byte[] bytes = System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(senha));
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

    public override string ToString()
    {
        return $"ID: {ParticipanteId} / Nome: {Nome} / CPF: {CPF} / Email: {Email}";
    }
}
