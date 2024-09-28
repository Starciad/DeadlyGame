using DeadlyGame.Core.Entities;
using DeadlyGame.Core.GameContent.Components;
using DeadlyGame.Core.GameContent.Items.Materials;

namespace DeadlyGame.Core.GameContent.Entities.Natural
{
    public sealed class DGTerrainStone : DGEntity
    {
        private readonly DGTransformComponent _transform;
        private readonly DGInventoryComponent _inventory;
        private readonly DGHealthComponent _health;

        public DGTerrainStone(DGGame game) : base(game)
        {
            this.Name = "Pedra";

            this._transform = this.ComponentContainer.AddComponent<DGTransformComponent>();
            this._inventory = this.ComponentContainer.AddComponent<DGInventoryComponent>();
            this._health = this.ComponentContainer.AddComponent<DGHealthComponent>();
        }

        public override void Start()
        {
            this._transform.SetPosition(this.DGGameInstance.WorldManager.GetRandomPosition());
            this._health.SetMaximumHealth(this.DGGameInstance.RandomMath.Range(20, 35));
            this._health.SetCurrentHealth(this._health.MaximumHealth);
            _ = this._inventory.TryAddItem(new DGStone(this.DGGameInstance), this.DGGameInstance.RandomMath.Range(5, 18));
        }
    }
}
