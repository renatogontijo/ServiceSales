using Rebus.Bus;
using Rebus.Handlers;
using ServiceSalesMessages;
using ServiceSalesProcessor.Business.Functions;
using System.Threading.Tasks;

namespace ServiceSalesProcessor.Business.Processes
{
    public class SendContractCopyByEmailHandler : IHandleMessages<SendContractCopyByEmail>
    {
        private readonly IBus _bus;

        public SendContractCopyByEmailHandler(IBus bus)
        {
            _bus = bus;
        }

        public Task Handle(SendContractCopyByEmail message)
        {
            SendEmailMessage(message);

            return Task.CompletedTask;
        }

        private void SendEmailMessage(SendContractCopyByEmail message)
        {
            var subject = $"Copy of contract {message.ContractId}";
            SendEmail.Send(message.Email, subject, CreateMailMessage(message));
        }

        private string CreateMailMessage(SendContractCopyByEmail message)
        {
            return $@"Attached to this message a copy of your contract {message.ContractId}
                     
                     Best Regards!";
        }
    }
}
