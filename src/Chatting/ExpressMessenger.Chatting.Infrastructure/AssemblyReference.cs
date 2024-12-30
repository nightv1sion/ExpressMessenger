using System.Reflection;

namespace ExpressMessenger.Chatting.Infrastructure;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
