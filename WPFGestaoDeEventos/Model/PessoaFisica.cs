namespace WPFGestaoDeEventos.Model;

public class PessoaFisica : Organizador
{
    public string CPF { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"ID: {IdOrganizador} / Nome: {Nome} / Número: {Numero} / Email: {Email} / CPF: {CPF}";
    }
}
