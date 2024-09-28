using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;

namespace DeadlyGame.CLI.Tools
{
    internal sealed class DGJsonSerializer
    {
        private readonly DGGameInfo _gameInfo;

        internal DGJsonSerializer(DGGameInfo gameInfo)
        {
            this._gameInfo = gameInfo;
        }

        [RequiresUnreferencedCode("Calls System.Text.Json.JsonSerializer.Serialize<TValue>(TValue, JsonSerializerOptions)")]
        internal void Serialize(string filename)
        {
            using StreamWriter sw = new(filename);
            sw.Write(JsonSerializer.Serialize(this._gameInfo, JsonSerializerOptions.Default));
            sw.Close();
        }
    }
}
