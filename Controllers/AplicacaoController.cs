namespace Controllers;

using Models;
using Views;

public class AplicacaoController
{
    private readonly BemVindoView _bemVindoView;
    private readonly CriarUsuarioView _criarUsuarioView;
    private readonly LoginView _loginView;
    private readonly HomeView _homeView;
    private readonly ComprarCreditosView _comprarCreditosView;
    private readonly AdquirirRefeicaoView _adquirirRefeicaoView;
    private readonly DevolverRefeicaoView _devolverRefeicaoView;
    private readonly PerfilView _perfilView;

    public AplicacaoController(
      BemVindoView bemVindoView,
      CriarUsuarioView criarUsuarioView,
      LoginView loginView,
      HomeView homeView,
      ComprarCreditosView comprarCreditosView,
      AdquirirRefeicaoView adquirirRefeicaoView,
      DevolverRefeicaoView devolverRefeicaoView,
      PerfilView perfilView
    )
    {
        _bemVindoView = bemVindoView ?? throw new ArgumentNullException(nameof(bemVindoView));
        _criarUsuarioView = criarUsuarioView ?? throw new ArgumentNullException(nameof(criarUsuarioView));
        _loginView = loginView ?? throw new ArgumentNullException(nameof(loginView));
        _homeView = homeView ?? throw new ArgumentNullException(nameof(homeView));
        _comprarCreditosView = comprarCreditosView ??
          throw new ArgumentNullException(nameof(comprarCreditosView));
        _adquirirRefeicaoView = adquirirRefeicaoView ??
          throw new ArgumentNullException(nameof(adquirirRefeicaoView));
        _devolverRefeicaoView = devolverRefeicaoView ??
          throw new ArgumentNullException(nameof(devolverRefeicaoView));
        _perfilView = perfilView ?? throw new ArgumentNullException(nameof(perfilView));
    }

    public void Executar()
    {
        while (true)
        {
            switch (_bemVindoView.Exibir())
            {
                case OpcaoBemVindo.Login:
                    var usuario = _loginView.Exibir();
                    if (usuario is not null)
                        ExecutarSessao(usuario);
                    break;

                case OpcaoBemVindo.CriarUsuario:
                    _criarUsuarioView.Exibir();
                    break;

                case OpcaoBemVindo.Sair:
                    return;
            }
        }
    }

    private void ExecutarSessao(Usuario usuario)
    {
        while (true)
        {
            var opcao = _homeView.Exibir(usuario);

            switch (opcao)
            {
                case OpcaoHome.AdquirirRefeicao:
                    _adquirirRefeicaoView.Exibir(usuario);
                    break;

                case OpcaoHome.DevolverRefeicao:
                    _devolverRefeicaoView.Exibir(usuario);
                    break;

                case OpcaoHome.ComprarCreditos:
                    _comprarCreditosView.Exibir(usuario);
                    break;

                case OpcaoHome.Perfil:
                    _perfilView.Exibir(usuario);
                    break;

                case OpcaoHome.Sair:
                    return;
            }
        }
    }
}
