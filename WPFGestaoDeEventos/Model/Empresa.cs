using System;

namespace WPFGestaoDeEventos.Model;

public class Empresa : Organizador
{
    public string CNPJ { get; set; } = null!;
    public string RazaoSocial { get; set; } = null!;



    public override string ToString()
    {
        return $"ID: {OrganizadorId} / Número: {Numero} / Email: {Email} / CNPJ: {CNPJ} / Razão Social: {RazaoSocial}";
    }
}
