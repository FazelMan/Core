using System.Threading.Tasks;

namespace FazelMan.Sms
{
    public interface ISmsService
    {
        Task<bool> SendAsync(string templateName, string phoneNumber, string value);
        Task<bool> SendAsync(string templateName, string phoneNumber, params string[] values);
    }
}