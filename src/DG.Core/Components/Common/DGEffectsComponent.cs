using DG.Core.Effects;
using DG.Core.Exceptions.Effects;

using System;
using System.Collections.Generic;

namespace DG.Core.Components.Common
{
    internal sealed class DGEffectsComponent : DGComponent
    {
        private readonly Dictionary<Type, DGEffect> _effects = [];

        private DGHealthComponent _healthComponent;

        protected override void OnAwake()
        {
            base.OnAwake();

            this.Game.RoundManager.OnRoundStarted += UpdateEffects;

            if (this.Entity.ComponentContainer.TryGetComponent(out DGHealthComponent healthComponent))
            {
                this._healthComponent = healthComponent;
                this._healthComponent.OnDied += HealthComponent_OnDied;
            }
        }
        private void UpdateEffects()
        {
            List<Type> finishedEffects = [];

            // Update active effects.
            foreach (KeyValuePair<Type, DGEffect> effect in this._effects)
            {
                if (effect.Value.IsFinished)
                {
                    finishedEffects.Add(effect.Key);
                    continue;
                }

                effect.Value.Update();
            }

            // Remove finished effects.
            foreach (Type effectType in finishedEffects)
            {
                RemoveEffect(effectType);
            }
        }

        internal void AddEffect<T>() where T : DGEffect
        {
            AddEffect(typeof(T));
        }
        internal void RemoveEffect<T>() where T : DGEffect
        {
            RemoveEffect(typeof(T));
        }
        internal DGEffect GetEffect<T>() where T : DGEffect
        {
            return GetEffect(typeof(T));
        }
        internal bool HasEffect<T>() where T : DGEffect
        {
            return HasEffect(typeof(T));
        }

        internal void AddEffect(Type effectType)
        {
            if (!effectType.IsSubclassOf(typeof(DGEffect)))
            {
                throw new DGInvalidEffectTypeException($"The type of effect you are trying to create is invalid.");
            }

            DGEffect effect = (DGEffect)Activator.CreateInstance(effectType);

            effect.SetEntity(this.Entity);
            effect.SetGame(this.Game);

            _ = _effects.TryAdd(effectType, effect);
        }
        internal void RemoveEffect(Type effectType)
        {
            _ = _effects.Remove(effectType);
        }
        internal DGEffect GetEffect(Type effectType)
        {
            if (this._effects.ContainsKey(effectType))
            {
                return this._effects[effectType];
            }

            return null;
        }
        internal bool HasEffect(Type effectType)
        {
            return this._effects.ContainsKey(effectType);
        }

        // ===== EVENTS =====
        private void HealthComponent_OnDied()
        {
            this.Game.RoundManager.OnRoundStarted -= UpdateEffects;
            this._healthComponent.OnDied -= HealthComponent_OnDied;
        }
    }
}
