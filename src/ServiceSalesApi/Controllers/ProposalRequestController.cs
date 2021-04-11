using Microsoft.AspNetCore.Mvc;
using Rebus.Bus;
using ServiceSalesMessages;
using System.Threading.Tasks;

namespace ServiceSalesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProposalRequestController : ControllerBase
    {
        private readonly IBus _bus;

        public ProposalRequestController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var customer = new FakeCustomer().Create();

            await _bus.Send(new ProposalRequest() { Name = customer.Name, Email = customer.Email });
            
            return Ok();
        }
    }
}
