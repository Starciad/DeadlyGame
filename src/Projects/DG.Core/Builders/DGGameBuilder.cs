using System.IO;

namespace DeadlyGame.Core.Builders
{
    public struct DGGameBuilder
    {
        public required DGPlayerBuilder[] Players { get; set; }

        public DGGameBuilder()
        {
            this.Players = [];
        }
    }
}
