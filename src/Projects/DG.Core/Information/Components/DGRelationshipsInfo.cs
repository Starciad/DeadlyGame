namespace DeadlyGame.Core.Information.Components
{
    public struct DGRelationshipsInfo
    {
        public DGRelationshipInfo[] Relationships { get; set; }

        public DGRelationshipsInfo()
        {
            this.Relationships = [];
        }

        internal static DGRelationshipsInfo Create(DGRelationshipsComponent component)
        {
            DGRelationship[] relationships = component.Relationships;
            int relationshipsLength = relationships.Length;

            DGRelationshipInfo[] relationshipsInfo = new DGRelationshipInfo[relationshipsLength];
            for (int i = 0; i < relationshipsLength; i++)
            {
                relationshipsInfo[i] = new()
                {
                    RelationshipValue = relationships[i].RelationshipValue,
                    CurrentEntityId = relationships[i].CurrentEntity.Id,
                    AnotherEntityId = relationships[i].AnotherEntity.Id
                };
            }

            return new()
            {
                Relationships = relationshipsInfo,
            };
        }
    }

    public struct DGRelationshipInfo
    {
        public float RelationshipValue { get; set; }
        public int CurrentEntityId { get; set; }
        public int AnotherEntityId { get; set; }

        public DGRelationshipInfo()
        {
            this.RelationshipValue = 0;
            this.CurrentEntityId = 0;
            this.AnotherEntityId = 0;
        }
    }
}
