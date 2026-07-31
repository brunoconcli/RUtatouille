using Models;
namespace Repositories;

public class InMemoryReservaRepository : IReservaRepository
{
    private readonly List<Reserva> _reservas = new();
    private int _proximoId = 1;

    public void Adicionar(Reserva reserva)
    {
        ArgumentNullException.ThrowIfNull(reserva);

        if (_reservas.Any(item => item.Id == reserva.Id))
            throw new InvalidOperationException("Já existe uma reserva com esse ID.");
        if (ObterPorCodigoDeUsuarioEDia(reserva.UsuarioCodigo, reserva.Dia) is not null)
            throw new InvalidOperationException("O usuário já possui uma reserva ativa para esse dia.");

        _reservas.Add(reserva);
    }

    public int GerarProximoId()
    {
        return _proximoId++;
    }

    public IEnumerable<Reserva> ListarPorUsuario(string codigo)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(codigo);
        return _reservas.Where(reserva => reserva.UsuarioCodigo == codigo.Trim());
    }

    public Reserva? ObterPorCodigoDeUsuarioEDia(string codigo, DiaSemana diaSemana)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(codigo);
        ValidarDiaSemana(diaSemana);

        return _reservas.FirstOrDefault(reserva =>
          reserva.UsuarioCodigo == codigo.Trim() &&
          reserva.Dia == diaSemana &&
          reserva.Status == StatusReserva.Ativa);
    }

    private static void ValidarDiaSemana(DiaSemana diaSemana)
    {
        if (!Enum.IsDefined(typeof(DiaSemana), diaSemana))
            throw new ArgumentOutOfRangeException(nameof(diaSemana), "O dia da semana é inválido.");
    }
}
