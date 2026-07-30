namespace Controllers;

public class AplicacaoController
{
  private readonly UsuarioController _usuarioController;
  private readonly RefeicaoController _refeicaoController;

  public AplicacaoController(
    UsuarioController usuarioController,
    RefeicaoController refeicaoController
  )
  {
    _usuarioController = usuarioController;
    _refeicaoController = refeicaoController;
  }

  public void Executar()
  {
    // O fluxo de navegação entre as telas será iniciado aqui.
  }
}
