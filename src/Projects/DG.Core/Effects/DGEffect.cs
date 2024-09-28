using DeadlyGame.Core.Entities;

namespace DeadlyGame.Core.Effects
{
    public enum DGEffectType
    {
        None = -1,
        Positive = 0,
        Negative = 1
    }

    internal abstract class DGEffect
    {
        internal string Name { get; set; }
        internal string Description { get; set; }
        internal DGEffectType EffectType { get; set; }
        internal int Durability { get; set; }
        internal int RemainingDurability => this._remainingDurability;
        internal bool IsFinished => this._remainingDurability == 0;

        protected DGGame Game => this._game;

        private DGEntity _entity;
        private DGGame _game;
        private int _remainingDurability;

        internal DGEffect()
        {
            Build();
            this._remainingDurability = this.Durability;
        }
        internal void SetEntity(DGEntity entity)
        {
            this._entity = entity;
        }
        internal void SetGame(DGGame game)
        {
            this._game = game;
        }
        internal void Update()
        {
            this._remainingDurability--;
            this._remainingDurability = this._remainingDurability < 0 ? 0 : this._remainingDurability;
            OnApplyEffect(this._entity);
        }

        protected abstract void Build();
        protected abstract void OnApplyEffect(DGEntity entity);
    }
}
