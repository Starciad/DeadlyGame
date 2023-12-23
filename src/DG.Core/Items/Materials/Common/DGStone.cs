namespace DG.Core.Items.Materials.Common
{
    internal sealed class DGStone : DGMaterial
    {
        public DGStone()
        {
            this.Name = "Pedra";
            this.Description = "Pedra simple usada para fabricação de recursos.";
            this.Rarity = DGItemRarity.Common;
            this.HasDurability = false;
        }
    }
}
