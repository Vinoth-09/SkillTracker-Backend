using Amazon.DynamoDBv2.DataModel;

namespace SkillTracker.Entities
{
    [DynamoDBTable("st-skill")]
    public class SkillEntity : EntityBase
    {
        [DynamoDBProperty("empId")]
        [DynamoDBHashKey]
        public string EmpId { get; set; }

        [DynamoDBProperty("skillId")]
        [DynamoDBRangeKey]
        public int SkillId { get; set; }

        [DynamoDBProperty("technical")]
        public bool IsTechnical { get; set; }

        [DynamoDBProperty("name")]
        public string Name { get; set; }

        [DynamoDBProperty("proficiency")]
        public int Proficiency { get; set; }
    }
}
