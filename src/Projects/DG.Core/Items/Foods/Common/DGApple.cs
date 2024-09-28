namespace DeadlyGame.Core.Items.Foods.Common
{
    [DGItemRegister]
    internal sealed class DGApple : DGFood
    {
        public DGApple()
        {
            this.Name = DGLocalization.Read("Items", "Food_Apple");
            this.Rarity = DGItemRarity.Common;
            this.HasDurability = false;
            this.SatietyFactor = 10;
        }
    }
}
