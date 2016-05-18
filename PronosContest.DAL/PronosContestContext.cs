using PronosContest.DAL.Authentification;
using PronosContest.DAL.Pronos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PronosContest.DAL
{
    public class PronosContestContext : DbContext
    {
		public PronosContestContext():base("name=PronosContestConnectionString")
        {
			//Database.SetInitializer(new MigrateDatabaseToLatestVersion<PronosContestContext, ENTRE_AMIS.DAL.Migrations.Configuration>());
			return;
		}
		public DbSet<CompteUtilisateur> CompteUtilisateurs { get; set; }
		public DbSet<Competition> Competitions { get; set; }
		public DbSet<PhaseGroupe> Groupes { get; set; }
		public DbSet<PhaseFinale> PhasesFinales { get; set; }
		public DbSet<Equipe> Equipes { get; set; }
		public DbSet<Match> Matchs { get; set; }
		public DbSet<Concours> Concours { get; set; }
		public DbSet<Pronostic> Pronostics { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
		}
	}
}
