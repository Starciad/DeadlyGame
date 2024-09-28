using DeadlyGame.Core.Behaviors;
using DeadlyGame.Core.Behaviors.Models;
using DeadlyGame.Core.Constants;
using DeadlyGame.Core.Entities;
using DeadlyGame.Core.GameContent.Components;
using DeadlyGame.Core.Items;
using DeadlyGame.Core.Localization;
using DeadlyGame.Core.Mathematics.Primitives;
using DeadlyGame.Core.Models;
using DeadlyGame.Core.Models.Infos.Actions;

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DeadlyGame.Core.GameContent.Behaviors
{
    public sealed class DGItemAcquisitionBehavior : IDGBehaviour
    {
        // System
        private DGEntity _entity;
        private DGGame _game;

        // Infos
        private DGWorldItem[] _nearbyItems;

        // Components
        private DGTransformComponent _transformComponent;
        private DGInventoryComponent _inventoryComponent;

        // Consts
        private const string S_ITEM_ACQUISITION_BEHAVIOUR = "Item_Acquisition_Behavior";

        public bool CanAct(DGEntity entity, DGGame game)
        {
            this._entity = entity;
            this._game = game;

            if (!entity.ComponentContainer.TryGetComponent(out this._transformComponent) ||
                !entity.ComponentContainer.TryGetComponent(out this._inventoryComponent))
            {
                return false;
            }

            this._nearbyItems = game.WorldManager.GetNearbyItems(this._transformComponent.Position).Where(x => DGPoint.Distance(x.Position, this._transformComponent.Position) < DGInteractionsConstants.MAXIMUM_RANGE).ToArray();

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
            StringBuilder itemsObtainedString = new();

            List<DGWorldItem> itemsObtained = [];
            List<DGWorldItem> distinctItemsObtained = [];

            foreach (DGWorldItem worldItem in this._nearbyItems)
            {
                this._game.WorldManager.RemoveWorldItem(worldItem);
                if (this._inventoryComponent.TryAddItem(worldItem.Item, worldItem.Amount))
                {
                    itemsObtained.Add(worldItem);
                }
                else
                {
                    break;
                }
            }

            int distinctItemsObtainedCount = distinctItemsObtained.Count;
            for (int i = 0; i < distinctItemsObtainedCount; i++)
            {
                DGWorldItem itemObtained = distinctItemsObtained[i];
                int count = itemsObtained.Count(x => x.Item.Name == itemObtained.Item.Name);
                _ = itemsObtainedString.AppendFormat(DGLocalization.Read(S_ITEM_ACQUISITION_BEHAVIOUR, "New_Item") + (i < distinctItemsObtainedCount ? ", " : string.Empty), count, itemObtained.Item.Name);
            }

            _ = DGLocalization.Read(S_ITEM_ACQUISITION_BEHAVIOUR, "Description");
            DGPlayerActionInfo infos = new();
            infos.WithName(DGLocalization.Read(S_ITEM_ACQUISITION_BEHAVIOUR, "Name"));
            infos.WithTitle(DGLocalization.Read(S_ITEM_ACQUISITION_BEHAVIOUR, "Title"));
            infos.WithDescription(itemsObtainedString.ToString());
            infos.WithPriorityLevel(4);
            infos.WithAuthor(this._entity.Id);
            infos.WithColor(DGColor.LightYellow);
            return infos;
        }
    }
}
