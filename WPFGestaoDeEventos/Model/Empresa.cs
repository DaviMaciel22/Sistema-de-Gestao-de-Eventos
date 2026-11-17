namespace WPFGestaoDeEventos.Model;

public class Empresa : Organizador
{
    public string CNPJ { get; set; } = string.Empty;
    public string RazaoSocial { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"ID: {IdOrganizador} / Nome: {Nome} / Número: {Numero} / Email: {Email} / CNPJ: {CNPJ} / Razão Social: {RazaoSocial}";
    }
}
