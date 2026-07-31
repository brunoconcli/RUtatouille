using Controllers;
using Models;

namespace Views;

public class LoginView : ViewBase
{
  private readonly UsuarioController _usuarioController;

  public LoginView(UsuarioController usuarioController)
  {
    _usuarioController = usuarioController ??
      throw new ArgumentNullException(nameof(usuarioController));
  }

  public Usuario? Exibir()
  {
    PrepararTela("ACESSAR");
    Console.WriteLine("Pressione [Enter] sem digitar nada para cancelar.\n");

    var codigo = LerEntrada("Digite seu código: ");
    if (string.IsNullOrWhiteSpace(codigo))
      return null;

    try
    {
      var usuario = _usuarioController.ObterPorCodigo(codigo);
      ExibirSucesso("Usuário autenticado com sucesso!");
      AguardarContinuacao();
      return usuario;
    }
    catch (ArgumentException excecao)
    {
      ExibirErro(excecao);
      return null;
    }
    catch (InvalidOperationException excecao)
    {
      ExibirErro(excecao);
      return null;
    }
  }
}
