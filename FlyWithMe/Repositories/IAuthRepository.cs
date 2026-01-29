namespace AirlineManagement.Repositories
{
    public interface IAuthRepository
    {
        int ValidateLogin(string username, string password);
    }
}
