using Bogus;

namespace ServiceSalesApi
{
    public record Customer
    {
        public string Name { get; init; }

        public string Email { get; init; }
    }

    public class FakeCustomer : Faker<Customer>
    {
        public FakeCustomer() :
            base("pt_BR")
        {
            RuleFor(c => c.Name, (f, c) => f.Name.FullName());
            RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.Name.ToLower()));
        }
        
        public Customer Create()
        {
            return Generate();
        }
    }
}
