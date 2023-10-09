using Microsoft.AspNetCore.Mvc;
using TesteTecnicoImobiliaria.Modelo.Interfaces.Regra;

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
    }
}
