using DTOs.Models;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IPositionService
    {
        IEnumerable<PositionDto> All();

        void Save(PositionDto model);

        void Delete(int id);
    }
}
