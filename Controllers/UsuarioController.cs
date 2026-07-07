public class UsuarioController
{
  // Lista em memória simulando o banco de dados
  private readonly List<Usuario> _usuarios = new();

  // Método para tratar a criação de um usuário
  public void RegistrarNovoUsuario(string email, string nome)
  {
    var fabrica = new Usuario();
    Usuario novoUsuario;
    if (_usuarios.Any())
    {
      int ultimoCodigo = int.Parse(_usuarios.Last().Codigo);
      ultimoCodigo++;
      novoUsuario = fabrica.CriarUsuario(ultimoCodigo.ToString(), email.ToLower(), nome);
    }
    else
      novoUsuario = fabrica.CriarUsuario("1", email, nome);

    _usuarios.Add(novoUsuario);
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
  
}