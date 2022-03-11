using Amazon.DynamoDBv2.DataModel;
using System;

namespace SkillTracker.Entities
{
    public abstract class EntityBase
    {
        [DynamoDBProperty("createdBy")]
        public string CreatedBy { get; set; }

        [DynamoDBProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [DynamoDBProperty("modifiedBy")]
        public string LastModifiedBy { get; set; }

        [DynamoDBProperty("modifiedDate")]
        public DateTime? LastModifiedDate { get; set; }
    }
}
