using System;
using System.Linq;
using NpgsqlTypes;
using Titan.Contexts;
using Titan.Entities;

namespace Titan.Initializers
{
    // NOTES:
    /*
    dotnet ef --startup-project ../ui-services/ migrations add Initial
    dotnet ef --startup-project ../ui-services/ migrations script Initial -o ../data/Migrations/Scripts/initial.sql 
     */

    public static class DbInitializer
    {
        public static void Initialize(TitanContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
                new User{
                    FirstName="Carson",
                    LastName="Alexander",
                    Email="test1@test.com",
                    DateOfBirth=new DateTime(1999, 1, 1),
                    Gender=Gender.Male,
                    UserName="user1",
                    Created=DateTime.UtcNow,
                    Location= new Postgis​Point(33.676244d, -117.867390d)
                },
                new User
                {
                    FirstName="Meredith",
                    LastName="Alonso",
                    Email="test2@test.com",
                    DateOfBirth=new DateTime(1999, 1, 1),
                    Gender=Gender.Female,
                    UserName="user2",
                    Created=DateTime.UtcNow,
                    Location= new Postgis​Point(33.9434d, -118.4079d)
                },
                new User
                {
                    FirstName="Arturo",
                    LastName="Anand",
                    Email="test3@test.com",
                    DateOfBirth=new DateTime(1999, 1, 1),
                    Gender=Gender.Male,
                    UserName="user3",
                    Created=DateTime.UtcNow,
                    Location= new Postgis​Point(37.621500d, -122.379191d)
                }
            };

            foreach (var u in users)
            {
                context.Users.Add(u);
            }
            
            context.SaveChanges();
        }
    }
}