using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace SBTestTask.Common.Models
{
    public class User
    {
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }

        [BsonElement]
        [Required]
        [StringLength(16)]
        public string Name { get; set; } = string.Empty;

        [BsonElement]
        [DataType(DataType.DateTime)]
        public DateTime Created { get; } = DateTime.Now;
    }
}