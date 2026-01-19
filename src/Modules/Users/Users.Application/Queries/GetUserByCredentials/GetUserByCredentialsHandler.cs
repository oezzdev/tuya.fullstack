using Shared.Application.Cqrs;
using Shared.Application.Results;
using Users.Domain.Entities;
using Users.Domain.Ports;

namespace Users.Application.Queries.GetUserByCredentials;

public class GetUserByCredentialsHandler(IUserRepository userRepository) : IRequestHandler<GetUserByCredentialsQuery, GetUserByCredentialsResult>
{
    public async Task<Result<GetUserByCredentialsResult>> Handle(GetUserByCredentialsQuery request, CancellationToken cancellationToken = default)
    {
        User? user = await userRepository.GetByCredentials(request.Username, request.Password, cancellationToken);
        if (user is null)
        {
            return Error.NotFound("User not found.");
        }
        return new GetUserByCredentialsResult(user.Id, user.Username);
    }
}