using Rebus.Bus;
using Rebus.Handlers;
using ServiceSalesMessages;
using System;
using System.Threading.Tasks;

namespace ServiceSalesProcessor.Business.Interactions
{
    public class CreateContractHandler : IHandleMessages<CreateContract>
    {
        private readonly IBus _bus;

        public CreateContractHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(CreateContract message)
        {
            var contractId = Guid.NewGuid().ToString("d");

            await _bus.Reply(new ContractCreated() { ProposalId = message.ProposalId, ContractId = contractId, Email = message.Email, Value = 12333.56M });
        }
    }
}
