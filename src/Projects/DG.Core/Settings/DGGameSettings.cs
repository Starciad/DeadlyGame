using DG.Core.Builders;

namespace DG.Core.Settings
{
    internal struct DGGameSettings
    {
        internal DGPlayerBuilder[] Players { get; private set; }

        internal DGGameSettings(DGGameBuilder builder)
        {
            this.Players = [.. builder.Players];
        }
    }
}