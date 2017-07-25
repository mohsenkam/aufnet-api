using Aufnet.Backend.ApiServiceShared.Models;

namespace Aufnet.Backend.ApiServiceShared.Shared
{
    public class ErrorCodesConstants
    {

        //Error codes 0000 tO 0999 are general errors
        public static readonly ErrorDto NotExistingRole = new ErrorDto("0000", "The role to be assigned to the user doesn't exist"); //e.g. the role president doesn't exist
        public static readonly ErrorDto InvalidRole = new ErrorDto("0001", "The role to be assigned to the user is not valid for this user"); //e.g. a customer cannot be an admin
        public static readonly ErrorDto NotExistingUser = new ErrorDto("0002", "The user doesn't exist"); //e.g. changing the password of a user who doesn't exist
        public static readonly ErrorDto ArgumentMissing = new ErrorDto("0003", "The required argument is not provided: "); //e.g. the password sent to the change password is null. Usage: Add the name of the argument to the description part


        //Error codes 1000 tO 1999 are customer related errors


        //Error codes 2000 tO 2999 are merchant related errors


        //Error codes 3000 tO 3999 are system related errors



    }
}
