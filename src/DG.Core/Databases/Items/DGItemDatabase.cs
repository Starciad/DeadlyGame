using DG.Core.Items;
using DG.Core.Items.Attributes;
using DG.Core.Objects;

using System;
using System.Collections.Generic;
using System.Reflection;

namespace DG.Core.Databases.Items
{
    internal sealed class DGItemDatabase : DGObject
    {
        private readonly Dictionary<Type, DGItem> _items = [];

        protected override void OnAwake()
        {
            Type[] types = typeof(DGItemDatabase).Assembly.GetTypes();
            int length = types.Length;

            List<DGItem> items = [];
            for (int i = 0; i < length; i++)
            {
                Type type = types[i];
                var itemRegisterAttribute = type.GetCustomAttribute<DGItemRegisterAttribute>();
                if (itemRegisterAttribute == null)
                {
                    continue;
                }

                this._items.Add(type, (DGItem)Activator.CreateInstance(type));
            }
        }

        internal T GetItem<T>() where T : DGItem
        {
            return (T)GetItem(typeof(T));
        }

        internal bool TryGetItem<T>(out T item) where T : DGItem
        {
            bool result = TryGetItem(typeof(T), out DGItem itemValue);
            item = (T)itemValue;
            return result;
        }

        internal DGItem GetItem(Type itemType)
        {
            _ = TryGetItem(itemType, out DGItem item);
            return item;
        }

        internal bool TryGetItem(Type itemType, out DGItem item)
        {
            if (this._items.TryGetValue(itemType, out item))
            {
                return true;
            }

            return false;
        }
    }
}
