using System;

namespace WPFGestaoDeEventos.Model;

public class Palestrante
{
    public int PalestranteId { get; set; }
    public string Nome { get; set; } = null!;
    public string CPF { get; set; } = null!;
    public string Email { get; set; } = null!;

    public ICollection<Palestra> Palestras { get; set; } = new List<Palestra>();

    public override string ToString()
    {
        return $"ID: {PalestranteId} / Nome: {Nome} / CPF: {CPF} / Email: {Email}";
    }
}
