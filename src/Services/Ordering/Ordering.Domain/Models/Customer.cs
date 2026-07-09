using Ordering.Domain.Abstractions;


namespace Ordering.Domain.Models
{
    public class Customer:Entity<Guid>
    {
        public string Name { get; private set; } = default!;
        public string Email { get; private set; } = default!;

        // factory method
        public static Customer Create(Guid customerId, string name, string email)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(email);
            var customer = new Customer
            {
                Id = customerId,
                Name = name,
                Email = email
            };
            return customer;
        }
    }
}
