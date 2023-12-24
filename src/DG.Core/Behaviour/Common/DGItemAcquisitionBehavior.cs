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
        private DGWorldItem[] _nearbyItems;

        // components
        private DGTransformComponent _transform;

        public DGBehaviourWeight GetWeight(DGEntity entity, DGGame game)
        {
            DGBehaviourWeight weight = new();
            this._transform = entity.ComponentContainer.GetComponent<DGTransformComponent>();

            this._nearbyItems = game.WorldManager.GetNearbyItems(this._transform.Position).Where(x => Vector2.Distance(x.Position, this._transform.Position) < DGInteractionsConstants.MAXIMUM_RANGE).ToArray();
            foreach (DGWorldItem item in this._nearbyItems)
            {
                weight.Add(item.Amount);
            }

            return weight;
        }

        public DGPlayerActionInfo Act(DGEntity entity, DGGame game)
        {
            DGPlayerActionInfo infos = new();

            if (this._nearbyItems.Length == 0)
            {
                return infos;
            }

            DGInventoryComponent inventory = entity.ComponentContainer.GetComponent<DGInventoryComponent>();

            DGWorldItem selectedItem = this._nearbyItems[0];

            game.WorldManager.RemoveWorldItem(selectedItem);
            _ = inventory.TryAddItem(selectedItem);

            return infos;
        }
    }
}
