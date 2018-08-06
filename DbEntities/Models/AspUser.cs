using DbEntities.Models;
using Microsoft.AspNetCore.Identity;

namespace EmployeeSystem.Models
{
    public class AspUser : IdentityUser
    {
        public EmployeeUser EmployeeUser { get; set; }
    }
}
