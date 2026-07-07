public class Usuario
{
  public string Codigo { get; set; }
  private string Email { get; set; }
  private string Nome { get; set; }
  private double Extrato { get; set; }

  public Usuario CriarUsuario(string codigo, string email, string nome)
  {
    return new Usuario()
    {
      Codigo = codigo,
      Email = email,
      Nome = nome,
      Extrato = 0.0
    };
  }
  public void AdicionarCredito(double valor)
  {
    Extrato += valor;
  }
}
