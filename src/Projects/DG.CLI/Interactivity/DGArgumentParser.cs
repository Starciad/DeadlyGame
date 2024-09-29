using System.Collections.Generic;

namespace DeadlyGame.CLI.Interactivity
{
    internal sealed class DGArgumentParser
    {
        private readonly Dictionary<string, string> _options = [];

        internal DGArgumentParser(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith('-'))
                {
                    string key = args[i].TrimStart('-');
                    string value = i + 1 < args.Length && !args[i + 1].StartsWith('-') ? args[++i] : null;
                    this._options[key] = value;
                }
            }
        }

        internal bool HasOption(string name)
        {
            return this._options.ContainsKey(name);
        }

        internal string GetOption(string name)
        {
            return this._options.TryGetValue(name, out string value) ? value : null;
        }

        internal IEnumerable<KeyValuePair<string, string>> GetAllOptions()
        {
            return this._options;
        }
    }
}
