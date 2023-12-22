using DG.Core.Components.Common;
using DG.Core.Items.Foods;
using DG.Core.Items.Materials.Common;

namespace DG.Core.Entities.Natural
{
    internal sealed class DGTree : DGEntity
    {
        internal DGTree()
        {
            this.Name = "Árvore";
        }

        public override void Initialize()
        {
            base.Initialize();

            var transform = this.ComponentContainer.AddComponent<DGTransformComponent>();
            var inventory = this.ComponentContainer.AddComponent<DGInventoryComponent>();
            var health = this.ComponentContainer.AddComponent<DGHealthComponent>();

            transform.SetPosition(this.Game.WorldManager.GetRandomPosition());
            health.SetMaximumHealth(this.Game.Random.Range(15, 25));
            health.SetCurrentHealth(health.MaximumHealth);
            inventory.TryAddItem(new DGWood(), this.Game.Random.Range(4, 9));

            if (this.Game.Random.Chance(50, 100))
            {
                inventory.TryAddItem(new DGApple(), this.Game.Random.Range(2, 4));
            }
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
