namespace PriceCompareModel.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PriceCompareModel.PriceCompareContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PriceCompareModel.PriceCompareContext context)
        {
            DbManager db = new DbManager();
           db.PopulateDB();

        }
    }
}
