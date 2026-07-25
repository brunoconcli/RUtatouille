using Models;

namespace Repositories;

public interface IRefeicaoRepository
{
  Refeicao? ObterPorDia(DiaSemana dia);
  IReadOnlyDictionary<DiaSemana, Refeicao> ListarTodas();
}
