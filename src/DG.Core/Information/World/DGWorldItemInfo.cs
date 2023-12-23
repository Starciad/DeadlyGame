using DG.Core.Items;

using System.Numerics;

namespace DG.Core.Information.World
{
    public readonly struct DGWorldItemInfo(DGItem item, int amount, Vector2 position)
    {
        public readonly DGItem Item => this.item;
        public readonly int Amount => this.amount;
        public readonly Vector2 Position => this.position;

        private readonly DGItem item = item;
        private readonly int amount = amount;
        private readonly Vector2 position = position;
    }
}
