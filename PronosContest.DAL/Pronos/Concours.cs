using PronosContest.DAL.Authentification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PronosContest.DAL.Pronos
{
	
	public enum EtatConcours
	{
		EnCours = 0,
		Termine = 1
	}
	public class Concours
	{
		#region Propriétés primitives
		[Key]
		public int ID { get; set; }
		
		public EtatConcours EtatConcours { get; set; }		
		public DateTime DateDebut { get; set; }
		public DateTime? DateFin { get; set; }
		public DateTime DateLimiteSaisie { get; set; }

		[ForeignKey("Competition")]
		public int CompetitionID { get; set; }
		[ForeignKey("CompteUtilisateur")]
		public int CompteUtilisateurID { get; set; }
		#endregion

		public Concours()
		{
			this.Pronostics = new List<Pronostic>();
			this.ConcoursCompteUtilisateurs = new List<ConcoursCompteUtilisateur>();
		}

		#region Propriétés de navigation
		public virtual ICollection<ConcoursCompteUtilisateur> ConcoursCompteUtilisateurs { get; set; }
		public virtual ICollection<Pronostic> Pronostics { get; set; }
		public virtual Competition Competition { get; set; }
		public virtual CompteUtilisateur CompteUtilisateur { get; set; }
        #endregion

        public List<ClassementConcoursModel> Classement()
        {
            var classementFinal = new List<ClassementConcoursModel>();

            foreach (var concoursUser in this.ConcoursCompteUtilisateurs)
            {
                var user = concoursUser.CompteUtilisateur;
                if (user != null)
                {
                    var elementClassement = new ClassementConcoursModel();
                    elementClassement.CompteUtilisateurID = user.ID;
                    elementClassement.NomComplet = user.Prenom + " " + user.Nom;
                    foreach (var p in this.Pronostics.Where(c => c.CompteUtilisateurID == user.ID && c.Match != null && c.Match.ButsEquipeDomicile != null && c.Match.ButsEquipeExterieur != null))
                    {
                        var match = p.Match;
                        if (match != null)
                        {
                            if (match.PhaseGroupe != null)
                            {
                                // Points matchs phases de groupes
                                if (match.VainqueurID == p.VainqueurID)
                                    elementClassement.NombrePronosGagnes += 1;
                                else if (match.ButsEquipeDomicile != null && match.ButsEquipeExterieur != null)
                                    elementClassement.NombrePronosPerdus += 1;
                                if (match.ButsEquipeDomicile == p.ButsEquipeDomicile && match.ButsEquipeExterieur == p.ButsEquipeExterieur)
                                    elementClassement.NombreScoreExact += 1;
                            }
                            else if (match.PhaseFinale != null)
                            {
                                if (p.IsNouveauProno)
                                {
                                    if (match.ButsEquipeDomicile > match.ButsEquipeExterieur && p.ButsEquipeDomicile > p.ButsEquipeExterieur)
                                        elementClassement.NombrePronosGagnesNouveauProno += 1;
                                    else if (match.ButsEquipeDomicile < match.ButsEquipeExterieur && p.ButsEquipeDomicile < p.ButsEquipeExterieur)
                                        elementClassement.NombrePronosGagnesNouveauProno += 1;
                                    else if (match.ButsEquipeDomicile == match.ButsEquipeExterieur && p.ButsEquipeDomicile == p.ButsEquipeExterieur)
                                    {
                                        elementClassement.NombrePronosGagnesNouveauProno += 1;
                                        if (match.ButsPenaltiesEquipeDomicile > match.ButsPenaltiesEquipeExterieur && p.ButsPenaltiesEquipeDomicile > p.ButsPenaltiesEquipeExterieur)
                                            elementClassement.NombrePronosGagnesPenaltyNouveauProno++;
                                        else if (match.ButsPenaltiesEquipeDomicile < match.ButsPenaltiesEquipeExterieur && p.ButsPenaltiesEquipeDomicile < p.ButsPenaltiesEquipeExterieur)
                                            elementClassement.NombrePronosGagnesPenaltyNouveauProno++;
                                        else
                                            elementClassement.NombrePronosPerdusPenaltyNouveauProno++;
                                        if (match.ButsPenaltiesEquipeDomicile == p.ButsPenaltiesEquipeDomicile && match.ButsPenaltiesEquipeExterieur == p.ButsPenaltiesEquipeExterieur)
                                            elementClassement.NombreScoreExactPenaltyNouveauProno++;
                                    }
                                    else
                                        elementClassement.NombrePronosPerdusNouveauProno += 1;
                                    if (match.ButsEquipeDomicile == p.ButsEquipeDomicile && match.ButsEquipeExterieur == p.ButsEquipeExterieur)
                                        elementClassement.NombreScoreExactNouveauProno += 1;
                                }
                                else
                                {

                                }
                            }
                        }
                    }

                    var allGroupMatchesAreFinished = true;
                    // Points sur les classements
                    foreach (var g in this.Competition.Groupes)
                    {
                        if (g.Matchs.Count(c => c.ButsEquipeDomicile != null && c.ButsEquipeExterieur != null) == g.Matchs.Count())
                        {
                            var classementReel = g.Classement();
                            var classementUser = g.ClassementWithPronostics(this.Pronostics.Where(c => c.CompteUtilisateurID == user.ID).ToList());

                            if (classementReel.Count == classementUser.Count)
                            {
                                int nbBonnesPositionsGroupe = 0;
                                for (int i = 0; i < classementReel.Count; i++)
                                {
                                    if (classementReel[i].IDEquipe == classementUser[i].IDEquipe)
                                    {
                                        elementClassement.NombreBonnePositionPoule += 1;
                                        nbBonnesPositionsGroupe++;
                                    }
                                }
                                if (this.Competition.GetEquipesQualifiees().Contains(classementUser[0].IDEquipe))
                                    elementClassement.NombreBonneEquipeQualifiee++;
                                if (this.Competition.GetEquipesQualifiees().Contains(classementUser[1].IDEquipe))
                                    elementClassement.NombreBonneEquipeQualifiee++;
                                if (nbBonnesPositionsGroupe == classementUser.Count)
                                    elementClassement.NombrePouleComplete += 1;
                            }
                        }
                        else
                            allGroupMatchesAreFinished = false;
                    }

                    if (allGroupMatchesAreFinished)
                    {
                        var equipes3emesQualifies = this.Competition.GetClassement3emesPronostics(this.Pronostics.Where(c => c.CompteUtilisateurID == user.ID).ToList()).Take(4);

                        foreach (var equipe in equipes3emesQualifies)
                        {
                            if (this.Competition.GetEquipesQualifiees().Contains(equipe.IDEquipe))
                                elementClassement.NombreBonneEquipeQualifiee++;
                        }
                    }

                    classementFinal.Add(elementClassement);
                }
            }
            return classementFinal.OrderByDescending(c => c.NombrePronosGagnes).OrderByDescending(c => c.Points).ToList();
        }
        public List<ClassementConcoursModel> ClassementAvantDate(DateTime pDate)
        {
            var classementFinal = new List<ClassementConcoursModel>();
            
            foreach (var concoursUser in this.ConcoursCompteUtilisateurs)
            {
                var user = concoursUser.CompteUtilisateur;
                if (user != null)
                {
                    var elementClassement = new ClassementConcoursModel();
                    elementClassement.CompteUtilisateurID = user.ID;
                    elementClassement.NomComplet = user.Prenom + " " + user.Nom;
                    foreach (var p in this.Pronostics.Where(c => c.CompteUtilisateurID == user.ID && c.Match != null && c.Match.ButsEquipeDomicile != null && c.Match.ButsEquipeExterieur != null && c.Match.Date <= pDate))
                    {
                        var match = this.Competition.AllMatchs.Where(m => m.ID == p.MatchID).FirstOrDefault();
                        if (match != null)
                        {
                            if (match.VainqueurID == p.VainqueurID)
                                elementClassement.NombrePronosGagnes += 1;
                            else if (match.ButsEquipeDomicile != null && match.ButsEquipeExterieur != null)
                                elementClassement.NombrePronosPerdus += 1;
                            if (match.ButsEquipeDomicile == p.ButsEquipeDomicile && match.ButsEquipeExterieur == p.ButsEquipeExterieur)
                                elementClassement.NombreScoreExact += 1;
                        }
                    }

                    var allGroupMatchesAreFinished = true;
                    // Points sur les classements
                    foreach (var g in this.Competition.Groupes)
                    {
                        if (g.Matchs.Count(c => c.ButsEquipeDomicile != null && c.ButsEquipeExterieur != null) == g.Matchs.Count())
                        {
                            var classementReel = g.Classement();
                            var classementUser = g.ClassementWithPronostics(this.Pronostics.Where(c => c.CompteUtilisateurID == user.ID).ToList());

                            if (classementReel.Count == classementUser.Count)
                            {
                                int nbBonnesPositionsGroupe = 0;
                                for (int i = 0; i < classementReel.Count; i++)
                                {
                                    if (classementReel[i].IDEquipe == classementUser[i].IDEquipe)
                                    {
                                        elementClassement.NombreBonnePositionPoule += 1;
                                        nbBonnesPositionsGroupe++;
                                    }
                                }
                                if (this.Competition.GetEquipesQualifiees().Contains(classementUser[0].IDEquipe))
                                    elementClassement.NombreBonneEquipeQualifiee++;
                                if (this.Competition.GetEquipesQualifiees().Contains(classementUser[1].IDEquipe))
                                    elementClassement.NombreBonneEquipeQualifiee++;
                                if (nbBonnesPositionsGroupe == classementUser.Count)
                                    elementClassement.NombrePouleComplete += 1;
                            }
                        }
                        else
                            allGroupMatchesAreFinished = false;
                    }

                    if (allGroupMatchesAreFinished)
                    {
                        var equipes3emesQualifies = this.Competition.GetClassement3emesPronostics(this.Pronostics.Where(c => c.CompteUtilisateurID == user.ID).ToList()).Take(4);

                        foreach (var equipe in equipes3emesQualifies)
                        {
                            if (this.Competition.GetEquipesQualifiees().Contains(equipe.IDEquipe))
                                elementClassement.NombreBonneEquipeQualifiee++;
                        }
                    }

                    classementFinal.Add(elementClassement);
                }
            }
            return classementFinal.OrderByDescending(c => c.NombreScoreExact).OrderByDescending(c => c.Points).ToList();
        }
        public Dictionary<Match, List<ClassementConcoursModel>> ClassementParMatch()
        {
            var classementParMatch = new Dictionary<Match, List<ClassementConcoursModel>>();

            foreach (var match in this.Competition.AllMatchs.Where(m => m.ButsEquipeDomicile != null && m.ButsEquipeExterieur != null).OrderBy(m => m.Date))
                classementParMatch.Add(match, ClassementAvantDate(match.Date));

            return classementParMatch;
        }
        public List<ClassementConcoursModel> ClassementProvisoire()
		{
			var classementFinal = new List<ClassementConcoursModel>();

			foreach (var concoursUser in this.ConcoursCompteUtilisateurs)
			{
				var user = concoursUser.CompteUtilisateur;
				if (user != null)
				{
					var elementClassement = new ClassementConcoursModel();
					elementClassement.CompteUtilisateurID = user.ID;
					elementClassement.NomComplet = user.Prenom + " " + user.Nom;
					// Points sur les pronos
					foreach (var p in this.Pronostics.Where(c => c.CompteUtilisateurID == user.ID && c.Match != null && c.Match.ButsEquipeDomicile != null && c.Match.ButsEquipeExterieur != null))
					{
						var match = this.Competition.AllMatchs.Where(m => m.ID == p.MatchID).FirstOrDefault();
						if (match != null)
						{
							if (match.VainqueurID == p.VainqueurID)
								elementClassement.NombrePronosGagnes += 1;
							else if (match.ButsEquipeDomicile != null && match.ButsEquipeExterieur != null)
								elementClassement.NombrePronosPerdus += 1;
							if (match.ButsEquipeDomicile == p.ButsEquipeDomicile && match.ButsEquipeExterieur == p.ButsEquipeExterieur)
								elementClassement.NombreScoreExact += 1;
						}
					}
					
					// Points sur les classements
					foreach (var g in this.Competition.Groupes)
					{
						var classementReel = g.Classement();
						var classementUser = g.ClassementWithPronostics(this.Pronostics.Where(c => c.CompteUtilisateurID == user.ID).ToList());

						if (classementReel.Count == classementUser.Count)
						{
							int nbBonnesPositionsGroupe = 0;
							for (int i = 0; i < classementReel.Count; i++)
							{
								if (classementReel[i].IDEquipe == classementUser[i].IDEquipe)
								{
									elementClassement.NombreBonnePositionPoule += 1;
									nbBonnesPositionsGroupe++;
                                }
							}
							if (nbBonnesPositionsGroupe == classementUser.Count)
								elementClassement.NombrePouleComplete += 1;
						}
					}

					// Points sur les equipes qualifiées
					var equipesQualifiesReel = this.Competition.GetEquipesQualifiees();
					var equipesQualifiesUser = this.Competition.GetEquipesQualifiees(this.Pronostics.Where(c => c.CompteUtilisateurID == user.ID).ToList());
					elementClassement.NombreBonneEquipeQualifiee = equipesQualifiesUser.Intersect(equipesQualifiesReel).Count();

					classementFinal.Add(elementClassement);
				}
			}
			return classementFinal.OrderByDescending(c => c.NombrePronosGagnes).OrderByDescending(c => c.Points).ToList();
		}

		public class ClassementConcoursModel
        {
            public int CompteUtilisateurID { get; set; }
            public string NomComplet { get; set; }
            public int NombreScoreExact { get; set; }
            public int NombrePronosGagnes { get; set; }
            public int NombrePronosPerdus { get; set; }
			public int NombreBonneEquipeQualifiee { get; set; }
			public int NombreBonnePositionPoule { get; set; }
			public int NombrePouleComplete { get; set; }
            public int NombrePronosGagnesNouveauProno { get; set; }
            public int NombrePronosPerdusNouveauProno { get; set; }
            public int NombreScoreExactNouveauProno { get; set; }
            public int NombrePronosGagnesPenaltyNouveauProno { get; set; }
            public int NombrePronosPerdusPenaltyNouveauProno { get; set; }
            public int NombreScoreExactPenaltyNouveauProno { get; set; }
            public int NombreBonneEquipeQualifieePourQuarts { get; set; }
            public int NombreBonneEquipeQualifieePourDemis { get; set; }
            public int NombreBonneEquipeQualifieePourFinale { get; set; }
            public int Points
            {
                get
                {
                    return (this.NombreScoreExact * 2) + this.NombrePronosGagnes + this.NombreBonneEquipeQualifiee + this.NombreBonnePositionPoule + (this.NombrePouleComplete * 2)
                        + this.NombrePronosGagnesNouveauProno + (this.NombreScoreExactNouveauProno * 2) + this.NombrePronosGagnesPenaltyNouveauProno + (this.NombreScoreExactPenaltyNouveauProno * 2);
                }
            }
        }
    }
}
