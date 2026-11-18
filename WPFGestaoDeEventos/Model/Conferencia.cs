using System;

namespace WPFGestaoDeEventos.Model;

// Composição: as palestras fazem parte da conferência e não existem sem ela
public class Conferencia : Evento
{
    public ICollection<Palestra> Palestras { get; set; } = new List<Palestra>();
    public int QtdPalestras => Palestras.Count;

    public override string ToString()
    {
        return $"ID: {EventoId} / Título: {Titulo} / Descrição: {Descricao} / Data e Hora: {DataEHora}" +
            $" / Quantidade de Inscrições: {QntInscricoes} / Vagas Restantes: {VagasRestantes} / Valor: {Valor}" +
            $" / Duração: {Duracao} / Status: {Status} / Organizador: {Organizador?.OrganizadorId} / Local: {Local?.Nome} / Quantidade de Palestras: {QtdPalestras}";
    }
}
