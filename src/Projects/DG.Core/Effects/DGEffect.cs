using DeadlyGame.Core.Entities;
using DeadlyGame.Core.Enums.Effects;

namespace DeadlyGame.Core.Effects
{
    public abstract class DGEffect
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DGEffectType EffectType { get; set; }
        public int Durability { get; set; }
        public int RemainingDurability => this._remainingDurability;
        public bool IsFinished => this._remainingDurability == 0;

        protected DGGame Game => this._game;

        private DGEntity _entity;
        private DGGame _game;
        private int _remainingDurability;

        public DGEffect()
        {
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
            this._remainingDurability = this._remainingDurability < 0 ? 0 : this._remainingDurability;
            OnApplyEffect(this._entity);
        }

        protected abstract void OnApplyEffect(DGEntity entity);
    }
}
