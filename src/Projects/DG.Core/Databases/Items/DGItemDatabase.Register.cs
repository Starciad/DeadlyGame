using DeadlyGame.Core.GameContent.Items.Foods;
using DeadlyGame.Core.GameContent.Items.Materials;
using DeadlyGame.Core.GameContent.Items.Weapons;
using DeadlyGame.Core.Objects;

namespace DeadlyGame.Core.Databases.Items
{
    public sealed partial class DGItemDatabase : DGObject
    {
        private void RegisterItems()
        {
            // Foods
            this._items.Add(typeof(DGApple), new DGApple());
            this._items.Add(typeof(DGBerry), new DGBerry());

            // Materials
            this._items.Add(typeof(DGStone), new DGStone());
            this._items.Add(typeof(DGWood), new DGWood());

            // Weapons
            this._items.Add(typeof(DGStoneAxe), new DGStoneAxe());
            this._items.Add(typeof(DGWoodenAxe), new DGWoodenAxe());
        }
    }
}
