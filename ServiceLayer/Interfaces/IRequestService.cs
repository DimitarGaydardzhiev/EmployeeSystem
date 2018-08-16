using DTOs.ViewModels;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IRequestService
    {
        IEnumerable<BaseViewModel> GetRequestTypes();

        void Add(RequestViewModel model);

        IEnumerable<RequestViewModel> GetMyRequests();
    }
}
