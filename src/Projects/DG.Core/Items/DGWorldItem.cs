using DeadlyGame.Core.Mathematics.Primitives;

namespace DeadlyGame.Core.Items
{
    internal sealed class DGWorldItem(DGItem item, int amount, DGPoint position)
    {
        internal DGItem Item => this.item;
        internal int Amount => this.amount;
        internal DGPoint Position => this.position;

        private readonly DGItem item = item;
        private readonly int amount = amount;
        private readonly DGPoint position = position;
    }
}
