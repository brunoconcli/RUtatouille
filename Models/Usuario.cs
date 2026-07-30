namespace Models;

public class Usuario
{
  public string Codigo { get; set; }
  public string Email { get; private set; }
  public string Nome { get; private set; }
  public decimal Saldo { get; private set; }

  public Usuario(string codigo, string email, string nome)
  {
    Codigo = codigo;
    Email = email;
    Nome = nome;
    Saldo = 0m;
  }
  
  public void AdicionarCredito(decimal valor)
  {
    if (valor <= 0)
      throw new ArgumentException("O valor deve ser positivo");
    Saldo += valor;
  }

  public void DebitarCredito(decimal valor)
  {
    if (valor <= 0)
      throw new ArgumentException("O valor deve ser positivo!!");
    if (valor > Saldo)
      throw new ArgumentException("O valor deve ser menor ou igual ao saldo disponível!!");

    Saldo -= valor;
  }
}

