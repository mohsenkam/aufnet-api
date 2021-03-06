﻿using Aufnet.Backend.ApiServiceShared.Models.Shared;

namespace Aufnet.Backend.ApiServiceShared.Shared
{
    public class ErrorCodesConstants
    {

        //Error codes 0000 tO 0999 are general errors
        public static readonly ErrorDto NotExistingRole = new ErrorDto("0000", "The role to be assigned to the user doesn't exist"); //e.g. the role president doesn't exist
        public static readonly ErrorDto InvalidRole = new ErrorDto("0001", "The role to be assigned to the user is not valid for this user"); //e.g. a customer cannot be an admin

        //todo: remove this, as it is too informative and can cause securiy issues
        public static readonly ErrorDto NotExistingUser = new ErrorDto("0002", "The user doesn't exist"); //e.g. changing the password of a user who doesn't exist

        public static readonly ErrorDto ArgumentMissing = new ErrorDto("0003", "The required argument is not provided: "); //e.g. the password sent to the change password is null. Usage: Add the name of the argument to the description part
        public static readonly ErrorDto ManipulatingMissingEntity = new ErrorDto("0004", "Trying to update/delete/Get an entity which doesn't exist."); //e.g. updating/deleting the customer profile before the profile is added at first place
        public static readonly ErrorDto AddingDuplicateEntry = new ErrorDto("0005", "Trying to add an entry to an entity which already exists in that entity."); //e.g. adding the same event to the customer's calendar
        public static readonly ErrorDto InvalidArgument = new ErrorDto("0006", "Invalid Argument: "); //e.g. adding a not-existing event to the customer's calendar
        public static readonly ErrorDto ManipulatingMissingEntry = new ErrorDto("0007", "Trying to update/delete an entry of an entity which doesn't exist."); //e.g. deleting a customer calendar bookmark before the being added at first place
        public static readonly ErrorDto UserRolesNotValidRole = new ErrorDto("0008", "Trying to Get an data which doesn't belong's this role."); //e.g. user doesn't have a correct role ( customer && merchant )
        public static readonly ErrorDto RepeatedOperation = new ErrorDto("0009", "Trying to repeat an action which is already done."); //e.g. user tries to confirm the email again ( customer )
        public static readonly ErrorDto InvalidOperation = new ErrorDto("0010", "Invalid Operation"); //e.g. confirming the email for a non-existing user. DISPLAYED FOR SECURITY REASONS
        public static readonly ErrorDto EntityNotFound = new ErrorDto("0011", "Entity Not Found"); //e.g. trying to get a category by an id which doesn't exist




        //Error codes 1000 tO 1999 are customer related errors


        //Error codes 2000 tO 2999 are merchant related errors


        //Error codes 3000 tO 3999 are system related errors
        public static readonly ErrorDto OperationFailed = new ErrorDto("3000", "Operation Failed"); //e.g. the record cannot be saved to the database
        public static readonly ErrorDto EmailNotConfirmed = new ErrorDto("3001", "Email not confirmed");



    }
}
