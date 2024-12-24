using ExpressMessenger.Common.Api;
using ExpressMessenger.UsersManagement.Api.OpenApi;
using ExpressMessenger.UsersManagement.Application;
using ExpressMessenger.UsersManagement.Infrastructure;
using ExpressMessenger.UsersManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapEndpoints();

app.Run();