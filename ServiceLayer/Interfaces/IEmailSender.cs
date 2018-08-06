using System.Threading.Tasks;

namespace EmployeeSystem.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
