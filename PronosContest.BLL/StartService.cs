using PronosContest.DAL;
using PronosContest.DAL.Authentification;
using PronosContest.DAL.Pronos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

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
                        DateDebut = new DateTime(2016, 6, 10, 21, 00, 00),
                        DateFin = new DateTime(2016, 7, 10, 23, 59, 59),
                        TypeSport = TypeDeSport.Football
                    });

                    #region Groupe A
                    var equipeFrance = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "France", ShortName = "FRA", Logo = "http://flags.fmcdn.net/data/flags/w580/fr.png" });
                    var equipeRoumanie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Roumanie", ShortName = "ROU", Logo = "http://flags.fmcdn.net/data/flags/w580/ro.png" });
                    var equipeAlbanie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Albanie", ShortName= "ALB", Logo = "http://flags.fmcdn.net/data/flags/w580/al.png" });
                    var equipeSuisse = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Suisse", ShortName="SUI", Logo = "http://flags.fmcdn.net/data/flags/normal/ch.png" });
                    var groupeA = _pronosContestContextDatabase.Groupes.Add(new PhaseGroupe()
                    {
                        Lettre = "A",
                        Equipes = new List<Equipe>()
                    {
                        equipeFrance,
                        equipeRoumanie,
                        equipeAlbanie,
                        equipeSuisse
                    }
                    });

                    _pronosContestContextDatabase.SaveChanges();

                    groupeA.Matchs.Add(new Match() { NumeroMatch = 1, EquipeAID = equipeFrance.ID, EquipeBID = equipeRoumanie.ID, Date = new DateTime(2016, 6, 10, 21, 00, 00), Stade = "Stade de France, Saint-Denis" });
                    groupeA.Matchs.Add(new Match() { NumeroMatch = 2, EquipeAID = equipeAlbanie.ID, EquipeBID = equipeSuisse.ID, Date = new DateTime(2016, 6, 11, 15, 00, 00), Stade = "Stade Bollaert-Delelis, Lens" });
                    groupeA.Matchs.Add(new Match() { NumeroMatch = 3, EquipeAID = equipeRoumanie.ID, EquipeBID = equipeSuisse.ID, Date = new DateTime(2016, 6, 15, 18, 00, 00), Stade = "Parc des Princes, Paris" });
                    groupeA.Matchs.Add(new Match() { NumeroMatch = 4, EquipeAID = equipeFrance.ID, EquipeBID = equipeAlbanie.ID, Date = new DateTime(2016, 6, 15, 21, 00, 00), Stade = "Stade Vélodrome, Marseille" });
                    groupeA.Matchs.Add(new Match() { NumeroMatch = 5, EquipeAID = equipeRoumanie.ID, EquipeBID = equipeAlbanie.ID, Date = new DateTime(2016, 6, 19, 21, 00, 00), Stade = "Parc Olympique Lyonnais, Lyon" });
                    groupeA.Matchs.Add(new Match() { NumeroMatch = 6, EquipeAID = equipeSuisse.ID, EquipeBID = equipeFrance.ID, Date = new DateTime(2016, 6, 19, 21, 00, 00), Stade = "Stade Pierre-Mauroy, Lille" });
                    #endregion

                    #region Groupe B
                    var equipeGalles = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Pays de Galles", ShortName = "GAL", Logo = "http://vignette3.wikia.nocookie.net/tomodachi/images/9/95/Flag_of_Wales.png" });
                    var equipeSlovaquie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Slovaquie", ShortName = "SLO", Logo = "http://flags.fmcdn.net/data/flags/w580/sk.png" });
                    var equipeAngleterre = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Angleterre", ShortName = "ANG", Logo = "https://upload.wikimedia.org/wikipedia/commons/c/c2/Flag_of_England.PNG" });
                    var equipeRussie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Russie", ShortName = "RUS", Logo = "http://flags.fmcdn.net/data/flags/w580/ru.png" });
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
                    groupeB.Matchs.Add(new Match() { NumeroMatch = 7, EquipeA = equipeGalles, EquipeB = equipeSlovaquie, Date = new DateTime(2016, 6, 11, 18, 00, 00), Stade = "Stade de Bordeaux, Bordeaux" });
                    groupeB.Matchs.Add(new Match() { NumeroMatch = 8, EquipeA = equipeAngleterre, EquipeB = equipeRussie, Date = new DateTime(2016, 6, 11, 21, 00, 00), Stade = "Stade Vélodrome, Marseille" });
                    groupeB.Matchs.Add(new Match() { NumeroMatch = 9, EquipeA = equipeRussie, EquipeB = equipeSlovaquie, Date = new DateTime(2016, 6, 15, 15, 00, 00), Stade = "Stade Pierre-Mauroy, Lille" });
                    groupeB.Matchs.Add(new Match() { NumeroMatch = 10, EquipeA = equipeAngleterre, EquipeB = equipeGalles, Date = new DateTime(2016, 6, 16, 15, 00, 00), Stade = "Stade Bollaert-Delelis, Lens" });
                    groupeB.Matchs.Add(new Match() { NumeroMatch = 11, EquipeA = equipeRussie, EquipeB = equipeGalles, Date = new DateTime(2016, 6, 20, 21, 00, 00), Stade = "Stadium Municipal, Toulouse" });
                    groupeB.Matchs.Add(new Match() { NumeroMatch = 12, EquipeA = equipeSlovaquie, EquipeB = equipeAngleterre, Date = new DateTime(2016, 6, 20, 21, 00, 00), Stade = "Stade Geoffroy-Guichard, Saint-Etienne" });
                    #endregion

                    #region Groupe C
                    var equipeAllemagne = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Allemagne", ShortName = "ALL", Logo = "http://flags.fmcdn.net/data/flags/w580/de.png" });
                    var equipeIrlandeDuNord = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Irlande du Nord", ShortName = "IRN", Logo = "http://www.drapeaux-nationaux.fr/media/flags/flagge-nordirland.gif" });
                    var equipeUkraine = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Ukraine", ShortName = "UKR", Logo = "http://flags.fmcdn.net/data/flags/w580/ua.png" });
                    var equipePologne = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Pologne", ShortName = "POL", Logo = "http://flags.fmcdn.net/data/flags/w580/pl.png" });
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
                    groupeC.Matchs.Add(new Match() { NumeroMatch = 13, EquipeA = equipePologne, EquipeB = equipeIrlandeDuNord, Date = new DateTime(2016, 6, 12, 18, 00, 00), Stade = "Allianz Arena, Nice" });
                    groupeC.Matchs.Add(new Match() { NumeroMatch = 14, EquipeA = equipeAllemagne, EquipeB = equipeUkraine, Date = new DateTime(2016, 6, 12, 21, 00, 00), Stade = "Stade Pierre-Mauroy, Lille" });
                    groupeC.Matchs.Add(new Match() { NumeroMatch = 15, EquipeA = equipeUkraine, EquipeB = equipeIrlandeDuNord, Date = new DateTime(2016, 6, 16, 18, 00, 00), Stade = "Parc Olympique Lyonnais, Lyon" });
                    groupeC.Matchs.Add(new Match() { NumeroMatch = 16, EquipeA = equipeAllemagne, EquipeB = equipePologne, Date = new DateTime(2016, 6, 16, 21, 00, 00), Stade = "Stade de France, Saint Denis" });
                    groupeC.Matchs.Add(new Match() { NumeroMatch = 17, EquipeA = equipeUkraine, EquipeB = equipePologne, Date = new DateTime(2016, 6, 21, 18, 00, 00), Stade = "Stade Vélodrome, Marseille" });
                    groupeC.Matchs.Add(new Match() { NumeroMatch = 18, EquipeA = equipeIrlandeDuNord, EquipeB = equipeAllemagne, Date = new DateTime(2016, 6, 21, 18, 00, 00), Stade = "Parc des Princes, Paris" });
                    #endregion

                    #region Groupe D
                    var equipeTurquie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Turquie", ShortName = "TUR", Logo = "http://flags.fmcdn.net/data/flags/w580/tr.png" });
                    var equipeEspagne = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Espagne", ShortName = "ESP", Logo = "http://flags.fmcdn.net/data/flags/w580/es.png" });
                    var equipeCroatie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Croatie", ShortName = "CRO", Logo = "http://flags.fmcdn.net/data/flags/w580/hr.png" });
                    var equipeRepTcheque = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "République de Tchèque", ShortName = "RPT", Logo = "http://flags.fmcdn.net/data/flags/w580/cz.png" });
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
                    groupeD.Matchs.Add(new Match() { NumeroMatch = 19, EquipeA = equipeTurquie, EquipeB = equipeCroatie, Date = new DateTime(2016, 6, 12, 15, 00, 00), Stade = "Parc des Princes, Paris" });
                    groupeD.Matchs.Add(new Match() { NumeroMatch = 20, EquipeA = equipeEspagne, EquipeB = equipeRepTcheque, Date = new DateTime(2016, 6, 13, 15, 00, 00), Stade = "Stadium Municipal, Toulouse" });
                    groupeD.Matchs.Add(new Match() { NumeroMatch = 21, EquipeA = equipeRepTcheque, EquipeB = equipeCroatie, Date = new DateTime(2016, 6, 17, 18, 00, 00), Stade = "Stade Geoffroy-Guichard, Saint-Etienne" });
                    groupeD.Matchs.Add(new Match() { NumeroMatch = 22, EquipeA = equipeEspagne, EquipeB = equipeTurquie, Date = new DateTime(2016, 6, 17, 21, 00, 00), Stade = "Allianz Arena, Nice" });
                    groupeD.Matchs.Add(new Match() { NumeroMatch = 23, EquipeA = equipeRepTcheque, EquipeB = equipeTurquie, Date = new DateTime(2016, 6, 21, 21, 00, 00), Stade = "Stade Bollaert-Delelis, Lens" });
                    groupeD.Matchs.Add(new Match() { NumeroMatch = 24, EquipeA = equipeCroatie, EquipeB = equipeEspagne, Date = new DateTime(2016, 6, 21, 21, 00, 00), Stade = "Matmut Atlantique, Bordeaux" });
                    #endregion

                    #region Groupe E
                    var equipeItalie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Italie", ShortName = "ITA", Logo = "http://flags.fmcdn.net/data/flags/w580/it.png" });
                    var equipeSuede = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Suède", ShortName = "SUE", Logo = "http://flags.fmcdn.net/data/flags/w580/se.png" });
                    var equipeIrlande = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Irlande", ShortName = "IRL", Logo = "http://flags.fmcdn.net/data/flags/w580/ie.png" });
                    var equipeBelgique = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Belgique", ShortName = "BEL", Logo = "http://flags.fmcdn.net/data/flags/w580/be.png" });
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
                    groupeE.Matchs.Add(new Match() { NumeroMatch = 25, EquipeA = equipeIrlande, EquipeB = equipeSuede, Date = new DateTime(2016, 6, 13, 18, 00, 00), Stade = "Stade de France, Saint Denis" });
                    groupeE.Matchs.Add(new Match() { NumeroMatch = 26, EquipeA = equipeBelgique, EquipeB = equipeItalie, Date = new DateTime(2016, 6, 13, 21, 00, 00), Stade = "Parc Olympique Lyonnais, Lyon" });
                    groupeE.Matchs.Add(new Match() { NumeroMatch = 27, EquipeA = equipeItalie, EquipeB = equipeSuede, Date = new DateTime(2016, 6, 17, 15, 00, 00), Stade = "Stadium Municipal, Toulouse" });
                    groupeE.Matchs.Add(new Match() { NumeroMatch = 28, EquipeA = equipeBelgique, EquipeB = equipeIrlande, Date = new DateTime(2016, 6, 18, 15, 00, 00), Stade = "Matmut Atlantique, Bordeaux" });
                    groupeE.Matchs.Add(new Match() { NumeroMatch = 29, EquipeA = equipeItalie, EquipeB = equipeIrlande, Date = new DateTime(2016, 6, 22, 21, 00, 00), Stade = "Stade Pierre-Mauroy, Lille" });
                    groupeE.Matchs.Add(new Match() { NumeroMatch = 30, EquipeA = equipeSuede, EquipeB = equipeBelgique, Date = new DateTime(2016, 6, 22, 21, 00, 00), Stade = "Allianz Arena, Nice" });
                    #endregion

                    #region Groupe F
                    var equipeIslande = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Islande", ShortName = "ISL", Logo = "http://flags.fmcdn.net/data/flags/w580/is.png" });
                    var equipeAutriche = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Autriche", ShortName = "AUT", Logo = "http://flags.fmcdn.net/data/flags/w580/at.png" });
                    var equipePortugal = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Portugal", ShortName = "POR", Logo = "http://flags.fmcdn.net/data/flags/w580/pt.png" });
                    var equipeHongrie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Hongrie", ShortName = "HON", Logo = "http://flags.fmcdn.net/data/flags/w580/hu.png" });
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
                    groupeF.Matchs.Add(new Match() { NumeroMatch = 31, EquipeA = equipeAutriche, EquipeB = equipeHongrie, Date = new DateTime(2016, 6, 14, 18, 00, 00), Stade = "Matmut Atlantique, Bordeaux" });
                    groupeF.Matchs.Add(new Match() { NumeroMatch = 32, EquipeA = equipePortugal, EquipeB = equipeIslande, Date = new DateTime(2016, 6, 14, 21, 00, 00), Stade = "Stade Geoffroy-Guichard, Saint-Etienne" });
                    groupeF.Matchs.Add(new Match() { NumeroMatch = 33, EquipeA = equipeIslande, EquipeB = equipeHongrie, Date = new DateTime(2016, 6, 18, 18, 00, 00), Stade = "Stade Vélodrome, Marseille" });
                    groupeF.Matchs.Add(new Match() { NumeroMatch = 34, EquipeA = equipePortugal, EquipeB = equipeAutriche, Date = new DateTime(2016, 6, 18, 21, 00, 00), Stade = "Parc des Princes, Paris" });
                    groupeF.Matchs.Add(new Match() { NumeroMatch = 35, EquipeA = equipeIslande, EquipeB = equipeAutriche, Date = new DateTime(2016, 6, 22, 18, 00, 00), Stade = "Stade de France, Saint Denis" });
                    groupeF.Matchs.Add(new Match() { NumeroMatch = 36, EquipeA = equipeHongrie, EquipeB = equipePortugal, Date = new DateTime(2016, 6, 22, 18, 00, 00), Stade = "Parc Olympique Lyonnais, Lyon" });
                    #endregion

                    compet.Groupes.Add(groupeA);
                    compet.Groupes.Add(groupeB);
                    compet.Groupes.Add(groupeC);
                    compet.Groupes.Add(groupeD);
                    compet.Groupes.Add(groupeE);
                    compet.Groupes.Add(groupeF);

                    _pronosContestContextDatabase.SaveChanges();

                    var match_huitieme_1 = new Match() { NumeroMatch = 37, EquipePossibleDomicile_Libelle = "2° Gr. A", EquipePossibleDomicile_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'A' }), EquipePossibleDomicile_Place = 2, EquipePossibleExterieur_Libelle = "2° Gr. C", EquipePossibleExterieur_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'C' }), EquipePossibleExterieur_Place = 2, Date = new DateTime(2016, 6, 25, 15, 00, 00), Stade = "Stade Geofrroy Guichard, Saint-Etienne" };
                    var match_huitieme_2 = new Match() { NumeroMatch = 38, EquipePossibleDomicile_Libelle = "1° Gr. B", EquipePossibleDomicile_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'B' }), EquipePossibleDomicile_Place = 1, EquipePossibleExterieur_Libelle = "3° Gr. A-C-D", EquipePossibleExterieur_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'A', 'C', 'D' }), EquipePossibleExterieur_Place = 3, Date = new DateTime(2016, 6, 25, 18, 00, 00), Stade = "Parc des Princes, Paris" };
                    var match_huitieme_3 = new Match() { NumeroMatch = 39, EquipePossibleDomicile_Libelle = "1° Gr. D", EquipePossibleDomicile_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'D' }), EquipePossibleDomicile_Place = 1, EquipePossibleExterieur_Libelle = "3° Gr. B-E-F", EquipePossibleExterieur_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'B', 'E', 'F' }), EquipePossibleExterieur_Place = 3, Date = new DateTime(2016, 6, 25, 21, 00, 00), Stade = "Stade Bollaert-Delelis, Lens" };
                    var match_huitieme_4 = new Match() { NumeroMatch = 40, EquipePossibleDomicile_Libelle = "1° Gr. A", EquipePossibleDomicile_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'A' }), EquipePossibleDomicile_Place = 1, EquipePossibleExterieur_Libelle = "3° Gr. C-D-E", EquipePossibleExterieur_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'C', 'D', 'E' }), EquipePossibleExterieur_Place = 3, Date = new DateTime(2016, 6, 26, 15, 00, 00), Stade = "Parc Olympique Lyonnais, Lyon" };
                    var match_huitieme_5 = new Match() { NumeroMatch = 41, EquipePossibleDomicile_Libelle = "1° Gr. C", EquipePossibleDomicile_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'C' }), EquipePossibleDomicile_Place = 1, EquipePossibleExterieur_Libelle = "3° Gr. A-B-F", EquipePossibleExterieur_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'A', 'B', 'F' }), EquipePossibleExterieur_Place = 3, Date = new DateTime(2016, 6, 26, 18, 00, 00), Stade = "Stade Pierre-Mauroy, Lille" };
                    var match_huitieme_6 = new Match() { NumeroMatch = 42, EquipePossibleDomicile_Libelle = "1° Gr. F", EquipePossibleDomicile_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'F' }), EquipePossibleDomicile_Place = 1, EquipePossibleExterieur_Libelle = "2° Gr. E", EquipePossibleExterieur_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'E' }), EquipePossibleExterieur_Place = 2, Date = new DateTime(2016, 6, 26, 21, 00, 00), Stade = "Stadium Municipal, Toulouse" };
                    var match_huitieme_7 = new Match() { NumeroMatch = 43, EquipePossibleDomicile_Libelle = "1° Gr. E", EquipePossibleDomicile_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'E' }), EquipePossibleDomicile_Place = 1, EquipePossibleExterieur_Libelle = "2° Gr. D", EquipePossibleExterieur_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'D' }), EquipePossibleExterieur_Place = 2, Date = new DateTime(2016, 6, 27, 18, 00, 00), Stade = "Stade de France, Saint-Denis" };
                    var match_huitieme_8 = new Match() { NumeroMatch = 44, EquipePossibleDomicile_Libelle = "2° Gr. B", EquipePossibleDomicile_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'B' }), EquipePossibleDomicile_Place = 2, EquipePossibleExterieur_Libelle = "2° Gr. F", EquipePossibleExterieur_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'F' }), EquipePossibleExterieur_Place = 2, Date = new DateTime(2016, 6, 27, 21, 00, 00), Stade = "Stade Allianz Riviera, Nice" };
                    
                    compet.PhasesFinales.Add(new PhaseFinale()
                    {
                        TypePhaseFinale = TypePhaseFinale.Huitieme,
                        Matchs = new List<Match>()
                        {
                            match_huitieme_1, 
                            match_huitieme_2,
                            match_huitieme_3, 
                            match_huitieme_4,
                            match_huitieme_5,
                            match_huitieme_6,
                            match_huitieme_7,
                            match_huitieme_8
                        }
                    });

                    var match_quart_1 = new Match() { NumeroMatch = 45, EquipePossibleDomicile_Libelle = "Vainqueur 37", MatchVainqueurDomicileID = match_huitieme_1.NumeroMatch, EquipePossibleExterieur_Libelle = "Vainqueur 39", MatchVainqueurExterieurID = match_huitieme_3.NumeroMatch, Date = new DateTime(2016, 6, 30, 21, 00, 00), Stade = "Stade Vélodrome, Marseille" };
                    var match_quart_2 = new Match() { NumeroMatch = 46, EquipePossibleDomicile_Libelle = "Vainqueur 38", MatchVainqueurDomicileID = match_huitieme_2.NumeroMatch, EquipePossibleExterieur_Libelle = "Vainqueur 42", MatchVainqueurExterieurID = match_huitieme_6.NumeroMatch, Date = new DateTime(2016, 7, 1, 21, 00, 00), Stade = "Stade Pierre Mauroy, Lille" };
                    var match_quart_3 = new Match() { NumeroMatch = 47, EquipePossibleDomicile_Libelle = "Vainqueur 41", MatchVainqueurDomicileID = match_huitieme_5.NumeroMatch, EquipePossibleExterieur_Libelle = "Vainqueur 43", MatchVainqueurExterieurID = match_huitieme_7.NumeroMatch, Date = new DateTime(2016, 7, 2, 21, 00, 00), Stade = "Stade de Bordeaux, Bordeaux" };
                    var match_quart_4 = new Match() { NumeroMatch = 48, EquipePossibleDomicile_Libelle = "Vainqueur 40", MatchVainqueurDomicileID = match_huitieme_4.NumeroMatch, EquipePossibleExterieur_Libelle = "Vainqueur 44", MatchVainqueurExterieurID = match_huitieme_8.NumeroMatch, Date = new DateTime(2016, 7, 3, 21, 00, 00), Stade = "Stade de France, Saint-Denis" };
                    
                    compet.PhasesFinales.Add(new PhaseFinale()
                    {
                        TypePhaseFinale = TypePhaseFinale.Quart,
                        Matchs = new List<Match>()
                        {
                            match_quart_1,
                            match_quart_2,
                            match_quart_3,
                            match_quart_4
                        }
                    });

                    _pronosContestContextDatabase.SaveChanges();

                    var match_demi_1 = new Match() { NumeroMatch = 49, EquipePossibleDomicile_Libelle = "Vainqueur 45", MatchVainqueurDomicileID = match_quart_1.NumeroMatch, EquipePossibleExterieur_Libelle = "Vainqueur 46", MatchVainqueurExterieurID = match_quart_2.NumeroMatch, Date = new DateTime(2016, 7, 6, 21, 00, 00), Stade = "Parc Olympique Lyonnais, Lyon" };
                    var match_demi_2 = new Match() { NumeroMatch = 50, EquipePossibleDomicile_Libelle = "Vainqueur 47", MatchVainqueurDomicileID = match_quart_3.NumeroMatch, EquipePossibleExterieur_Libelle = "Vainqueur 48", MatchVainqueurExterieurID = match_quart_4.NumeroMatch, Date = new DateTime(2016, 7, 7, 21, 00, 00), Stade = "Stade Vélodrome, Marseille" };
                    
                    compet.PhasesFinales.Add(new PhaseFinale()
                    {
                        TypePhaseFinale = TypePhaseFinale.Demi,
                        Matchs = new List<Match>()
                        {
                            match_demi_1,
                            match_demi_2
                        }
                    });

                    _pronosContestContextDatabase.SaveChanges();

                    var match_finale = new Match() { NumeroMatch = 51, EquipePossibleDomicile_Libelle = "Vainqueur 49", MatchVainqueurDomicileID = match_demi_1.NumeroMatch, EquipePossibleExterieur_Libelle = "Vainqueur 50", MatchVainqueurExterieurID = match_demi_2.NumeroMatch, Date = new DateTime(2016, 7, 10, 21, 00, 00), Stade = "Stade de France, Saint Denis" };

                    compet.PhasesFinales.Add(new PhaseFinale()
                    {
                        TypePhaseFinale = TypePhaseFinale.Finale,
                        Matchs = new List<Match>()
                        {
                            match_finale
                        }
                    });

                    _pronosContestContextDatabase.SaveChanges();

                    var concours = _pronosContestContextDatabase.Concours.Add(new Concours()
                    {
                        Competition = compet,
                        CompteUtilisateurID = user.ID,
                        DateDebut = DateTime.Now.Date,
                        EtatConcours = EtatConcours.EnCours,
						DateLimiteSaisie = new DateTime(2016, 06, 12, 23, 59, 59)
					});
                    
                    _pronosContestContextDatabase.SaveChanges();
                }
            }
		}

		public void UpdateUrlMatchs()
		{
			int i = 1;
			foreach (var m in _pronosContestContextDatabase.Matchs.OrderBy(m => m.Date))
			{
				switch (i)
				{
					case 1:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017877/index.html";
						
						break;
					case 2:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017878/index.html";
						break;
					case 3:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017880/index.html";
						break;
					case 4:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017879/index.html";
						break;
					case 5:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017884/index.html";
						break;
					case 6:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017882/index.html";
						break;
					case 7:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017881/index.html";
						break;
					case 8:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017883/index.html";
						break;
					case 9:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017954/index.html";
						break;
					case 10:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017953/index.html";
						break;
					case 11:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017960/index.html";
						break;
					case 12:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017959/index.html";
						break;
					case 13:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017888/index.html";
						break;
					case 14:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017886/index.html";
						break;
					case 15:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017885/index.html";
						break;
					case 16:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017887/index.html";
						break;
					case 17:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017890/index.html";
						break;
					case 18:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017889/index.html";
						break;
					case 19:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017956/index.html";
						break;
					case 20:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017892/index.html";
						break;
					case 21:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017891/index.html";
						break;
					case 22:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017955/index.html";
						break;
					case 23:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017962/index.html";
						break;
					case 24:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017961/index.html";
						break;
					case 37:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000744/match=2017996/index.html";
						break;
					case 38:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000744/match=2017997/index.html";
						break;
					case 39:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000744/match=2017998/index.html";
						break;
					case 40:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000744/match=2017999/index.html";
						break;
					case 41:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000744/match=2018000/index.html";
						break;
					case 42:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000744/match=2018001/index.html";
						break;
					case 43:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000744/match=2018002/index.html";
						break;
					case 44:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000744/match=2018003/index.html";
						break;
					case 45:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000449/match=2017901/index.html";
						break;
					case 46:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000449/match=2017902/index.html";
						break;
					case 47:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000449/match=2017903/index.html";
						break;
					case 48:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000449/match=2017904/index.html";
						break;
					case 49:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000450/match=2017905/index.html";
						break;
					case 50:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000450/match=2017906/index.html";
						break;
					case 51:
						m.InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000451/match=2017907/index.html";
						break;
				}
				i++;
			}
			_pronosContestContextDatabase.Matchs.Where(m => m.EquipeA.ShortName == "ROU" && m.EquipeB.ShortName == "ALB").First().InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017894/index.html";
			_pronosContestContextDatabase.Matchs.Where(m => m.EquipeA.ShortName == "SUI" && m.EquipeB.ShortName == "FRA").First().InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017893/index.html";
			_pronosContestContextDatabase.Matchs.Where(m => m.EquipeA.ShortName == "RUS" && m.EquipeB.ShortName == "GAL").First().InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017896/index.html";
			_pronosContestContextDatabase.Matchs.Where(m => m.EquipeA.ShortName == "SLO" && m.EquipeB.ShortName == "ANG").First().InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017895/index.html";
			_pronosContestContextDatabase.Matchs.Where(m => m.EquipeA.ShortName == "UKR" && m.EquipeB.ShortName == "POL").First().InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017898/index.html";
			_pronosContestContextDatabase.Matchs.Where(m => m.EquipeA.ShortName == "IRN" && m.EquipeB.ShortName == "ALL").First().InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017897/index.html";
			_pronosContestContextDatabase.Matchs.Where(m => m.EquipeA.ShortName == "RPT" && m.EquipeB.ShortName == "TUR").First().InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017900/index.html";
			_pronosContestContextDatabase.Matchs.Where(m => m.EquipeA.ShortName == "CRO" && m.EquipeB.ShortName == "ESP").First().InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017899/index.html";
			_pronosContestContextDatabase.Matchs.Where(m => m.EquipeA.ShortName == "ISL" && m.EquipeB.ShortName == "AUT").First().InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017963/index.html";
			_pronosContestContextDatabase.Matchs.Where(m => m.EquipeA.ShortName == "HON" && m.EquipeB.ShortName == "POR").First().InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017964/index.html";
			_pronosContestContextDatabase.Matchs.Where(m => m.EquipeA.ShortName == "ITA" && m.EquipeB.ShortName == "IRL").First().InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017958/index.html";
			_pronosContestContextDatabase.Matchs.Where(m => m.EquipeA.ShortName == "SUE" && m.EquipeB.ShortName == "BEL").First().InformationsMatchURL = "http://fr.uefa.com/uefaeuro/season=2016/matches/round=2000448/match=2017957/index.html";
			
			_pronosContestContextDatabase.SaveChanges();
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
