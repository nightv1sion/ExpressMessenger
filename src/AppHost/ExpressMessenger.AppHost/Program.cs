using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgresUsername = builder.AddParameter("PostgresUsername", secret: true);
var postgresPassword = builder.AddParameter("PostgresPassword", secret: true);

IResourceBuilder<PostgresServerResource> postgres = builder
    .AddPostgres("postgres", postgresUsername, postgresPassword)
    .WithHttpEndpoint(port: 5432, targetPort: 5432)
    .WithDataVolume("Database");

IResourceBuilder<ProjectResource> usersManagementApi = builder
    .AddProject<ExpressMessenger_UsersManagement_Api>("users-management-api")
    .WithReference(postgres)
    .WaitFor(postgres);
    // .WithHttpEndpoint(5500, 80);

IResourceBuilder<ProjectResource> chattingApi = builder
    .AddProject<ExpressMessenger_Chatting_Api>("chatting-api")
    .WithReference(postgres)
    .WaitFor(postgres);
    // .WithHttpEndpoint(5600, 80);

var gateway = builder
    .AddProject<ExpressMessenger_Gateway>("gateway")
    .WithReference(usersManagementApi)
    .WithReference(chattingApi);

IResourceBuilder<ProjectResource> blazorWebApp = builder
    .AddProject<ExpressMessenger_BlazorWebApp>("blazor-web-app")
    .WithReference(gateway);

builder.Build().Run();