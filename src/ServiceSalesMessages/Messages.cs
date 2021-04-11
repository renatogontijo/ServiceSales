namespace ServiceSalesMessages
{
    #region [ Business Process ]

    public record ProposalRequest
    {
        public string Name { get; init; }

        public string Email { get; init; }
    }

    public record CreateProposal
    {
        public string Name { get; init; }

        public string Email { get; init; }
    }

    public record SendProposalByEmail
    {
        public long ProposalId { get; init; }

        public string Name { get; init; }

        public string Email { get; init; }
    }

    public record ApproveProposal
    {
        public long ProposalId { get; init; }

        public string Email { get; init; }
    }

    public record CreateContract
    {
        public long ProposalId { get; init; }

        public string Email { get; init; }
    }

    public record SendContractCopyByEmail
    {
        public long ProposalId { get; init; }

        public string Name { get; init; }

        public string Email { get; init; }

        public string ContractId { get; init; }
    }

    #endregion

    #region [ Events ]

    public record ProposalCreated
    {
        public long ProposalId { get; init; }

        public string Name { get; init; }

        public string Email { get; init; }
    }

    public record ProposalApproved
    {
        public long ProposalId { get; init; }
    }

    public record ContractCreated
    {
        public long ProposalId { get; init; }

        public string ContractId { get; init; }

        public string Email { get; init; }

        public decimal Value { get; init; }
    }

    #endregion
}
