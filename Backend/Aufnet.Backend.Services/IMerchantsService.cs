using System.Threading.Tasks;
using Aufnet.Backend.Services.Base;

namespace Aufnet.Backend.Services
{
    public interface IMerchantsService
    {
        Task<IServiceResult> SignUpAsync(string username, string password, string role);
        IServiceResult SignIn(string username, string password);
        IServiceResult ResetPasswordByMail(string email);
        IServiceResult ResetPasswordByPhone(string phone);
    }
}