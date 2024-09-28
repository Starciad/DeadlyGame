using DeadlyGame.Core.Information.Actions;
using DeadlyGame.Core.Information.Players;
using DeadlyGame.Core.Information.Round;
using DeadlyGame.Core.Information.World;

using System;

namespace DeadlyGame.Core.Information
{
    [Serializable]
    public struct DGGameInfo
    {
        public DGPlayerActionCollectionInfo ActionsInfo { get; set; }
        public DGPlayerCollectionInfo PlayersInfo { get; set; }
        public DGWorldInfo WorldInfo { get; set; }
        public DGRoundInfo RoundInfo { get; set; }

        public DGGameInfo()
        {
            this.ActionsInfo = new();
            this.PlayersInfo = new();
            this.WorldInfo = new();
            this.RoundInfo = new();
        }
    }
}
