namespace Models;

public class Refeicao
{
    public string PratoPrincipal { get; private set;}
    public string Acompanhamento { get; private set;}
    public string Sobremesa { get; private set;}
    public string Suco { get; private set;}
    public double Preco {get; private set;}

    public Refeicao (string pratoPrincipal, string acompanhamento, string sobremesa, string suco, double preco)
    {
      if (preco <= 0)
        throw new ArgumentException("O preço deve ser um valor positivo.");
      if (string.IsNullOrEmpty(pratoPrincipal))
        throw new ArgumentException("O prato principal não pode ser nulo ou vazio.");

      PratoPrincipal = pratoPrincipal;
      Acompanhamento = acompanhamento;
      Sobremesa = sobremesa;
      Suco = suco;
      Preco = preco;
    }
}
