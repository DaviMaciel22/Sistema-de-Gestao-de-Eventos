using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WPFGestaoDeEventos.Model;

public class Participante
{
    public int ParticipanteId { get; set; }
    public string Nome { get; set; } = null!;
    public string CPF { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string SenhaHash { get; set; } = null!;
    public ICollection<Inscricao> Inscricoes { get; set; } = new List<Inscricao>();

    public void confirmar()
    {
        Console.WriteLine("Inscrição confirmada.");
        //adicionar insert no banco
    }

    public void cancelar()
    {
        Console.WriteLine("Inscrição cancelada.");
    }



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
