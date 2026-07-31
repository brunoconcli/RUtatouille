using Controllers;
using Repositories;

namespace RUtatouille.Tests;

public class UsuarioControllerTests
{
  [Fact]
  public void RegistrarUsuario_AdicionaUsuarioQuePodeSerConsultado()
  {
    var repository = new InMemoryUsuarioRepository();
    var controller = new UsuarioController(repository);

    var usuarioRegistrado = controller.RegistrarUsuario("usuario@unesp.br", "Usuário");

    var usuario = repository.ObterPorCodigo("1");
    Assert.NotNull(usuario);
    Assert.Same(usuarioRegistrado, usuario);
    Assert.Equal("usuario@unesp.br", usuario.Email);
    Assert.Equal("Usuário", usuario.Nome);
    Assert.Equal(0m, usuario.Saldo);
  }

  [Fact]
  public void RegistrarUsuarios_GeraCodigosSequenciais()
  {
    var repository = new InMemoryUsuarioRepository();
    var controller = new UsuarioController(repository);

    controller.RegistrarUsuario("primeiro@unesp.br", "Primeiro");
    controller.RegistrarUsuario("segundo@unesp.br", "Segundo");

    Assert.NotNull(repository.ObterPorCodigo("1"));
    Assert.NotNull(repository.ObterPorCodigo("2"));
  }

  [Fact]
  public void ObterPorCodigo_ComUsuarioExistente_RetornaUsuario()
  {
    var repository = new InMemoryUsuarioRepository();
    var controller = new UsuarioController(repository);
    var usuarioRegistrado = controller.RegistrarUsuario("usuario@unesp.br", "Usuário");

    var usuarioEncontrado = controller.ObterPorCodigo(usuarioRegistrado.Codigo);

    Assert.Same(usuarioRegistrado, usuarioEncontrado);
  }

  [Fact]
  public void ObterPorCodigo_ComUsuarioInexistente_LancaExcecao()
  {
    var controller = new UsuarioController(new InMemoryUsuarioRepository());

    Assert.Throws<InvalidOperationException>(() => controller.ObterPorCodigo("999"));
  }

  [Theory]
  [InlineData("")]
  [InlineData("   ")]
  public void ObterPorCodigo_ComCodigoVazio_LancaExcecao(string codigo)
  {
    var controller = new UsuarioController(new InMemoryUsuarioRepository());

    Assert.Throws<ArgumentException>(() => controller.ObterPorCodigo(codigo));
  }
}
