using System.Net.Mail;

namespace Models;

public class Usuario
{
  public string Codigo { get; }
  public string Email { get; private set; }
  public string Nome { get; private set; }
  public decimal Saldo { get; private set; }

  public Usuario(string codigo, string email, string nome)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(codigo);
    ArgumentException.ThrowIfNullOrWhiteSpace(email);
    ArgumentException.ThrowIfNullOrWhiteSpace(nome);

    if (!int.TryParse(codigo, out var codigoNumerico) || codigoNumerico <= 0)
      throw new ArgumentException("O código deve ser um número inteiro positivo.", nameof(codigo));

    if (!MailAddress.TryCreate(email, out _))
      throw new ArgumentException("O e-mail informado é inválido.", nameof(email));

    Codigo = codigo.Trim();
    Email = email.Trim();
    Nome = nome.Trim();
    Saldo = 0m;
  }
  
  public void AdicionarCredito(decimal valor)
  {
    if (valor <= 0)
      throw new ArgumentOutOfRangeException(nameof(valor), "O valor deve ser positivo.");

    Saldo += valor;
  }

  public void DebitarCredito(decimal valor)
  {
    if (valor <= 0)
      throw new ArgumentOutOfRangeException(nameof(valor), "O valor deve ser positivo.");
    if (valor > Saldo)
      throw new InvalidOperationException("Saldo insuficiente.");

    Saldo -= valor;
  }
}

