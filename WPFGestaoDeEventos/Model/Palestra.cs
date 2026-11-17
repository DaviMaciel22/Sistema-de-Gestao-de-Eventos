using System;

namespace WPFGestaoDeEventos.Model;

public class Palestra
{
    public int PalestraId { get; set; }
    public double Duracao { get; set; }
    public string Nome { get; set; } = null!;

    public int PalestranteId { get; set; }
    public Palestrante Palestrante { get; set; } = null!;

    public int? ConferenciaId { get; set; }
    public Conferencia? Conferencia { get; set; } 
    
    public int? WorkshopId { get; set; }
    public Workshop? Workshop { get; set; }



    public override string ToString()
    {
        return $"ID: {PalestraId} / Palestrante: {Palestrante.Nome} / Workshop: {Workshop?.Titulo ?? "N/A"} " +
            $"/ Conferência: {Conferencia?.Titulo ?? "N/A"} / Duração: {Duracao}";
    }
}
