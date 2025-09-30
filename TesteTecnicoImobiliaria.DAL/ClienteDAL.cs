using Dapper;
using Dapper.Contrib.Extensions;
using TesteTecnicoImobiliaria.Modelo.Interfaces;
using TesteTecnicoImobiliaria.Modelo.Interfaces.DAL;
using TesteTecnicoImobiliaria.Modelo.Models;

namespace TesteTecnicoImobiliaria.DAL
{
    internal class ClienteDAL : IClienteDAL
    {
        private readonly IContextDAL contexto;

        public ClienteDAL(IContextDAL contexto)
        {
            this.contexto = contexto;
        }

        public void AtualizarCliente(ClienteModel cliente)
        {
            using (var connection = contexto.CreateConnection())
            {
                connection.Update<ClienteModel>(cliente);
            }
        }

        public void CadastrarCliente(ClienteModel cliente)
        {
            if (cliente.NR_CPF != null)
            {
                cliente.NR_CNPJ = null;
            }
            else if (cliente.NR_CNPJ != null)
            {
                cliente.NR_CPF = null;
            }

            using (var connection = contexto.CreateConnection())
            {
                connection.Insert<ClienteModel>(cliente);
            }
        }

        public void AtivarCliente(int id)
        {
            using (var connection = contexto.CreateConnection())
            {
                var query = "UPDATE CLIENTE SET FL_ATIVO = 1 WHERE CD_CLIENTE = @id";
                connection.Execute(query, new { id });
            }
        }

        public void DesativarCliente(int id)
        {
            using (var connection = contexto.CreateConnection())
            {
                var query = "UPDATE CLIENTE SET FL_ATIVO = 0 WHERE CD_CLIENTE = @id";
                connection.Execute(query, new { id });
            }
        }

        public List<ClienteModel> ListarClientes()
        {
            List<ClienteModel> clientes = new List<ClienteModel>();
            using (var connection = contexto.CreateConnection())
            {
                clientes = connection.GetAll<ClienteModel>().ToList();
            }

            return clientes;
        }

        public ClienteModel SelecionarCliente(int id)
        {
            ClienteModel cliente;
            using (var connection = contexto.CreateConnection())
            {
                cliente = connection.Get<ClienteModel>(id);
            }

            return cliente;
        }

        public bool ExisteCpf(string cpf, int? ignorarId = null)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                return false;
            }

            using (var connection = contexto.CreateConnection())
            {
                const string query = "SELECT COUNT(1) FROM CLIENTE WHERE NR_CPF = @cpf AND (@ignorarId IS NULL OR CD_CLIENTE <> @ignorarId)";
                var quantidade = connection.ExecuteScalar<int>(query, new { cpf, ignorarId });
                return quantidade > 0;
            }
        }

        public bool ExisteCnpj(string cnpj, int? ignorarId = null)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
            {
                return false;
            }

            using (var connection = contexto.CreateConnection())
            {
                const string query = "SELECT COUNT(1) FROM CLIENTE WHERE NR_CNPJ = @cnpj AND (@ignorarId IS NULL OR CD_CLIENTE <> @ignorarId)";
                var quantidade = connection.ExecuteScalar<int>(query, new { cnpj, ignorarId });
                return quantidade > 0;
            }
        }
    }
}
