using Controllers;
using Repositories;
using System.Globalization;
using Views;

namespace RUtatouille;

class Program
{
  public static void Main(string[] args)
  {
    CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
    CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");

    var usuarioRepository = new InMemoryUsuarioRepository();
    var refeicaoRepository = new InMemoryRefeicaoRepository();
    var reservaRepository = new InMemoryReservaRepository();

    var usuarioController = new UsuarioController(usuarioRepository);
    var refeicaoController = new RefeicaoController(
      refeicaoRepository,
      usuarioRepository,
      reservaRepository
    );

    var bemVindoView = new BemVindoView();
    var criarUsuarioView = new CriarUsuarioView(usuarioController);
    var loginView = new LoginView(usuarioController);
    var homeView = new HomeView();
    var comprarCreditosView = new ComprarCreditosView(usuarioController);
    var adquirirRefeicaoView = new AdquirirRefeicaoView(refeicaoController);
    var devolverRefeicaoView = new DevolverRefeicaoView(refeicaoController);
    var perfilView = new PerfilView(refeicaoController);

    var aplicacaoController = new AplicacaoController(
      bemVindoView,
      criarUsuarioView,
      loginView,
      homeView,
      comprarCreditosView,
      adquirirRefeicaoView,
      devolverRefeicaoView,
      perfilView);

    aplicacaoController.Executar();
  }
}
