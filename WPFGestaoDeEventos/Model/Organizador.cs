using System;
using System.Security.Cryptography.X509Certificates;

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

    public void CancelarEvento(Evento evento)
    {
        //cancela o evento
    }
}
