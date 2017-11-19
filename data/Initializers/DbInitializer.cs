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
                    Gender=Gender.Male
                },
                new User
                {
                    FirstName="Meredith",
                    LastName="Alonso",
                    Email="test2@test.com",
                    DateOfBirth=new DateTime(1999, 1, 1),
                    Gender=Gender.Female
                },
                new User
                {
                    FirstName="Arturo",
                    LastName="Anand",
                    Email="test3@test.com",
                    DateOfBirth=new DateTime(1999, 1, 1),
                    Gender=Gender.Male
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