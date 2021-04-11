using Microsoft.AspNetCore.Mvc;
using Rebus.Bus;
using ServiceSalesApi.Requests;
using ServiceSalesMessages;
using System.Threading.Tasks;

namespace ServiceSalesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApproveProposalController : ControllerBase
    {
        private readonly IBus _bus;

        public ApproveProposalController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ApproveProposalRequest request)
        {
            if (request == null)
                return BadRequest();

            await _bus.Send(new ApproveProposal() { ProposalId = request.ProposalId, Email = request.Email });
            return Ok();
        }
    }
}
