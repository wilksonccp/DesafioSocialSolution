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

        public List<ImovelModel> ListarImoveis()
        {
            List<ImovelModel> Imovels = new List<ImovelModel>();
            using (var connection = contexto.CreateConnection())
            {
                Imovels = connection.GetAll<ImovelModel>().ToList();
            }

            return Imovels;
        }

        public ImovelModel SelecionarImovel(int id)
        {
            ImovelModel Imovel;
            using (var connection = contexto.CreateConnection())
            {
                Imovel = connection.Get<ImovelModel>(id);
            }

            return Imovel;
        }
    }
}
