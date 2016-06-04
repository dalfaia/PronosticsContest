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
            _deleteTables();
			if (_pronosContestContextDatabase.Competitions.Count() == 0)
			{
                var user = _pronosContestContextDatabase.CompteUtilisateurs.FirstOrDefault();
                if (user != null)
                {
                    var compet = _pronosContestContextDatabase.Competitions.Add(new Competition()
                    {
                        Libelle = "Euro 2016",
                        DateDebut = new DateTime(2016, 6, 10),
                        DateFin = new DateTime(2016, 7, 10),
                        TypeSport = TypeDeSport.Football
                    });

                    #region Groupe A
                    var equipeFrance = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "France", Logo = "http://flags.fmcdn.net/data/flags/w580/fr.png" });
                    var equipeRoumanie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Roumanie", Logo = "http://flags.fmcdn.net/data/flags/w580/ro.png" });
                    var equipeAlbanie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Albanie", Logo = "http://flags.fmcdn.net/data/flags/w580/al.png" });
                    var equipeSuisse = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Suisse", Logo = "http://flags.fmcdn.net/data/flags/w580/sw.png" });
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
                    groupeA.Matchs.Add(new Match() { EquipeAID = equipeRoumanie.ID, EquipeBID = equipeSuisse.ID, Date = new DateTime(2016, 6, 15, 18, 00, 00), Stade = "Parc des Princes, Paris" });
                    groupeA.Matchs.Add(new Match() { EquipeAID = equipeFrance.ID, EquipeBID = equipeAlbanie.ID, Date = new DateTime(2016, 6, 15, 21, 00, 00), Stade = "Stade Vélodrome, Marseille" });
                    groupeA.Matchs.Add(new Match() { EquipeAID = equipeRoumanie.ID, EquipeBID = equipeAlbanie.ID, Date = new DateTime(2016, 6, 19, 21, 00, 00), Stade = "Parc Olympique Lyonnais, Lyon" });
                    groupeA.Matchs.Add(new Match() { EquipeAID = equipeSuisse.ID, EquipeBID = equipeFrance.ID, Date = new DateTime(2016, 6, 19, 21, 00, 00), Stade = "Stade Pierre-Mauroy, Lille" });
                    #endregion

                    #region Groupe B
                    var equipeGalles = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Pays de Galles", Logo = "http://flags.fmcdn.net/data/flags/w580/wa.png" });
                    var equipeSlovaquie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Slovaquie", Logo = "http://flags.fmcdn.net/data/flags/w580/sk.png" });
                    var equipeAngleterre = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Angleterre", Logo = "http://flags.fmcdn.net/data/flags/w580/en.png" });
                    var equipeRussie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Russie", Logo = "http://flags.fmcdn.net/data/flags/w580/ru.png" });
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
                    groupeB.Matchs.Add(new Match() { EquipeA = equipeRussie, EquipeB = equipeSlovaquie, Date = new DateTime(2016, 6, 15, 15, 00, 00), Stade = "Stade Pierre-Mauroy, Lille" });
                    groupeB.Matchs.Add(new Match() { EquipeA = equipeAngleterre, EquipeB = equipeGalles, Date = new DateTime(2016, 6, 16, 15, 00, 00), Stade = "Stade Bollaert-Delelis, Lens" });
                    groupeB.Matchs.Add(new Match() { EquipeA = equipeRussie, EquipeB = equipeGalles, Date = new DateTime(2016, 6, 20, 21, 00, 00), Stade = "Stadium Municipal, Toulouse" });
                    groupeB.Matchs.Add(new Match() { EquipeA = equipeSlovaquie, EquipeB = equipeAngleterre, Date = new DateTime(2016, 6, 20, 21, 00, 00), Stade = "Stade Geoffroy-Guichard, Saint-Etienne" });
                    #endregion

                    #region Groupe C
                    var equipeAllemagne = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Allemagne", Logo = "http://flags.fmcdn.net/data/flags/w580/ge.png" });
                    var equipeIrlandeDuNord = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Irlande du Nord", Logo = "http://www.drapeaux-nationaux.fr/media/flags/flagge-nordirland.gif" });
                    var equipeUkraine = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Ukraine", Logo = "http://flags.fmcdn.net/data/flags/w580/ua.png" });
                    var equipePologne = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Pologne", Logo = "http://flags.fmcdn.net/data/flags/w580/pl.png" });
                    var groupeC = _pronosContestContextDatabase.Groupes.Add(new PhaseGroupe()
                    {
                        Lettre = "C",
                        Equipes = new List<Equipe>()
                    {
                        equipeAllemagne,
                        equipeIrlandeDuNord,
                        equipeUkraine,
                        equipePologne
                    }
                    });
                    groupeC.Matchs.Add(new Match() { EquipeA = equipePologne, EquipeB = equipeIrlandeDuNord, Date = new DateTime(2016, 6, 12, 18, 00, 00), Stade = "Allianz Arena, Nice" });
                    groupeC.Matchs.Add(new Match() { EquipeA = equipeAllemagne, EquipeB = equipeUkraine, Date = new DateTime(2016, 6, 12, 21, 00, 00), Stade = "Stade Pierre-Mauroy, Lille" });
                    groupeC.Matchs.Add(new Match() { EquipeA = equipeUkraine, EquipeB = equipeIrlandeDuNord, Date = new DateTime(2016, 6, 16, 18, 00, 00), Stade = "Parc Olympique Lyonnais, Lyon" });
                    groupeC.Matchs.Add(new Match() { EquipeA = equipeAllemagne, EquipeB = equipePologne, Date = new DateTime(2016, 6, 16, 21, 00, 00), Stade = "Stade de France, Saint Denis" });
                    groupeC.Matchs.Add(new Match() { EquipeA = equipeUkraine, EquipeB = equipePologne, Date = new DateTime(2016, 6, 21, 18, 00, 00), Stade = "Stade Vélodrome, Marseille" });
                    groupeC.Matchs.Add(new Match() { EquipeA = equipeIrlandeDuNord, EquipeB = equipeAllemagne, Date = new DateTime(2016, 6, 21, 18, 00, 00), Stade = "Parc des Princes, Paris" });
                    #endregion

                    #region Groupe D
                    var equipeTurquie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Turquie", Logo = "http://flags.fmcdn.net/data/flags/w580/tr.png" });
                    var equipeEspagne = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Espagne", Logo = "http://flags.fmcdn.net/data/flags/w580/sp.png" });
                    var equipeCroatie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Croatie", Logo = "http://flags.fmcdn.net/data/flags/w580/hr.png" });
                    var equipeRepTcheque = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "République de Tchèque", Logo = "http://flags.fmcdn.net/data/flags/w580/cz.png" });
                    var groupeD = _pronosContestContextDatabase.Groupes.Add(new PhaseGroupe()
                    {
                        Lettre = "D",
                        Equipes = new List<Equipe>()
                    {
                        equipeTurquie,
                        equipeEspagne,
                        equipeCroatie,
                        equipeRepTcheque
                    }
                    });
                    groupeD.Matchs.Add(new Match() { EquipeA = equipeTurquie, EquipeB = equipeCroatie, Date = new DateTime(2016, 6, 12, 15, 00, 00), Stade = "Parc des Princes, Paris" });
                    groupeD.Matchs.Add(new Match() { EquipeA = equipeEspagne, EquipeB = equipeRepTcheque, Date = new DateTime(2016, 6, 13, 15, 00, 00), Stade = "Stadium Municipal, Toulouse" });
                    groupeD.Matchs.Add(new Match() { EquipeA = equipeRepTcheque, EquipeB = equipeCroatie, Date = new DateTime(2016, 6, 17, 18, 00, 00), Stade = "Stade Geoffroy-Guichard, Saint-Etienne" });
                    groupeD.Matchs.Add(new Match() { EquipeA = equipeEspagne, EquipeB = equipeTurquie, Date = new DateTime(2016, 6, 17, 21, 00, 00), Stade = "Allianz Arena, Nice" });
                    groupeD.Matchs.Add(new Match() { EquipeA = equipeRepTcheque, EquipeB = equipeTurquie, Date = new DateTime(2016, 6, 21, 21, 00, 00), Stade = "Stade Bollaert-Delelis, Lens" });
                    groupeD.Matchs.Add(new Match() { EquipeA = equipeCroatie, EquipeB = equipeEspagne, Date = new DateTime(2016, 6, 21, 21, 00, 00), Stade = "Matmut Atlantique, Bordeaux" });
                    #endregion

                    #region Groupe E
                    var equipeItalie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Italie", Logo = "http://flags.fmcdn.net/data/flags/w580/it.png" });
                    var equipeSuede = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Suède", Logo = "http://flags.fmcdn.net/data/flags/w580/se.png" });
                    var equipeIrlande = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Irlande", Logo = "http://flags.fmcdn.net/data/flags/w580/ie.png" });
                    var equipeBelgique = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Belgique", Logo = "http://flags.fmcdn.net/data/flags/w580/be.png" });
                    var groupeE = _pronosContestContextDatabase.Groupes.Add(new PhaseGroupe()
                    {
                        Lettre = "E",
                        Equipes = new List<Equipe>()
                    {
                        equipeItalie,
                        equipeSuede,
                        equipeIrlande,
                        equipeBelgique
                    }
                    });
                    groupeE.Matchs.Add(new Match() { EquipeA = equipeIrlande, EquipeB = equipeSuede, Date = new DateTime(2016, 6, 13, 18, 00, 00), Stade = "Stade de France, Saint Denis" });
                    groupeE.Matchs.Add(new Match() { EquipeA = equipeBelgique, EquipeB = equipeItalie, Date = new DateTime(2016, 6, 13, 21, 00, 00), Stade = "Parc Olympique Lyonnais, Lyon" });
                    groupeE.Matchs.Add(new Match() { EquipeA = equipeItalie, EquipeB = equipeSuede, Date = new DateTime(2016, 6, 17, 15, 00, 00), Stade = "Stadium Municipal, Toulouse" });
                    groupeE.Matchs.Add(new Match() { EquipeA = equipeBelgique, EquipeB = equipeIrlande, Date = new DateTime(2016, 6, 18, 15, 00, 00), Stade = "Matmut Atlantique, Bordeaux" });
                    groupeE.Matchs.Add(new Match() { EquipeA = equipeItalie, EquipeB = equipeIrlande, Date = new DateTime(2016, 6, 22, 21, 00, 00), Stade = "Stade Pierre-Mauroy, Lille" });
                    groupeE.Matchs.Add(new Match() { EquipeA = equipeSuede, EquipeB = equipeBelgique, Date = new DateTime(2016, 6, 22, 21, 00, 00), Stade = "Allianz Arena, Nice" });
                    #endregion

                    #region Groupe F
                    var equipeIslande = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Islande", Logo = "http://flags.fmcdn.net/data/flags/w580/is.png" });
                    var equipeAutriche = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Autriche", Logo = "http://flags.fmcdn.net/data/flags/w580/at.png" });
                    var equipePortugal = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Portugal", Logo = "http://flags.fmcdn.net/data/flags/w580/pt.png" });
                    var equipeHongrie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Hongrie", Logo = "http://flags.fmcdn.net/data/flags/w580/hu.png" });
                    var groupeF = _pronosContestContextDatabase.Groupes.Add(new PhaseGroupe()
                    {
                        Lettre = "F",
                        Equipes = new List<Equipe>()
                    {
                        equipeIslande,
                        equipeAutriche,
                        equipePortugal,
                        equipeHongrie
                    }
                    });
                    groupeF.Matchs.Add(new Match() { EquipeA = equipeAutriche, EquipeB = equipeHongrie, Date = new DateTime(2016, 6, 14, 18, 00, 00), Stade = "Matmut Atlantique, Bordeaux" });
                    groupeF.Matchs.Add(new Match() { EquipeA = equipePortugal, EquipeB = equipeIslande, Date = new DateTime(2016, 6, 14, 21, 00, 00), Stade = "Stade Geoffroy-Guichard, Saint-Etienne" });
                    groupeF.Matchs.Add(new Match() { EquipeA = equipeIslande, EquipeB = equipeHongrie, Date = new DateTime(2016, 6, 18, 18, 00, 00), Stade = "Stade Vélodrome, Marseille" });
                    groupeF.Matchs.Add(new Match() { EquipeA = equipePortugal, EquipeB = equipeAutriche, Date = new DateTime(2016, 6, 18, 21, 00, 00), Stade = "Parc des Princes, Paris" });
                    groupeF.Matchs.Add(new Match() { EquipeA = equipeIslande, EquipeB = equipeAutriche, Date = new DateTime(2016, 6, 22, 18, 00, 00), Stade = "Stade de France, Saint Denis" });
                    groupeF.Matchs.Add(new Match() { EquipeA = equipeHongrie, EquipeB = equipePortugal, Date = new DateTime(2016, 6, 22, 18, 00, 00), Stade = "Parc Olympique Lyonnais, Lyon" });
                    #endregion

                    compet.Groupes.Add(groupeA);
                    compet.Groupes.Add(groupeB);
                    compet.Groupes.Add(groupeC);
                    compet.Groupes.Add(groupeD);
                    compet.Groupes.Add(groupeE);
                    compet.Groupes.Add(groupeF);

                    _pronosContestContextDatabase.SaveChanges();

                    var concours = _pronosContestContextDatabase.Concours.Add(new Concours()
                    {
                        Competition = compet,
                        CompteUtilisateurID = user.ID,
                        DateDebut = DateTime.Now.Date,
                        EtatConcours = EtatConcours.EnCours
                    });
                    
                    _pronosContestContextDatabase.SaveChanges();
                }
            }
		}

		private void _deleteTables()
        {
            _pronosContestContextDatabase.Matchs.RemoveRange(_pronosContestContextDatabase.Matchs);
            _pronosContestContextDatabase.Equipes.RemoveRange(_pronosContestContextDatabase.Equipes);
            _pronosContestContextDatabase.PhasesFinales.RemoveRange(_pronosContestContextDatabase.PhasesFinales);
            _pronosContestContextDatabase.Groupes.RemoveRange(_pronosContestContextDatabase.Groupes);
            _pronosContestContextDatabase.Competitions.RemoveRange(_pronosContestContextDatabase.Competitions);
            _pronosContestContextDatabase.ConcoursCompteUtilisateurs.RemoveRange(_pronosContestContextDatabase.ConcoursCompteUtilisateurs);
            _pronosContestContextDatabase.Pronostics.RemoveRange(_pronosContestContextDatabase.Pronostics);
            _pronosContestContextDatabase.Concours.RemoveRange(_pronosContestContextDatabase.Concours);

            _pronosContestContextDatabase.SaveChanges();
        }
	}
}
