using DG.Core.Information.Items;
using DG.Core.Items;

using System.Numerics;

namespace DG.Core.Information.World
{
    public struct DGWorldItemInfo
    {
        public DGItemInfo Item { get; set; }
        public int Amount { get; set; }
        public Vector2 Position { get; set; }

        internal static DGWorldItemInfo Create(DGWorldItem item)
        {
            return new()
            {
                Item = DGItemInfo.Create(item.Item),
                Amount = item.Amount,
                Position = item.Position
            };
        }
    }
}
