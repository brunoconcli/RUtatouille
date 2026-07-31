namespace Views;

public enum OpcaoBemVindo
{
    Login = 1,
    CriarUsuario = 2,
    Sair = 0
}

public class BemVindoView : ViewBase
{
    public OpcaoBemVindo Exibir()
    {
        while (true)
        {
            PrepararTela("BEM-VINDO");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Criar usuário");
            Console.WriteLine();
            Console.WriteLine("0. Fechar");

            var entrada = LerEntrada("\nEscolha uma opção: ");

            if (entrada is null)
                return OpcaoBemVindo.Sair;

            if (int.TryParse(entrada, out var opcao) && Enum.IsDefined(typeof(OpcaoBemVindo), opcao))
                return (OpcaoBemVindo)opcao;

            Console.WriteLine("\nOpção inválida.");
            AguardarContinuacao();
        }
    }
}
