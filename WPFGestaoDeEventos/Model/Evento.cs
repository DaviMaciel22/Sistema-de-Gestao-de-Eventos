using System;

namespace WPFGestaoDeEventos.Model;

public class Evento
{
    public int IdEvento { get; set; } = 0;
    public string Titulo { get; set; } = null!;
    public string Descricao { get; set; } = null!;   
    public DateTime DataEHora { get; set; }
    public int QntInscricoes { get; set; }
    public int VagasRestantes { get; set; }
    public double Valor { get; set; }
    public string Duracao { get; set; } = null!;
    public string Status { get; set; } = null!;

    public int IdOrganizador { get; set; }
    public Organizador Organizador { get; set; } = null!;

    public int IdLocal { get; set; }
    public Local Local { get; set; } = null!;

    public ICollection<Inscricao> Inscricoes { get; set; } = new List<Inscricao>();

    public void abrirInscricoes()
    {
        //abre as inscrições
    }


    public void encerrarInscricoes()
    {
        //encerra as inscrições do evento
    }


    public void vagasDisponiveis()
    {
        //verifica se ainda há vagas disponíveis e mostra o número de vagas restantes
    }



}
