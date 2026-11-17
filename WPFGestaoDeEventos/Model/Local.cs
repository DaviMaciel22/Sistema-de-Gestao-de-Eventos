namespace WPFGestaoDeEventos.Model;

public class Local
{
    public int IdLocal { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Capacidade { get; set; }
    public string Rua { get; set; } = string.Empty;
    public int Numero { get; set; }
    public string Bairro { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string Pais { get; set; } = string.Empty;

    // Agregação: o local pode receber vários eventos e continua existindo sem eles
    public ICollection<Evento> Eventos { get; set; } = new List<Evento>();

    public override string ToString()
    {
        return $"ID: {IdLocal} / Nome: {Nome} / Capacidade: {Capacidade} / Rua: {Rua} / Número: {Numero} " +
            $"/ Bairro: {Bairro} / Cidade: {Cidade} / Estado: {Estado} / País: {Pais}";
    }
}
