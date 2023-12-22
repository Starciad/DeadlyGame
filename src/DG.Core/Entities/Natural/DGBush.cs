using DG.Core.Components.Common;
using DG.Core.Items.Foods;
using DG.Core.Items.Materials.Common;

namespace DG.Core.Entities.Natural
{
    internal sealed class DGBush : DGEntity
    {
        internal DGBush()
        {
            this.Name = "Arbusto";
        }

        public override void Initialize()
        {
            var transform = this.ComponentContainer.AddComponent<DGTransformComponent>();
            var inventory = this.ComponentContainer.AddComponent<DGInventoryComponent>();
            var health = this.ComponentContainer.AddComponent<DGHealthComponent>();

            transform.SetPosition(this.Game.WorldManager.GetRandomPosition());
            health.SetMaximumHealth(this.Game.Random.Range(3, 6));
            health.SetCurrentHealth(health.MaximumHealth);
            inventory.TryAddItem(new DGWood(), this.Game.Random.Range(1, 3));
            inventory.TryAddItem(new DGBerry(), this.Game.Random.Range(2, 4));

            base.Initialize();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
