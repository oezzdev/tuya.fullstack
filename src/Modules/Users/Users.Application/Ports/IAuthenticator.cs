namespace Users.Application.Ports;

public interface IAuthenticator
{
    string Hash(string password);
    bool Verify(string hashedPassword, string password);
}
