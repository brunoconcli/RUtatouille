using Models;

namespace Repositories;

public interface IRefeicaoRepository
{
  public Refeicao? ObterPorDia(DiaSemana dia);
  public IReadOnlyDictionary<DiaSemana, Refeicao> ListarTodas();
}
