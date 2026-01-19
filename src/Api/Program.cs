using Cards.Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Payments.Api;
using Shared.Api.Extensions;
using System.Text;
using Users.Api;

var builder = WebApplication.CreateBuilder(args);

const string CorsPolicy = "CORS";

builder.Services
    .AddMediator(options => options.Locations = [
        Cards.Application.Assembly.Instance, Users.Application.Assembly.Instance, Payments.Application.Assembly.Instance
    ]);

builder.Services
    .AddCors(o => o.AddPolicy(CorsPolicy, policy => policy
        .WithOrigins(builder.Configuration["AllowedHosts"]?.Split(',') ?? [])
        .AllowAnyMethod()
        .AllowAnyHeader())
    );

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SignatureOptions:SymmetricKey"]!)),
            ValidAlgorithms = [SecurityAlgorithms.HmacSha256],
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization();

builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend API", Description = "Backend API", Version = "v1" });
    });

builder.ConfigureCardsModule();
builder.ConfigureUsersModule();
builder.ConfigurePaymentsModule();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => options.RouteTemplate = "openapi/{documentName}/openapi.json");

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1/openapi.json", "v1");
        options.RoutePrefix = "docs";
    });
}

app.UseStaticFiles();
app.UseCors(CorsPolicy);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCardsModule();
app.UseUsersModule();
app.UsePaymentsModule();
app.MapFallbackToFile("index.html");

await app.RunAsync();