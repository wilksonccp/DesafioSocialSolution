using Microsoft.AspNetCore.Mvc;
using TesteTecnicoImobiliaria.Modelo.Interfaces.Regra;

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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ClienteController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ClienteController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ClienteController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClienteController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
