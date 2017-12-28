using System.IO;
using System.Threading.Tasks;
using Titan.Entities;

namespace Titan.Services
{
    public interface IUserService
    {
        Task<bool> RegisterNewUser(string userName, string password, string firstName, string lastName,
                string email, Gender gender, double latitude, double longitude);
        Task<User> ValidateCredentials(string userName, string password);
        Task<User> GetUserById(int id);
        Task<double> DistanceBetweenUsers(User firstUser, User secondUser);
        Task<bool> UploadPicture(string objectName, string contentType, MemoryStream stream);
    }
}