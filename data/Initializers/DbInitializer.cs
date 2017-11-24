using System;
using System.Linq;
using Titan.Contexts;
using Titan.Entities;

namespace Titan.Initializers
{
    // NOTES:
    // command: dotnet ef --startup-project ../ui-services/ migrations add InitialMigration
    // command: dotnet ef --startup-project ../ui-services/ database update

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
                    Location="POINT(-117.867390 33.676244)"
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
                    Location="POINT(-118.4079 33.9434)"
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
                    Location="POINT(-122.379191 37.621500)"
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