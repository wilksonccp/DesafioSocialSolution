using TesteTecnicoImobiliaria.Modelo.Models;

namespace TesteTecnicoImobiliaria.Modelo.Interfaces
{
    public interface IClienteDAL
    {
        void CadastrarCliente(ClienteModel cliente);
        void AtualizarCliente(ClienteModel cliente);
        List<ClienteModel> ListarClientes();
        void DesativarCliente(int id);
        void AtivarCliente(int id);
        ClienteModel SelecionarCliente(int id);
    }
}
