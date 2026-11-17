using System;
using System.Collections.Generic;
using System.Text;

namespace WPFGestaoDeEventos.Model
{
    internal interface IEmissorCertificado
    {
        abstract Certificado emitirCertificado(Inscricao i);

    }
}
