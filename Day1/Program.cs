using Shared.Helpers;

namespace Day1;

public class Program
{
    private const string inputFileName = "input1.txt";
    private const string testFileName = "testInput.txt";

    public static void Main(string[] args)
    {
        PrintHeader(dayNum: 1);

        ShowResultPart1(GetInputLines(inputFileName));
        ShowResultPart2(GetInputLines(inputFileName));

        Exit();
    }

    private static void ShowResultPart1(List<string> lines)
    {
        var linesAsInts = lines.Select(line => Convert.ToInt32(line)).ToList();

        if (linesAsInts.Count < 2)
        {
            throw new Exception("Input lines as ints are less than 2");
        }

        int increaseCounter = CountIncreasments(linesAsInts);

        PrettyConsole.Info($"Part1 result is {increaseCounter}");
    }

    private static void ShowResultPart2(List<string> lines)
    {
        var linesAsInts = lines.Select(line => Convert.ToInt32(line)).ToList();

        var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWYZ";
        Dictionary<char, List<int>> letterValuesDict = new();

        // Aggregate by letter in alphabet
        for (int i = 0; i < linesAsInts.Count - 2; i++)
        {
            char key = alphabet[i % alphabet.Length];
            List<int> value = new() { linesAsInts[i], linesAsInts[i + 1], linesAsInts[i + 2] };

            if (!letterValuesDict.ContainsKey(key))
            {
                letterValuesDict.Add(key, value);
            }
            else
            {
                var oldValue = letterValuesDict[key];
                oldValue.AddRange(value);
                letterValuesDict[key] = oldValue;
            }
        }

        // Get the list of sums by letter in alphabet order
        List<int> sumByLetterOrdered = new();
        foreach (var letter in alphabet)
        {
            if (letterValuesDict.TryGetValue(letter, out List<int>? elements))
            {
                sumByLetterOrdered.Add(elements.Sum());
            }
        }

        var increaseCounter = CountIncreasments(sumByLetterOrdered);

        PrettyConsole.Info($"Part2 result is {increaseCounter}");
    }

    private static int CountIncreasments(List<int> linesAsInts)
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

    private static void PrintHeader(int dayNum)
    {
        PrettyConsole.WriteWrappedHeader("Advent of code!", '*', headerColor: ConsoleColor.Green);
        PrettyConsole.WriteWrappedHeader($"Day{dayNum}");
    }

    private static void Exit(bool isSucces = true, string? errorMessage = null)
    {
        if (isSucces)
        {
            PrettyConsole.WriteWrappedHeader("All completed. Quitting the application ...", headerColor: ConsoleColor.Green);

            CloseConsole(0);
        }

        PrettyConsole.WriteWrappedHeader(
            $"Somenthing went wrong. Quitting the application ... " +
            (string.IsNullOrEmpty(errorMessage) ? string.Empty : "\nError message: " + errorMessage)
            , headerColor: ConsoleColor.Red);

        CloseConsole(-1);
    }

    private static void CloseConsole(int errorCode)
    {
        Console.WriteLine("Press any key to close the console ...");
        Console.ReadKey();
        Environment.Exit(errorCode);
    }
}