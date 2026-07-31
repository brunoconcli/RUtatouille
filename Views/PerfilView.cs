using Controllers;
using Models;

namespace Views;

public class PerfilView : ViewBase
{
    private readonly RefeicaoController _refeicaoController;

    public PerfilView(RefeicaoController refeicaoController)
    {
        _refeicaoController = refeicaoController ??
          throw new ArgumentNullException(nameof(refeicaoController));
    }

    public void Exibir(Usuario usuario)
    {
        ArgumentNullException.ThrowIfNull(usuario);
        var reservas = _refeicaoController.ListarReservasPorUsuario(usuario.Codigo);

        PrepararTela("PERFIL");
        Console.WriteLine($"Código: {usuario.Codigo}");
        Console.WriteLine($"Nome: {usuario.Nome}");
        Console.WriteLine($"E-mail: {usuario.Email}");
        Console.WriteLine($"Saldo: {usuario.Saldo:C}");
        Console.WriteLine("\nRefeições:");

        if (reservas.Count == 0)
        {
            Console.WriteLine("Nenhuma refeição adquirida.");
        }
        else
        {
            foreach (var reserva in reservas.OrderBy(reserva => reserva.Dia))
            {
                var status = reserva.Status == StatusReserva.Ativa ? "Ativa" : "Devolvida";
                Console.WriteLine($"- {FormatarDia(reserva.Dia)} — {reserva.ValorPago:C} — {status}");
            }
        }

        AguardarContinuacao();
    }
}
