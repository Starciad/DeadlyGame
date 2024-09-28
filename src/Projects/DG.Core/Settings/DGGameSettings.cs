using DeadlyGame.Core.Builders;

namespace DeadlyGame.Core.Settings
{
    public struct DGGameSettings
    {
        public DGPlayerBuilder[] Players { get; private set; }

        public DGGameSettings(DGGameBuilder builder)
        {
            this.Players = [.. builder.Players];
        }
    }
}