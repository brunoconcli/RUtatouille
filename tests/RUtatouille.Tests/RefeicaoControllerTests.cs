using Controllers;
using Models;
using Repositories;

namespace RUtatouille.Tests;

public class RefeicaoControllerTests
{
  [Fact]
  public void AdquirirRefeicao_ComSaldoInsuficiente_NaoCriaReserva()
  {
    var contexto = CriarContexto(1m);

    Assert.Throws<InvalidOperationException>(() =>
      contexto.Controller.AdquirirRefeicao(contexto.Usuario.Codigo, DiaSemana.Segunda));

    Assert.Empty(contexto.ReservaRepository.ListarPorUsuario(contexto.Usuario.Codigo));
    Assert.Equal(1m, contexto.Usuario.Saldo);
  }

  [Fact]
  public void AdquirirRefeicao_DuasVezesNoMesmoDia_ImpedeReservaDuplicada()
  {
    var contexto = CriarContexto(10m);
    contexto.Controller.AdquirirRefeicao(contexto.Usuario.Codigo, DiaSemana.Segunda);

    Assert.Throws<InvalidOperationException>(() =>
      contexto.Controller.AdquirirRefeicao(contexto.Usuario.Codigo, DiaSemana.Segunda));

    Assert.Single(contexto.ReservaRepository.ListarPorUsuario(contexto.Usuario.Codigo));
    Assert.Equal(7.50m, contexto.Usuario.Saldo);
  }

  [Fact]
  public void DevolverRefeicao_EstornaValorPagoEMarcaReservaComoDevolvida()
  {
    var contexto = CriarContexto(10m);
    contexto.Controller.AdquirirRefeicao(contexto.Usuario.Codigo, DiaSemana.Quarta);
    var reserva = Assert.Single(
      contexto.ReservaRepository.ListarPorUsuario(contexto.Usuario.Codigo));

    contexto.Controller.DevolverRefeicao(contexto.Usuario.Codigo, DiaSemana.Quarta);

    Assert.Equal(10m, contexto.Usuario.Saldo);
    Assert.Equal(StatusReserva.Devolvida, reserva.Status);
    Assert.NotNull(reserva.DataDevolucao);
  }

  [Fact]
  public void DevolverRefeicao_DuasVezes_ImpedeSegundaDevolucao()
  {
    var contexto = CriarContexto(10m);
    contexto.Controller.AdquirirRefeicao(contexto.Usuario.Codigo, DiaSemana.Sexta);
    contexto.Controller.DevolverRefeicao(contexto.Usuario.Codigo, DiaSemana.Sexta);

    Assert.Throws<InvalidOperationException>(() =>
      contexto.Controller.DevolverRefeicao(contexto.Usuario.Codigo, DiaSemana.Sexta));

    Assert.Equal(10m, contexto.Usuario.Saldo);
  }

  [Fact]
  public void AdquirirRefeicao_ComUsuarioInexistente_LancaExcecao()
  {
    var controller = new RefeicaoController(
      new InMemoryRefeicaoRepository(),
      new InMemoryUsuarioRepository(),
      new InMemoryReservaRepository());

    Assert.Throws<InvalidOperationException>(() =>
      controller.AdquirirRefeicao("999", DiaSemana.Segunda));
  }

  [Fact]
  public void AdquirirRefeicao_SemRefeicaoNoDia_LancaExcecao()
  {
    var usuarioRepository = new InMemoryUsuarioRepository();
    var usuario = new Usuario("1", "usuario@unesp.br", "Usuário");
    usuario.AdicionarCredito(10m);
    usuarioRepository.Adicionar(usuario);
    var controller = new RefeicaoController(
      new RefeicaoRepositoryVazio(),
      usuarioRepository,
      new InMemoryReservaRepository());

    Assert.Throws<InvalidOperationException>(() =>
      controller.AdquirirRefeicao(usuario.Codigo, DiaSemana.Segunda));
  }

  [Fact]
  public void ListarReservasPorUsuario_RetornaAtivasEDevolvidas()
  {
    var contexto = CriarContexto(10m);
    contexto.Controller.AdquirirRefeicao(contexto.Usuario.Codigo, DiaSemana.Segunda);
    contexto.Controller.AdquirirRefeicao(contexto.Usuario.Codigo, DiaSemana.Terca);
    contexto.Controller.DevolverRefeicao(contexto.Usuario.Codigo, DiaSemana.Segunda);

    var reservas = contexto.Controller.ListarReservasPorUsuario(contexto.Usuario.Codigo);

    Assert.Equal(2, reservas.Count);
    Assert.Contains(reservas, reserva => reserva.Status == StatusReserva.Ativa);
    Assert.Contains(reservas, reserva => reserva.Status == StatusReserva.Devolvida);
  }

  [Fact]
  public void ListarReservasAtivas_NaoRetornaReservasDevolvidas()
  {
    var contexto = CriarContexto(10m);
    contexto.Controller.AdquirirRefeicao(contexto.Usuario.Codigo, DiaSemana.Quarta);
    contexto.Controller.AdquirirRefeicao(contexto.Usuario.Codigo, DiaSemana.Quinta);
    contexto.Controller.DevolverRefeicao(contexto.Usuario.Codigo, DiaSemana.Quarta);

    var reservas = contexto.Controller.ListarReservasAtivas(contexto.Usuario.Codigo);

    var reserva = Assert.Single(reservas);
    Assert.Equal(DiaSemana.Quinta, reserva.Dia);
    Assert.Equal(StatusReserva.Ativa, reserva.Status);
  }

  [Fact]
  public void ListarReservas_DeUsuarioSemReservas_RetornaListaVazia()
  {
    var contexto = CriarContexto(10m);

    var reservas = contexto.Controller.ListarReservasPorUsuario(contexto.Usuario.Codigo);
    var reservasAtivas = contexto.Controller.ListarReservasAtivas(contexto.Usuario.Codigo);

    Assert.Empty(reservas);
    Assert.Empty(reservasAtivas);
  }

  [Fact]
  public void ListarReservas_DeUsuarioInexistente_LancaExcecao()
  {
    var controller = new RefeicaoController(
      new InMemoryRefeicaoRepository(),
      new InMemoryUsuarioRepository(),
      new InMemoryReservaRepository());

    Assert.Throws<InvalidOperationException>(() =>
      controller.ListarReservasPorUsuario("999"));
    Assert.Throws<InvalidOperationException>(() =>
      controller.ListarReservasAtivas("999"));
  }

  private static ContextoTeste CriarContexto(decimal saldoInicial)
  {
    var usuarioRepository = new InMemoryUsuarioRepository();
    var reservaRepository = new InMemoryReservaRepository();
    var usuario = new Usuario("1", "usuario@unesp.br", "Usuário");

    if (saldoInicial > 0)
      usuario.AdicionarCredito(saldoInicial);

    usuarioRepository.Adicionar(usuario);

    var controller = new RefeicaoController(
      new InMemoryRefeicaoRepository(),
      usuarioRepository,
      reservaRepository);

    return new ContextoTeste(controller, reservaRepository, usuario);
  }

  private sealed record ContextoTeste(
    RefeicaoController Controller,
    InMemoryReservaRepository ReservaRepository,
    Usuario Usuario);

  private sealed class RefeicaoRepositoryVazio : IRefeicaoRepository
  {
    public Refeicao? ObterPorDia(DiaSemana dia) => null;

    public IReadOnlyDictionary<DiaSemana, Refeicao> ListarTodas()
    {
      return new Dictionary<DiaSemana, Refeicao>();
    }
  }
}
