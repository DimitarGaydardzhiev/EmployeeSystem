using DTOs.Enums;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace EmployeeSystem.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly IToastNotification toastNotification;

        public BaseController(IToastNotification toastNotification)
        {
            this.toastNotification = toastNotification;
        }

        protected void ShowNotification(string message, ToastrSeverity severity)
        {
            switch (severity)
            {
                case ToastrSeverity.Success:
                    toastNotification.AddSuccessToastMessage(message);
                    break;
                case ToastrSeverity.Info:
                    toastNotification.AddInfoToastMessage(message);
                    break;
                case ToastrSeverity.Warning:
                    toastNotification.AddWarningToastMessage(message);
                    break;
                case ToastrSeverity.Error:
                    toastNotification.AddErrorToastMessage(message);
                    break;
                case ToastrSeverity.Alert:
                    toastNotification.AddAlertToastMessage(message);
                    break;
            }
        }
    }
}
