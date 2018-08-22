using DTOs.InputModels;
using DTOs.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IPositionService
    {
        IEnumerable<PositionViewModel> All();

        void Add(PositionInputModel model);

        void Delete(int id);
    }
}
