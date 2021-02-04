using BC.Shared;
using BC.Shared.Models;
using System.Threading.Tasks;

namespace BC.Server.Services
{
    public interface IUserValidationService
    {
        Task<UserProfile> LogInUser(LogInVM request);
        Task<UserProfile> RegisterUser(RegistrationVM request);
    }
}