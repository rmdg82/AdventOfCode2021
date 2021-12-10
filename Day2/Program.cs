using Shared.Helpers;

namespace Day2;

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
        int horizontalPosition = 0;
        int depth = 0;

        foreach (var line in lines)
        {
            var direction = GetDirection(line);
            var value = GetValue(line);

            switch (direction)
            {
                case "forward":
                    horizontalPosition += value;
                    break;

                case "down":
                    depth += value;
                    break;

                case "up":
                    depth -= value;
                    break;

                default:
                    throw new Exception($"Case {direction} not found!");
            }
        }

        var result = horizontalPosition * depth;

        PrettyConsole.Info($"Part1 result is {result}");
    }

    private static void ShowResultPart2(List<string> lines)
    {
        int horizontalPosition = 0;
        int depth = 0;
        int aim = 0;

        foreach (var line in lines)
        {
            var direction = GetDirection(line);
            var value = GetValue(line);

            switch (direction)
            {
                case "forward":
                    horizontalPosition += value;
                    depth += aim * value;
                    break;

                case "down":
                    aim += value;
                    break;

                case "up":
                    aim -= value;
                    break;

                default:
                    throw new Exception($"Case {GetDirection(line)} not found!");
            }
        }

        var result = horizontalPosition * depth;

        PrettyConsole.Info($"Part2 result is {result}");
    }

    private static int GetValue(string line)
    {
        return Convert.ToInt32(line.Split(' ')[1]);
    }

    private static string GetDirection(string line)
    {
        return line.Split(' ')[0];
    }

    private static List<string> GetInputLines(string fileName)
    {
        var inputReader = new InputReader(fileName);
        return inputReader.ReadFile();
    }
}