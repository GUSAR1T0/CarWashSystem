using FluentMigrator;

namespace VXDesign.Store.CarWashSystem.Server.Database.Migrations
{
    [Migration(4)]
    public class CarWashScheduleFeature : Migration
    {
        public override void Up()
        {
            var appointmentSchema = Schema.Schema(Common.Schema.Appointment);
            if (!appointmentSchema.Exists())
            {
                Execute.EmbeddedScript("CreateAppointmentSchema.sql");
            }

            if (!appointmentSchema.Table(Common.Table.AppointmentStatusEnum).Exists())
            {
                Execute.EmbeddedScript("CreateAppointmentStatusEnumTable.sql");
            }

            if (!appointmentSchema.Table(Common.Table.Appointment).Exists())
            {
                Execute.EmbeddedScript("CreateAppointmentTable.sql");
            }

            if (!appointmentSchema.Table(Common.Table.AppointmentCarWashService).Exists())
            {
                Execute.EmbeddedScript("CreateAppointmentCarWashServiceTable.sql");
            }

            if (!appointmentSchema.Table(Common.Table.AppointmentHistory).Exists())
            {
                Execute.EmbeddedScript("CreateAppointmentHistoryTable.sql");
            }
        }

        public override void Down()
        {
            var appointmentSchema = Schema.Schema(Common.Schema.Appointment);
            if (appointmentSchema.Exists())
            {
                if (appointmentSchema.Table(Common.Table.AppointmentHistory).Exists())
                {
                    Execute.EmbeddedScript("DropAppointmentHistoryTable.sql");
                }

                if (appointmentSchema.Table(Common.Table.AppointmentCarWashService).Exists())
                {
                    Execute.EmbeddedScript("DropAppointmentCarWashServiceTable.sql");
                }

                if (appointmentSchema.Table(Common.Table.Appointment).Exists())
                {
                    Execute.EmbeddedScript("DropAppointmentTable.sql");
                }

                if (appointmentSchema.Table(Common.Table.AppointmentStatusEnum).Exists())
                {
                    Execute.EmbeddedScript("DropAppointmentStatusEnumTable.sql");
                }

                Execute.EmbeddedScript("DropAppointmentSchema.sql");
            }
        }
    }
}