using Controllers;
using Models;
using System.Globalization;

namespace Views;

public class ComprarCreditosView : ViewBase
{
    private readonly UsuarioController _usuarioController;

    public ComprarCreditosView(UsuarioController usuarioController)
    {
        _usuarioController = usuarioController ??
          throw new ArgumentNullException(nameof(usuarioController));
    }

    public void Exibir(Usuario usuario)
    {
        ArgumentNullException.ThrowIfNull(usuario);
        PrepararTela("COMPRAR CRÉDITOS");
        Console.WriteLine($"Saldo atual: {usuario.Saldo:C}");
        Console.WriteLine("Pressione [Enter] sem digitar nada para cancelar.\n");

        var entrada = LerEntrada("Digite o valor: R$ ");
        if (string.IsNullOrWhiteSpace(entrada))
            return;

        if (!decimal.TryParse(
          entrada,
          NumberStyles.Number,
          CultureInfo.CurrentCulture,
          out var valor))
        {
            ExibirErro(new ArgumentException("Informe um valor monetário válido."));
            return;
        }

        try
        {
            _usuarioController.AdicionarCredito(usuario, valor);
            ExibirSucesso($"Crédito adicionado! Novo saldo: {usuario.Saldo:C}");
            AguardarContinuacao();
        }
        catch (ArgumentException excecao)
        {
            ExibirErro(excecao);
        }
    }
}
