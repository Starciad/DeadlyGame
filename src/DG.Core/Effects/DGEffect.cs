using DG.Core.Entities;
using DG.Core.Objects;

namespace DG.Core.Effects
{
    public enum DGEffectType
    {
        Positive,
        Negative
    }

    public abstract class DGEffect
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public DGEffectType EffectType { get; protected set; }
        public int Durability { get; protected set; }
        public bool IsFinished => this._remainingDurability == 0;

        protected DGGame Game => this._game;

        private DGEntity _entity;
        private DGGame _game;
        private int _remainingDurability;

        public DGEffect()
        {
            Build();
            this._remainingDurability = this.Durability;
        }
        public void SetEntity(DGEntity entity)
        {
            this._entity = entity;
        }
        public void SetGame(DGGame game)
        {
            this._game = game;
        }
        public void Update()
        {
            this._remainingDurability--;
            this._remainingDurability = (this._remainingDurability < 0 ? 0 : this._remainingDurability);
            OnApplyEffect(this._entity);
        }

        protected abstract void Build();
        protected abstract void OnApplyEffect(DGEntity entity);
    }
}
