using ExpressMessenger.Chatting.Application;
using ExpressMessenger.Chatting.Infrastructure;
using ExpressMessenger.Chatting.Infrastructure.Persistence;
using ExpressMessenger.Common.Api;
using ExpressMessenger.Common.Api.OpenApi;
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

builder.Services.AddProblemDetails();

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