using System.Linq;
using Titan.Contexts;
using Titan.Entities;

namespace Titan.Initializers
{
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
                new User{FirstName="Carson",LastName="Alexander",Email="test1@test.com"},
                new User{FirstName="Meredith",LastName="Alonso",Email="test2@test.com"},
                new User{FirstName="Arturo",LastName="Anand",Email="test3@test.com"}
            };

            foreach (var u in users)
            {
                context.Users.Add(u);
            }
            
            context.SaveChanges();
        }
    }
}