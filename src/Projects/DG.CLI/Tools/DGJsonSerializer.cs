using DG.Core.Information;

using System.IO;
using System.Text.Json;

namespace DG.Tools
{
    internal sealed class DGJsonSerializer
    {
        private readonly DGGameInfo _gameInfo;

        internal DGJsonSerializer(DGGameInfo gameInfo)
        {
            this._gameInfo = gameInfo;
        }

        internal void Serialize(string filename)
        {
            using StreamWriter sw = new(filename);
            sw.Write(JsonSerializer.Serialize(this._gameInfo, JsonSerializerOptions.Default));
            sw.Close();
        }
    }
}
