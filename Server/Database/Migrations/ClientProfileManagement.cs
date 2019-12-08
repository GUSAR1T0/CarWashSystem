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
            if (!clientSchema.Table(Common.Table.BrandEnum).Exists())
            {
                Execute.EmbeddedScript("CreateBrandEnumTable.sql");
            }

            if (!clientSchema.Table(Common.Table.ModelEnum).Exists())
            {
                Execute.EmbeddedScript("CreateModelEnumTable.sql");
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

            if (clientSchema.Table(Common.Table.ModelEnum).Exists())
            {
                Execute.EmbeddedScript("DropModelEnumTable.sql");
            }

            if (clientSchema.Table(Common.Table.BrandEnum).Exists())
            {
                Execute.EmbeddedScript("DropBrandEnumTable.sql");
            }
        }
    }
}