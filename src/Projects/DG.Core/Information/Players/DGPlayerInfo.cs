using DG.Core.Components.Common;
using DG.Core.Entities.Players;
using DG.Core.Information.Components;

namespace DG.Core.Information.Players
{
    public struct DGPlayerInfo
    {
        public string Name { get; set; }
        public int Id { get; set; }

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

        public DGPlayerInfo()
        {
            this.Name = string.Empty;
            this.Id = 0;
            this.Transform = new();
            this.Informations = new();
            this.Personality = new();
            this.Characteristics = new();
            this.Health = new();
            this.Hunger = new();
            this.Combat = new();
            this.Effects = new();
            this.Inventory = new();
            this.Equipment = new();
            this.Relationships = new();
        }

        internal static DGPlayerInfo Create(DGPlayer player)
        {
            return new()
            {
                Name = player.Name,
                Id = player.Id,
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
            return component == null ? default : DGTransformInfo.Create(component);
        }
        private static DGInformationsInfo CreatePlayerInformationsComponent(DGInformationsComponent component)
        {
            return component == null ? default : DGInformationsInfo.Create(component);
        }
        private static DGPersonalityInfo CreatePlayerPersonalityComponent(DGPersonalityComponent component)
        {
            return component == null ? default : DGPersonalityInfo.Create(component);
        }
        private static DGCharacteristicsInfo CreatePlayerCharacteristicsComponent(DGCharacteristicsComponent component)
        {
            return component == null ? default : DGCharacteristicsInfo.Create(component);
        }
        private static DGHealthInfo CreatePlayerHealthComponent(DGHealthComponent component)
        {
            return component == null ? default : DGHealthInfo.Create(component);
        }
        private static DGHungerInfo CreatePlayerHungerComponent(DGHungerComponent component)
        {
            return component == null ? default : DGHungerInfo.Create(component);
        }
        private static DGCombatInfo CreatePlayerCombatComponent(DGCombatComponent component)
        {
            return component == null ? default : DGCombatInfo.Create(component);
        }
        private static DGEffectsInfo CreatePlayerEffectsComponent(DGEffectsComponent component)
        {
            return component == null ? default : DGEffectsInfo.Create(component);
        }
        private static DGInventoryInfo CreatePlayerInventoryComponent(DGInventoryComponent component)
        {
            return component == null ? default : DGInventoryInfo.Create(component);
        }
        private static DGEquipmentInfo CreatePlayerEquipmentComponent(DGEquipmentComponent component)
        {
            return component == null ? default : DGEquipmentInfo.Create(component);
        }
        private static DGRelationshipsInfo CreatePlayerRelationshipsComponent(DGRelationshipsComponent component)
        {
            return component == null ? default : DGRelationshipsInfo.Create(component);
        }
    }
}