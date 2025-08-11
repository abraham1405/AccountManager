namespace AccountManager.Domain.Entities
{
    public class Account
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Email { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public int? PhoneNumber { get; set; }
    }
}
