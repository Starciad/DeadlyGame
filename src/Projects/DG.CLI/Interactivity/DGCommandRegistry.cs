using System;
using System.Collections.Generic;
using System.Linq;

namespace DeadlyGame.CLI.Interactivity
{
    internal class DGCommandRegistry
    {
        private readonly Dictionary<string, DGCommand> _commands = [];

        internal void RegisterCommand(DGCommand command)
        {
            this._commands[command.Name] = command;
            foreach (string alias in command.Aliases)
            {
                this._commands[alias] = command;
            }
        }

        internal DGCommand GetCommand(string name)
        {
            return this._commands.TryGetValue(name, out DGCommand command) ? command : null;
        }

        internal void DisplayHelp()
        {
            Console.WriteLine("Below you can find a detailed list containing all the commands and arguments that can be used in the program.");
            Console.WriteLine();

            foreach (DGCommand command in this._commands.Values.Distinct())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"> --{command.Name} ({string.Join(", ", command.Aliases.Select(x => x))}): ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{command.Description}");
            }
        }
    }
}
