namespace VXDesign.Store.CarWashSystem.Server.Database
{
    public static class Common
    {
        public enum MigrationAction
        {
            Up,
            Down
        }

        public static class Schema
        {
            public const string Authentication = "authentication";
            public const string Client = "client";
            public const string Company = "company";
        }

        public static class Table
        {
            public const string InternalUser = "InternalUser";
            public const string ExternalUserAuthenticationSchemaEnum = "ExternalUserAuthenticationSchemaEnum";
            public const string ExternalUser = "ExternalUser";
            public const string User = "User";
            public const string Client = "Client";
            public const string Company = "Company";
        }
    }
}