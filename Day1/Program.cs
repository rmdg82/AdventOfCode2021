using Shared.Helpers;

namespace Day1;

public class Program
{
    private const string inputFileName = "input1.txt";
    private const string testFileName = "testInput.txt";

    public static void Main(string[] args)
    {
        ConsoleHelper.PrintHeader(dayNum: 1);

        ShowResultPart1(GetInputLines(inputFileName));
        ShowResultPart2(GetInputLines(inputFileName));

        ConsoleHelper.Exit();
    }

    private static void ShowResultPart1(List<string> lines)
    {
        var linesAsInts = lines.Select(line => Convert.ToInt32(line)).ToList();

        if (linesAsInts.Count < 2)
        {
            throw new Exception("Input lines as ints are less than 2");
        }

        int increaseCounter = CountIncreasements(linesAsInts);

        PrettyConsole.Info($"Part1 result is {increaseCounter}");
    }

    private static void ShowResultPart2(List<string> lines)
    {
        var linesAsInts = lines.Select(line => Convert.ToInt32(line)).ToList();

        // Create a new list where in position i there is the sum of lines[i] + lines[i+1] + lines[i+2]
        var sumBy3 = new List<int>();

        for (int i = 0; i < linesAsInts.Count - 2; i++)
        {
            sumBy3.Add(linesAsInts[i] + linesAsInts[i + 1] + linesAsInts[i + 2]);
        }

        var increaseCounter = CountIncreasements(sumBy3);

        PrettyConsole.Info($"Part2 result is {increaseCounter}");
    }

    private static int CountIncreasements(List<int> linesAsInts)
    {
        var increaseCounter = 0;

        for (int i = 1; i < linesAsInts.Count; i++)
        {
            if (linesAsInts[i] > linesAsInts[i - 1])
            {
                increaseCounter++;
            }
        }

        return increaseCounter;
    }

    private static List<string> GetInputLines(string fileName)
    {
        var inputReader = new InputReader(fileName);
        return inputReader.ReadFile();
    }
}