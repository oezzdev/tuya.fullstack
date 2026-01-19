using Shared.Domain.Entities;

namespace Users.Domain.Entities;

public sealed class User(string username, string email, string password) : Entity
{
    public string Username { get; init; } = username;
    public string Email { get; init; } = email;
    public string Password { get; init; } = password;
}
