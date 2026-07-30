using Controllers;
using Repositories;

namespace RUtatouille;

class Program
{
  public static void Main(string[] args)
  {
    var usuarioRepository = new InMemoryUsuarioRepository();
    var refeicaoRepository = new InMemoryRefeicaoRepository();
    var reservaRepository = new InMemoryReservaRepository();

    var usuarioController = new UsuarioController(usuarioRepository);
    var refeicaoController = new RefeicaoController(
      refeicaoRepository,
      usuarioRepository,
      reservaRepository
    );

    var aplicacaoController = new AplicacaoController(
      usuarioController,
      refeicaoController
    );

    aplicacaoController.Executar();
  }
}
