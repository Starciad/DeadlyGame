using DG.Core.Information;

using System.Diagnostics.CodeAnalysis;
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

        [RequiresUnreferencedCode("Calls System.Text.Json.JsonSerializer.Serialize<TValue>(TValue, JsonSerializerOptions)")]
        [UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.", Justification = "<Pending>")]
        internal void Serialize(string filename)
        {
            using StreamWriter sw = new(filename);
            sw.Write(JsonSerializer.Serialize(this._gameInfo, JsonSerializerOptions.Default));
            sw.Close();
        }
    }
}
