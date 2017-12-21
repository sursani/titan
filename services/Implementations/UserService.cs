using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Npgsql;
using NpgsqlTypes;
using Titan.Contexts;
using Titan.Data;
using Titan.Entities;

namespace Titan.Services
{
    public class UserService : IUserService
    {
        private readonly TitanContext _context;
        private readonly IOptions<ConnectionStringOptions> _connStrOptions;

        public UserService(TitanContext context, IOptions<ConnectionStringOptions> connStrOptions)
        {
            _context = context;
            _connStrOptions = connStrOptions;
        }

        public async Task<bool> RegisterNewUser(string userName, string password, string firstName, string lastName,
                string email, Gender gender, double latitude, double longitude)
        {
            var user = new User
            {
                UserName = userName,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Gender = gender,
                Location = new PostgisPoint(latitude, longitude){SRID=4326}
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<double> DistanceBetweenUsers(User firstUser, User secondUser)
        {
            using (var conn = new NpgsqlConnection(new ApplicationContext().GetConnectionString(_connStrOptions.Value.TitanContext)))
            {
                await conn.OpenAsync();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT ST_Distance(a.""Location"", b.""Location"") FROM ""User"" a, ""User"" b WHERE a.""Id""=@p1 AND b.""Id""=@p2";
                    cmd.Parameters.AddWithValue("p1", firstUser.Id);
                    cmd.Parameters.AddWithValue("p2", secondUser.Id);
                    
                    var obj = await cmd.ExecuteScalarAsync();
                    return Convert.ToDouble(obj);
                }
            }
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> ValidateCredentials(string userName, string password)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.UserName == userName && x.Password == password);
        }
    }
}