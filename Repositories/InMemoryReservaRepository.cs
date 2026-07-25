using Models;
namespace Repositories;

public class InMemoryReservaRepository : IReservaRepository
{
  private readonly List<Reserva> _reservas = new();
  private int _proximoId = 1;

  public void Adicionar(Reserva reserva)
  {
    _reservas.Add(reserva);
  }

  public int GerarProximoId()
  {
    return _proximoId++;
  }

  public IEnumerable<Reserva> ListarPorUsuario(string codigo)
  {
    return _reservas.Where(reserva => reserva.UsuarioCodigo == codigo);
  }

  public Reserva? ObterPorCodigoDeUsuarioEDia(string codigo, DiaSemana diaSemana)
  {
    return _reservas.FirstOrDefault(reserva =>
      reserva.UsuarioCodigo == codigo &&
      reserva.Dia == diaSemana &&
      reserva.Status == StatusReserva.Ativa);
  }
}