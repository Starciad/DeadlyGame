namespace DG.Core.Items.Foods
{
    internal sealed class DGBerry : DGFood
    {
        internal override void Build()
        {
            this.Name = "Baga";
            this.Description = "Uma pequena frutinha que trás um pouco de saciedade.";
            this.Rarity = DGItemRarity.Common;
            this.HasDurability = false;
            this.SatietyFactor = 5;
        }
    }
}
