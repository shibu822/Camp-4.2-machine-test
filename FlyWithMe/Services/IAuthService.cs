namespace AirlineManagement.Services
{
    public interface IAuthService
    {
        int Login(string username, string password);
    }
}
