namespace VXDesign.Store.CarWashSystem.Server.Core.Common
{
    public static class ExceptionMessage
    {
        public const string DatabaseConnectionIsMissed = "Database connection string is not set";
        public const string ModelIsInvalid = "Input data is invalid";

        #region Authentication

        public const string UserHasAlreadyAuthenticated = "Authentication process failed: you have already authenticated";
        public const string EmailHasAlreadyExist = "Authentication process failed: such email is already used by the system";
        public const string CompanySignInFailedDueToInvalidModel = "Authentication process failed: you couldn't be signed in as company representative due to invalid model data";
        public const string CompanySignInFailedDueToInactiveUserOrUnsuitableData = "Authentication process failed: you couldn't be signed in as company representative due to inactive user is or data is not suitable";
        public const string CompanySignUpFailedDueToInvalidModel = "Authentication process failed: you couldn't be signed up as company representative due to invalid model data";
        public const string CompanySignUpFailedDueToInactiveUserOrUnsuitableData = "Authentication process failed: you couldn't be signed up as company representative due to inactive user is or data is not suitable";
        public const string ClientSignInFailedDueToInvalidModel = "Authentication process failed: you couldn't be signed in as client due to invalid model data";
        public const string ClientSignInFailedDueToInactiveUserOrUnsuitableData = "Authentication process failed: you couldn't be signed in as client due to inactive user is or data is not suitable";
        public const string ClientSignUpFailedDueToInvalidModel = "Authentication process failed: you couldn't be signed up as client due to invalid model data";
        public const string ClientSignUpFailedDueToInactiveUserOrUnsuitableData = "Authentication process failed: you couldn't be signed up as client due to inactive user is or data is not suitable";
        public const string ExternalClientSignUpFailedDueToInactiveUserOrUnsuitableData = "Authentication process failed: you couldn't be signed in as external client due to inactive user is or data is not suitable";
        public const string FailedToIdentifyUserId = "Failed to identify user ID";
        public const string CouldNotGetAccountProfile = "Authentication process failed: couldn't get account profile";

        public static string RoleIsNotSuitable(string userRole) => userRole switch
        {
            UserRole.Company => "Couldn't get company profile because you didn't authenticated as company representative",
            UserRole.Client => "Couldn't get client profile because you didn't authenticated as client",
            _ => "Couldn't get profile because you didn't authenticated"
        };

        #endregion

        #region Company

        public const string CarWashIsNotExist = "Such car wash is not exist";

        #endregion
    }
}