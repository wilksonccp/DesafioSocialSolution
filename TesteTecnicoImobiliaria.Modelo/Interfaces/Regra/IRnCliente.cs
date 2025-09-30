using TesteTecnicoImobiliaria.Modelo.ViewModels;

namespace TesteTecnicoImobiliaria.Modelo.Interfaces.Regra
{
    public interface IRnCliente
    {
        ClienteViewModel SelecionarCliente(int id);
        void SalvarCliente(ClienteViewModel cliente);
        List<ClienteViewModel> ListarClientes(string? nome = null, string? cpf = null, string? cnpj = null, string? email = null);
        void DesativarCliente(int id);
        void AtivarCliente(int id);
    }
}
