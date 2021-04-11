using Rebus.Bus;
using Rebus.Handlers;
using Serilog;
using ServiceSalesMessages;
using System.Threading.Tasks;

namespace ServiceSalesProcessor.Business.Processes
{
    public class ValidateContractHandler : IHandleMessages<ValidateContract>
    {
        private readonly IBus _bus;

        public ValidateContractHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(ValidateContract message)
        {
            Log.Information($"Contract {message.ContractId} changed");

            await _bus.Reply(new ContractValidated() { ContractId = message.ContractId });
        }
    }
}
