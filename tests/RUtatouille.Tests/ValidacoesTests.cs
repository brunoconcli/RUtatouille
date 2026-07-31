using Controllers;
using Models;
using Repositories;

namespace RUtatouille.Tests;

public class ValidacoesTests
{
  [Theory]
  [InlineData("", "usuario@unesp.br", "Usuário")]
  [InlineData("abc", "usuario@unesp.br", "Usuário")]
  [InlineData("0", "usuario@unesp.br", "Usuário")]
  [InlineData("1", "email-invalido", "Usuário")]
  [InlineData("1", "usuario@unesp.br", "")]
  public void CriarUsuario_ComDadosInvalidos_LancaExcecao(
    string codigo,
    string email,
    string nome)
  {
    Assert.Throws<ArgumentException>(() => new Usuario(codigo, email, nome));
  }

  [Theory]
  [InlineData("", "Arroz", "Fruta", "Suco")]
  [InlineData("Prato", "", "Fruta", "Suco")]
  [InlineData("Prato", "Arroz", "", "Suco")]
  [InlineData("Prato", "Arroz", "Fruta", "")]
  public void CriarRefeicao_ComComponenteVazio_LancaExcecao(
    string prato,
    string acompanhamento,
    string sobremesa,
    string suco)
  {
    Assert.Throws<ArgumentException>(() =>
      new Refeicao(prato, acompanhamento, sobremesa, suco, 2.50m));
  }

  [Fact]
  public void CriarReserva_ComIdInvalido_LancaExcecao()
  {
    Assert.Throws<ArgumentOutOfRangeException>(() =>
      new Reserva(0, "1", DiaSemana.Segunda, 2.50m));
  }

  [Fact]
  public void CriarReserva_ComDiaInvalido_LancaExcecao()
  {
    Assert.Throws<ArgumentOutOfRangeException>(() =>
      new Reserva(1, "1", (DiaSemana)99, 2.50m));
  }

  [Fact]
  public void AdicionarUsuario_ComCodigoDuplicado_LancaExcecao()
  {
    var repository = new InMemoryUsuarioRepository();
    repository.Adicionar(new Usuario("1", "primeiro@unesp.br", "Primeiro"));

    Assert.Throws<InvalidOperationException>(() =>
      repository.Adicionar(new Usuario("1", "segundo@unesp.br", "Segundo")));
  }

  [Fact]
  public void RegistrarUsuario_ComEmailDuplicadoIgnorandoMaiusculas_LancaExcecao()
  {
    var repository = new InMemoryUsuarioRepository();
    var controller = new UsuarioController(repository);
    controller.RegistrarUsuario("usuario@unesp.br", "Primeiro");

    Assert.Throws<InvalidOperationException>(() =>
      controller.RegistrarUsuario("USUARIO@UNESP.BR", "Segundo"));
  }

  [Fact]
  public void GerarProximoCodigo_UsaOMaiorCodigoExistente()
  {
    var repository = new InMemoryUsuarioRepository();
    repository.Adicionar(new Usuario("10", "dez@unesp.br", "Dez"));
    repository.Adicionar(new Usuario("2", "dois@unesp.br", "Dois"));

    Assert.Equal("11", repository.GerarProximoCodigo());
  }

  [Fact]
  public void AdicionarReservaAtivaDuplicadaDiretamente_LancaExcecao()
  {
    var repository = new InMemoryReservaRepository();
    repository.Adicionar(new Reserva(1, "1", DiaSemana.Segunda, 2.50m));

    Assert.Throws<InvalidOperationException>(() =>
      repository.Adicionar(new Reserva(2, "1", DiaSemana.Segunda, 2.50m)));
  }

  [Fact]
  public void ConsultarRepositorio_ComDiaInvalido_LancaExcecao()
  {
    var repository = new InMemoryRefeicaoRepository();

    Assert.Throws<ArgumentOutOfRangeException>(() =>
      repository.ObterPorDia((DiaSemana)99));
  }
}
