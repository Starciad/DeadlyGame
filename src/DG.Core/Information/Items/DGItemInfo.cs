﻿using DG.Core.Items;

namespace DG.Core.Information.Items
{
    public struct DGItemInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DGItemRarity Rarity { get; set; }
        public bool HasDurability { get; set; }
        public int Durability { get; set; }

        internal static DGItemInfo Create(DGItem item)
        {
            return new()
            {
                Name = item.Name,
                Description = item.Description,
                Rarity = item.Rarity,
                HasDurability = item.HasDurability,
                Durability = item.Durability
            };
        }
    }
}