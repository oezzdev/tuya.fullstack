using Shared.Application.Cqrs;

namespace Users.Application.Queries.GetUserByCredentials;

public record GetUserByCredentialsQuery(string Username, string Password) : IRequest<GetUserByCredentialsResult>;
