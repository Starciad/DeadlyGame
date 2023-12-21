using DG.Core.Components;
using DG.Core.Exceptions;
using DG.Core.Objects;

using System;
using System.Collections.Generic;

namespace DG.Core.Entities
{
    internal abstract class DGEntity : DGObject
    {
        internal string Name { get; set; }


        private readonly Dictionary<Type, DGComponent> _components = [];

        internal T AddComponent<T>() where T : DGComponent
        {
            return (T)AddComponent(typeof(T));
        }
        internal T GetComponent<T>() where T : DGComponent
        {
            return (T)GetComponent(typeof(T));
        }
        internal bool TryGetComponent<T>(out T value) where T : DGComponent
        {
            if (TryGetComponent(typeof(T), out DGComponent component))
            {
                value = (T)component;
                return true;
            }

            value = null;
            return false;
        }
        internal bool TryRemoveComponent<T>() where T : DGComponent
        {
            return TryRemoveComponent(typeof(T));
        }

        internal DGComponent AddComponent(Type componentType)
        {
            if (componentType.IsSubclassOf(typeof(DGComponent)))
            {
                throw new DGInvalidComponentTypeException($"The type '{componentType.Name}' is not a valid DGComponent.");
            }

            if (this._components.ContainsKey(componentType))
            {
                throw new InvalidOperationException($"The entity already contains a '{componentType.Name}' component.");
            }

            DGComponent componentValue = (DGComponent)Activator.CreateInstance(componentType);
            componentValue.Initialize();

            this._components.Add(componentType, componentValue);
            return componentValue;
        }
        internal DGComponent GetComponent(Type componentType)
        {
            if (this._components.TryGetValue(componentType, out DGComponent value))
            {
                return value;
            }

            return null;
        }
        internal bool TryGetComponent(Type componentType, out DGComponent value)
        {
            DGComponent result = GetComponent(componentType);
            if (result != null)
            {
                value = result;
                return true;
            }

            value = null;
            return false;
        }
        internal bool TryRemoveComponent(Type componentType)
        {
            return this._components.Remove(componentType);
        }
    }
}
