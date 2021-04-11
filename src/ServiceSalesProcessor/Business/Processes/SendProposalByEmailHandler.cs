using Rebus.Bus;
using Rebus.Handlers;
using ServiceSalesMessages;
using ServiceSalesProcessor.Business.Functions;
using System.Threading.Tasks;

namespace ServiceSalesProcessor.Business.Processes
{
    public class SendProposalByEmailHandler : IHandleMessages<SendProposalByEmail>
    {
        private readonly IBus _bus;

        public SendProposalByEmailHandler(IBus bus)
        {
            _bus = bus;
        }

        public Task Handle(SendProposalByEmail message)
        {
            SendEmailMessage(message);

            return Task.CompletedTask;
        }

        private void SendEmailMessage(SendProposalByEmail message)
        {
            var subject = "Proposal for evaluation";
            SendEmail.Send(message.Email, subject, CreateEmailMessage(message));
        }

        private string CreateEmailMessage(SendProposalByEmail message)
        {
            return $@"Dear Mr. {message.Name}

                      Attached to this message your proposal for evaluation.
                      
                      Best Regards!";
        }
    }
}
