using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Sagas;
using Serilog;
using ServiceSalesMessages;
using System.Threading.Tasks;

namespace ServiceSalesProcessor.Business.Processes
{
    public class ProposalCreatingWorkflow : Saga<ProposalCreatingData>,
        IAmInitiatedBy<ProposalRequest>,
        IHandleMessages<ProposalCreated>,
        IHandleMessages<ProposalApproved>,
        IHandleMessages<ContractCreated>,
        IHandleMessages<ContractValidated>
    {
        private readonly IBus _bus;

        public ProposalCreatingWorkflow(IBus bus)
        {
            _bus = bus;
        }

        protected override void CorrelateMessages(ICorrelationConfig<ProposalCreatingData> config)
        {
            config.Correlate<ProposalRequest>(c => c.Email, d => d.CustomerEmail);
            config.Correlate<ProposalCreated>(c => c.Email, d => d.CustomerEmail);
            config.Correlate<ProposalApproved>(c => c.ProposalId, d => d.ProposalId);
            config.Correlate<ContractCreated>(c => c.ProposalId, d => d.ProposalId);
            config.Correlate<ContractValidated>(c => c.ContractId, d => d.ContractId);
        }

        private void TryComplete()
        {
            if (Data.IsCompleted)
            {
                Log.Information($"Proposal workflow ended for {Data.CustomerName}");

                MarkAsComplete();
            }
        }

        public async Task Handle(ProposalRequest message)
        {
            if (!IsNew) return;

            Data.CustomerName = message.Name;
            Data.CustomerEmail = message.Email;

            await _bus.Send(new CreateProposal() { Name = message.Name, Email = message.Email });
        }

        public async Task Handle(ProposalCreated message)
        {
            Data.ProposalId = message.ProposalId;
            Data.ProposalCreated = true;

            Log.Information($"ProposalId -> {message.ProposalId}");

            await _bus.Send(new SendProposalByEmail() { ProposalId = message.ProposalId, Name = message.Name, Email = message.Email });

            TryComplete();
        }

        public async Task Handle(ProposalApproved message)
        {
            Data.ProposalApproved = true;

            Log.Information($"Client approved proposal {message.ProposalId}.");

            await _bus.Send(new CreateContract() { ProposalId = message.ProposalId, Email = Data.CustomerEmail });

            TryComplete();
        }

        public async Task Handle(ContractCreated message)
        {
            Data.ContractId = message.ContractId;
            Data.ContractCreated = true;

            Log.Information($"ContractId -> {message.ContractId}");

            await _bus.Send(new SendContractCopyByEmail()
            {
                ProposalId = message.ProposalId,
                Name = Data.CustomerName,
                Email = message.Email,
                ContractId = message.ContractId
            });

            TryComplete();
        }

        public Task Handle(ContractValidated message)
        {
            Data.ContractValidated = true;

            Log.Information($"ContractId {message.ContractId} validated!");

            TryComplete();

            return Task.CompletedTask;
        }
    }
}
