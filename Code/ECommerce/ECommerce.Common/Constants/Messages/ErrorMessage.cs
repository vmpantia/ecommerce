namespace ECommerce.Common.Constants.Messages
{
    public class ErrorMessage
    {
        public const string NO_DATA_FOUND = "No data found in the system.";

        //User
        public const string USER_NOT_ACTIVATED = "User is currently not activated, Please check your email to activate this user.";
        public const string USER_DISABLED = "User is currently disabled, Please contact administrator of this system.";
        public const string LOGIN_USER_REQUEST_EMPTY = "Login user request cannot be empty or null.";


        public const string GET_USERS = "Error in getting of users information in the system.";
        public const string SAVE_USER_REQUEST_EMPTY = "Save user request cannot be empty or null.";
        public const string REGISTER_USER_REQUEST_EMPTY = "Register user request cannot be empty or null.";

        //Product
        public const string GET_PRODUCTS = "Error in getting of products information in the system.";
        public const string SAVE_PRODUCT_REQUEST_EMPTY = "Save product request cannot be empty or null.";
    }
}
