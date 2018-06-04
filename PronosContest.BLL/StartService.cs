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
            //_deleteTables();
            if (!_pronosContestContextDatabase.Competitions.Any(c => c.Libelle == "Euro 2016"))
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
                    var equipeAlbanie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Albanie", ShortName = "ALB", Logo = "http://flags.fmcdn.net/data/flags/w580/al.png" });
                    var equipeSuisse = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Suisse", ShortName = "SUI", Logo = "http://flags.fmcdn.net/data/flags/normal/ch.png" });
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
            if (!_pronosContestContextDatabase.Competitions.Any(c => c.Libelle == "Coupe du monde 2018"))
            {
                var user = _pronosContestContextDatabase.CompteUtilisateurs.FirstOrDefault();
                if (user != null)
                {
                    var compet = _pronosContestContextDatabase.Competitions.Add(new Competition()
                    {
                        Libelle = "Coupe du monde 2018",
                        DateDebut = new DateTime(2018, 6, 14, 21, 00, 00),
                        DateFin = new DateTime(2018, 7, 15, 23, 59, 59),
                        TypeSport = TypeDeSport.Football
                    });

                    #region Groupe A
                    var equipeRussie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Russie", ShortName = "RUS", Logo = "http://flags.fmcdn.net/data/flags/w580/ru.png" });
                    var equipeArabieSaoudite = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Arabie Saoudite", ShortName = "AS", Logo = "http://flags.fmcdn.net/data/flags/w580/sa.png" });
                    var equipeEgypte = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Egypte", ShortName = "EGY", Logo = "http://flags.fmcdn.net/data/flags/w580/eg.png" });
                    var equipeUruguay = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Uruguay", ShortName = "URU", Logo = "http://flags.fmcdn.net/data/flags/w580/uy.png" });
                    var groupeA = _pronosContestContextDatabase.Groupes.Add(new PhaseGroupe()
                    {
                        Lettre = "A",
                        Equipes = new List<Equipe>()
                    {
                        equipeRussie,
                        equipeArabieSaoudite,
                        equipeEgypte,
                        equipeUruguay
                    }
                    });

                    _pronosContestContextDatabase.SaveChanges();

                    groupeA.Matchs.Add(new Match() { NumeroMatch = 1, EquipeAID = equipeRussie.ID, EquipeBID = equipeArabieSaoudite.ID, Date = new DateTime(2018, 6, 14, 17, 00, 00), Stade = "Moscou" });
                    groupeA.Matchs.Add(new Match() { NumeroMatch = 2, EquipeAID = equipeEgypte.ID, EquipeBID = equipeUruguay.ID, Date = new DateTime(2018, 6, 15, 14, 00, 00), Stade = "Iekaterinbourg" });
                    groupeA.Matchs.Add(new Match() { NumeroMatch = 3, EquipeAID = equipeRussie.ID, EquipeBID = equipeEgypte.ID, Date = new DateTime(2018, 6, 19, 20, 00, 00), Stade = "Saint-Pétersbourg" });
                    groupeA.Matchs.Add(new Match() { NumeroMatch = 4, EquipeAID = equipeUruguay.ID, EquipeBID = equipeArabieSaoudite.ID, Date = new DateTime(2018, 6, 20, 17, 00, 00), Stade = "Rostov-sur-le-Don" });
                    groupeA.Matchs.Add(new Match() { NumeroMatch = 5, EquipeAID = equipeUruguay.ID, EquipeBID = equipeRussie.ID, Date = new DateTime(2018, 6, 25, 16, 00, 00), Stade = "Samara" });
                    groupeA.Matchs.Add(new Match() { NumeroMatch = 6, EquipeAID = equipeArabieSaoudite.ID, EquipeBID = equipeEgypte.ID, Date = new DateTime(2018, 6, 25, 16, 00, 00), Stade = "Volgongrad" });
                    #endregion

                    #region Groupe B
                    var equipeMaroc = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Maroc", ShortName = "MAR", Logo = "http://flags.fmcdn.net/data/flags/w580/ma.png" });
                    var equipeIran = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Iran", ShortName = "IRA", Logo = "http://flags.fmcdn.net/data/flags/w580/ir.png" });
                    var equipePortugal = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Egypte", ShortName = "EGY", Logo = "http://flags.fmcdn.net/data/flags/w580/pt.png" });
                    var equipeEspagne = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Espagne", ShortName = "ESP", Logo = "http://flags.fmcdn.net/data/flags/w580/es.png" });
                    var groupeB = _pronosContestContextDatabase.Groupes.Add(new PhaseGroupe()
                    {
                        Lettre = "B",
                        Equipes = new List<Equipe>()
                    {
                        equipeMaroc,
                        equipeIran,
                        equipePortugal,
                        equipeEspagne
                    }
                    });
                    groupeB.Matchs.Add(new Match() { NumeroMatch = 7, EquipeA = equipeMaroc, EquipeB = equipeIran, Date = new DateTime(2018, 6, 15, 17, 00, 00), Stade = "Saint-Pétersbourg" });
                    groupeB.Matchs.Add(new Match() { NumeroMatch = 8, EquipeA = equipePortugal, EquipeB = equipeEspagne, Date = new DateTime(2018, 6, 15, 20, 00, 00), Stade = "Sotchi" });
                    groupeB.Matchs.Add(new Match() { NumeroMatch = 9, EquipeA = equipePortugal, EquipeB = equipeMaroc, Date = new DateTime(2018, 6, 20, 14, 00, 00), Stade = "Moscou" });
                    groupeB.Matchs.Add(new Match() { NumeroMatch = 10, EquipeA = equipeIran, EquipeB = equipeEspagne, Date = new DateTime(2018, 6, 20, 20, 00, 00), Stade = "Kazan" });
                    groupeB.Matchs.Add(new Match() { NumeroMatch = 11, EquipeA = equipeIran, EquipeB = equipePortugal, Date = new DateTime(2018, 6, 25, 20, 00, 00), Stade = "Saransk" });
                    groupeB.Matchs.Add(new Match() { NumeroMatch = 12, EquipeA = equipeEspagne, EquipeB = equipeMaroc, Date = new DateTime(2018, 6, 25, 20, 00, 00), Stade = "Kaliningrad" });
                    #endregion

                    #region Groupe C
                    var equipeDanemark = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Danemark", ShortName = "DAN", Logo = "http://flags.fmcdn.net/data/flags/w580/dk.png" });
                    var equipeAustralie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Australie", ShortName = "AUS", Logo = "http://flags.fmcdn.net/data/flags/w580/au.png" });
                    var equipeFrance = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "France", ShortName = "FRA", Logo = "http://flags.fmcdn.net/data/flags/w580/fr.png" });
                    var equipePerou = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Perou", ShortName = "PER", Logo = "http://flags.fmcdn.net/data/flags/w580/pe.png" });
                    var groupeC = _pronosContestContextDatabase.Groupes.Add(new PhaseGroupe()
                    {
                        Lettre = "C",
                        Equipes = new List<Equipe>()
                    {
                        equipeDanemark,
                        equipeAustralie,
                        equipeFrance,
                        equipePerou
                    }
                    });
                    groupeC.Matchs.Add(new Match() { NumeroMatch = 13, EquipeA = equipeFrance, EquipeB = equipeAustralie, Date = new DateTime(2018, 6, 16, 12, 00, 00), Stade = "Kazan" });
                    groupeC.Matchs.Add(new Match() { NumeroMatch = 14, EquipeA = equipePerou, EquipeB = equipeDanemark, Date = new DateTime(2018, 6, 16, 18, 00, 00), Stade = "Saransk" });
                    groupeC.Matchs.Add(new Match() { NumeroMatch = 15, EquipeA = equipeDanemark, EquipeB = equipeAustralie, Date = new DateTime(2018, 6, 21, 14, 00, 00), Stade = "Samara" });
                    groupeC.Matchs.Add(new Match() { NumeroMatch = 16, EquipeA = equipeFrance, EquipeB = equipePerou, Date = new DateTime(2018, 6, 21, 17, 00, 00), Stade = "Iekaterinbourg" });
                    groupeC.Matchs.Add(new Match() { NumeroMatch = 17, EquipeA = equipeDanemark, EquipeB = equipeFrance, Date = new DateTime(2018, 6, 25, 16, 00, 00), Stade = "Samara" });
                    groupeC.Matchs.Add(new Match() { NumeroMatch = 18, EquipeA = equipeAustralie, EquipeB = equipePerou, Date = new DateTime(2018, 6, 25, 16, 00, 00), Stade = "Volgograd" });
                    #endregion

                    #region Groupe D
                    var equipeArgentine = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Argentine", ShortName = "ARG", Logo = "http://flags.fmcdn.net/data/flags/w580/ar.png" });
                    var equipeIslande = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Islande", ShortName = "ISL", Logo = "http://flags.fmcdn.net/data/flags/w580/is.png" });
                    var equipeCroatie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Croatie", ShortName = "CRO", Logo = "http://flags.fmcdn.net/data/flags/w580/hr.png" });
                    var equipeNigeria = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Nigéria", ShortName = "PER", Logo = "http://flags.fmcdn.net/data/flags/w580/ng.png" });
                    var groupeD = _pronosContestContextDatabase.Groupes.Add(new PhaseGroupe()
                    {
                        Lettre = "D",
                        Equipes = new List<Equipe>()
                    {
                        equipeArgentine,
                        equipeIslande,
                        equipeCroatie,
                        equipeNigeria
                    }
                    });
                    groupeD.Matchs.Add(new Match() { NumeroMatch = 19, EquipeA = equipeArgentine, EquipeB = equipeIslande, Date = new DateTime(2018, 6, 16, 15, 00, 00), Stade = "Moscou" });
                    groupeD.Matchs.Add(new Match() { NumeroMatch = 20, EquipeA = equipeCroatie, EquipeB = equipeNigeria, Date = new DateTime(2018, 6, 16, 21, 00, 00), Stade = "Kaliningrad" });
                    groupeD.Matchs.Add(new Match() { NumeroMatch = 21, EquipeA = equipeArgentine, EquipeB = equipeCroatie, Date = new DateTime(2018, 6, 21, 20, 00, 00), Stade = "Nijni Novgorod" });
                    groupeD.Matchs.Add(new Match() { NumeroMatch = 22, EquipeA = equipeNigeria, EquipeB = equipeIslande, Date = new DateTime(2018, 6, 22, 17, 00, 00), Stade = "Volgograd" });
                    groupeD.Matchs.Add(new Match() { NumeroMatch = 23, EquipeA = equipeNigeria, EquipeB = equipeArgentine, Date = new DateTime(2018, 6, 26, 20, 00, 00), Stade = "Saint-Pétersbourg" });
                    groupeD.Matchs.Add(new Match() { NumeroMatch = 24, EquipeA = equipeIslande, EquipeB = equipeCroatie, Date = new DateTime(2018, 6, 26, 20, 00, 00), Stade = "Rostov-sur-le-Don" });
                    #endregion

                    #region Groupe E
                    var equipeCostaRica = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Costa Rica", ShortName = "CR", Logo = "http://flags.fmcdn.net/data/flags/w580/cr.png" });
                    var equipeSerbie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Sérbie", ShortName = "SER", Logo = "http://flags.fmcdn.net/data/flags/w580/rs.png" });
                    var equipeBresil = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Brésil", ShortName = "BRE", Logo = "http://flags.fmcdn.net/data/flags/w580/br.png" });
                    var equipeSuisse = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Suisse", ShortName = "SUI", Logo = "http://flags.fmcdn.net/data/flags/w580/ch.png" });
                    var groupeE = _pronosContestContextDatabase.Groupes.Add(new PhaseGroupe()
                    {
                        Lettre = "E",
                        Equipes = new List<Equipe>()
                    {
                        equipeCostaRica,
                        equipeSerbie,
                        equipeBresil,
                        equipeSuisse
                    }
                    });
                    groupeE.Matchs.Add(new Match() { NumeroMatch = 25, EquipeA = equipeCostaRica, EquipeB = equipeSerbie, Date = new DateTime(2018, 6, 17, 14, 00, 00), Stade = "Samara" });
                    groupeE.Matchs.Add(new Match() { NumeroMatch = 26, EquipeA = equipeBresil, EquipeB = equipeSuisse, Date = new DateTime(2018, 6, 17, 20, 00, 00), Stade = "Rostov-sur-le-Don" });
                    groupeE.Matchs.Add(new Match() { NumeroMatch = 27, EquipeA = equipeBresil, EquipeB = equipeCostaRica, Date = new DateTime(2018, 6, 22, 14, 00, 00), Stade = "Saint-Pétersbourg" });
                    groupeE.Matchs.Add(new Match() { NumeroMatch = 28, EquipeA = equipeSerbie, EquipeB = equipeSuisse, Date = new DateTime(2018, 6, 22, 20, 00, 00), Stade = "Kaliningrad" });
                    groupeE.Matchs.Add(new Match() { NumeroMatch = 29, EquipeA = equipeSerbie, EquipeB = equipeBresil, Date = new DateTime(2018, 6, 27, 20, 00, 00), Stade = "Moscou" });
                    groupeE.Matchs.Add(new Match() { NumeroMatch = 30, EquipeA = equipeSuisse, EquipeB = equipeCostaRica, Date = new DateTime(2018, 6, 27, 20, 00, 00), Stade = "Nijni Novgorod" });
                    #endregion

                    #region Groupe F
                    var equipeAllemagne = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Allemagne", ShortName = "ALL", Logo = "http://flags.fmcdn.net/data/flags/w580/de.png" });
                    var equipeMexique = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Méxique", ShortName = "MEX", Logo = "http://flags.fmcdn.net/data/flags/w580/mx.png" });
                    var equipeSuede = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Suède", ShortName = "SUE", Logo = "http://flags.fmcdn.net/data/flags/w580/se.png" });
                    var equipeCoreeDuSud = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Corée du Sud", ShortName = "CDS", Logo = "http://flags.fmcdn.net/data/flags/w580/kr.png" });
                    var groupeF = _pronosContestContextDatabase.Groupes.Add(new PhaseGroupe()
                    {
                        Lettre = "F",
                        Equipes = new List<Equipe>()
                    {
                        equipeAllemagne,
                        equipeMexique,
                        equipeSuede,
                        equipeCoreeDuSud
                    }
                    });
                    groupeF.Matchs.Add(new Match() { NumeroMatch = 31, EquipeA = equipeAllemagne, EquipeB = equipeMexique, Date = new DateTime(2018, 6, 17, 17, 00, 00), Stade = "Moscou" });
                    groupeF.Matchs.Add(new Match() { NumeroMatch = 32, EquipeA = equipeSuede, EquipeB = equipeCoreeDuSud, Date = new DateTime(2018, 6, 18, 14, 00, 00), Stade = "Nijni Novgorod" });
                    groupeF.Matchs.Add(new Match() { NumeroMatch = 33, EquipeA = equipeCoreeDuSud, EquipeB = equipeMexique, Date = new DateTime(2018, 6, 23, 17, 00, 00), Stade = "Rostov-sur-le-Don" });
                    groupeF.Matchs.Add(new Match() { NumeroMatch = 34, EquipeA = equipeAllemagne, EquipeB = equipeSuede, Date = new DateTime(2018, 6, 23, 20, 00, 00), Stade = "Sotchi" });
                    groupeF.Matchs.Add(new Match() { NumeroMatch = 35, EquipeA = equipeCoreeDuSud, EquipeB = equipeAllemagne, Date = new DateTime(2018, 6, 27, 16, 00, 00), Stade = "Kazan" });
                    groupeF.Matchs.Add(new Match() { NumeroMatch = 36, EquipeA = equipeMexique, EquipeB = equipeSuede, Date = new DateTime(2018, 6, 27, 16, 00, 00), Stade = "Iekaterinbourg" });
                    #endregion

                    #region Groupe G
                    var equipeBelgique = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Belgique", ShortName = "BEL", Logo = "http://flags.fmcdn.net/data/flags/w580/be.png" });
                    var equipePanama = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Panama", ShortName = "PAN", Logo = "http://flags.fmcdn.net/data/flags/w580/pa.png" });
                    var equipeTunisie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Tunisie", ShortName = "TUN", Logo = "http://flags.fmcdn.net/data/flags/w580/tn.png" });
                    var equipeAngleterre = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Angleterre", ShortName = "ANG", Logo = "https://vignette.wikia.nocookie.net/rpfr/images/2/21/England.png/revision/latest?cb=20120618033546&path-prefix=fr" });
                    var groupeG = _pronosContestContextDatabase.Groupes.Add(new PhaseGroupe()
                    {
                        Lettre = "G",
                        Equipes = new List<Equipe>()
                    {
                        equipeBelgique,
                        equipePanama,
                        equipeTunisie,
                        equipeAngleterre
                    }
                    });
                    groupeG.Matchs.Add(new Match() { NumeroMatch = 37, EquipeA = equipeBelgique, EquipeB = equipePanama, Date = new DateTime(2018, 6, 18, 17, 00, 00), Stade = "Sotchi" });
                    groupeG.Matchs.Add(new Match() { NumeroMatch = 38, EquipeA = equipeTunisie, EquipeB = equipeAngleterre, Date = new DateTime(2018, 6, 18, 20, 00, 00), Stade = "Volgograd" });
                    groupeG.Matchs.Add(new Match() { NumeroMatch = 39, EquipeA = equipeBelgique, EquipeB = equipeTunisie, Date = new DateTime(2018, 6, 23, 14, 00, 00), Stade = "Moscou" });
                    groupeG.Matchs.Add(new Match() { NumeroMatch = 40, EquipeA = equipeAngleterre, EquipeB = equipePanama, Date = new DateTime(2018, 6, 24, 14, 00, 00), Stade = "Nijni Novgorod" });
                    groupeG.Matchs.Add(new Match() { NumeroMatch = 41, EquipeA = equipeAngleterre, EquipeB = equipeBelgique, Date = new DateTime(2018, 6, 28, 20, 00, 00), Stade = "Kaliningrad" });
                    groupeG.Matchs.Add(new Match() { NumeroMatch = 42, EquipeA = equipePanama, EquipeB = equipeTunisie, Date = new DateTime(2018, 6, 28, 20, 00, 00), Stade = "Saransk" });
                    #endregion

                    #region Groupe H
                    var equipeColombie = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Colombie", ShortName = "COL", Logo = "http://flags.fmcdn.net/data/flags/w580/co.png" });
                    var equipeJapon = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Japon", ShortName = "JAP", Logo = "http://flags.fmcdn.net/data/flags/w580/jp.png" });
                    var equipePologne = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Pologne", ShortName = "POL", Logo = "http://flags.fmcdn.net/data/flags/w580/pl.png" });
                    var equipeSenegal = _pronosContestContextDatabase.Equipes.Add(new Equipe() { Libelle = "Sénégal", ShortName = "SEN", Logo = "http://flags.fmcdn.net/data/flags/w580/sn.png" });
                    var groupeH = _pronosContestContextDatabase.Groupes.Add(new PhaseGroupe()
                    {
                        Lettre = "H",
                        Equipes = new List<Equipe>()
                    {
                        equipeColombie,
                        equipeJapon,
                        equipePologne,
                        equipeSenegal
                    }
                    });
                    groupeH.Matchs.Add(new Match() { NumeroMatch = 43, EquipeA = equipeColombie, EquipeB = equipeJapon, Date = new DateTime(2018, 6, 19, 14, 00, 00), Stade = "Saransk" });
                    groupeH.Matchs.Add(new Match() { NumeroMatch = 44, EquipeA = equipePologne, EquipeB = equipeSenegal, Date = new DateTime(2018, 6, 19, 17, 00, 00), Stade = "Moscou" });
                    groupeH.Matchs.Add(new Match() { NumeroMatch = 45, EquipeA = equipeJapon, EquipeB = equipeSenegal, Date = new DateTime(2018, 6, 24, 17, 00, 00), Stade = "Iekaterinbourg" });
                    groupeH.Matchs.Add(new Match() { NumeroMatch = 46, EquipeA = equipePologne, EquipeB = equipeColombie, Date = new DateTime(2018, 6, 24, 20, 00, 00), Stade = "Kazan" });
                    groupeH.Matchs.Add(new Match() { NumeroMatch = 47, EquipeA = equipeJapon, EquipeB = equipePologne, Date = new DateTime(2018, 6, 28, 16, 00, 00), Stade = "Volgograd" });
                    groupeH.Matchs.Add(new Match() { NumeroMatch = 48, EquipeA = equipeSenegal, EquipeB = equipeColombie, Date = new DateTime(2018, 6, 28, 16, 00, 00), Stade = "Samara" });
                    #endregion

                    compet.Groupes.Add(groupeA);
                    compet.Groupes.Add(groupeB);
                    compet.Groupes.Add(groupeC);
                    compet.Groupes.Add(groupeD);
                    compet.Groupes.Add(groupeE);
                    compet.Groupes.Add(groupeF);
                    compet.Groupes.Add(groupeG);
                    compet.Groupes.Add(groupeH);

                    _pronosContestContextDatabase.SaveChanges();

                    var match_huitieme_1 = new Match() { NumeroMatch = 49, EquipePossibleDomicile_Libelle = "1° Gr. A", EquipePossibleDomicile_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'A' }), EquipePossibleDomicile_Place = 1, EquipePossibleExterieur_Libelle = "2° Gr. B", EquipePossibleExterieur_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'B' }), EquipePossibleExterieur_Place = 2, Date = new DateTime(2018, 6, 30, 20, 00, 00), Stade = "Sotchi" };
                    var match_huitieme_2 = new Match() { NumeroMatch = 50, EquipePossibleDomicile_Libelle = "1° Gr. C", EquipePossibleDomicile_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'C' }), EquipePossibleDomicile_Place = 1, EquipePossibleExterieur_Libelle = "2° Gr. D", EquipePossibleExterieur_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'D' }), EquipePossibleExterieur_Place = 2, Date = new DateTime(2018, 6, 30, 16, 00, 00), Stade = "Kazan" };
                    var match_huitieme_3 = new Match() { NumeroMatch = 51, EquipePossibleDomicile_Libelle = "1° Gr. E", EquipePossibleDomicile_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'E' }), EquipePossibleDomicile_Place = 1, EquipePossibleExterieur_Libelle = "2° Gr. F", EquipePossibleExterieur_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'F' }), EquipePossibleExterieur_Place = 2, Date = new DateTime(2018, 7, 2, 16, 00, 00), Stade = "Samara" };
                    var match_huitieme_4 = new Match() { NumeroMatch = 52, EquipePossibleDomicile_Libelle = "1° Gr. G", EquipePossibleDomicile_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'G' }), EquipePossibleDomicile_Place = 1, EquipePossibleExterieur_Libelle = "2° Gr. H", EquipePossibleExterieur_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'H' }), EquipePossibleExterieur_Place = 2, Date = new DateTime(2018, 7, 2, 20, 00, 00), Stade = "Rostov-sur-le-Don" };
                    var match_huitieme_5 = new Match() { NumeroMatch = 53, EquipePossibleDomicile_Libelle = "1° Gr. B", EquipePossibleDomicile_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'B' }), EquipePossibleDomicile_Place = 1, EquipePossibleExterieur_Libelle = "2° Gr. A", EquipePossibleExterieur_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'A' }), EquipePossibleExterieur_Place = 2, Date = new DateTime(2018, 7, 1, 16, 00, 00), Stade = "Moscou" };
                    var match_huitieme_6 = new Match() { NumeroMatch = 54, EquipePossibleDomicile_Libelle = "1° Gr. D", EquipePossibleDomicile_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'D' }), EquipePossibleDomicile_Place = 1, EquipePossibleExterieur_Libelle = "2° Gr. C", EquipePossibleExterieur_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'C' }), EquipePossibleExterieur_Place = 2, Date = new DateTime(2018, 7, 1, 20, 00, 00), Stade = "Nijni Novgorod" };
                    var match_huitieme_7 = new Match() { NumeroMatch = 55, EquipePossibleDomicile_Libelle = "1° Gr. F", EquipePossibleDomicile_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'F' }), EquipePossibleDomicile_Place = 1, EquipePossibleExterieur_Libelle = "2° Gr. E", EquipePossibleExterieur_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'E' }), EquipePossibleExterieur_Place = 2, Date = new DateTime(2018, 7, 3, 16, 00, 00), Stade = "Saint-Pétersbourg" };
                    var match_huitieme_8 = new Match() { NumeroMatch = 56, EquipePossibleDomicile_Libelle = "1° Gr. H", EquipePossibleDomicile_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'H' }), EquipePossibleDomicile_Place = 1, EquipePossibleExterieur_Libelle = "2° Gr. G", EquipePossibleExterieur_Groupes = new JavaScriptSerializer().Serialize(new List<char> { 'G' }), EquipePossibleExterieur_Place = 2, Date = new DateTime(2018, 7, 3, 20, 00, 00), Stade = "Moscou" };

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

                    var match_quart_1 = new Match() { NumeroMatch = 57, EquipePossibleDomicile_Libelle = "Vainqueur 49", MatchVainqueurDomicileID = match_huitieme_1.NumeroMatch, EquipePossibleExterieur_Libelle = "Vainqueur 50", MatchVainqueurExterieurID = match_huitieme_2.NumeroMatch, Date = new DateTime(2018, 7, 6, 16, 00, 00), Stade = "Nijni Novgorod" };
                    var match_quart_2 = new Match() { NumeroMatch = 58, EquipePossibleDomicile_Libelle = "Vainqueur 51", MatchVainqueurDomicileID = match_huitieme_3.NumeroMatch, EquipePossibleExterieur_Libelle = "Vainqueur 52", MatchVainqueurExterieurID = match_huitieme_4.NumeroMatch, Date = new DateTime(2018, 7, 6, 20, 00, 00), Stade = "Kazan" };
                    var match_quart_3 = new Match() { NumeroMatch = 59, EquipePossibleDomicile_Libelle = "Vainqueur 53", MatchVainqueurDomicileID = match_huitieme_5.NumeroMatch, EquipePossibleExterieur_Libelle = "Vainqueur 54", MatchVainqueurExterieurID = match_huitieme_6.NumeroMatch, Date = new DateTime(2018, 7, 7, 20, 00, 00), Stade = "Sotchi" };
                    var match_quart_4 = new Match() { NumeroMatch = 60, EquipePossibleDomicile_Libelle = "Vainqueur 55", MatchVainqueurDomicileID = match_huitieme_7.NumeroMatch, EquipePossibleExterieur_Libelle = "Vainqueur 56", MatchVainqueurExterieurID = match_huitieme_8.NumeroMatch, Date = new DateTime(2018, 7, 7, 16, 00, 00), Stade = "Samara" };

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

                    var match_demi_1 = new Match() { NumeroMatch = 61, EquipePossibleDomicile_Libelle = "Vainqueur 57", MatchVainqueurDomicileID = match_quart_1.NumeroMatch, EquipePossibleExterieur_Libelle = "Vainqueur 58", MatchVainqueurExterieurID = match_quart_2.NumeroMatch, Date = new DateTime(2018, 7, 10, 20, 00, 00), Stade = "Saint-Pétersbourg" };
                    var match_demi_2 = new Match() { NumeroMatch = 62, EquipePossibleDomicile_Libelle = "Vainqueur 59", MatchVainqueurDomicileID = match_quart_3.NumeroMatch, EquipePossibleExterieur_Libelle = "Vainqueur 60", MatchVainqueurExterieurID = match_quart_4.NumeroMatch, Date = new DateTime(2016, 7, 11, 20, 00, 00), Stade = "Moscou" };

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

                    var match_finale = new Match() { NumeroMatch = 63, EquipePossibleDomicile_Libelle = "Vainqueur 61", MatchVainqueurDomicileID = match_demi_1.NumeroMatch, EquipePossibleExterieur_Libelle = "Vainqueur 62", MatchVainqueurExterieurID = match_demi_2.NumeroMatch, Date = new DateTime(2018, 7, 15, 17, 00, 00), Stade = "Moscou" };

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
                        DateLimiteSaisie = new DateTime(2018, 06, 13, 23, 59, 59)
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
