using System;

namespace DeadlyGame.Core.Serializers.Ini
{
    public static class DGIniSerializer
    {
        public static DGIni Deserialize(string text)
        {
            DGIni iniInfos = new();

            string currentCategory = string.Empty;

            foreach (string line in text.Split(Environment.NewLine))
            {
                string trimmedLine = line.Trim();

                if (string.IsNullOrWhiteSpace(trimmedLine) || trimmedLine.StartsWith('#'))
                {
                    continue;
                }

                if (trimmedLine.StartsWith('[') && trimmedLine.EndsWith(']'))
                {
                    currentCategory = trimmedLine.Trim('[', ']').ToLower();
                    iniInfos.AddSection(currentCategory);
                }
                else if (currentCategory != null && trimmedLine.Contains('='))
                {
                    string[] tokens = trimmedLine.Split(['='], 2);
                    if (tokens.Length == 2)
                    {
                        string tokenName = tokens[0].Trim();
                        string tokenValue = tokens[1].Trim();
                        iniInfos.GetSection(currentCategory).AddKey(tokenName, tokenValue);
                    }
                }
            }

            return iniInfos;
        }
    }
}