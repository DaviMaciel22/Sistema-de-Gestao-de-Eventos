using System;

namespace WPFGestaoDeEventos.Model;

// Herança: Empresa e PessoaFisica herdam de Organizador
public class Organizador
{
	public int OrganizadorId { get; set; }
    public string Numero { get; set; } = null!;
    public string Email { get; set; } = null!;

    public ICollection<Evento> Eventos { get; set; } = new List<Evento>();
}
