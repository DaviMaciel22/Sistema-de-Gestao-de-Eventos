using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Participante
{
    public int IdParticipante { get; set; } = 0;
    public string Nome { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }

    public void confirmar()
    {
        Console.WriteLine("Inscrição confirmada.");
        //adicionar insert no banco
    }

    public void cancelar()
    {
        Console.WriteLine("Inscrição cancelada.");
    }



    public override string ToString()
    {
        return $"ID: {IdParticipante} / Nome: {Nome} / CPF: {CPF} / Email: {Email}";
    }

    
}
