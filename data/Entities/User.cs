using System;
using System.ComponentModel.DataAnnotations.Schema;
using NpgsqlTypes;

namespace Titan.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        [Column(TypeName = "GEOGRAPHY(Point)")]
        public PostgisGeometry Location { get; set; }
    }
}