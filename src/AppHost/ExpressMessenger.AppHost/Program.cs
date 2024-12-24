using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgresUsername = builder.AddParameter("PostgresUsername", secret: true);
var postgresPassword = builder.AddParameter("PostgresPassword", secret: true);

IResourceBuilder<PostgresServerResource> postgres = builder
    .AddPostgres("postgres", postgresUsername, postgresPassword)
    .WithHttpEndpoint(port: 5432, targetPort: 5432);

var usersManagementDb = postgres
    .AddDatabase("Database", "express-messenger.users-management");

IResourceBuilder<ProjectResource> usersManagementApi = builder
    .AddProject<ExpressMessenger_UsersManagement_Api>("users-management-api")
    .WithReference(usersManagementDb)
    .WaitFor(postgres);

IResourceBuilder<ProjectResource> blazorWebApp = builder
    .AddProject<ExpressMessenger_BlazorWebApp>("blazor-web-app")
    .WithReference(usersManagementApi)
    .WaitFor(usersManagementApi);

builder.Build().Run();