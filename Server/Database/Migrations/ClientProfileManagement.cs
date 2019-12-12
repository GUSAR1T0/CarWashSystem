using FluentMigrator;

namespace VXDesign.Store.CarWashSystem.Server.Database.Migrations
{
    [Migration(2)]
    public class ClientProfileManagement : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("ClientProfileManagement.AlterClientTableAddColumns.sql");

            var clientSchema = Schema.Schema(Common.Schema.Client);
            if (!clientSchema.Table(Common.Table.CarBrandEnum).Exists())
            {
                Execute.EmbeddedScript("CreateCarBrandEnumTable.sql");
            }

            if (!clientSchema.Table(Common.Table.CarBrandModelEnum).Exists())
            {
                Execute.EmbeddedScript("CreateCarBrandModelEnumTable.sql");
            }

            if (!clientSchema.Table(Common.Table.Car).Exists())
            {
                Execute.EmbeddedScript("CreateCarTable.sql");
            }
        }

        public override void Down()
        {
            Execute.EmbeddedScript("ClientProfileManagement.AlterClientTableDropColumns.sql");

            var clientSchema = Schema.Schema(Common.Schema.Client);
            if (clientSchema.Table(Common.Table.Car).Exists())
            {
                Execute.EmbeddedScript("DropCarTable.sql");
            }

            if (clientSchema.Table(Common.Table.CarBrandModelEnum).Exists())
            {
                Execute.EmbeddedScript("DropCarBrandModelEnumTable.sql");
            }

            if (clientSchema.Table(Common.Table.CarBrandEnum).Exists())
            {
                Execute.EmbeddedScript("DropCarBrandEnumTable.sql");
            }
        }
    }
}