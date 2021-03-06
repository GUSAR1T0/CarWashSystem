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
            public const string Appointment = "appointment";
        }

        public static class Table
        {
            public const string InternalUser = "InternalUser";
            public const string ExternalUserAuthenticationSchemaEnum = "ExternalUserAuthenticationSchemaEnum";
            public const string ExternalUser = "ExternalUser";
            public const string User = "User";
            public const string Client = "Client";
            public const string Company = "Company";
            public const string CarWash = "CarWash";
            public const string CarWashWorkingHours = "CarWashWorkingHours";
            public const string CarWashService = "CarWashService";
            public const string CarBrandEnum = "CarBrandEnum";
            public const string CarBrandModelEnum = "CarBrandModelEnum";
            public const string Car = "Car";
            public const string AppointmentStatusEnum = "AppointmentStatusEnum";
            public const string Appointment = "Appointment";
            public const string AppointmentCarWashService = "AppointmentCarWashService";
            public const string AppointmentHistory = "AppointmentHistory";
        }
    }
}