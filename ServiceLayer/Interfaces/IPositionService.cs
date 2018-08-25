using DTOs.ViewModels;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IPositionService
    {
        IEnumerable<PositionViewModel> All();

        void Save(PositionViewModel model);

        void Delete(int id);
    }
}
