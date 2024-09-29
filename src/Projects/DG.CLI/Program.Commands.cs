using DeadlyGame.CLI.Interactivity;

using System;
using System.Collections.Generic;

namespace DeadlyGame.CLI
{
    internal static partial class Program
    {
        private static void RegisterCommands()
        {
            commandRegistry.RegisterCommand(new DGCommand(
                "help",
                "Displays available commands and their descriptions.",
                parser =>
                {
                    DisplayTitleInfo();
                    commandRegistry.DisplayHelp();
                    Environment.Exit(0);
                },
                "h", "?"
            ));

            // ============================================================== //

            commandRegistry.RegisterCommand(new DGCommand(
                "definition",
                "defines an .ini file that will be used to configure the game.",
                parser =>
                {

                }
            ));
        }

        private static void ExecuteCommands(DGArgumentParser parser)
        {
            foreach (KeyValuePair<string, string> option in parser.GetAllOptions())
            {
                DGCommand command = commandRegistry.GetCommand(option.Key);
                command?.Execute(parser);
            }
        }
    }
}
