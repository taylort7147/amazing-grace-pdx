using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace MessageManager.Authorization
{
    public static class ContactOperations
    {
        public static OperationAuthorizationRequirement Create =
            new OperationAuthorizationRequirement {Name=Constants.CreateOperationName};
        public static OperationAuthorizationRequirement View =
            new OperationAuthorizationRequirement {Name=Constants.ViewOperationName};
        public static OperationAuthorizationRequirement Edit =
            new OperationAuthorizationRequirement {Name=Constants.EditOperationName};
        public static OperationAuthorizationRequirement Delete =
            new OperationAuthorizationRequirement {Name=Constants.DeleteOperationName};
    }

    public class Constants
    {
        public const string CreateOperationName = "Create";
        public const string ViewOperationName = "View";
        public const string EditOperationName = "Edit";
        public const string DeleteOperationName = "Delete";
        public const string AdministratorRole = "Administrator";
        public const string ReadWriteRole = "ReadWrite";
        public const string ReadOnlyRole = "ReadOnly";
        public const string ReadWritePolicy = "ReadWrite";
        public const string ReadOnlyPolicy = "ReadOnly";
    }
}