using DG.Core.Behaviour.Models;
using DG.Core.Components.Common;
using DG.Core.Constants;
using DG.Core.Entities;
using DG.Core.Information.Actions;
using DG.Core.Items;

using System.Linq;
using System.Numerics;

namespace DG.Core.Behaviour.Common
{
    internal sealed class DGItemAcquisitionBehavior : IDGBehaviour
    {
        // System
        private DGGame _game;

        // Infos
        private DGWorldItem[] _nearbyItems;

        // Components
        private DGTransformComponent _transformComponent;
        private DGInventoryComponent _inventoryComponent;

        public bool CanAct(DGEntity entity, DGGame game)
        {
            this._game = game;

            if (!entity.ComponentContainer.TryGetComponent(out this._transformComponent) ||
                !entity.ComponentContainer.TryGetComponent(out this._inventoryComponent))
            {
                return false;
            }

            this._nearbyItems = game.WorldManager.GetNearbyItems(this._transformComponent.Position).Where(x => Vector2.Distance(x.Position, this._transformComponent.Position) < DGInteractionsConstants.MAXIMUM_RANGE).ToArray();

            return this._nearbyItems != null && this._nearbyItems.Length != 0;
        }
        public DGBehaviourWeight GetWeight()
        {
            DGBehaviourWeight weight = new();

            foreach (DGWorldItem item in this._nearbyItems)
            {
                weight.Add(item.Amount);
            }

            return weight;
        }
        public DGPlayerActionInfo Act()
        {
            foreach (DGWorldItem worldItem in this._nearbyItems)
            {
                this._game.WorldManager.RemoveWorldItem(worldItem);
                if (!this._inventoryComponent.TryAddItem(worldItem))
                {
                    break;
                }
            }

            DGPlayerActionInfo infos = new();
            return infos;
        }
    }
}
