using AutoMapper;
using TesteTecnicoImobiliaria.Modelo.Interfaces;
using TesteTecnicoImobiliaria.Modelo.Interfaces.Regra;
using TesteTecnicoImobiliaria.Modelo.Models;
using TesteTecnicoImobiliaria.Modelo.ViewModels;

namespace TesteTecnicoImobiliaria.Regra
{
    internal class RnCliente : IRnCliente
    {
        private readonly IMapper mapper;
        private readonly IClienteDAL clienteDAL;

        public RnCliente(IMapper mapper, IClienteDAL clienteDAL)
        {
            this.mapper = mapper;
            this.clienteDAL = clienteDAL;
        }

        public ClienteViewModel SelecionarCliente(int id)
        {
            ClienteModel clienteModel = clienteDAL.SelecionarCliente(id);
            var cliente = mapper.Map<ClienteViewModel>(clienteModel);

            return cliente;
        }

        public void AtivarCliente(int id)
        {
            clienteDAL.AtivarCliente(id);
        }

        public void DesativarCliente(int id)
        {
            clienteDAL.DesativarCliente(id);
        }

        public List<ClienteViewModel> ListarClientes()
        {
            var retorno = new List<ClienteViewModel>();
            var clientes = clienteDAL.ListarClientes();
            retorno = mapper.Map<List<ClienteViewModel>>(clientes);

            return retorno;
        }

        public void SalvarCliente(ClienteViewModel cliente)
        {
            ClienteModel clienteModel = mapper.Map<ClienteModel>(cliente);
            if (cliente.Id == 0)
            {
                clienteDAL.CadastrarCliente(clienteModel);
            }
            else
            {
                clienteDAL.AtualizarCliente(clienteModel);
            }
        }
    }
}
