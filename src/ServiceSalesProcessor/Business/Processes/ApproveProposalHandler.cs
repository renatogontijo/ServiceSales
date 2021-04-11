using Rebus.Bus;
using Rebus.Handlers;
using Serilog;
using ServiceSalesMessages;
using System.Threading.Tasks;

namespace ServiceSalesProcessor.Business.Processes
{
    public class ApproveProposalHandler : IHandleMessages<ApproveProposal>
    {
        private readonly IBus _bus;

        public ApproveProposalHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(ApproveProposal message)
        {
            await _bus.Reply(new ProposalApproved() { ProposalId = message.ProposalId });
        }
    }
}
