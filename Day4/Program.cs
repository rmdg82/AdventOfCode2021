using Day4.Models;
using Shared.Helpers;

namespace Day4
{
    public class Program
    {
        private const string _inputFileName = "input1.txt";
        private const string _testFileName = "testInput.txt";

        public static List<int> DrawnNumbers { get; set; } = new();
        public static List<Board>? Boards { get; set; }

        private const int _boardSize = 5;

        public static void Main(string[] args)
        {
            ConsoleHelper.PrintHeader(dayNum: 4);

            ShowResultPart1(GetInputLines(_inputFileName));
            ShowResultPart2(GetInputLines(_inputFileName));

            ConsoleHelper.Exit();
        }

        private static void ShowResultPart1(List<string> lines)
        {
            DrawnNumbers = ParseDrawnNumbers(lines);

            Boards = ParseBoards(lines);

            int result = 0;
            foreach (var number in DrawnNumbers)
            {
                ExtractNumber(Boards, number);
                var winningBoard = CheckIfThereIsAWinningBoard(Boards);
                if (winningBoard != null)
                {
                    result = CalculateWinningScore(winningBoard, number);
                    break;
                }
            }

            if (result != 0)
            {
                PrettyConsole.Info($"Part1 result is {result}");
            }
        }

        private static void ShowResultPart2(List<string> lines)
        {
            var result = "";
            PrettyConsole.Info($"Part2 result is {result}");
        }

        private static List<int> ParseDrawnNumbers(List<string> lines)
        {
            return lines[0].Split(",").Select(x => Convert.ToInt32(x)).ToList();
        }

        private static int CalculateWinningScore(Board winningBoard, int number)
        {
            List<int> unmarkedNums = winningBoard.GetUnmarkedNumbers();

            return unmarkedNums.Sum() * number;
        }

        private static Board? CheckIfThereIsAWinningBoard(List<Board> boards)
        {
            foreach (var board in boards)
            {
                if (board.HasWin())
                {
                    return board;
                };
            }

            return null;
        }

        private static void ExtractNumber(List<Board> boards, int number)
        {
            foreach (var board in boards)
            {
                board.DrawNumber(number);
            }
        }

        private static List<Board> ParseBoards(List<string> lines)
        {
            // First line always contains drawn numbers
            lines.RemoveAt(0);

            List<Board> boards = new();
            int linesInserted = 0;
            Board currentBoard = new(_boardSize, _boardSize);
            for (int line = 0; line < lines.Count; line++)
            {
                if (linesInserted < 5)
                {
                    currentBoard.AddRow(GetRow(lines[line]), linesInserted);
                    linesInserted++;
                }
                else
                {
                    if (currentBoard.IsFullyPopulated())
                    {
                        boards.Add(currentBoard);
                    }

                    currentBoard = new(_boardSize, _boardSize);
                    linesInserted = 0;

                    currentBoard.AddRow(GetRow(lines[line]), linesInserted);
                    linesInserted++;
                }
            }

            // Add the last one
            if (currentBoard.IsFullyPopulated())
            {
                boards.Add(currentBoard);
            }

            return boards;
        }

        private static List<Number> GetRow(string lineContent)
        {
            if (string.IsNullOrWhiteSpace(lineContent))
            {
                throw new ArgumentException($"'{nameof(lineContent)}' cannot be null or whitespace.", nameof(lineContent));
            }

            var content = lineContent.Trim().Split(" ");
            var numbers = content.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => Convert.ToInt32(x));

            return numbers.Select(x => new Number(x)).ToList();
        }

        private static List<string> GetInputLines(string fileName)
        {
            var inputReader = new InputReader(fileName);
            return inputReader.ReadFile();
        }
    }
}