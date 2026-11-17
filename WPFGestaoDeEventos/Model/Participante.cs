using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WPFGestaoDeEventos.Model;

public class Participante
{
    public int ParticipanteId { get; set; }
    public string Nome { get; set; } = null!;
    public string CPF { get; set; } = null!;
    public string Email { get; set; } = null!;

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
        return $"ID: {ParticipanteId} / Nome: {Nome} / CPF: {CPF} / Email: {Email}";
    }

    
}
