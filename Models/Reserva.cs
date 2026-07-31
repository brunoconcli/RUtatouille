namespace Models;

public enum StatusReserva
{
  Ativa,
  Devolvida
}

public class Reserva
{
  public int Id { get; }
  public string UsuarioCodigo { get; private set; }
  public DiaSemana Dia { get; private set; }
  public decimal ValorPago { get; private set; }
  public StatusReserva Status { get; private set; }
  public DateTime DataAquisicao { get; private set; }
  public DateTime? DataDevolucao { get; private set; }

  public Reserva(int id, string usuarioCodigo, DiaSemana dia, decimal valorPago)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(usuarioCodigo);

    if (id <= 0)
      throw new ArgumentOutOfRangeException(nameof(id), "O ID deve ser positivo.");
    if (!Enum.IsDefined(typeof(DiaSemana), dia))
      throw new ArgumentOutOfRangeException(nameof(dia), "O dia da semana é inválido.");
    if (valorPago <= 0)
      throw new ArgumentOutOfRangeException(nameof(valorPago), "O valor pago deve ser positivo.");

    Id = id;
    UsuarioCodigo = usuarioCodigo.Trim();
    Dia = dia;
    ValorPago = valorPago;
    Status = StatusReserva.Ativa;
    DataAquisicao = DateTime.Now;
    DataDevolucao = null;
  }

  public void Devolver()
  {
    if (Status == StatusReserva.Devolvida)
      throw new InvalidOperationException("Esta reserva já foi devolvida.");

    Status = StatusReserva.Devolvida;
    DataDevolucao = DateTime.Now;
  }
}
