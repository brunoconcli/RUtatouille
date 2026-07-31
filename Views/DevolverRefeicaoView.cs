using Controllers;
using Models;

namespace Views;

public class DevolverRefeicaoView : ViewBase
{
    private readonly RefeicaoController _refeicaoController;

    public DevolverRefeicaoView(RefeicaoController refeicaoController)
    {
        _refeicaoController = refeicaoController ??
          throw new ArgumentNullException(nameof(refeicaoController));
    }

    public void Exibir(Usuario usuario)
    {
        ArgumentNullException.ThrowIfNull(usuario);
        var reservas = _refeicaoController
          .ListarReservasAtivas(usuario.Codigo)
          .OrderBy(reserva => reserva.Dia)
          .ToList();

        PrepararTela("DEVOLVER REFEIÇÃO");

        if (reservas.Count == 0)
        {
            Console.WriteLine("Você não possui reservas ativas para devolver.");
            AguardarContinuacao();
            return;
        }

        Console.WriteLine("Selecione a refeição que deseja devolver:\n");
        for (var indice = 0; indice < reservas.Count; indice++)
        {
            var reserva = reservas[indice];
            Console.WriteLine($"{indice + 1}. {FormatarDia(reserva.Dia)} — {reserva.ValorPago:C}");
        }

        Console.WriteLine("\n0. Cancelar");
        var entrada = LerEntrada("\nEscolha uma reserva: ");

        if (entrada is null || entrada == "0")
            return;

        if (!int.TryParse(entrada, out var opcao) || opcao < 1 || opcao > reservas.Count)
        {
            ExibirErro(new ArgumentException("Opção inválida."));
            return;
        }

        try
        {
            var reserva = reservas[opcao - 1];
            _refeicaoController.DevolverRefeicao(usuario.Codigo, reserva.Dia);
            ExibirSucesso($"Refeição devolvida! {reserva.ValorPago:C} foram estornados.");
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
