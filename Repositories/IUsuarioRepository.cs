using Models;

namespace Repositories;

public interface IUsuarioRepository
{
  Usuario? ObterPorCodigo(string codigo);
  void Adicionar(Usuario usuario);
  string GerarProximoCodigo();
}