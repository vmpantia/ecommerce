namespace ECommerce.Common.Constants.Messages
{
    public class Error
    {
        public const string NO_DATA_FOUND = "No data found in the system.";
        public const string PARAMETERS_NULL = "Parameters cannot be null.";

        //User
        public const string GET_USRS_NULL = "Get users cannot be null.";
        public const string SAVE_USR_REQUEST_NULL = "Save user request cannot be null.";
        public const string REG_USR_REQUEST_NULL = "Register user request cannot be null.";
        public const string LOGIN_USR_REQUEST_NULL = "Login user request cannot be null.";

        public const string ATTR_USR_NO_CHANGES_MADE = "No changes made in user.";

        public const string ATTR_USR_STATUS_NOT_ACTIVATED = "User is currently not activated, Please check your email to activate this user.";
        public const string ATTR_USR_STATUS_NOT_ENABLED = "User is currently not able to login, Please contact administrator of this system.";
        public const string ATTR_USR_CONFIRM_PASSWORD_NOT_SAME = "The Password and Confirm Password field must be same.";
        public const string ATTR_USR_LOGON_NAME_EXIST = "The Username or Email field is already exist in the system.";

        public const string ATTR_USR_USERNAME_EXIST = "The Username field is already exist in the system.";
        public const string ATTR_USR_EMAIL_EXIST = "The Email field is already exist in the system.";

        public const string ATTR_USR_BIRTHDATE_PAST_DATE = "The Birth Date field must past date.";
        public const string ATTR_USR_BIRTHDATE_VALID_DATE = "The Birth Date field must be valid date.";
        public const string ATTR_USR_BIRTHDATE_REQUIRED = "The Birth Date field is required.";

        //Product
        public const string GET_PRDCTS_NULL = "Get products cannot be null.";
        public const string SAVE_PRDCTS_REQUEST_NULL = "Save product request cannot be null.";
    }
}
