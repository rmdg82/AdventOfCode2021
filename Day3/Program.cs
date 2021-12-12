using Shared.Helpers;

namespace Day3
{
    public class Program
    {
        private const string _inputFileName = "input1.txt";
        private const string _testFileName = "testInput.txt";

        public static void Main(string[] args)
        {
            ConsoleHelper.PrintHeader(dayNum: 3);

            ShowResultPart1(GetInputLines(_inputFileName));
            ShowResultPart2(GetInputLines(_inputFileName));

            ConsoleHelper.Exit();
        }

        private static void ShowResultPart1(List<string> lines)
        {
            string gammaRate = string.Empty;
            string epsilonRate = string.Empty;

            for (int position = 0; position < lines[0].Length; position++)
            {
                int counter0 = 0;
                int counter1 = 0;

                for (int line = 0; line < lines.Count; line++)
                {
                    if (lines[line][position] == '1')
                    {
                        counter1++;
                    }
                    else
                    {
                        counter0++;
                    }
                }

                if (counter1 > counter0)
                {
                    gammaRate += "1";
                    epsilonRate += "0";
                }
                else
                {
                    gammaRate += "0";
                    epsilonRate += "1";
                }
            }

            int gammaDecimal = ConvertToDecimal(gammaRate);
            int epsilonDecimal = ConvertToDecimal(epsilonRate);

            var result = gammaDecimal * epsilonDecimal;
            PrettyConsole.Info($"Part1 result is {result}");
        }

        private static int ConvertToDecimal(string binaryAsString)
        {
            return Convert.ToInt32(binaryAsString, 2);
        }

        private static void ShowResultPart2(List<string> lines)
        {
            //PrettyConsole.Info($"Part2 result is {result}");
            throw new NotImplementedException();
        }

        private static List<string> GetInputLines(string fileName)
        {
            var inputReader = new InputReader(fileName);
            return inputReader.ReadFile();
        }
    }
}