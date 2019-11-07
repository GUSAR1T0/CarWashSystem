using FluentMigrator;

namespace VXDesign.Store.CarWashSystem.Server.Database.Migrations
{
    [Migration(1)]
    public class UserAuthenticationFeature : Migration
    {
        public override void Up()
        {
            var authorizationSchema = Schema.Schema(Common.Schema.Authentication);
            if (!authorizationSchema.Exists())
            {
                Execute.EmbeddedScript("CreateAuthenticationSchema.sql");
            }

            if (!authorizationSchema.Table(Common.Table.User).Exists())
            {
                Execute.EmbeddedScript("CreateUserTable.sql");
            }

            if (!authorizationSchema.Table(Common.Table.Company).Exists())
            {
                Execute.EmbeddedScript("CreateCompanyTable.sql");
            }
        }

        public override void Down()
        {
            var authorizationSchema = Schema.Schema(Common.Schema.Authentication);
            if (authorizationSchema.Exists())
            {
                if (authorizationSchema.Table(Common.Table.User).Exists())
                {
                    Execute.EmbeddedScript("DropUserTable.sql");
                }

                if (authorizationSchema.Table(Common.Table.Company).Exists())
                {
                    Execute.EmbeddedScript("DropCompanyTable.sql");
                }

                Execute.EmbeddedScript("DropAuthenticationSchema.sql");
            }
        }
    }
}