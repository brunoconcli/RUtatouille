using Controllers;

namespace Views;

public class CriarUsuarioView : ViewBase
{
    private readonly UsuarioController _usuarioController;

    public CriarUsuarioView(UsuarioController usuarioController)
    {
        _usuarioController = usuarioController ??
          throw new ArgumentNullException(nameof(usuarioController));
    }

    public void Exibir()
    {
        PrepararTela("CRIAR USUÁRIO");
        Console.WriteLine("Pressione [Enter] sem digitar nada para cancelar.\n");

        var email = LerEntrada("E-mail: ");
        if (string.IsNullOrWhiteSpace(email))
            return;

        var nome = LerEntrada("Nome: ");
        if (string.IsNullOrWhiteSpace(nome))
            return;

        try
        {
            var usuario = _usuarioController.RegistrarUsuario(email, nome);
            ExibirSucesso($"Usuário criado com sucesso! Seu código é: {usuario.Codigo}");
            AguardarContinuacao();
        }
        catch (ArgumentException excecao)
        {
            ExibirErro(excecao);
        }
        catch (InvalidOperationException excecao)
        {
            ExibirErro(excecao);
        }
    }
}
