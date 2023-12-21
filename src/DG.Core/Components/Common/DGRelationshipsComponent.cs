using System.Collections.Generic;
using System.Linq;

using DG.Core.Entities;
using DG.Core.Relationships;

namespace DG.Core.Components.Common
{
    internal sealed class DGRelationshipsComponent : DGComponent
    {
        private readonly Dictionary<DGEntity, DGRelationship> relationships = [];

        internal DGRelationship AddRelationship(DGEntity anotherEntity, float value = 50)
        {
            DGRelationship relationship = new(this.Entity, anotherEntity, value);
            this.relationships.TryAdd(anotherEntity, relationship);
            return relationship;
        }

        internal DGRelationship GetRelationship(DGEntity anotherEntity)
        {
            if (this.relationships.TryGetValue(anotherEntity, out DGRelationship value))
            {
                return value;
            }
            else
            {
                return AddRelationship(anotherEntity);
            }
        }

        internal float GetRelationshipValue(DGEntity anotherEntity)
        {
            return GetRelationship(anotherEntity).RelationshipValue;
        }

        internal DGRelationshipLevelType GetRelationshipLevelType(DGEntity anotherEntity)
        {
            return GetRelationship(anotherEntity).GetRelationshipLevelType();
        }

        internal DGRelationship[] GetBetterRelationships()
        {
            return
            [
                .. relationships.Values.OrderByDescending(x => x.RelationshipValue)
            ];
        }

        internal DGRelationship[] GetWorstRelationships()
        {
            return
            [
                .. relationships.Values.OrderBy(x => x.RelationshipValue)
            ];
        }

        internal void SetRelationshipValue(DGEntity anotherEntity, float value)
        {
            GetRelationship(anotherEntity).SetRelationshipValue(value);
        }

        internal void IncreaseRelationshipValue(DGEntity anotherEntity, float value)
        {
            GetRelationship(anotherEntity).IncreaseRelationshipValue(value);
        }

        internal void DecreaseRelationshipValue(DGEntity anotherEntity, float value)
        {
            GetRelationship(anotherEntity).DecreaseRelationshipValue(value);
        }
    }
}
