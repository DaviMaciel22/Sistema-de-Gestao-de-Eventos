using System;
using System.Security.Cryptography.X509Certificates;

namespace WPFGestaoDeEventos.Model;

public class Organizador : IEmissorCertificado
{
	public int OrganizadorId { get; set; }
    public string Numero { get; set; } = null!;
    public string Email { get; set; } = null!;

    public ICollection<Evento> Eventos { get; set; } = new List<Evento>();

    public Certificado emitirCertificado(Inscricao i)
    {
        // implementação para emitir certificado
        return new Certificado();
    }

    public void CriarEvento()
    {
        Evento e = new();
    }

    public void CancelarEvento(Evento evento)
    {
        //cancela o evento
    }
}
