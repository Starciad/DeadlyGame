using DG.Core.Components.Common;
using DG.Core.Items.Materials.Common;

namespace DG.Core.Entities.Natural
{
    internal sealed class DGTerrainStone : DGEntity
    {
        internal DGTerrainStone()
        {
            this.Name = "Pedra";
        }

        public override void Initialize()
        {
            var transform = this.ComponentContainer.AddComponent<DGTransformComponent>();
            var inventory = this.ComponentContainer.AddComponent<DGInventoryComponent>();
            var health = this.ComponentContainer.AddComponent<DGHealthComponent>();

            transform.SetPosition(this.Game.WorldManager.GetRandomPosition());
            health.SetMaximumHealth(this.Game.Random.Range(20, 35));
            health.SetCurrentHealth(health.MaximumHealth);
            inventory.TryAddItem(new DGStone(), this.Game.Random.Range(5, 18));

            base.Initialize();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
