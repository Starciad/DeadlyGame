using DG.Core.Components.Common;
using DG.Core.Items.Foods.Common;
using DG.Core.Items.Materials.Common;

namespace DG.Core.Entities.Natural
{
    internal sealed class DGBush : DGEntity
    {
        internal DGBush()
        {
            this.Name = "Arbusto";
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
            this._health.SetMaximumHealth(this.Game.Random.Range(3, 6));
            this._health.SetCurrentHealth(this._health.MaximumHealth);

            // inventory
            this._inventory.TryAddItem(new DGWood(), this.Game.Random.Range(1, 3));
            this._inventory.TryAddItem(new DGBerry(), this.Game.Random.Range(2, 4));
        }
    }
}
