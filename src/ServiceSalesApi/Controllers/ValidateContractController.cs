using Microsoft.AspNetCore.Mvc;
using Rebus.Bus;
using ServiceSalesApi.Requests;
using ServiceSalesMessages;
using System.Threading.Tasks;

namespace ServiceSalesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValidateContractController : ControllerBase
    {
        private readonly IBus _bus;

        public ValidateContractController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ValidateContractRequest request)
        {
            if (request == null)
                return BadRequest();

            await _bus.Send(new ValidateContract() { ContractId = request.ContractId, ContractClauses = request.ContractClauses });

            return Ok();
        }
    }
}
