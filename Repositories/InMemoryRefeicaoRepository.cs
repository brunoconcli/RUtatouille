using Models;

namespace Repositories;

public class InMemoryRefeicaoRepository : IRefeicaoRepository
{
    private readonly Dictionary<DiaSemana, Refeicao> _cardapio;

    public InMemoryRefeicaoRepository()
    {
        _cardapio = new Dictionary<DiaSemana, Refeicao>
    {
      { DiaSemana.Segunda, new Refeicao("Bife à caçarola", "Arroz e feijão", "Paçoca", "Suco de uva", Refeicao.PrecoPadrao) },
      { DiaSemana.Terca, new Refeicao("Franguinho", "Arroz e feijão", "Sagu de abacaxi", "Suco de laranja", Refeicao.PrecoPadrao) },
      { DiaSemana.Quarta, new Refeicao("Feijoada", "Arroz e couve", "Pudim", "Suco de maracujá", Refeicao.PrecoPadrao) },
      { DiaSemana.Quinta, new Refeicao("Lasanha", "Salada verde", "Gelatina", "Suco de manga", Refeicao.PrecoPadrao) },
      { DiaSemana.Sexta, new Refeicao("Peixe assado", "Arroz e legumes", "Fruta da estação", "Suco de limão", Refeicao.PrecoPadrao) },
    };
    }

    public IReadOnlyDictionary<DiaSemana, Refeicao> ListarTodas()
    {
        return _cardapio;
    }

    public Refeicao? ObterPorDia(DiaSemana dia)
    {
        if (!Enum.IsDefined(typeof(DiaSemana), dia))
            throw new ArgumentOutOfRangeException(nameof(dia), "O dia da semana é inválido.");

        return _cardapio.GetValueOrDefault(dia);
    }
}
