using Grocery.Core.Helpers;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IClientService _clientService;

        public AuthService(IClientService clientService)
        {
            _clientService = clientService;
        }

        public Client? Login(string email, string password)
        {
            Client? client = _clientService.Get(email);
            if (client == null) return null;
            if (PasswordHelper.VerifyPassword(password, client.Password)) return client;
            return null;
        }

        public Client Register(string email, string password, string name)
        {
            // Check if email is already used
            if (_clientService.Get(email) != null)
                throw new Exception("Email already in use"); // replace with UsedEmailException

            // Hash the password
            string passwordHash = PasswordHelper.HashPassword(password);

            // Generate a new ID (e.g., 0 for demo, or get next ID from service)
            int newId = 0;

            // Create the Client using the constructor
            var client = new Client(newId, name, email, passwordHash);

            // Add to client service
            _clientService.Add(client);

            return client;
        }

    }
}

