public class Usuario
{
  public string Codigo { get; set; }
  public string Email { get; private set; }
  public string Nome { get; private set; }
  public double Extrato { get; private set; }

  public Usuario(string codigo, string email, string nome)
  {
    Codigo = codigo;
    Email = email;
    Nome = nome;
    Extrato = 0.0;
  }
  
  public void AdicionarCredito(double valor)
  {
    if (valor <= 0)
      throw new ArgumentException("O valor deve ser positivo");
    Extrato += valor;
  }
}
