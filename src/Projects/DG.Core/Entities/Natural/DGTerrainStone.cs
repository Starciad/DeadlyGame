namespace DeadlyGame.Core.Entities.Natural
{
    internal sealed class DGTerrainStone : DGEntity
    {
        internal DGTerrainStone()
        {
            this.Name = "Pedra";
        }

        private DGTransformComponent _transform;
        private DGInventoryComponent _inventory;
        private DGHealthComponent _health;

        protected override void OnAwake()
        {
            base.OnAwake();

            this._transform = this.ComponentContainer.AddComponent<DGTransformComponent>();
            this._inventory = this.ComponentContainer.AddComponent<DGInventoryComponent>();
            this._health = this.ComponentContainer.AddComponent<DGHealthComponent>();
        }

        protected override void OnStart()
        {
            base.OnStart();

            // ===== COMPONENTS =====
            // transform
            this._transform.SetPosition(this.Game.WorldManager.GetRandomPosition());

            // health
            this._health.SetMaximumHealth(this.Game.Random.Range(20, 35));
            this._health.SetCurrentHealth(this._health.MaximumHealth);

            // inventory
            _ = this._inventory.TryAddItem(new DGStone(), this.Game.Random.Range(5, 18));
        }
    }
}
