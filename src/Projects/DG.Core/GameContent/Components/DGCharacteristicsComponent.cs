﻿using DeadlyGame.Core.Components;
using DeadlyGame.Core.Entities;

namespace DeadlyGame.Core.GameContent.Components
{
    public sealed class DGCharacteristicsComponent : DGComponent
    {
        /// <summary>
        /// Gets or sets the strength attribute of the character.
        /// </summary>
        /// <remarks>
        /// The Strength attribute represents the physical power of the character, determining their raw physical prowess for tasks requiring sheer force or might in the game.
        /// </remarks>
        public int Strength { get; set; }

        /// <summary>
        /// Gets or sets the constitution attribute of the character.
        /// </summary>
        /// <remarks>
        /// The Constitution attribute signifies the character's endurance and overall health. It influences the character's resilience against physical strain, injuries, and the ability to withstand harsh conditions in the game.
        /// </remarks>
        public int Constitution { get; set; }

        /// <summary>
        /// Gets or sets the dexterity attribute of the character.
        /// </summary>
        /// <remarks>
        /// Dexterity determines the character's agility, reflexes, and coordination. It impacts actions requiring precision and swiftness during gameplay.
        /// </remarks>
        public int Dexterity { get; set; }

        /// <summary>
        /// Gets or sets the charisma attribute of the character.
        /// </summary>
        /// <remarks>
        /// Charisma represents the character's charm, persuasiveness, and ability to influence others within the game world. It can affect interactions with other characters and factions.
        /// </remarks>
        public int Charisma { get; set; }

        /// <summary>
        /// Gets or sets the intelligence attribute of the character.
        /// </summary>
        /// <remarks>
        /// Intelligence signifies the character's mental acuity, logic, and problem-solving capabilities. It may impact various decisions and outcomes in the game requiring mental prowess.
        /// </remarks>
        public int Intelligence { get; set; }

        /// <summary>
        /// Gets or sets the wisdom attribute of the character.
        /// </summary>
        /// <remarks>
        /// Wisdom reflects the character's insight, intuition, and judgment. It can influence the character's ability to make sound decisions or interpret situations effectively within the game's narrative.
        /// </remarks>
        public int Wisdom { get; set; }

        public DGCharacteristicsComponent(DGGame game, DGEntity entity) : base(game, entity)
        {

        }

        public override void Start()
        {
            return;
        }

        public override void Update()
        {
            return;
        }

        public void Randomize()
        {
            this.Strength = this.DGGameInstance.Dice.RollAndGetTotalSum(3, 6);
            this.Constitution = this.DGGameInstance.Dice.RollAndGetTotalSum(3, 6);
            this.Dexterity = this.DGGameInstance.Dice.RollAndGetTotalSum(3, 6);
            this.Charisma = this.DGGameInstance.Dice.RollAndGetTotalSum(3, 6);
            this.Intelligence = this.DGGameInstance.Dice.RollAndGetTotalSum(3, 6);
            this.Wisdom = this.DGGameInstance.Dice.RollAndGetTotalSum(3, 6);
        }
    }
}