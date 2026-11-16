using System;

public class Palestrante
{
    public int IdPalestrante { get; set; } = 0;
    public string Nome { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }

    public override string ToString()
    {
        return $"ID: {IdPalestrante} / Nome: {Nome} / CPF: {CPF} / Email: {Email}";
    }
}
