using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers
{
    public static class ConsoleHelper
    {
        public static void PrintHeader(int dayNum)
        {
            PrettyConsole.WriteWrappedHeader("Advent of code!", '*', headerColor: ConsoleColor.Green);
            PrettyConsole.WriteWrappedHeader($"Day{dayNum}");
        }

        public static void Exit(bool isSucces = true, string? errorMessage = null)
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
}