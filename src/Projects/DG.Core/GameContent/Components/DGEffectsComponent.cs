using DeadlyGame.Core.Components;
using DeadlyGame.Core.Effects;
using DeadlyGame.Core.Entities;
using DeadlyGame.Core.Exceptions.Effects;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DeadlyGame.Core.GameContent.Components
{
    public sealed class DGEffectsComponent : DGComponent
    {
        public DGEffect[] Effects => [.. this._effects.Values];

        // effects
        private readonly Dictionary<Type, DGEffect> _effects = [];

        // components
        private readonly DGHealthComponent _healthComponent;

        public DGEffectsComponent(DGGame game, DGEntity entity) : base(game, entity)
        {
            game.RoundManager.OnRoundStarted += UpdateEffects;

            if (entity.ComponentContainer.TryGetComponent(out DGHealthComponent healthComponent))
            {
                this._healthComponent = healthComponent;
                this._healthComponent.OnDied += HealthComponent_OnDied;
            }
        }

        public override void Start()
        {
            return;
        }

        public override void Update()
        {
            return;
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

        public void AddEffect<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] T>() where T : DGEffect
        {
            AddEffect(typeof(T));
        }
        public void RemoveEffect<T>() where T : DGEffect
        {
            RemoveEffect(typeof(T));
        }
        public DGEffect GetEffect<T>() where T : DGEffect
        {
            return GetEffect(typeof(T));
        }
        public bool HasEffect<T>() where T : DGEffect
        {
            return HasEffect(typeof(T));
        }

        public void AddEffect([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] Type effectType)
        {
            if (!effectType.IsSubclassOf(typeof(DGEffect)))
            {
                throw new DGInvalidEffectTypeException($"The type of effect you are trying to create is invalid.");
            }

            DGEffect effect = (DGEffect)Activator.CreateInstance(effectType);

            effect.SetEntity(this.DGEntityInstance);
            effect.SetGame(this.DGGameInstance);

            _ = this._effects.TryAdd(effectType, effect);
        }
        public void RemoveEffect(Type effectType)
        {
            _ = this._effects.Remove(effectType);
        }
        public DGEffect GetEffect(Type effectType)
        {
            return this._effects.TryGetValue(effectType, out DGEffect value) ? value : null;
        }
        public bool HasEffect(Type effectType)
        {
            return this._effects.ContainsKey(effectType);
        }

        // ===== EVENTS =====
        private void HealthComponent_OnDied()
        {
            this.DGGameInstance.RoundManager.OnRoundStarted -= UpdateEffects;
            this._healthComponent.OnDied -= HealthComponent_OnDied;
        }
    }
}
