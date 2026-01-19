using Microsoft.AspNetCore.Mvc;
using Shared.Api.Extensions;
using Shared.Application.Cqrs;
using Shared.Application.Results;
using Users.Api.Models;
using Users.Application.Queries.GetUserByCredentials;
using Users.Infrastructure.Services;

namespace Users.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IMediator mediator, TokenGenerator tokenGenerator) : ControllerBase
{
    [HttpPost("auth")]
    public async Task<ActionResult<TokenResult>> Authenticate(GetUserByCredentialsQuery query)
    {
        Result<GetUserByCredentialsResult> result = await mediator.Send<GetUserByCredentialsQuery, GetUserByCredentialsResult>(query);

        return result.Match(
            user =>
            {
                string token = tokenGenerator.Generate(user);
                return Ok(new TokenResult(token));
            },
            ResultsExtensions.HandleError
        );
    }
}
