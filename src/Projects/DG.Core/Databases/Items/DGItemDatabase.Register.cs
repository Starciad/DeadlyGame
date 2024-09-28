using DeadlyGame.Core.Items.Common.Foods;
using DeadlyGame.Core.Items.Common.Materials;
using DeadlyGame.Core.Items.Common.Weapons;
using DeadlyGame.Core.Objects;

namespace DeadlyGame.Core.Databases.Items
{
    internal sealed partial class DGItemDatabase : DGObject
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
