using DeadlyGame.Core.Items;
using DeadlyGame.Core.Objects;

using System;
using System.Collections.Generic;

namespace DeadlyGame.Core.Databases.Items
{
    public sealed partial class DGItemDatabase : DGObject
    {
        private readonly Dictionary<Type, DGItem> _items = [];

        protected override void OnAwake()
        {
            RegisterItems();
        }

        public T GetItem<T>() where T : DGItem
        {
            return (T)GetItem(typeof(T));
        }

        public bool TryGetItem<T>(out T item) where T : DGItem
        {
            bool result = TryGetItem(typeof(T), out DGItem itemValue);
            item = (T)itemValue;
            return result;
        }

        public DGItem GetItem(Type itemType)
        {
            _ = TryGetItem(itemType, out DGItem item);
            return item;
        }

        public bool TryGetItem(Type itemType, out DGItem item)
        {
            return this._items.TryGetValue(itemType, out item);
        }
    }
}
