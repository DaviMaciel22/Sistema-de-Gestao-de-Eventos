using System;

namespace WPFGestaoDeEventos.Model;

public class PessoaFisica : Organizador
{
    public string CPF { get; set; } = null!;
    public string Nome { get; set; } = null!;



    public override string ToString()
    {
        return $"ID: {OrganizadorId} / Número: {Numero} / Email: {Email} / CPF: {CPF} / Nome: {Nome}";
    }
}
