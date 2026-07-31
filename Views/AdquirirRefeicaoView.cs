using Controllers;
using Models;

namespace Views;

public class AdquirirRefeicaoView : ViewBase
{
  private readonly RefeicaoController _refeicaoController;

  public AdquirirRefeicaoView(RefeicaoController refeicaoController)
  {
    _refeicaoController = refeicaoController ??
      throw new ArgumentNullException(nameof(refeicaoController));
  }

  public void Exibir(Usuario usuario)
  {
    ArgumentNullException.ThrowIfNull(usuario);
    var cardapio = _refeicaoController.ListarCardapio()
      .OrderBy(item => item.Key)
      .ToList();

    while (true)
    {
      PrepararTela("ADQUIRIR REFEIÇÃO");
      Console.WriteLine($"Saldo: {usuario.Saldo:C}\n");

      for (var indice = 0; indice < cardapio.Count; indice++)
      {
        var (dia, refeicao) = cardapio[indice];
        Console.WriteLine($"{indice + 1}. {FormatarDia(dia)} — {refeicao.Preco:C}");
        Console.WriteLine($"   {refeicao.PratoPrincipal}, {refeicao.Acompanhamento}");
        Console.WriteLine($"   {refeicao.Sobremesa} e {refeicao.Suco}\n");
      }

      Console.WriteLine("0. Cancelar");
      var entrada = LerEntrada("\nEscolha uma refeição: ");

      if (entrada is null || entrada == "0")
        return;

      if (!int.TryParse(entrada, out var opcao) || opcao < 1 || opcao > cardapio.Count)
      {
        Console.WriteLine("\nOpção inválida.");
        AguardarContinuacao();
        continue;
      }

      try
      {
        var diaSelecionado = cardapio[opcao - 1].Key;
        _refeicaoController.AdquirirRefeicao(usuario.Codigo, diaSelecionado);
        ExibirSucesso($"Refeição de {FormatarDia(diaSelecionado)} adquirida com sucesso!");
        AguardarContinuacao();
        return;
      }
      catch (ArgumentException excecao)
      {
        ExibirErro(excecao);
        return;
      }
      catch (InvalidOperationException excecao)
      {
        ExibirErro(excecao);
        return;
      }
    }
  }
}
