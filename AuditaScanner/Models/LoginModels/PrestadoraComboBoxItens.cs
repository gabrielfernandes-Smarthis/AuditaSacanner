namespace AuditaScanner.Models.LoginModels;

public class PrestadoraComboBoxItens
{
    public int Id { get; set; }
    public string Fantasia { get; set; }
    public string Cnpj { get; set; }

    public PrestadoraComboBoxItens(int id, string fantasia, string cnpj)
    {
        Id = id;
        Fantasia = fantasia;
        Cnpj = cnpj;
    }

    public override string ToString()
    {
        return Fantasia;
    }
}
