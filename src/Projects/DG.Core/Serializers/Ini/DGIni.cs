using System.Collections.Generic;

namespace DeadlyGame.Core.Serializers.Ini
{
    internal sealed class DGIni
    {
        private readonly Dictionary<string, EPIniSection> sections = [];

        internal void AddSection(string name)
        {
            this.sections.Add(name, new(name));
        }

        internal EPIniSection GetSection(string name)
        {
            return this.sections[name];
        }
    }

    internal sealed class EPIniSection(string name)
    {
        internal string Name => name;

        private readonly Dictionary<string, string> keys = [];

        internal void AddKey(string name, string value)
        {
            this.keys.Add(name, value);
        }

        internal string GetKey(string name)
        {
            return this.keys[name];
        }
    }
}