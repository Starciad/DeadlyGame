using DeadlyGame.Core.Entities;
using DeadlyGame.Core.GameContent.Components;
using DeadlyGame.Core.GameContent.Items.Foods;
using DeadlyGame.Core.GameContent.Items.Materials;

namespace DeadlyGame.Core.GameContent.Entities.Natural
{
    public sealed class DGBush : DGEntity
    {
        private readonly DGTransformComponent _transform;
        private readonly DGInventoryComponent _inventory;
        private readonly DGHealthComponent _health;

        public DGBush(DGGame game) : base(game)
        {
            this.Name = "Arbusto";

            this._transform = this.ComponentContainer.AddComponent<DGTransformComponent>();
            this._inventory = this.ComponentContainer.AddComponent<DGInventoryComponent>();
            this._health = this.ComponentContainer.AddComponent<DGHealthComponent>();
        }

        public override void Start()
        {
            this._transform.SetPosition(this.DGGameInstance.WorldManager.GetRandomPosition());

            this._health.SetMaximumHealth(this.DGGameInstance.RandomMath.Range(3, 6));
            this._health.SetCurrentHealth(this._health.MaximumHealth);

            _ = this._inventory.TryAddItem(new DGWood(this.DGGameInstance), this.DGGameInstance.RandomMath.Range(1, 3));
            _ = this._inventory.TryAddItem(new DGBerry(this.DGGameInstance), this.DGGameInstance.RandomMath.Range(2, 4));
        }
    }
}
