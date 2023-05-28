using System.Net;

namespace ECommerce.BAL.Models.Requests
{
    public class BaseRequest
    {
        public Guid UserID { get; set; }
        public string IPAddress { get; set; }
        public string Browser { get; set; }
        public string WindowsVersion { get; set; }
    }
}
