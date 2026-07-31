using Models;

namespace Views;

public enum OpcaoHome
{
  AdquirirRefeicao = 1,
  DevolverRefeicao = 2,
  ComprarCreditos = 3,
  Perfil = 4,
  Sair = 0
}

public class HomeView : ViewBase
{
  public OpcaoHome Exibir(Usuario usuario)
  {
    ArgumentNullException.ThrowIfNull(usuario);

    while (true)
    {
      PrepararTela("PÁGINA INICIAL");
      Console.WriteLine($"Olá, {usuario.Nome}!");
      Console.WriteLine($"Saldo: {usuario.Saldo:C}\n");
      Console.WriteLine("1. Adquirir refeição");
      Console.WriteLine("2. Devolver refeição");
      Console.WriteLine("3. Comprar créditos");
      Console.WriteLine("4. Perfil");
      Console.WriteLine();
      Console.WriteLine("0. Sair");

      var entrada = LerEntrada("\nEscolha uma opção: ");

      if (entrada is null)
        return OpcaoHome.Sair;

      if (int.TryParse(entrada, out var opcao) && Enum.IsDefined(typeof(OpcaoHome), opcao))
        return (OpcaoHome)opcao;

      Console.WriteLine("\nOpção inválida.");
      AguardarContinuacao();
    }
  }
}
