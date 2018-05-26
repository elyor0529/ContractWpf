using System.Data.Entity;
using Contract.Data.Migrations;

namespace Contract.Data
{
    internal class MainInitializer : MigrateDatabaseToLatestVersion<MainContext, MainConfiguration>
    {
        public override void InitializeDatabase(MainContext context)
        {
        }
    }
}