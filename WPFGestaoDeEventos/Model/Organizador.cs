namespace WPFGestaoDeEventos.Model;

public abstract class Organizador
{
    public int IdOrganizador { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    // 1xN: um organizador possui vários eventos
    public ICollection<Evento> Eventos { get; set; } = new List<Evento>();

    public Evento CriarEvento(Evento evento)
    {
        evento.Organizador = this;
        Eventos.Add(evento);
        return evento;
    }

    public void CancelarEvento(Evento evento)
    {
        if (!ReferenceEquals(evento.Organizador, this))
        {
            throw new InvalidOperationException("O evento pertence a outro organizador.");
        }

        evento.Status = "Cancelado";
    }
}
