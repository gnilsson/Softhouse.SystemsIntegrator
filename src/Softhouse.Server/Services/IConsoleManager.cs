namespace Softhouse.Server.Services;

internal interface IConsoleManager
{
    void WriteLine(string? value);
    void WriteLine(string? value, ConsoleColor consoleColor);
    string? ReadLine();
    string? ReadToEnd();
}
