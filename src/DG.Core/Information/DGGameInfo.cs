using DG.Core.Information.Players;
using DG.Core.Information.Round;
using DG.Core.Information.World;

namespace DG.Core.Information
{
    public struct DGGameInfo
    {
        public DGPlayersInfo PlayersInfo { get; set; }
        public DGWorldInfo WorldInfo { get; set; }
        public DGRoundInfo RoundInfo { get; set; }
    }
}
