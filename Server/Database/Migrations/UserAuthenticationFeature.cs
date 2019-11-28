using FluentMigrator;

namespace VXDesign.Store.CarWashSystem.Server.Database.Migrations
{
    [Migration(1)]
    public class UserAuthenticationFeature : Migration
    {
        public override void Up()
        {
            PrepareAuthenticationSchema();
            PrepareClientSchema();
            PrepareCompanySchema();
        }

        private void PrepareAuthenticationSchema()
        {
            var authorizationSchema = Schema.Schema(Common.Schema.Authentication);
            if (!authorizationSchema.Exists())
            {
                Execute.EmbeddedScript("CreateAuthenticationSchema.sql");
            }

            if (!authorizationSchema.Table(Common.Table.InternalUser).Exists())
            {
                Execute.EmbeddedScript("CreateInternalUserTable.sql");
            }

            if (!authorizationSchema.Table(Common.Table.ExternalUserAuthenticationSchemaEnum).Exists())
            {
                Execute.EmbeddedScript("CreateExternalUserAuthenticationSchemaEnumTable.sql");
            }

            if (!authorizationSchema.Table(Common.Table.ExternalUser).Exists())
            {
                Execute.EmbeddedScript("CreateExternalUserTable.sql");
            }

            if (!authorizationSchema.Table(Common.Table.User).Exists())
            {
                Execute.EmbeddedScript("CreateUserTable.sql");
            }
        }

        private void PrepareClientSchema()
        {
            var clientSchema = Schema.Schema(Common.Schema.Client);
            if (!clientSchema.Exists())
            {
                Execute.EmbeddedScript("CreateClientSchema.sql");
            }

            if (!clientSchema.Table(Common.Table.Client).Exists())
            {
                Execute.EmbeddedScript("CreateClientTable.sql");
            }
        }

        private void PrepareCompanySchema()
        {
            var companySchema = Schema.Schema(Common.Schema.Company);
            if (!companySchema.Exists())
            {
                Execute.EmbeddedScript("CreateCompanySchema.sql");
            }

            if (!companySchema.Table(Common.Table.Company).Exists())
            {
                Execute.EmbeddedScript("CreateCompanyTable.sql");
            }
        }

        public override void Down()
        {
            DropClientSchema();
            DropCompanySchema();
            DropAuthenticationSchema();
        }

        private void DropClientSchema()
        {
            var clientSchema = Schema.Schema(Common.Schema.Client);
            if (clientSchema.Exists())
            {
                if (clientSchema.Table(Common.Table.Client).Exists())
                {
                    Execute.EmbeddedScript("DropClientTable.sql");
                }

                Execute.EmbeddedScript("DropClientSchema.sql");
            }
        }

        private void DropCompanySchema()
        {
            var companySchema = Schema.Schema(Common.Schema.Company);
            if (companySchema.Exists())
            {
                if (companySchema.Table(Common.Table.Company).Exists())
                {
                    Execute.EmbeddedScript("DropCompanyTable.sql");
                }

                Execute.EmbeddedScript("DropCompanySchema.sql");
            }
        }

        private void DropAuthenticationSchema()
        {
            var authorizationSchema = Schema.Schema(Common.Schema.Authentication);
            if (authorizationSchema.Exists())
            {
                if (authorizationSchema.Table(Common.Table.User).Exists())
                {
                    Execute.EmbeddedScript("DropUserTable.sql");
                }

                if (authorizationSchema.Table(Common.Table.ExternalUser).Exists())
                {
                    Execute.EmbeddedScript("DropExternalUserTable.sql");
                }

                if (authorizationSchema.Table(Common.Table.ExternalUserAuthenticationSchemaEnum).Exists())
                {
                    Execute.EmbeddedScript("DropExternalUserAuthenticationSchemaEnumTable.sql");
                }

                if (authorizationSchema.Table(Common.Table.InternalUser).Exists())
                {
                    Execute.EmbeddedScript("DropInternalUserTable.sql");
                }

                Execute.EmbeddedScript("DropAuthenticationSchema.sql");
            }
        }
    }
}