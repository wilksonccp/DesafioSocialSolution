using Microsoft.AspNetCore.Mvc;
using TesteTecnicoImobiliaria.Modelo.Interfaces.Regra;
using TesteTecnicoImobiliaria.Modelo.ViewModels;

namespace TesteTecnicoImobiliaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImovelController : ControllerBase
    {
        private readonly IRnImovel rnImovel;

        public ImovelController(IRnImovel rnImovel)
        {
            this.rnImovel = rnImovel;
        }

        // GET: api/<ImovelController>
        [HttpGet]
        public IEnumerable<ImovelViewModel> Get()
        {
            return rnImovel.ListarImoveis();
        }

        // GET api/<ImovelController>/5
        [HttpGet("{id}")]
        public ImovelViewModel Get(int id)
        {
            return rnImovel.SelecionarImovel(id);
        }

        // POST api/<ImovelController>
        [HttpPost]
        public void Post([FromBody] ImovelViewModel Imovel)
        {
            rnImovel.SalvarImovel(Imovel);
        }

        // POST api/<ImovelController>/Ativar/5
        [HttpPost]
        [Route("Ativar/{id}")]
        public void AtivarImovel(int id)
        {
            rnImovel.AtivarImovel(id);
        }

        // POST api/<ImovelController>/Desativar/5
        [HttpPost]
        [Route("Desativar/{id}")]
        public void DesativarImovel(int id)
        {
            rnImovel.DesativarImovel(id);
        }
    }
}
