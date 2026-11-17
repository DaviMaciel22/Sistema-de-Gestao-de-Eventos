using System;

namespace WPFGestaoDeEventos.Model;

public class Local
{
    public int LocalId { get; set; }
    public string Nome { get; set; } = null!;
    public int Capacidade { get; set; }
    public string Rua { get; set; } = null!;
    public int Numero { get; set; }
    public string Bairro { get; set; } = null!;
    public string Cidade { get; set; } = null!;
    public string Estado { get; set; } = null!;
    public string Pais { get; set; } = null!;

    public ICollection<Evento> Eventos { get; set; } = new List<Evento>();

    public override string ToString()
    {
        return $"ID: {LocalId} / Nome: {Nome} / Capacidade: {Capacidade} / Rua: {Rua} / Número: {Numero} " +
            $"/ Bairro: {Bairro} / Cidade: {Cidade} / Estado: {Estado} / País: {Pais}";
    }



}
