using Microsoft.AspNetCore.Mvc;

namespace TesteTecnico.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DesafioTecnicoController : ControllerBase
    {
        public DesafioTecnicoController() { }

        [HttpGet(Name = "NumeroPorExtenso/{numero}")]
        public string GetNumeroPorExtenso(int numero)
        {
            return null;
        }
    }
}
