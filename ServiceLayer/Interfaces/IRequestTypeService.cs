using DTOs.ViewModels;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IRequestTypeService
    {
        IEnumerable<BaseViewModel> All();
    }
}
