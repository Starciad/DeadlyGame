namespace DG.Core.Items.Foods.Common
{
    internal sealed class DGBerry : DGFood
    {
        public DGBerry()
        {
            this.Name = "Baga";
            this.Description = "Uma pequena frutinha que trás um pouco de saciedade.";
            this.Rarity = DGItemRarity.Common;
            this.HasDurability = false;
            this.SatietyFactor = 5;
        }
    }
}
