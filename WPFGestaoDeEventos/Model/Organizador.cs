using System;
using System.Security.Cryptography.X509Certificates;

namespace WPFGestaoDeEventos.Model;

public class Organizador
{
	public int OrganizadorId { get; set; }
    public string Numero { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int TipoOrganizador { get; set; }

    public ICollection<Evento> Eventos { get; set; } = new List<Evento>();

    public void CriarEvento()
    {
        Evento e = new();
    }

    public void CancelarEvento(Evento evento)
    {
        //cancela o evento
    }
}
