namespace AccountManager.Request
{
    public class AccountDataRequest
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public int? PhoneNumber { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
}
