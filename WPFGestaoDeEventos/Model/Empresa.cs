using System;

namespace WPFGestaoDeEventos.Model;

public class Empresa : Organizador
{
    public string CNPJ { get; set; }
    public string RazaoSocial { get; set; }



    public override string ToString()
    {
        return $"ID: {IdOrganizador} / Número: {Numero} / Email: {Email} / CNPJ: {CNPJ} / Razão Social: {RazaoSocial}";
    }
}
