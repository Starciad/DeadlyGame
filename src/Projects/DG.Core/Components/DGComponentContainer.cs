﻿using DeadlyGame.Core.Entities;
using DeadlyGame.Core.Exceptions.Components;
using DeadlyGame.Core.Objects;

using System;
using System.Collections.Generic;

namespace DeadlyGame.Core.Components
{
    public sealed class DGComponentContainer : DGGameObject
    {
        public int Count => this._components.Count;

        private readonly Dictionary<Type, DGComponent> _components = [];
        private DGEntity _entity;

        public DGComponentContainer(DGGame game, DGEntity entity) : base(game)
        {
            this._entity = entity;
        }

        public override void Start()
        {
            foreach (DGComponent component in this._components.Values)
            {
                component.Start();
            }
        }

        public override void Update()
        {
            foreach (DGComponent component in this._components.Values)
            {
                component.Update();
            }
        }

        public void SetEntityInstance(DGEntity entity)
        {
            this._entity = entity;
        }

        public T AddComponent<T>() where T : DGComponent
        {
            return (T)AddComponent(typeof(T));
        }
        public T GetComponent<T>() where T : DGComponent
        {
            return (T)GetComponent(typeof(T));
        }
        public bool TryGetComponent<T>(out T value) where T : DGComponent
        {
            if (TryGetComponent(typeof(T), out DGComponent component))
            {
                value = (T)component;
                return true;
            }

            value = null;
            return false;
        }
        public bool TryRemoveComponent<T>() where T : DGComponent
        {
            return TryRemoveComponent(typeof(T));
        }
        public bool HasComponent<T>() where T : DGComponent
        {
            return HasComponent(typeof(T));
        }

        public DGComponent AddComponent(Type componentType)
        {
            if (!componentType.IsSubclassOf(typeof(DGComponent)))
            {
                throw new DGInvalidComponentTypeException($"The type '{componentType.Name}' is not a valid {nameof(DGComponent)}.");
            }

            if (this._components.ContainsKey(componentType))
            {
                throw new DGDuplicateComponentsException($"The entity already contains a '{componentType.Name}' component.");
            }

            DGComponent componentValue = (DGComponent)Activator.CreateInstance(componentType, [this.DGGameInstance, this._entity]);

            this._components.Add(componentType, componentValue);
            return componentValue;
        }
        public DGComponent GetComponent(Type componentType)
        {
            return this._components.TryGetValue(componentType, out DGComponent value) ? value : null;
        }
        public bool TryGetComponent(Type componentType, out DGComponent value)
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
        public bool TryRemoveComponent(Type componentType)
        {
            return this._components.Remove(componentType);
        }
        public bool HasComponent(Type componentType)
        {
            return !componentType.IsSubclassOf(typeof(DGComponent))
                ? throw new DGInvalidComponentTypeException($"The type '{componentType.Name}' is not a valid {nameof(DGComponent)}.")
                : this._components.ContainsKey(componentType);
        }
        public void RemoveAllComponents()
        {
            this._components.Clear();
        }
    }
}
