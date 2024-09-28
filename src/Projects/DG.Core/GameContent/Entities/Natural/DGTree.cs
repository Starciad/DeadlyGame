using DeadlyGame.Core.Entities;
using DeadlyGame.Core.GameContent.Components;
using DeadlyGame.Core.GameContent.Items.Foods;
using DeadlyGame.Core.GameContent.Items.Materials;

namespace DeadlyGame.Core.GameContent.Entities.Natural
{
    public sealed class DGTree : DGEntity
    {
        private readonly DGTransformComponent _transform;
        private readonly DGInventoryComponent _inventory;
        private readonly DGHealthComponent _health;

        public DGTree(DGGame game) : base(game)
        {
            this.Name = "Árvore";

            this._transform = this.ComponentContainer.AddComponent<DGTransformComponent>();
            this._inventory = this.ComponentContainer.AddComponent<DGInventoryComponent>();
            this._health = this.ComponentContainer.AddComponent<DGHealthComponent>();
        }

        public override void Start()
        {
            this._transform.SetPosition(this.DGGameInstance.WorldManager.GetRandomPosition());
            this._health.SetMaximumHealth(this.DGGameInstance.RandomMath.Range(15, 25));
            this._health.SetCurrentHealth(this._health.MaximumHealth);

            _ = this._inventory.TryAddItem(new DGWood(this.DGGameInstance), this.DGGameInstance.RandomMath.Range(4, 9));

            if (this.DGGameInstance.RandomMath.Chance(50, 100))
            {
                _ = this._inventory.TryAddItem(new DGApple(this.DGGameInstance), this.DGGameInstance.RandomMath.Range(2, 4));
            }
        }
    }
}
