using System;

namespace WPFGestaoDeEventos.Model;

// Herança: Conferencia e Workshop herdam de Evento
public class Evento
{
    public int EventoId { get; set; }
    public string Titulo { get; set; } = null!;
    public string Descricao { get; set; } = null!;   
    public DateTime DataEHora { get; set; }
    public int QntInscricoes { get; set; }
    public int VagasRestantes { get; set; }
    public double Valor { get; set; }
    public string Duracao { get; set; } = null!;
    public string Status { get; set; } = null!;

    public int OrganizadorId { get; set; }
    public Organizador Organizador { get; set; } = null!;

    public int LocalId { get; set; }
    public Local Local { get; set; } = null!;

    // Usado pelo card da Home (somente leitura, o EF ignora)
    public string TipoEvento => this is Conferencia ? "Conferência" : "Workshop";

    public ICollection<Inscricao> Inscricoes { get; set; } = new List<Inscricao>();
}
