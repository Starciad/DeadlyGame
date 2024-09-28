using DeadlyGame.Core.Information.Items;
using DeadlyGame.Core.Information.Utils;
using DeadlyGame.Core.Items;

namespace DeadlyGame.Core.Information.World
{
    public struct DGWorldItemInfo
    {
        public DGItemInfo Item { get; set; }
        public int Amount { get; set; }
        public DGVector2 Position { get; set; }

        public DGWorldItemInfo()
        {
            this.Item = new();
            this.Amount = 0;
            this.Position = new();
        }

        internal static DGWorldItemInfo Create(DGWorldItem item)
        {
            return new()
            {
                Item = DGItemInfo.Create(item.Item),
                Amount = item.Amount,
                Position = new(item.Position)
            };
        }
    }
}
