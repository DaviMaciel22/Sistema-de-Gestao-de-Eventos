namespace WPFGestaoDeEventos.Model;

public class Palestrante
{
    public int IdPalestrante { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public ICollection<Palestra> Palestras { get; set; } = new List<Palestra>();

    public override string ToString()
    {
        return $"ID: {IdPalestrante} / Nome: {Nome} / CPF: {CPF} / Email: {Email}";
    }
}
