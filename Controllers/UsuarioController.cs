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

  public Usuario ObterPorCodigo(string codigo)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(codigo);

    return _usuarioRepository.ObterPorCodigo(codigo) ??
      throw new InvalidOperationException("Usuário não encontrado.");
  }

  public Usuario RegistrarUsuario(string email, string nome)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(email);
    ArgumentException.ThrowIfNullOrWhiteSpace(nome);

    if (_usuarioRepository.ObterPorEmail(email) is not null)
      throw new InvalidOperationException("Já existe um usuário com esse e-mail.");

    var codigo = _usuarioRepository.GerarProximoCodigo();
    var usuario = new Usuario(codigo, email, nome);
    _usuarioRepository.Adicionar(usuario);
    return usuario;
  }
  
}
