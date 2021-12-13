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
                (int counter0, int counter1) = CountOneAndZero(lines, position);

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

        private static void ShowResultPart2(List<string> lines)
        {
            string oxygenGenRating = string.Empty;
            string cO2ScrubberRating = string.Empty;

            List<string> mostCommonElements = lines;
            List<string> leastCommonElements = lines;

            for (int position = 0; position < lines[0].Length; position++)
            {
                if (mostCommonElements.Count > 1)
                {
                    (int zeros, int ones) = CountOneAndZero(mostCommonElements, position);
                    // In case counter0 == counter1 keep '1'
                    char mostCommonValue = zeros > ones ? '0' : '1';

                    mostCommonElements = mostCommonElements.Where(x => x[position] == mostCommonValue).ToList();
                }

                if (leastCommonElements.Count > 1)
                {
                    (int zeros, int ones) = CountOneAndZero(leastCommonElements, position);

                    // In case counter0 == counter1 keep '0'
                    char leastCommonValue = zeros > ones ? '1' : '0';

                    leastCommonElements = leastCommonElements.Where(x => x[position] == leastCommonValue).ToList();
                }
            }

            oxygenGenRating = mostCommonElements.Single();
            cO2ScrubberRating = leastCommonElements.Single();

            int oxygenDecimal = ConvertToDecimal(oxygenGenRating);
            int cO2Decimal = ConvertToDecimal(cO2ScrubberRating);

            var result = oxygenDecimal * cO2Decimal;
            PrettyConsole.Info($"Part2 result is {result}");
        }

        private static (int count0, int count1) CountOneAndZero(List<string> lines, int position)
        {
            if (position < 0 || position >= lines[0].Length)
            {
                throw new ArgumentException($"Position {position} must be between 0 and {lines[0].Length}");
            }

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

            return (counter0, counter1);
        }

        private static int ConvertToDecimal(string binaryAsString)
        {
            return Convert.ToInt32(binaryAsString, 2);
        }

        private static List<string> GetInputLines(string fileName)
        {
            var inputReader = new InputReader(fileName);
            return inputReader.ReadFile();
        }
    }
}