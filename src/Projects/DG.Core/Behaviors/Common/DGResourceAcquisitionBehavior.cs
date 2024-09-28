using DeadlyGame.Core.Behaviors.Models;
using DeadlyGame.Core.Components.Common;
using DeadlyGame.Core.Constants;
using DeadlyGame.Core.Entities;
using DeadlyGame.Core.Localization;
using DeadlyGame.Core.Mathematics.Primitives;
using DeadlyGame.Core.Models.Infos.Actions;

using System.Drawing;
using System.Linq;

namespace DeadlyGame.Core.Behaviors.Common
{
    internal sealed class DGResourceAcquisitionBehavior : IDGBehaviour
    {
        private DGEntity[] _nearbyResources;

        // System
        private DGEntity _entity;

        // Components
        private DGTransformComponent _transformComponent;
        private DGCombatComponent _combatComponent;

        private DGHealthComponent _resourceHealthComponent;

        // Infos
        private DGEntity _resourceEntity;

        // Consts
        private const string S_RESOURCE_ACQUISITION = "Resource_Acquisition";

        public bool CanAct(DGEntity entity, DGGame game)
        {
            this._entity = entity;

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
            this._nearbyResources = game.WorldManager.GetNearbyResources(this._transformComponent.Position).Where(x => DGPoint.Distance(x.ComponentContainer.GetComponent<DGTransformComponent>().Position, this._transformComponent.Position) < DGInteractionsConstants.MAXIMUM_RANGE).ToArray();
            return this._nearbyResources != null && this._nearbyResources.Length != 0 && !this._nearbyResources[0].ComponentContainer.TryGetComponent(out this._resourceHealthComponent);
        }
        public DGBehaviourWeight GetWeight()
        {
            this._resourceEntity = this._nearbyResources[0];

            DGBehaviourWeight weight = new();
            weight.Add(this._nearbyResources.Length);
            return weight;
        }
        public DGPlayerActionInfo Act()
        {
            int totalDamage = this._combatComponent.GetFullAttackDamage(false);
            this._resourceHealthComponent.Hurt(totalDamage);

            DGPlayerActionInfo infos = new();
            infos.WithName(DGLocalization.Read(S_RESOURCE_ACQUISITION, "Name"));
            infos.WithTitle(string.Format(DGLocalization.Read(S_RESOURCE_ACQUISITION, "Title"), this._entity.Name));
            infos.WithDescription(string.Format(DGLocalization.Read(S_RESOURCE_ACQUISITION, "Description"), this._entity.Name, totalDamage, this._resourceEntity.Name));
            infos.WithPriorityLevel(5);
            infos.WithAuthor(this._entity.Id);
            infos.WithColor(Color.SandyBrown);
            return infos;
        }
    }
}
