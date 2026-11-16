using System;

public class Local
{
    public int IdLocal { get; set; } = 0;
    public int Capacidade { get; set; }
    public string Rua { get; set; }
    public int Numero { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string País { get; set; }
    public ICollection<Evento> Eventos { get; set; } = new List<Evento>();

    public override string ToString()
    {
        return $"ID: {IdLocal} / Capacidade: {Capacidade} / Rua: {Rua} / Número: {Numero} " +
            $"/ Bairro: {Bairro} / Cidade: {Cidade} / Estado: {Estado} / País: {País}";
    }



}
