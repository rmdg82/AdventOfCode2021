using Shared.Helpers;

namespace $projectname$;

public class Program
{
    private const string _inputFileName = "input1.txt";
    private const string _testFileName = "testInput.txt";

    public static void Main(string[] args)
    {
        ConsoleHelper.PrintHeader(dayNum: 2);

        ShowResultPart1(GetInputLines(_inputFileName));
        ShowResultPart2(GetInputLines(_inputFileName));

        ConsoleHelper.Exit();
    }

    private static void ShowResultPart1(List<string> lines)
    {
        throw new NotImplementedException();
    }

    private static void ShowResultPart2(List<string> lines)
    {
        throw new NotImplementedException();
    }

    private static List<string> GetInputLines(string fileName)
    {
        var inputReader = new InputReader(fileName);
        return inputReader.ReadFile();
    }
}