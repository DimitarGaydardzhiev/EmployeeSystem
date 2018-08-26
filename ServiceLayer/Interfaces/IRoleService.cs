using DTOs.Models;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<RoleDto> All();
    }
}
