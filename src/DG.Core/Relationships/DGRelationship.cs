using DG.Core.Entities;

using System;
using System.ComponentModel.DataAnnotations;

namespace DG.Core.Relationships
{
    internal enum DGRelationshipLevelType
    {
        VeryLow,
        Low,
        Neutral,
        High,
        VeryHigh
    }

    internal sealed class DGRelationship
    {
        internal float RelationshipValue => this.relationshipValue;
        internal DGEntity CurrentEntity => this.currentEntity;
        internal DGEntity AnotherEntity => this.anotherEntity;

        [Range(-100, 100)] private float relationshipValue;
        private readonly DGEntity currentEntity;
        private readonly DGEntity anotherEntity;

        internal DGRelationship(DGEntity current, DGEntity another)
        {
            this.currentEntity = current;
            this.anotherEntity = another;
            this.relationshipValue = 50;
        }

        internal DGRelationship(DGEntity current, DGEntity another, float relationshipValue)
        {
            this.currentEntity = current;
            this.anotherEntity = another;
            this.relationshipValue = relationshipValue;
            Clamp();
        }

        internal void SetRelationshipValue(float value)
        {
            this.relationshipValue = value;
            Clamp();
        }

        internal void IncreaseRelationshipValue(float value)
        {
            this.relationshipValue += value;
            Clamp();
        }

        internal void DecreaseRelationshipValue(float value)
        {
            this.relationshipValue -= value;
            Clamp();
        }

        private void Clamp()
        {
            this.relationshipValue = Math.Clamp(this.relationshipValue, -100, 100);
        }

        internal DGRelationshipLevelType GetRelationshipLevelType()
        {
            if (this.relationshipValue <= -80)
            {
                return DGRelationshipLevelType.VeryLow;
            }
            else if (this.relationshipValue <= -50)
            {
                return DGRelationshipLevelType.Low;
            }
            else if (this.relationshipValue <= 50)
            {
                return DGRelationshipLevelType.Neutral;
            }
            else if (this.relationshipValue <= 80)
            {
                return DGRelationshipLevelType.High;
            }
            else
            {
                return DGRelationshipLevelType.VeryHigh;
            }
        }
    }
}