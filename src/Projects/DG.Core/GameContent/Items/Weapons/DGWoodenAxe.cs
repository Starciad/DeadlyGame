using DeadlyGame.Core.Enums.Items;
using DeadlyGame.Core.Enums.Items.Weapons;
using DeadlyGame.Core.Items.Types;
using DeadlyGame.Core.Localization;

namespace DeadlyGame.Core.GameContent.Items.Weapons
{
    public sealed class DGWoodenAxe : DGWeapon
    {
        public DGWoodenAxe(DGGame game) : base(game)
        {
            this.Name = DGLocalization.ITEMS_WEAPONS_WOODEN_AXE;
            this.Rarity = DGItemRarity.Common;
            this.HasDurability = true;
            this.Durability = 20;
            this.WeaponType = DGWeaponType.Melee;
            this.Damage = 4;
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
