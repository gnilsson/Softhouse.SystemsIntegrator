using Softhouse.Converter;
using Softhouse.Parser;

namespace Softhouse.Server.Services;

internal sealed class ServerService : IServerService
{
    private readonly IFormatParsingService _formatParsingService;
    private readonly IXmlConvertingService _xmlConvertingService;
    private readonly IConsoleManager _consoleManager;

    public ServerService(IFormatParsingService formatParsingService, IXmlConvertingService xmlConvertingService, IConsoleManager consoleManager)
    {
        _formatParsingService = formatParsingService;
        _xmlConvertingService = xmlConvertingService;
        _consoleManager = consoleManager;
    }

    public void Run()
    {
        _consoleManager.WriteLine("starting...\n");

        do
        {
            _consoleManager.WriteLine("convert format to xml?\n");

            if (ReadExitContinueDialog())
            {
                _consoleManager.WriteLine("exiting...\n");

                return;
            }

            try
            {
                RunConverter();
            }
            catch (Exception e)
            {
                _consoleManager.WriteLine($"an unhandled error occured in converter.\n", ConsoleColor.Red);

                _consoleManager.WriteLine($"\n{e.Message}\n");

                _consoleManager.ReadLine();

                continue;
            }

        } while (true);
    }

    private bool ReadExitContinueDialog()
    {
        _consoleManager.WriteLine("press [\"Y\"]/[\"N\"] to continue.\n", ConsoleColor.Yellow);

        do
        {
            var answer = _consoleManager.ReadLine();

            if (string.IsNullOrEmpty(answer)) continue;

            answer = answer.ToLower();

            if (answer == "y" || answer == "n") return answer == "n";

        } while (true);
    }

    private void RunConverter()
    {
        do
        {
            _consoleManager.WriteLine("write input to parse:\n");

            var input = _consoleManager.ReadToEnd();

            if (string.IsNullOrEmpty(input)) continue;

            var parsingResults = _formatParsingService.Parse(input).ToArray();

            if (parsingResults.Any(x => x.Error is not null))
            {
                _consoleManager.WriteLine($"\nerror occured in parser.\n", ConsoleColor.Red);

                foreach (var parsingResult in parsingResults.Where(x => x.Error is not null))
                {
                    _consoleManager.WriteLine(
                        $"row: {Array.IndexOf(parsingResults, parsingResult) + 1}, status: {parsingResult.Error!.Status}, message: {parsingResult.Error!.Message}\n");
                }

                return;
            }

            foreach (var parsingResult in parsingResults.Where(x => x.Warning is not null))
            {
                _consoleManager.WriteLine($"row: {Array.IndexOf(parsingResults, parsingResult) + 1} message: {parsingResult.Warning}\n", ConsoleColor.DarkYellow);
            }

            var rowInputFormats = parsingResults
                .Select(x => x.RowInputFormat)
                .ToArray();

            var xml = _xmlConvertingService.ConstructXmlDocument(rowInputFormats!);

            if (xml.Error is not null)
            {
                _consoleManager.WriteLine($"error occured in converter.\nstatus: {xml.Error.Status}, message: {string.Join("\n", xml.Error.Messages)}", ConsoleColor.Red);

                return;
            }

            _consoleManager.WriteLine("successfully created xml.\n", ConsoleColor.Green);

            _consoleManager.WriteLine(xml.Text);

            _consoleManager.ReadLine();

            return;

        } while (true);
    }
}
