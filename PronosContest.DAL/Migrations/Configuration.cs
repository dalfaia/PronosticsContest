namespace PronosContest.DAL.Migrations
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using Pronos;
	using System.Collections.Generic;

	internal sealed class Configuration : DbMigrationsConfiguration<PronosContest.DAL.PronosContestContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "PronosContest.DAL.PronosContestContext";
        }

        protected override void Seed(PronosContestContext context)
        {
			
        }
    }
}
