using System;

namespace Titan.Entities
{
    public class UserImage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}