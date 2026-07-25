using Models;
using Repositories;

public class InMemoryUsuarioRepository : IUsuarioRepository
{
  private readonly List<Usuario> _usuarios = new();
  public void Adicionar(Usuario usuario)
  {
    _usuarios.Add(usuario);
  }

  public string GerarProximoCodigo()
  {
    return _usuarios.Any()
      ? (int.Parse(_usuarios.Last().Codigo) + 1).ToString()
      :  "1";
  }

  public Usuario? ObterPorCodigo(string codigo)
  {
    return _usuarios.FirstOrDefault((usuario) => usuario.Codigo == codigo);
  }
}
