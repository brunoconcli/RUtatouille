namespace RUtatouille;

class Refeicao
{
  public string? principal;
  public string? sobremesa;
  public string? suco;
}

class Usuario
{
  public string? nome;
  public string? codigo;
  public double extrato;
}

class Program
{
  public static void Main(string[] args)
  {
    // Set up console styling
    Console.Title = "Basic User Interaction App";
    Console.ForegroundColor = ConsoleColor.Cyan;

    Console.WriteLine(
      "==================================\n" +
      "\tRUtatouille\t\n" +
      "==================================\n"
    );
    Console.ResetColor();
    var refeicoes = new Dictionary<string, Refeicao>
    {
      {"segunda", new Refeicao() {principal="bife a caçarola", sobremesa="paçoca", suco="uva"}},
      {"terca", new Refeicao() {principal="franguinho", sobremesa="sagu de abacaxi", suco="laranja"}},
    };

    var usuarios = new Dictionary<string, Usuario>
    {
      {"123", new Usuario() { nome = "bruno", codigo = "123", extrato = 12.5 }}
    };

    var codigosValidos = new Dictionary<string, string>
    {
      {"123", "Bruno"},
      {"456", "Caio"},
      {"789", "Vini"}
    };
    bool isCodigoValido = false;
    var userName = "";

    while (!isCodigoValido)
    {
      Console.Clear();
      Console.Write("Bem vindo\nInsira seu codigo: ");
      string codigo = Console.ReadLine() ?? "";

      if (codigosValidos.ContainsKey(codigo))
      {
        isCodigoValido = true;
        userName = codigosValidos[codigo];
      }
      else
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Error: Please enter a valid positive number.");
        Console.ResetColor();
      }
    }
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"Olá, {userName}!");
    Console.ResetColor();

    Console.WriteLine("\n[ENTER] para deixar a aplicação...");
    Console.ReadKey();
  }
}