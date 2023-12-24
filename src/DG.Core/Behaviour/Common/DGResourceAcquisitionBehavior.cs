using DG.Core.Behaviour.Models;
using DG.Core.Components.Common;
using DG.Core.Constants;
using DG.Core.Entities;
using DG.Core.Information.Actions;

using System.Linq;
using System.Numerics;

namespace DG.Core.Behaviour.Common
{
    internal sealed class DGResourceAcquisitionBehavior : IDGBehaviour
    {
        private DGEntity[] _nearbyResources;

        // Components
        private DGTransformComponent _transformComponent;
        private DGCombatComponent _combatComponent;

        private DGHealthComponent _resourceHealthComponent;

        public bool CanAct(DGEntity entity, DGGame game)
        {
            // Components
            if (!entity.ComponentContainer.TryGetComponent(out this._transformComponent))
            {
                return false;
            }

            if (!entity.ComponentContainer.TryGetComponent(out this._combatComponent))
            {
                return false;
            }

            // Infos
            this._nearbyResources = game.WorldManager.GetNearbyResources(this._transformComponent.Position).Where(x => Vector2.Distance(x.ComponentContainer.GetComponent<DGTransformComponent>().Position, this._transformComponent.Position) < DGInteractionsConstants.MAXIMUM_RANGE).ToArray();
            return this._nearbyResources != null && this._nearbyResources.Length != 0
&& !this._nearbyResources[0].ComponentContainer.TryGetComponent(out this._resourceHealthComponent);
        }
        public DGBehaviourWeight GetWeight()
        {
            DGBehaviourWeight weight = new();
            weight.Add(this._nearbyResources.Length);
            return weight;
        }
        public DGPlayerActionInfo Act()
        {
            DGPlayerActionInfo infos = new();
            this._resourceHealthComponent.Hurt(this._combatComponent.GetFullAttackDamage(false));
            return infos;
        }
    }
}
