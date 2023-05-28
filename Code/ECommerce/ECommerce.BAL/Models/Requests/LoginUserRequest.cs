namespace ECommerce.BAL.Models.Requests
{
    public class LoginUserRequest
    {
        public string LogonName { get; set; } /*It's either Username or Email*/
        public string Password { get; set; }
    }
}
