namespace ServiceLayer.ErrorUtils
{
    public static class ErrorMessages
    {
        public const string HasEmployeesMessage = "The record can not be deleted because it has already added employees";
        public const string ObjectAlreadyAddedMessage = "Object already exists in the database";
        public const string ConNotDeleteApprovedRequestMessage = "You can not delete approved request";
        public const string UnableToEditPositionWithEmployeesMessage = "You can not update position with employees";
        public const string ThereIsAlreadyRequestForTheseDatesMessage = "There is already a request for these dates";
        public const string CanNotEditAnotherUserRequest = "You can not edit another user requests";
        public const string UnauthorizedAccess = "Unauthorized Access";
    }
}
