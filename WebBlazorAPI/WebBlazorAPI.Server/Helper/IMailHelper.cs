using WebBlazorAPI.Shared.Enums;

namespace WebBlazorAPI.Server.Helper
{
    public interface IMailHelper
    {
        Response SendMail(string toName, string toEmail, string subject, string body);
    }
}
