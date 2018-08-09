using DTOs.ViewModels;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<RoleViewModel> All();
    }
}
