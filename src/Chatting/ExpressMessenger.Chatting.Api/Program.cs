using ExpressMessenger.Chatting.Api.Notifications;
using ExpressMessenger.Chatting.Application;
using ExpressMessenger.Chatting.Application.Chats;
using ExpressMessenger.Chatting.Application.Notifications;
using ExpressMessenger.Chatting.Infrastructure;
using ExpressMessenger.Chatting.Infrastructure.Messaging;
using ExpressMessenger.Chatting.Infrastructure.Persistence;
using ExpressMessenger.Common.Api;
using ExpressMessenger.Common.Api.OpenApi;
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
        if (context.Exception is ChatNotFoundException)
        {
            context.ProblemDetails = new ProblemDetails
            {
                Status = 404,
                Title = context.Exception.Message,
                Detail = context.Exception.Message,
            };

            context.HttpContext.Response.StatusCode = 404;
            context.HttpContext.Response.WriteAsJsonAsync(context.ProblemDetails);
        }
    };
});

builder.Services
    .AddScoped<IUserNotifier, UserNotifier>()
    .AddSignalR();

// builder.Services.RegisterMessaging(builder.Configuration);

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
app.MapHub<NotificationHub>("/notifications/hub");
app.MapEndpoints();

app.Run();