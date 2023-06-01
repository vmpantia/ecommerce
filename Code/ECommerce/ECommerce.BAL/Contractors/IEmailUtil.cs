namespace ECommerce.BAL.Contractors
{
    public interface IEmailUtil
    {
        Task SendEmailToMany(IEnumerable<string> Tos,
                                          IEnumerable<string>? Ccs,
                                          IEnumerable<string>? Bccs,
                                          string subject,
                                          string body);
    }
}