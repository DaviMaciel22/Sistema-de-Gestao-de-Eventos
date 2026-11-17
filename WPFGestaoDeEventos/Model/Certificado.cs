using System;

namespace WPFGestaoDeEventos.Model;

public class Certificado
{
    public int CertificadoId { get; set; }
    public DateTime DataEmissao { get; set; }

    public int InscricaoId { get; set; }
    public Inscricao Inscricao { get; set; } = null!;

    public override string ToString()
    {
        return $"ID: {CertificadoId} / Data de Emissão: {DataEmissao} / Nome do Evento: {Inscricao.Evento.Titulo}" +
            $" / Duração do Evento: {Inscricao.Evento.Duracao} / Nome do Participante: {Inscricao.Participante.Nome}";
    }


}
