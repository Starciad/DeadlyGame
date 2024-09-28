using System.Collections.Generic;

namespace DeadlyGame.Core.Serializers.Ini
{
    public sealed class DGIni
    {
        private readonly Dictionary<string, EPIniSection> sections = [];

        public void AddSection(string name)
        {
            this.sections.Add(name, new(name));
        }

        public EPIniSection GetSection(string name)
        {
            return this.sections[name];
        }
    }

    public sealed class EPIniSection(string name)
    {
        public string Name => name;

        private readonly Dictionary<string, string> keys = [];

        public void AddKey(string name, string value)
        {
            this.keys.Add(name, value);
        }

        public string GetKey(string name)
        {
            return this.keys[name];
        }
    }
}