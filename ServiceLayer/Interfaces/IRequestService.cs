using DTOs.ViewModels;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IRequestService
    {
        IEnumerable<BaseViewModel> GetRequestTypes();

        IEnumerable<RequestViewModel> GetMyRequests();

        IEnumerable<RequestViewModel> GetPendingRequests();

        IEnumerable<RequestViewModel> GetApprovedRequests();

        void Save(RequestViewModel model);

        void ApproveRequest(int requestId);

        void UnapproveRequest(int requestId);

        void Delete(int id);

        bool CanEdit(int id);
    }
}
