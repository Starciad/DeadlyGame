using DeadlyGame.CLI.Interactivity;

using System;
using System.Text;

namespace DeadlyGame.CLI
{
    internal static partial class Program
    {
        private static readonly DGCommandRegistry commandRegistry = new();

        [STAThread]
        private static int Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            DGArgumentParser parser = new(args);

            if (args.Length == 0)
            {
                DisplayTitleInfo();
            }
            else
            {
                RegisterCommands();
                ExecuteCommands(parser);
                StartGame();
            }

            return 0;
        }
    }
}