using System;

namespace WPFGestaoDeEventos.Model;

public class Workshop : Evento
{
    public Palestra Palestra { get; set; }


    public override string ToString()
    {
        return $"ID: {IdEvento} / Título: {Titulo} / Descrição: {Descricao} / Data e Hora: {DataEHora}" +
            $" / Quantidade de Inscrições: {QntInscricoes} / Vagas Restantes: {VagasRestantes} / Valor: {Valor}" +
            $" / Duração: {Duracao} / Status: {Status} / Organizador: {Organizador} / Local: {Local}";
    }
}
