using System.Numerics;

namespace DeadlyGame.Core.Items
{
    internal readonly struct DGWorldItem(DGItem item, int amount, Vector2 position)
    {
        internal readonly DGItem Item => this.item;
        internal readonly int Amount => this.amount;
        internal readonly Vector2 Position => this.position;

        private readonly DGItem item = item;
        private readonly int amount = amount;
        private readonly Vector2 position = position;
    }
}
