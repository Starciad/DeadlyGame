namespace DeadlyGame.Core.Items.Materials.Common
{
    [DGItemRegister]
    internal sealed class DGStone : DGMaterial
    {
        public DGStone()
        {
            this.Name = DGLocalization.Read("Items", "Material_Stone");
            this.Rarity = DGItemRarity.Common;
            this.HasDurability = false;
        }
    }
}
