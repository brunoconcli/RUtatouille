using Models;
using Repositories;

namespace Controllers;

public class UsuarioController
{
  private readonly IUsuarioRepository _usuarioRepository;
  public UsuarioController(IUsuarioRepository usuarioRepository)
  {
    _usuarioRepository = usuarioRepository ??
      throw new ArgumentNullException(nameof(usuarioRepository));
  }

  public void AdicionarCredito(Usuario usuario, decimal valor)
  {
    ArgumentNullException.ThrowIfNull(usuario);
    usuario.AdicionarCredito(valor);
  }

  public void RegistrarUsuario(string email, string nome)
  {
    var codigo = _usuarioRepository.GerarProximoCodigo();
    var usuario = new Usuario(codigo, email, nome);
    _usuarioRepository.Adicionar(usuario);
  }
  
}
