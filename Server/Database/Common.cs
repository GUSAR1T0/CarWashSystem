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
        }

        public static class Table
        {
            public const string User = "User";
            public const string Company = "Company";
        }
    }
}