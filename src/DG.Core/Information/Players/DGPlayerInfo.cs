using DG.Core.Components.Common;
using DG.Core.Entities.Players;
using DG.Core.Information.Components;

namespace DG.Core.Information.Players
{
    public struct DGPlayerInfo
    {
        public string Name { get; set; }
        public int Index { get; set; }

        public DGTransformInfo Transform { get; set; }
        public DGInformationsInfo Informations { get; set; }
        public DGPersonalityInfo Personality { get; set; }
        public DGCharacteristicsInfo Characteristics { get; set; }
        public DGHealthInfo Health { get; set; }
        public DGHungerInfo Hunger { get; set; }
        public DGCombatInfo Combat { get; set; }
        public DGEffectsInfo Effects { get; set; }
        public DGInventoryInfo Inventory { get; set; }
        public DGEquipmentInfo Equipment { get; set; }
        public DGRelationshipsInfo Relationships { get; set; }

        internal static DGPlayerInfo Create(DGPlayer player)
        {
            return new()
            {
                Name = player.Name,
                Index = player.Index,
                Transform = CreatePlayerTransformComponent(player.ComponentContainer.GetComponent<DGTransformComponent>()),
                Informations = CreatePlayerInformationsComponent(player.ComponentContainer.GetComponent<DGInformationsComponent>()),
                Personality = CreatePlayerPersonalityComponent(player.ComponentContainer.GetComponent<DGPersonalityComponent>()),
                Characteristics = CreatePlayerCharacteristicsComponent(player.ComponentContainer.GetComponent<DGCharacteristicsComponent>()),
                Health = CreatePlayerHealthComponent(player.ComponentContainer.GetComponent<DGHealthComponent>()),
                Hunger = CreatePlayerHungerComponent(player.ComponentContainer.GetComponent<DGHungerComponent>()),
                Combat = CreatePlayerCombatComponent(player.ComponentContainer.GetComponent<DGCombatComponent>()),
                Effects = CreatePlayerEffectsComponent(player.ComponentContainer.GetComponent<DGEffectsComponent>()),
                Inventory = CreatePlayerInventoryComponent(player.ComponentContainer.GetComponent<DGInventoryComponent>()),
                Equipment = CreatePlayerEquipmentComponent(player.ComponentContainer.GetComponent<DGEquipmentComponent>()),
                Relationships = CreatePlayerRelationshipsComponent(player.ComponentContainer.GetComponent<DGRelationshipsComponent>())
            };
        }

        private static DGTransformInfo CreatePlayerTransformComponent(DGTransformComponent component)
        {
            if (component == null)
            {
                return default;
            }

            return DGTransformInfo.Create(component);
        }
        private static DGInformationsInfo CreatePlayerInformationsComponent(DGInformationsComponent component)
        {
            if (component == null)
            {
                return default;
            }

            return DGInformationsInfo.Create(component);
        }
        private static DGPersonalityInfo CreatePlayerPersonalityComponent(DGPersonalityComponent component)
        {
            if (component == null)
            {
                return default;
            }

            return DGPersonalityInfo.Create(component);
        }
        private static DGCharacteristicsInfo CreatePlayerCharacteristicsComponent(DGCharacteristicsComponent component)
        {
            if (component == null)
            {
                return default;
            }

            return DGCharacteristicsInfo.Create(component);
        }
        private static DGHealthInfo CreatePlayerHealthComponent(DGHealthComponent component)
        {
            if (component == null)
            {
                return default;
            }

            return DGHealthInfo.Create(component);
        }
        private static DGHungerInfo CreatePlayerHungerComponent(DGHungerComponent component)
        {
            if (component == null)
            {
                return default;
            }

            return DGHungerInfo.Create(component);
        }
        private static DGCombatInfo CreatePlayerCombatComponent(DGCombatComponent component)
        {
            if (component == null)
            {
                return default;
            }

            return DGCombatInfo.Create(component);
        }
        private static DGEffectsInfo CreatePlayerEffectsComponent(DGEffectsComponent component)
        {
            if (component == null)
            {
                return default;
            }

            return DGEffectsInfo.Create(component);
        }
        private static DGInventoryInfo CreatePlayerInventoryComponent(DGInventoryComponent component)
        {
            if (component == null)
            {
                return default;
            }

            return DGInventoryInfo.Create(component);
        }
        private static DGEquipmentInfo CreatePlayerEquipmentComponent(DGEquipmentComponent component)
        {
            if (component == null)
            {
                return default;
            }

            return DGEquipmentInfo.Create(component);
        }
        private static DGRelationshipsInfo CreatePlayerRelationshipsComponent(DGRelationshipsComponent component)
        {
            if (component == null)
            {
                return default;
            }

            return DGRelationshipsInfo.Create(component);
        }
    }
}