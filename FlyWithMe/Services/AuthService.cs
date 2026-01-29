using AirlineManagement.Repositories;

namespace AirlineManagement.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repo = new AuthRepository();

        public int Login(string username, string password)
        {
            return _repo.ValidateLogin(username, password);
        }
    }
}
