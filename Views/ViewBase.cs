namespace Views;

using Models;

public abstract class ViewBase
{
  protected static void PrepararTela(string titulo)
  {
    if (!Console.IsOutputRedirected)
      Console.Clear();

    Console.WriteLine("==================================");
    Console.WriteLine($"\t{titulo}");
    Console.WriteLine("==================================");
    Console.WriteLine();
  }

  protected static string? LerEntrada(string mensagem)
  {
    Console.Write(mensagem);
    return Console.ReadLine();
  }

  protected static void ExibirErro(Exception excecao)
  {
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"\n{excecao.Message}");
    Console.ResetColor();
    AguardarContinuacao();
  }

  protected static void ExibirSucesso(string mensagem)
  {
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\n{mensagem}");
    Console.ResetColor();
  }

  protected static void AguardarContinuacao()
  {
    Console.WriteLine("\nPressione [Enter] para continuar...");
    Console.ReadLine();
  }

  protected static string FormatarDia(DiaSemana dia)
  {
    return dia switch
    {
      DiaSemana.Segunda => "Segunda-feira",
      DiaSemana.Terca => "Terça-feira",
      DiaSemana.Quarta => "Quarta-feira",
      DiaSemana.Quinta => "Quinta-feira",
      DiaSemana.Sexta => "Sexta-feira",
      _ => dia.ToString()
    };
  }
}
