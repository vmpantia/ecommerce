namespace ECommerce.BAL.Contractors
{
    public interface IEmailService
    {
        Task SendEmailToMany(IEnumerable<string> Tos,
                                          IEnumerable<string>? Ccs,
                                          IEnumerable<string>? Bccs,
                                          string subject,
                                          string body);
    }
}