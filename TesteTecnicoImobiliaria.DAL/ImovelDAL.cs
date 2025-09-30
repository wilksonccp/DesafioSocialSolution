using System;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using TesteTecnicoImobiliaria.Modelo.Interfaces;
using TesteTecnicoImobiliaria.Modelo.Interfaces.DAL;
using TesteTecnicoImobiliaria.Modelo.Models;

namespace TesteTecnicoImobiliaria.DAL
{
    internal class ImovelDAL : IImovelDAL
    {
        private readonly IContextDAL contexto;

        public ImovelDAL(IContextDAL contexto)
        {
            this.contexto = contexto;
        }

        public void AtualizarImovel(ImovelModel Imovel)
        {
            using (var connection = contexto.CreateConnection())
            {
                connection.Update<ImovelModel>(Imovel);
            }
        }

        public void CadastrarImovel(ImovelModel Imovel)
        {
            using (var connection = contexto.CreateConnection())
            {
                connection.Insert<ImovelModel>(Imovel);
            }
        }

        public void AtivarImovel(int id)
        {
            using (var connection = contexto.CreateConnection())
            {
                var query = "UPDATE IMOVEL SET FL_ATIVO = 1 WHERE CD_IMOVEL = @id";
                connection.Execute(query, new { id });
            }
        }

        public void DesativarImovel(int id)
        {
            using (var connection = contexto.CreateConnection())
            {
                var query = "UPDATE IMOVEL SET FL_ATIVO = 0 WHERE CD_IMOVEL = @id";
                connection.Execute(query, new { id });
            }
        }

        public List<ImovelModel> ListarImoveis(decimal? valorMaximo = null, DateTime? dataPublicacao = null, int? tipoNegocio = null)
        {
            using (var connection = contexto.CreateConnection())
            {
                const string query = @"SELECT I.*
                                       FROM IMOVEL AS I
                                       INNER JOIN CLIENTE AS C ON I.CD_CLIENTE = C.CD_CLIENTE
                                       WHERE C.FL_ATIVO = 1
                                         AND (@valorMaximo IS NULL OR I.VL_IMOVEL <= @valorMaximo)
                                         AND (@dataPublicacao IS NULL OR CAST(I.DT_PUBLICACAO AS DATE) = @dataPublicacao)
                                         AND (@tipoNegocio IS NULL OR I.CD_TIPO_IMOVEL = @tipoNegocio)";

                var parametros = new
                {
                    valorMaximo,
                    dataPublicacao = dataPublicacao?.Date,
                    tipoNegocio
                };

                return connection.Query<ImovelModel>(query, parametros).ToList();
            }
        }

        public ImovelModel SelecionarImovel(int id)
        {
            ImovelModel imovel;
            using (var connection = contexto.CreateConnection())
            {
                imovel = connection.Get<ImovelModel>(id);
            }

            return imovel;
        }

        public bool ClientePossuiImovelAtivo(int idCliente)
        {
            using (var connection = contexto.CreateConnection())
            {
                const string query = "SELECT COUNT(1) FROM IMOVEL WHERE CD_CLIENTE = @idCliente AND FL_ATIVO = 1";
                var quantidade = connection.ExecuteScalar<int>(query, new { idCliente });
                return quantidade > 0;
            }
        }
    }
}
