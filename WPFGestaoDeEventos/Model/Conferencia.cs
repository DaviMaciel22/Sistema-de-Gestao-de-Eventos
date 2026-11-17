namespace WPFGestaoDeEventos.Model;

public class Conferencia : Evento
{
    public override string TipoEvento => "Conferência";

    public override string ToString()
    {
        return base.ToString() + $" / Quantidade de Palestras: {QtdPalestras}";
    }
}
