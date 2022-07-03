namespace Softhouse.Server.Services;

internal sealed class ConsoleManager : IConsoleManager
{
    public string? ReadLine()
    {
        return Console.ReadLine();
    }

    public string? ReadToEnd()
    {
        return Console.In.ReadToEnd();
    }

    public void WriteLine(string? value)
    {
        Console.WriteLine(value);
    }

    public void WriteLine(string? value, ConsoleColor consoleColor)
    {
        Console.ForegroundColor = consoleColor;
        Console.WriteLine(value);
        Console.ForegroundColor = ConsoleColor.White;
    }
}
