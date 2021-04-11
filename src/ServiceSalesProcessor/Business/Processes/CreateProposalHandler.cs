using Rebus.Bus;
using Rebus.Handlers;
using Serilog;
using ServiceSalesMessages;
using ServiceSalesProcessor.Business.Functions;
using System.Threading.Tasks;

namespace ServiceSalesProcessor.Business.Processes
{
    public class CreateProposalHandler : IHandleMessages<CreateProposal>
    {
        private readonly IBus _bus;

        public CreateProposalHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(CreateProposal message)
        {
            Log.Information($"Create Cliente Proposal {message.Name}");

            await _bus.Reply(new ProposalCreated() { ProposalId = NewId.Get(), Name = message.Name, Email = message.Email });
        }
    }
}
