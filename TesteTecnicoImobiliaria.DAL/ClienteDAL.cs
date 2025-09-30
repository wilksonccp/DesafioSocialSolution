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

        public List<ClienteModel> ListarClientes(string? nome = null, string? cpf = null, string? cnpj = null, string? email = null)
        {
            using (var connection = contexto.CreateConnection())
            {
                const string query = @"SELECT *
                                       FROM CLIENTE
                                       WHERE (@nome IS NULL OR UPPER(NM_CLIENTE) LIKE '%' + UPPER(@nome) + '%')
                                         AND (@cpf IS NULL OR NR_CPF = @cpf)
                                         AND (@cnpj IS NULL OR NR_CNPJ = @cnpj)
                                         AND (@email IS NULL OR UPPER(DS_EMAIL) LIKE '%' + UPPER(@email) + '%')";

                var parametros = new
                {
                    nome = string.IsNullOrWhiteSpace(nome) ? null : nome.Trim(),
                    cpf,
                    cnpj,
                    email = string.IsNullOrWhiteSpace(email) ? null : email.Trim()
                };

                return connection.Query<ClienteModel>(query, parametros).ToList();
            }
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
