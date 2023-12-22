namespace DG.Core.Items.Materials.Common
{
    internal sealed class DGWood : DGMaterial
    {
        internal override void Build()
        {
            this.Name = "Madeira";
            this.Description = "Tábuas simples de madeira usadas para fabricação de recursos.";
            this.Rarity = DGItemRarity.Common;
            this.HasDurability = false;
        }
    }
}
