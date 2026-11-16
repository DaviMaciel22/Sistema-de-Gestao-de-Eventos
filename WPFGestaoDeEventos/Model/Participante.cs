using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Participante
{
    public int IdParticipante { get; set; } = 0;
    public string Nome { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }

    public override string ToString()
    {
        return $"ID: {IdParticipante} / Nome: {Nome} / CPF: {CPF} / Email: {Email}";
    }
}
