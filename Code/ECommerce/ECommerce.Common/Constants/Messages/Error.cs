namespace ECommerce.Common.Constants.Messages
{
    public class Error
    {
        public const string NO_DATA_FOUND = "No data found in the system.";

        //User
        public const string GET_USRS_NULL = "Get users cannot be null.";
        public const string SAVE_USR_REQUEST_NULL = "Save user request cannot be null.";
        public const string REG_USR_REQUEST_NULL = "Register user request cannot be null.";
        public const string LOGIN_USR_REQUEST_NULL = "Login user request cannot be null.";

        public const string ATTR_USR_STATUS_NOT_ACTIVATED = "User is currently not activated, Please check your email to activate this user.";
        public const string ATTR_USR_STATUS_NOT_ENABLED = "User is currently not able to login, Please contact administrator of this system.";
        public const string ATTR_USR_LOGON_NAME_EXIST = "The Email or Username field is already exist in the system.";
        public const string ATTR_USR_CONFIRM_PASSWORD_NOT_SAME = "The Password and Confirm Password field must be same.";

        //Product
        public const string GET_PRDCTS_NULL = "Get products cannot be null.";
        public const string SAVE_PRDCTS_REQUEST_NULL = "Save product request cannot be null.";
    }
}
