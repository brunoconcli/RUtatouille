using Models;

namespace Repositories;

public interface IUsuarioRepository
{
    Usuario? ObterPorCodigo(string codigo);
    Usuario? ObterPorEmail(string email);
    void Adicionar(Usuario usuario);
    string GerarProximoCodigo();
}
