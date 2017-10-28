using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models;
using Microsoft.Extensions.Options;

namespace Aufnet.Backend.Services.Shared
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailModel email);
    }
}
