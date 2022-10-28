namespace PetProject.Application.Tests.Csharp;

public static class TestUtility
{
    public static Program.Submarine ExecuteCommand(this Program.Submarine submarine, string command) =>
        Program.executeCommand(submarine, command);
}