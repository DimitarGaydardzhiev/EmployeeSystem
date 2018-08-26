using DataLayer.Interfaces;
using EmployeeSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace DataLayer
{
    public class UserResolverService : IUserResolver
    {
        private readonly IHttpContextAccessor context;

        private readonly UserManager<AspUser> userManager;

        public UserResolverService(IHttpContextAccessor context,
                                   UserManager<AspUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public int GetUserId()
        {
            int number = 0;
            var aspUserId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (aspUserId != null)
            {
                var user = userManager.Users
                        .Include(u => u.EmployeeUser)
                        .FirstOrDefaultAsync(u => u.Id == aspUserId).Result;

                number = user.EmployeeUser.Id;
            }

            return number;
        }
    }
}
