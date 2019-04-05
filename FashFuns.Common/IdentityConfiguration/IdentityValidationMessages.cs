
namespace FashFuns.Common.IdentityConfiguration
{
    public class IdentityValidationMessages
    {
        public const string EMAIL_INVALID = "Email is not valid";

        public const string TOKEN_INVALID = "Invalid token";

        public const string PASSWORD_EXPIRED = "Your password has expired, please update it now";

        public const string PASSWORD_EXPIRED_PLEASE_RESET = "Your password has expired. Please contact the project administrator to reset it for you.";

        public const string INVALID_CREDENTIALS = "Email or password did not match the user credentials";

        public const string USER_NOT_ALLOW_TO_RESET_PASSWORD = "User is not allowed to reset an expired password";

        public const string PASSWORD_MUST_BE_DIFFERENT = "Password must be different to old password";

        public const string PASSWORD_WEAK_ERROR = "Password must be between 6 and 18 characters long and must contain an upper case letter, a lower case letter and a number";
    }
}
