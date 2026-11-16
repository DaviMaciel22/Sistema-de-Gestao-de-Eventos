using System;

public class PessoaFisica : Organizador
{
    public string CPF { get; set; }
    public string Nome { get; set; }



    public override string ToString()
    {
        return $"ID: {IdOrganizador} / Número: {Numero} / Email: {Email} / CPF: {CPF} / Nome: {Nome}";
    }
}
