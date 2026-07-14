namespace Models;

public class Reserva
{
  public int Id { get; }
  public string UsuarioCodigo { get; private set; }
  public DiaSemana Dia { get; private set; }
}
