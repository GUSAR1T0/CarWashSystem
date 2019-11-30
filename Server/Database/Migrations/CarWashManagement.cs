using FluentMigrator;

namespace VXDesign.Store.CarWashSystem.Server.Database.Migrations
{
    [Migration(3)]
    public class CarWashManagement : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("CarWashManagement.AlterCompanyTableAddColumn.sql");

            var companySchema = Schema.Schema(Common.Schema.Company);
            if (!companySchema.Table(Common.Table.CarWash).Exists())
            {
                Execute.EmbeddedScript("CreateCarWashTable.sql");
            }
        }

        public override void Down()
        {
            Execute.EmbeddedScript("CarWashManagement.AlterCompanyTableDropColumn.sql");

            var companySchema = Schema.Schema(Common.Schema.Company);
            if (companySchema.Table(Common.Table.CarWash).Exists())
            {
                Execute.EmbeddedScript("DropCarWashTable.sql");
            }
        }
    }
}