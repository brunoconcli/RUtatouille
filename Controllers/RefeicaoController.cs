using Models;
using Repositories;

namespace Controllers;

public class RefeicaoController
{
  private readonly IRefeicaoRepository _refeicaoRepository;
  private readonly IUsuarioRepository _usuarioRepository;
  private readonly IReservaRepository _reservaRepository;

  public RefeicaoController(IRefeicaoRepository refeicaoRepository, IUsuarioRepository usuarioRepository, IReservaRepository reservaRepository)
  {
    _refeicaoRepository = refeicaoRepository ??
      throw new ArgumentNullException(nameof(refeicaoRepository));
    _usuarioRepository = usuarioRepository ??
      throw new ArgumentNullException(nameof(usuarioRepository));
    _reservaRepository = reservaRepository ??
      throw new ArgumentNullException(nameof(reservaRepository));
  }

  public void AdquirirRefeicao(string usuarioCodigo, DiaSemana diaSemana)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(usuarioCodigo);

    var refeicao = _refeicaoRepository.ObterPorDia(diaSemana) ??
      throw new InvalidOperationException("Não há refeições registradas para esse dia.");

    var usuario = _usuarioRepository.ObterPorCodigo(usuarioCodigo) ??
      throw new InvalidOperationException("O usuário referenciado não existe.");

    if (_reservaRepository.ObterPorCodigoDeUsuarioEDia(usuarioCodigo, diaSemana) != null)
      throw new InvalidOperationException("Você já tem uma reserva ativa para esse dia.");

    usuario.DebitarCredito(refeicao.Preco);

    var reserva = new Reserva(_reservaRepository.GerarProximoId(), usuarioCodigo, diaSemana, refeicao.Preco);
    _reservaRepository.Adicionar(reserva);
  }
  public void DevolverRefeicao(string usuarioCodigo, DiaSemana diaSemana)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(usuarioCodigo);

    var reserva = _reservaRepository.ObterPorCodigoDeUsuarioEDia(usuarioCodigo, diaSemana) ??
      throw new InvalidOperationException("Você não possui uma reserva ativa para esse dia.");

    var usuario = _usuarioRepository.ObterPorCodigo(usuarioCodigo) ??
      throw new InvalidOperationException("O usuário referenciado não existe.");

    reserva.Devolver();
    usuario.AdicionarCredito(reserva.ValorPago);
  }

  public IReadOnlyDictionary<DiaSemana, Refeicao> ListarCardapio()
  {
    return _refeicaoRepository.ListarTodas();
  }

  public IReadOnlyList<Reserva> ListarReservasPorUsuario(string usuarioCodigo)
  {
    ValidarUsuario(usuarioCodigo);

    return _reservaRepository
      .ListarPorUsuario(usuarioCodigo)
      .ToList()
      .AsReadOnly();
  }

  public IReadOnlyList<Reserva> ListarReservasAtivas(string usuarioCodigo)
  {
    return ListarReservasPorUsuario(usuarioCodigo)
      .Where(reserva => reserva.Status == StatusReserva.Ativa)
      .ToList()
      .AsReadOnly();
  }

  private Usuario ValidarUsuario(string usuarioCodigo)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(usuarioCodigo);

    return _usuarioRepository.ObterPorCodigo(usuarioCodigo) ??
      throw new InvalidOperationException("O usuário referenciado não existe.");
  }
}
