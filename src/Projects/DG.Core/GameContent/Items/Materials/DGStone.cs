using DeadlyGame.Core.Enums.Items;
using DeadlyGame.Core.Items.Types;
using DeadlyGame.Core.Localization;

namespace DeadlyGame.Core.GameContent.Items.Materials
{
    public sealed class DGStone : DGMaterial
    {
        public DGStone(DGGame game) : base(game)
        {
            this.Name = DGLocalization.Read("Items", "Material_Stone");
            this.Rarity = DGItemRarity.Common;
            this.HasDurability = false;
        }

        public override void Start()
        {
            return;
        }

        public override void Update()
        {
            return;
        }
    }
}
