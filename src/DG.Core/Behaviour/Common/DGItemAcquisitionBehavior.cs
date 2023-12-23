using DG.Core.Behaviour.Models;
using DG.Core.Components.Common;
using DG.Core.Constants;
using DG.Core.Entities;
using DG.Core.Information.World;

using System.Linq;
using System.Numerics;

namespace DG.Core.Behaviour.Common
{
    internal sealed class DGItemAcquisitionBehavior : IDGBehaviour
    {
        private DGWorldItemInfo[] _nearbyItems;

        // components
        private DGTransformComponent _transform;

        public DGBehaviourWeight GetWeight(DGEntity entity, DGGame game)
        {
            DGBehaviourWeight weight = new();
            this._transform = entity.ComponentContainer.GetComponent<DGTransformComponent>();

            this._nearbyItems = game.WorldManager.GetNearbyItems(this._transform.Position).Where(x => Vector2.Distance(x.Position, _transform.Position) < DGInteractionsConstants.MAXIMUM_RANGE).ToArray();
            foreach (DGWorldItemInfo item in this._nearbyItems)
            {
                weight.Add(item.Amount);
            }

            return weight;
        }

        public DGBehaviourActInfos Act(DGEntity entity, DGGame game)
        {
            DGBehaviourActInfos infos = new();

            if (_nearbyItems.Length == 0)
            {
                return infos;
            }

            var inventory = entity.ComponentContainer.GetComponent<DGInventoryComponent>();

            DGWorldItemInfo selectedItem = _nearbyItems[0];

            game.WorldManager.RemoveWorldItem(selectedItem);
            inventory.TryAddItem(selectedItem);

            return infos;
        }
    }
}
