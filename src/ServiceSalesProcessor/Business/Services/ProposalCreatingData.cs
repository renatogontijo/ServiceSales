using System;
using Rebus.Sagas;

namespace ServiceSalesProcessor.Business.Processes
{
    public class ProposalCreatingData : ISagaData
    {
        public Guid Id { get; set; }
        public int Revision { get; set; }

        public long ProposalId { get; set; }
        public string ContractId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }

        public bool ProposalCreated { get; set; }
        public bool ProposalApproved { get; set; }
        public bool ContractCreated { get; set; }
        public bool ContractValidated { get; set; }

        public bool IsCompleted => ProposalCreated
                                && ProposalApproved
                                && ContractCreated
                                && ContractValidated;
    }
}
