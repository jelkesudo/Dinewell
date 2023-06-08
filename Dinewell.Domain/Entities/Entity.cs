using System;

namespace Dinewell.Domain.Entities
{
    public class Entity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
    }
}
