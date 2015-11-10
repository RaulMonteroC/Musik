namespace Musik.Core.Migrations
{
    using Musik.Core.Data;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Musik.Core.Data.MusikContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Musik.Core.Data.MusikContext context)
        {
            var users = context.Users;
            users.AddOrUpdate(new User()
            {
                Username = "raulmonteroc",
                Password = "123456"
            });            
        }
    }
}
