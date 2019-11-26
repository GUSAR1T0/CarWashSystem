using System;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace VXDesign.Store.CarWashSystem.Server.Database
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = CreateServices();
            using var scope = serviceProvider.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

            if (args.Length >= 1)
            {
                var manipulation = (Common.MigrationAction) Enum.Parse(typeof(Common.MigrationAction), args[0], true);
                var version = args.Length > 1 && long.TryParse(args[1], out var value) ? value : (long?) null;

                switch (manipulation)
                {
                    case Common.MigrationAction.Down:
                        DowngradeDatabase(runner, version);
                        break;
                    case Common.MigrationAction.Up:
                        UpgradeDatabase(runner, version);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                UpgradeDatabase(runner);
            }
        }

        private static IServiceProvider CreateServices() => new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSqlServer()
                .WithGlobalConnectionString("Data Source=localhost,1433;User ID=sa;Password=<2019!Pass>;Database=master")
                .ScanIn(typeof(Program).Assembly).For.EmbeddedResources().For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);

        private static void UpgradeDatabase(IMigrationRunner runner, long? version = null)
        {
            switch (version)
            {
                case null:
                    runner.MigrateUp();
                    break;
                default:
                    runner.MigrateUp(version.Value);
                    break;
            }
        }

        private static void DowngradeDatabase(IMigrationRunner runner, long? version = null) => runner.MigrateDown(version ?? 0);
    }
}