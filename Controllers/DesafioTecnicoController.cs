using Microsoft.AspNetCore.Mvc;
using TesteTecnico.Business.Services;

namespace TesteTecnico.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DesafioTecnicoController : ControllerBase
    {
        private readonly DesafioTecnicoServico _desafioTecnicoServico;
        public DesafioTecnicoController() 
        {;
            _desafioTecnicoServico = new DesafioTecnicoServico();
        }

        [HttpGet(Name = "NumeroPorExtenso/{numero}")]
        public string GetNumeroPorExtenso(long numero)
        {
            return _desafioTecnicoServico.GetNumeroPorExtenso(numero);
        }
    }
}
