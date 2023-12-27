using DG.Core.Behaviors.Models;
using DG.Core.Components.Common;
using DG.Core.Constants;
using DG.Core.Entities;
using DG.Core.Information.Actions;
using DG.Core.Items;
using DG.Core.Localization;

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;

namespace DG.Core.Behaviors.Common
{
    internal sealed class DGItemAcquisitionBehavior : IDGBehaviour
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
        private const string S_ITEM_ACQUISITION = "Item_Acquisition_Behavior";

        public bool CanAct(DGEntity entity, DGGame game)
        {
            this._entity = entity;
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
            StringBuilder itemsObtainedString = new();

            List<DGWorldItem> itemsObtained = [];
            List<DGWorldItem> distinctItemsObtained = [];

            foreach (DGWorldItem worldItem in this._nearbyItems)
            {
                this._game.WorldManager.RemoveWorldItem(worldItem);
                if (this._inventoryComponent.TryAddItem(worldItem))
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
                itemsObtainedString.AppendFormat(DGLocalization.Read(S_ITEM_ACQUISITION, "New_Item") + (i < distinctItemsObtainedCount ? ", " : string.Empty), count, itemObtained.Item.Name);
            }

            DGLocalization.Read(S_ITEM_ACQUISITION, "Description");
            DGPlayerActionInfo infos = new();
            infos.WithName(DGLocalization.Read(S_ITEM_ACQUISITION, "Name"));
            infos.WithTitle(DGLocalization.Read(S_ITEM_ACQUISITION, "Title"));
            infos.WithDescription(itemsObtainedString.ToString());
            infos.WithPriorityLevel(4);
            infos.WithAuthor(this._entity.Id);
            infos.WithColor(Color.LightYellow);
            return infos;
        }
    }
}
