using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
using Titan.Contexts;
using Titan.Entities;

namespace Titan.Services
{
    public class UserService : IUserService
    {
        private readonly TitanContext _context;
        private readonly IConfiguration _configuration;

        public UserService(TitanContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
            using (var conn = new NpgsqlConnection(_configuration.GetConnectionString("TitanContext")))
            {
                await conn.OpenAsync();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT ST_Distance(a.""Location"", b.""Location"") FROM ""User"" a, ""User"" b WHERE a.""Id""=@p1 AND b.""Id""=@p2";
                    cmd.Parameters.AddWithValue("p1", firstUser.Id);
                    cmd.Parameters.AddWithValue("p2", secondUser.Id);
                    
                    var o = await cmd.ExecuteScalarAsync();
                }
                return 0d;
            }
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}