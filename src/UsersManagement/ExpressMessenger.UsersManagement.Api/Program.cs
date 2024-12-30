using ExpressMessenger.Common.Api;
using ExpressMessenger.Common.Api.OpenApi;
using ExpressMessenger.UsersManagement.Application;
using ExpressMessenger.UsersManagement.Domain.UserAggregate.Exceptions;
using ExpressMessenger.UsersManagement.Infrastructure;
using ExpressMessenger.UsersManagement.Infrastructure.Authentication.Exceptions;
using ExpressMessenger.UsersManagement.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig)
    => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

builder.Services.AddHttpContextAccessor();

builder.Services
    .RegisterApplication()
    .RegisterInfrastructure(builder.Configuration);

builder.Services.AddEndpoints(typeof(Program).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AnyOrigin", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        if (context.Exception is InvalidRefreshToken or InvalidAccessTokenClaims)
        {
            context.ProblemDetails = new ProblemDetails
            {
                Status = 403,
                Title = context.Exception.Message,
                Detail = context.Exception.Message,
            };

            context.HttpContext.Response.StatusCode = 403;
            context.HttpContext.Response.WriteAsJsonAsync(context.ProblemDetails);
        }
    };
});

var app = builder.Build();

var scope = app.Services.CreateScope();
await scope.ServiceProvider.GetRequiredService<ApplicationDbContext>()
    .Database
    .MigrateAsync();

app.UseCors("AnyOrigin");

app.MapOpenApi();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapEndpoints();

app.Run();