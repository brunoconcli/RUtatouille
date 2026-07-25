using Models;
using Repositories;

public class UsuarioController
{
  private readonly IUsuarioRepository _usuarioRepository;
  public UsuarioController(IUsuarioRepository usuarioRepository)
  {
    _usuarioRepository = usuarioRepository;  
  }
  public void AdicionarCredito(Usuario usuario, double valor)
  {
    if(valor <= 0)
    {
      Console.WriteLine("O valor deve ser positivo!!");
      return; // TODO: Pensar nos valores de erro.
    }
    usuario.AdicionarCredito(valor);
  }
  public void RegistrarUsuario(string email, string nome)
  {
    var codigo = _usuarioRepository.GerarProximoCodigo();
    var usuario = new Usuario(codigo, email, nome);
    _usuarioRepository.Adicionar(usuario);
  }
  
}