using Models;

namespace RUtatouille.Tests;

public class UsuarioTests
{
  [Fact]
  public void AdicionarCredito_AumentaSaldo()
  {
    var usuario = CriarUsuario();

    usuario.AdicionarCredito(10m);

    Assert.Equal(10m, usuario.Saldo);
  }

  [Fact]
  public void DebitarCredito_ReduzSaldo()
  {
    var usuario = CriarUsuario();
    usuario.AdicionarCredito(10m);

    usuario.DebitarCredito(2.50m);

    Assert.Equal(7.50m, usuario.Saldo);
  }

  [Theory]
  [InlineData(0)]
  [InlineData(-1)]
  public void AlterarSaldo_ComValorNaoPositivo_LancaExcecao(decimal valor)
  {
    var usuario = CriarUsuario();

    Assert.Throws<ArgumentOutOfRangeException>(() => usuario.AdicionarCredito(valor));
    Assert.Throws<ArgumentOutOfRangeException>(() => usuario.DebitarCredito(valor));
  }

  [Fact]
  public void DebitarCredito_ComSaldoInsuficiente_NaoAlteraSaldo()
  {
    var usuario = CriarUsuario();
    usuario.AdicionarCredito(1m);

    Assert.Throws<InvalidOperationException>(() => usuario.DebitarCredito(2.50m));
    Assert.Equal(1m, usuario.Saldo);
  }

  private static Usuario CriarUsuario()
  {
    return new Usuario("1", "usuario@unesp.br", "Usuário");
  }
}
