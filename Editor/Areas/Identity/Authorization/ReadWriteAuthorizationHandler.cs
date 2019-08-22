using System.Threading.Tasks;
using Editor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Editor.Authorization
{
    public class ReadWriteAuthorizationHandler
        : AuthorizationHandler<OperationAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            if (requirement.Name != Constants.CreateOperationName &&
                    requirement.Name != Constants.EditOperationName &&
                    requirement.Name != Constants.DeleteOperationName)
            {
                return Task.CompletedTask;
            }

            // Managers can create, edit, and delete
            if (context.User.IsInRole(Constants.ReadWriteRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}