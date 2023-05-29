namespace ECommerce.BAL.Contractors
{
    public interface IEmailService
    {
        Task SendEmail(string body);
    }
}