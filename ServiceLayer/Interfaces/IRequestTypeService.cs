using DTOs.Models;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IRequestTypeService
    {
        IEnumerable<BaseDto> All();
    }
}
