namespace ECommerce.BAL.Contractors
{
    public interface IPasswordUtil
    {
        string ParsePassword(string password, bool isEncrypt);
    }
}