using System;
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

        public List<ClienteViewModel> ListarClientes(string? nome = null, string? cpf = null, string? cnpj = null, string? email = null)
        {
            var nomeFiltrado = string.IsNullOrWhiteSpace(nome) ? null : nome.Trim();
            var emailFiltrado = string.IsNullOrWhiteSpace(email) ? null : email.Trim();
            var cpfLimpo = cpf.LimparMascara();
            var cnpjLimpo = cnpj.LimparMascara();

            var clientes = clienteDAL.ListarClientes(nomeFiltrado, cpfLimpo, cnpjLimpo, emailFiltrado);
            return mapper.Map<List<ClienteViewModel>>(clientes);
        }

        public void SalvarCliente(ClienteViewModel cliente)
        {
            ValidarDocumento(cliente);

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

        private void ValidarDocumento(ClienteViewModel cliente)
        {
            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente));
            }

            var cpfLimpo = cliente.CPF.LimparMascara();
            var cnpjLimpo = cliente.CNPJ.LimparMascara();

            var possuiCpf = !string.IsNullOrWhiteSpace(cpfLimpo);
            var possuiCnpj = !string.IsNullOrWhiteSpace(cnpjLimpo);

            if (possuiCpf && possuiCnpj)
            {
                throw new ArgumentException("Informe apenas CPF ou CNPJ.");
            }

            if (!possuiCpf && !possuiCnpj)
            {
                throw new ArgumentException("Informe um CPF ou CNPJ.");
            }

            var idParaIgnorar = cliente.Id == 0 ? (int?)null : cliente.Id;

            if (possuiCpf)
            {
                if (!cliente.CPF.EhCpfValido())
                {
                    throw new ArgumentException("CPF invalido.");
                }

                if (cpfLimpo != null && clienteDAL.ExisteCpf(cpfLimpo, idParaIgnorar))
                {
                    throw new ArgumentException("CPF ja cadastrado.");
                }
            }

            if (possuiCnpj)
            {
                if (!cliente.CNPJ.EhCnpjValido())
                {
                    throw new ArgumentException("CNPJ invalido.");
                }

                if (cnpjLimpo != null && clienteDAL.ExisteCnpj(cnpjLimpo, idParaIgnorar))
                {
                    throw new ArgumentException("CNPJ ja cadastrado.");
                }
            }
        }
    }
}
