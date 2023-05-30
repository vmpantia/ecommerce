using ECommerce.Common.Constants;

namespace ECommerce.Common
{
    public class Parser
    {
        public static string ParseStatus(int status)
        {
            switch(status)
            {
                case Status.INVALID_INT:
                    return Status.INVALID_STR;
                case Status.ENABLED_INT:
                    return Status.ENABLED_STR;
                case Status.DISABLED_INT:
                    return Status.DISABLED_STR;
                case Status.DELETION_INT:
                    return Status.DELETION_STR;
                default:
                    return string.Empty;
            }
        }
    }
}
