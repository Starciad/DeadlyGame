using System.IO;

namespace DG.Core.Builders
{
    public struct DGGameBuilder
    {
        public required DGPlayerBuilder[] Players { get; set; }
        public string LocalizationFilename { get; set; }

        public DGGameBuilder()
        {
            this.Players = [];
            this.LocalizationFilename = Path.Combine(Directory.GetCurrentDirectory(), "Localization", "Localization.ini");
        }
    }
}
