using System;
using System.Security.Cryptography.X509Certificates;

namespace WPFGestaoDeEventos.Model;

public class Organizador : ICertificado
{
	public int IdOrganizador { get; set; } = 0;
    public string Numero { get; set; }
    public string Email { get; set; }
    public int TipoOrganizador { get; set; }

    public ICollection<Evento> Eventos { get; set; } = new List<Evento>();

    public void CriarEvento()
    {
        Evento e = new();
    }
}
