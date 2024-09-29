using System;
using System.Collections.Generic;

namespace DeadlyGame.CLI.Interactivity
{
    internal class DGCommand
    {
        internal string Name { get; }
        internal string Description { get; }
        internal string[] Aliases => [.. this.aliases];
        internal Action<DGArgumentParser> Execute { get; }

        private readonly List<string> aliases = [];

        internal DGCommand(string name, string description, Action<DGArgumentParser> execute)
        {
            this.Name = name;
            this.Description = description;
            this.Execute = execute;
        }

        internal DGCommand(string name, string description, Action<DGArgumentParser> execute, params string[] aliases)
        {
            this.Name = name;
            this.Description = description;
            this.Execute = execute;

            if (aliases != null && aliases.Length > 0)
            {
                this.aliases.AddRange(aliases);
            }
        }
    }
}
