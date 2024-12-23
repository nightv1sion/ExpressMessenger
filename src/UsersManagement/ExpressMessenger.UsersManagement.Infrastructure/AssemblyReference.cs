using System.Reflection;

namespace ExpressMessenger.UsersManagement.Infrastructure;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
