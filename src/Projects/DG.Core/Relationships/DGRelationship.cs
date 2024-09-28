using DeadlyGame.Core.Entities;
using DeadlyGame.Core.Enums.Relationships;

using System;
using System.ComponentModel.DataAnnotations;

namespace DeadlyGame.Core.Relationships
{
    public sealed class DGRelationship
    {
        public float RelationshipValue => this.relationshipValue;
        public DGEntity CurrentEntity => this.currentEntity;
        public DGEntity AnotherEntity => this.anotherEntity;

        [Range(-100, 100)] private float relationshipValue;
        private readonly DGEntity currentEntity;
        private readonly DGEntity anotherEntity;

        public DGRelationship(DGEntity current, DGEntity another)
        {
            this.currentEntity = current;
            this.anotherEntity = another;
            this.relationshipValue = 50;
        }

        public DGRelationship(DGEntity current, DGEntity another, float relationshipValue)
        {
            this.currentEntity = current;
            this.anotherEntity = another;
            this.relationshipValue = relationshipValue;
            Clamp();
        }

        public void SetRelationshipValue(float value)
        {
            this.relationshipValue = value;
            Clamp();
        }

        public void IncreaseRelationshipValue(float value)
        {
            this.relationshipValue += value;
            Clamp();
        }

        public void DecreaseRelationshipValue(float value)
        {
            this.relationshipValue -= value;
            Clamp();
        }

        private void Clamp()
        {
            this.relationshipValue = Math.Clamp(this.relationshipValue, -100, 100);
        }

        public DGRelationshipLevelType GetRelationshipLevelType()
        {
            return this.relationshipValue <= -80
                ? DGRelationshipLevelType.VeryLow
                : this.relationshipValue <= -50
                    ? DGRelationshipLevelType.Low
                    : this.relationshipValue <= 50
                                    ? DGRelationshipLevelType.Neutral
                                    : this.relationshipValue <= 80 ? DGRelationshipLevelType.High : DGRelationshipLevelType.VeryHigh;
        }
    }
}