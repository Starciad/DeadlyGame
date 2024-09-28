using DeadlyGame.Core.Mathematics.Primitives;

namespace DeadlyGame.Core.Items
{
    public sealed class DGWorldItem(DGItem item, int amount, DGPoint position)
    {
        public DGItem Item => this.item;
        public int Amount => this.amount;
        public DGPoint Position => this.position;

        private readonly DGItem item = item;
        private readonly int amount = amount;
        private readonly DGPoint position = position;
    }
}
