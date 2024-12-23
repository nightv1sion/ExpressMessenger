using Projects;

var builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<PostgresServerResource> postgres = builder
    .AddPostgres("postgres");

var usersManagementDb = postgres
    .AddDatabase("Database", "express-messenger.users-management");

IResourceBuilder<ProjectResource> usersManagementApi = builder
    .AddProject<ExpressMessenger_UsersManagement_Api>("users-management-api")
    .WithExternalHttpEndpoints()
    .WithReference(usersManagementDb)
    .WaitFor(postgres);

IResourceBuilder<ProjectResource> blazorWebApp = builder
    .AddProject<ExpressMessenger_BlazorWebApp>("blazor-web-app")
    .WithReference(usersManagementApi)
    .WaitFor(usersManagementApi);

builder.Build().Run();