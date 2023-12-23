using DG.Core.Information.Actions;
using DG.Core.Information.Players;
using DG.Core.Information.Round;
using DG.Core.Information.World;

namespace DG.Core.Information
{
    public struct DGGameInfo
    {
        public required DGPlayerActionCollectionInfo ActionsInfo { get; set; }
        public required DGPlayerCollectionInfo PlayersInfo { get; set; }
        public required DGWorldInfo WorldInfo { get; set; }
        public required DGRoundInfo RoundInfo { get; set; }
    }
}
