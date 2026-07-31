using Models;

namespace Repositories;

public interface IReservaRepository
{
    void Adicionar(Reserva reserva);
    Reserva? ObterPorCodigoDeUsuarioEDia(string codigo, DiaSemana diaSemana);
    int GerarProximoId();
    IEnumerable<Reserva> ListarPorUsuario(string codigo);
}