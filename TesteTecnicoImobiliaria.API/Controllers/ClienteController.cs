using Microsoft.AspNetCore.Mvc;
using TesteTecnicoImobiliaria.Modelo.Interfaces.Regra;
using TesteTecnicoImobiliaria.Modelo.ViewModels;

namespace TesteTecnicoImobiliaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IRnCliente rnCliente;

        public ClienteController(IRnCliente rnCliente)
        {
            this.rnCliente = rnCliente;
        }

        // GET: api/<ClienteController>
        [HttpGet]
        public IEnumerable<ClienteViewModel> Get()
        {
            return rnCliente.ListarClientes();
        }

        // GET api/<ClienteController>/5
        [HttpGet("{id}")]
        public ClienteViewModel Get(int id)
        {
            return rnCliente.SelecionarCliente(id);
        }

        // POST api/<ClienteController>
        [HttpPost]
        public void Post([FromBody] ClienteViewModel cliente)
        {
            rnCliente.SalvarCliente(cliente);
        }

        // POST api/<ClienteController>/Ativar/5
        [HttpPost]
        [Route("Ativar/{id}")]
        public void AtivarCliente(int id)
        {
            rnCliente.AtivarCliente(id);
        }

        // POST api/<ClienteController>/Desativar/5
        [HttpPost]
        [Route("Desativar/{id}")]
        public void DesativarCliente(int id)
        {
            rnCliente.DesativarCliente(id);
        }
    }
}
