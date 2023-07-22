﻿using Microsoft.AspNetCore.Authorization;
using nhomnetapi.Requirements;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;


namespace nhomnetapi.Handlers
{
    public class ValidBirthdayHandler : AuthorizationHandler<YearOldRequirement>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, YearOldRequirement requirement)
        {
            if (IsValidBirthday(context.User, requirement))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }

        private bool IsValidBirthday(ClaimsPrincipal user, YearOldRequirement requirement)
        {
            if (user == null) return false;
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var dataContext = new nhomnetapi.Entities.T22netContext();
            var userData = dataContext.Users.Find(Convert.ToInt32(userId));
            if (userData == null || userData.Birthday == null) return false;
            var birthday = DateTime.Parse(userData.Birthday.ToString());
            var diffYear = DateTime.Today.Year - birthday.Year;
            if (diffYear >= requirement.MinYear && diffYear <= requirement.MaxYear) return true;


            return false;
        }
    }
}
