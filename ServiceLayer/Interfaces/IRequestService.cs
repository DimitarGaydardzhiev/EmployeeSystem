using DTOs.Models;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IRequestService
    {
        IEnumerable<BaseDto> GetRequestTypes();

        IEnumerable<RequestDto> GetMyRequests();

        IEnumerable<RequestDto> GetPendingRequests();

        IEnumerable<RequestDto> GetApprovedRequests();

        void Save(RequestDto model);

        void ApproveRequest(int requestId);

        void UnapproveRequest(int requestId);

        void Delete(int id);

        bool CanEdit(int id);
    }
}
