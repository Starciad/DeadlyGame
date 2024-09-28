﻿using DeadlyGame.Core.Entities;
using DeadlyGame.Core.GameContent.Components;
using DeadlyGame.Core.GameContent.Items.Foods;
using DeadlyGame.Core.GameContent.Items.Materials;

namespace DeadlyGame.Core.GameContent.Entities.Natural
{
    public sealed class DGTree : DGEntity
    {
        public DGTree()
        {
            this.Name = "Árvore";
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
            this._health.SetMaximumHealth(this.Game.Random.Range(15, 25));
            this._health.SetCurrentHealth(this._health.MaximumHealth);

            // inventory
            _ = this._inventory.TryAddItem(new DGWood(), this.Game.Random.Range(4, 9));

            if (this.Game.Random.Chance(50, 100))
            {
                _ = this._inventory.TryAddItem(new DGApple(), this.Game.Random.Range(2, 4));
            }
        }
    }
}
