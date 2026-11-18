using System;

namespace WPFGestaoDeEventos.Model;

// Herança: Workshop é um tipo de Evento
public class Workshop : Evento
{
    public int PalestraId { get; set; }
    public Palestra Palestra { get; set; } = null!;


    public override string ToString()
    {
        return $"ID: {EventoId} / Título: {Titulo} / Descrição: {Descricao} / Data e Hora: {DataEHora}" +
            $" / Quantidade de Inscrições: {QntInscricoes} / Vagas Restantes: {VagasRestantes} / Valor: {Valor}" +
            $" / Duração: {Duracao} / Status: {Status} / Organizador: {Organizador?.OrganizadorId} / Local: {Local?.Nome}";
    }
}
