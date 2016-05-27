using PronosContest.DAL;
using PronosContest.DAL.Authentification;
using PronosContest.DAL.Pronos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PronosContest.BLL
{
	public class StartService
	{
		private PronosContestContext _pronosContestContextDatabase;

		internal StartService(PronosContestContext pPronosContestContextDatabase)
		{
			_pronosContestContextDatabase = pPronosContestContextDatabase;
		}
		public void StartDatabase()
		{
			_pronosContestContextDatabase.Competitions.RemoveRange(_pronosContestContextDatabase.Competitions);
			if (_pronosContestContextDatabase.Competitions.Count() == 0)
			{
				var compet = _pronosContestContextDatabase.Competitions.Add(new Competition()
				{
					Libelle = "Euro 2016",
					DateDebut = new DateTime(2016, 6, 10),
					DateFin = new DateTime(2016, 7, 10),
					TypeSport = TypeDeSport.Football
				});

				var equipeFrance = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "France" });
				var equipeRoumanie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Roumanie" });
				var equipeAlbanie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Albanie" });
				var equipeSuisse = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Suisse" });
				var equipeGalles = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Pays de Galles" });
				var equipeSlovaquie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Slovaquie" });
				var equipeAngleterre = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Angleterre" });
				var equipeRussie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Russie" });

				var groupeA = _pronosContestContextDatabase.Groupes.Add(new PhaseGroupe()
				{
					Lettre = "A",
					Equipes = new List<Equipe>()
					{
						equipeFrance,
						equipeRoumanie,
						equipeAlbanie,
						equipeAlbanie
					}
				});

				_pronosContestContextDatabase.SaveChanges();

				groupeA.Matchs.Add(new Match() { EquipeAID = equipeFrance.ID, EquipeBID = equipeRoumanie.ID, Date = new DateTime(2016, 6, 10, 21, 00, 00), Stade = "Stade de France, Saint-Denis" });
				groupeA.Matchs.Add(new Match() { EquipeAID = equipeAlbanie.ID, EquipeBID = equipeSuisse.ID, Date = new DateTime(2016, 6, 11, 15, 00, 00), Stade = "Stade Bollaert-Delelis, Lens" });

				var groupeB = _pronosContestContextDatabase.Groupes.Add(new PhaseGroupe()
				{
					Lettre = "B",
					Equipes = new List<Equipe>()
					{
						equipeGalles,
						equipeSlovaquie,
						equipeAngleterre,
						equipeRussie
					}
				});
				groupeB.Matchs.Add(new Match() { EquipeA = equipeGalles, EquipeB = equipeSlovaquie, Date = new DateTime(2016, 6, 11, 18, 00, 00), Stade = "Stade de Bordeaux, Bordeaux" });
				groupeB.Matchs.Add(new Match() { EquipeA = equipeAngleterre, EquipeB = equipeRussie, Date = new DateTime(2016, 6, 11, 21, 00, 00), Stade = "Stade Vélodrome, Marseille" });

				compet.Groupes.Add(groupeA);
				compet.Groupes.Add(groupeB);

				_pronosContestContextDatabase.SaveChanges();

				_pronosContestContextDatabase.Concours.Add(new Concours()
				{
					Competition = compet,
					CompteUtilisateurID = 1,
					DateDebut = DateTime.Now.Date,
					EtatConcours = EtatConcours.EnCours
				});

				_pronosContestContextDatabase.SaveChanges();
            }
		}
		
	}
}
