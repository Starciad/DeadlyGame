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

        public DGPlayerInfo(DGPlayer player)
        {
            this.Name = player.Name;
            this.Index = player.Index;

            GetPlayerTransformComponent(player.ComponentContainer.GetComponent<DGTransformComponent>());
            GetPlayerInformationsComponent(player.ComponentContainer.GetComponent<DGInformationsComponent>());
            GetPlayerPersonalityComponent(player.ComponentContainer.GetComponent<DGPersonalityComponent>());
            GetPlayerCharacteristicsComponent(player.ComponentContainer.GetComponent<DGCharacteristicsComponent>());
            GetPlayerHealthComponent(player.ComponentContainer.GetComponent<DGHealthComponent>());
            GetPlayerHungerComponent(player.ComponentContainer.GetComponent<DGHungerComponent>());
            GetPlayerCombatComponent(player.ComponentContainer.GetComponent<DGCombatComponent>());
            GetPlayerEffectsComponent(player.ComponentContainer.GetComponent<DGEffectsComponent>());
            GetPlayerInventoryComponent(player.ComponentContainer.GetComponent<DGInventoryComponent>());
            GetPlayerEquipmentComponent(player.ComponentContainer.GetComponent<DGEquipmentComponent>());
            GetPlayerRelationshipsComponent(player.ComponentContainer.GetComponent<DGRelationshipsComponent>());
        }

        private void GetPlayerTransformComponent(DGTransformComponent component)
        {
            if (component != null)
            {
                this.Transform = DGTransformInfo.Create(component);
            }
        }
        private void GetPlayerInformationsComponent(DGInformationsComponent component)
        {
            if (component != null)
            {
                this.Informations = DGInformationsInfo.Create(component);
            }
        }
        private void GetPlayerPersonalityComponent(DGPersonalityComponent component)
        {
            if (component != null)
            {
                this.Personality = DGPersonalityInfo.Create(component);
            }
        }
        private void GetPlayerCharacteristicsComponent(DGCharacteristicsComponent component)
        {
            if (component != null)
            {
                this.Characteristics = DGCharacteristicsInfo.Create(component);
            }
        }
        private void GetPlayerHealthComponent(DGHealthComponent component)
        {
            if (component != null)
            {
                this.Health = DGHealthInfo.Create(component);
            }
        }
        private void GetPlayerHungerComponent(DGHungerComponent component)
        {
            if (component != null)
            {
                this.Hunger = DGHungerInfo.Create(component);
            }
        }
        private void GetPlayerCombatComponent(DGCombatComponent component)
        {
            if (component != null)
            {
                this.Combat = DGCombatInfo.Create(component);
            }
        }
        private void GetPlayerEffectsComponent(DGEffectsComponent component)
        {
            if (component != null)
            {
                this.Effects = DGEffectsInfo.Create(component);
            }
        }
        private void GetPlayerInventoryComponent(DGInventoryComponent component)
        {
            if (component != null)
            {
                this.Inventory = DGInventoryInfo.Create(component);
            }
        }
        private void GetPlayerEquipmentComponent(DGEquipmentComponent component)
        {
            if (component != null)
            {
                Equipment = DGEquipmentInfo.Create(component);
            }
        }
        private void GetPlayerRelationshipsComponent(DGRelationshipsComponent component)
        {
            if (component != null)
            {
                Relationships = DGRelationshipsInfo.Create(component);
            }
        }
    }
}