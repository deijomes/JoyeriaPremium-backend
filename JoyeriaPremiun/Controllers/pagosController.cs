using JoyeriaPremiun.Settings;
using Microsoft.AspNetCore.Mvc;

namespace JoyeriaPremiun.Controllers
{
    [ApiController]
    [Route("api/pagos")]
    public class pagosController: ControllerBase
    {
        private readonly IPayPalService payPalService;

        public pagosController(IPayPalService payPalService)
        {
            this.payPalService = payPalService;
        }

        [HttpPost("crear-orden")]
        public async Task<IActionResult> CrearOrden([FromBody] CreateOrderRequest request)
        {
            try
            {
                var result = await payPalService.CreateOrder(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            
        }

       

        [HttpPost("capturar-orden")]
        public async Task<IActionResult> Capturar([FromBody] CapturaRequest request)
        {
            var resultado = await payPalService.CaptureOrder(request.OrderId);
            return Ok(resultado);
        }

        
    }
}
