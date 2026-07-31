using Models;

namespace Repositories;

public class InMemoryUsuarioRepository : IUsuarioRepository
{
    private readonly List<Usuario> _usuarios = new();
    public void Adicionar(Usuario usuario)
    {
        ArgumentNullException.ThrowIfNull(usuario);

        if (ObterPorCodigo(usuario.Codigo) is not null)
            throw new InvalidOperationException("Já existe um usuário com esse código.");
        if (ObterPorEmail(usuario.Email) is not null)
            throw new InvalidOperationException("Já existe um usuário com esse e-mail.");

        _usuarios.Add(usuario);
    }

    public string GerarProximoCodigo()
    {
        return _usuarios.Count == 0
          ? "1"
          : (_usuarios.Max(usuario => int.Parse(usuario.Codigo)) + 1).ToString();
    }

    public Usuario? ObterPorCodigo(string codigo)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(codigo);
        return _usuarios.FirstOrDefault(usuario => usuario.Codigo == codigo.Trim());
    }

    public Usuario? ObterPorEmail(string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        return _usuarios.FirstOrDefault(usuario =>
          string.Equals(usuario.Email, email.Trim(), StringComparison.OrdinalIgnoreCase));
    }
}
