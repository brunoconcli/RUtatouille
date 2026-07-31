namespace Models;

public class Refeicao
{
    public const decimal PrecoPadrao = 2.50m;

    public string PratoPrincipal { get; private set; }
    public string Acompanhamento { get; private set; }
    public string Sobremesa { get; private set; }
    public string Suco { get; private set; }
    public decimal Preco { get; private set; }

    public Refeicao(string pratoPrincipal, string acompanhamento, string sobremesa, string suco, decimal preco)
    {
        if (preco <= 0)
            throw new ArgumentOutOfRangeException(nameof(preco), "O preço deve ser positivo.");

        ArgumentException.ThrowIfNullOrWhiteSpace(pratoPrincipal);
        ArgumentException.ThrowIfNullOrWhiteSpace(acompanhamento);
        ArgumentException.ThrowIfNullOrWhiteSpace(sobremesa);
        ArgumentException.ThrowIfNullOrWhiteSpace(suco);

        PratoPrincipal = pratoPrincipal.Trim();
        Acompanhamento = acompanhamento.Trim();
        Sobremesa = sobremesa.Trim();
        Suco = suco.Trim();
        Preco = preco;
    }
}
