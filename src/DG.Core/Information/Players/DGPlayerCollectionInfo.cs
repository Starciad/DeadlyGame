using System.Linq;

namespace DG.Core.Information.Players
{
    public struct DGPlayerCollectionInfo
    {
        public readonly DGPlayerInfo[] TotalPlayers => this.Players;
        public readonly DGPlayerInfo[] ActivePlayers => this.Players.Where(x => !x.Health.IsDead).ToArray();
        public readonly DGPlayerInfo[] DisabledPlayers => this.Players.Where(x => x.Health.IsDead).ToArray();
        public readonly bool OnlyOneActivePlayer => this.ActivePlayers.Length == 1;

        public DGPlayerInfo[] Players { get; set; }
    }
}
