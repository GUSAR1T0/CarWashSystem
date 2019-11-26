namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Common
{
    public static class ExceptionMessage
    {
        public const string DatabaseConnectionIsMissed = "Database connection string is not set";
        
        #region Authentication

        public const string UserHasAlreadyAuthenticated = "Authentication process failed: you have already authenticated";
        public const string CompanySignInFailedDueToInvalidModel = "Authentication process failed: you couldn't be signed in as company representative due to invalid model data";
        public const string CompanySignUpFailedDueToInvalidModel = "Authentication process failed: you couldn't be signed up as company representative due to invalid model data";
        public const string ClientSignInFailedDueToInvalidModel = "Authentication process failed: you couldn't be signed in as client due to invalid model data";
        public const string ClientSignUpFailedDueToInvalidModel = "Authentication process failed: you couldn't be signed up as client due to invalid model data";
        public const string FailedToIdentifyUserId = "Failed to identify user ID";
        public const string CouldNotGetAccountProfile = "Authentication process failed: couldn't get account profile";

        #endregion
    }
}