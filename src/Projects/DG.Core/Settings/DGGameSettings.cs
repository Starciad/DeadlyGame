using DeadlyGame.Core.Builders;

namespace DeadlyGame.Core.Settings
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