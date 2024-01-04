namespace webapi.Services
{
    public interface ISendMailService
    {
        void setToken();
        int getToken();
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
