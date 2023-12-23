﻿using DG.Core.Behaviour.Models;
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

        // components
        private DGTransformComponent _transform;

        public DGBehaviourWeight GetWeight(DGEntity entity, DGGame game)
        {
            DGBehaviourWeight weight = new();
            this._transform = entity.ComponentContainer.GetComponent<DGTransformComponent>();

            this._nearbyResources = game.WorldManager.GetNearbyResources(this._transform.Position).Where(x => Vector2.Distance(x.ComponentContainer.GetComponent<DGTransformComponent>().Position, this._transform.Position) < DGInteractionsConstants.MAXIMUM_RANGE).ToArray();

            weight.Add(this._nearbyResources.Length);
            return weight;
        }
        public DGPlayerActionInfo Act(DGEntity entity, DGGame game)
        {
            DGPlayerActionInfo infos = new();

            if (this._nearbyResources.Length == 0)
            {
                return infos;
            }

            DGCombatComponent attackerCombat = entity.ComponentContainer.GetComponent<DGCombatComponent>();
            DGHealthComponent resourceHealth = this._nearbyResources[0].ComponentContainer.GetComponent<DGHealthComponent>();

            resourceHealth.Hurt(attackerCombat.GetFullAttackDamage(false));

            return infos;
        }
    }
}
