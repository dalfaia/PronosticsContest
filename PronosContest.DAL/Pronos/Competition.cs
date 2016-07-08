using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PronosContest.DAL.Pronos
{
	public enum TypeDeSport
	{
		Football = 0,
		Rugby = 1
	}

	public class Competition
	{
		#region Propriétés primitives
		[Key]
		public int ID { get; set; }

		[Required]
		[StringLength(256)]
		[Column(TypeName = "VARCHAR")]
		public string Libelle { get; set; }

		public TypeDeSport TypeSport { get; set; }

		public DateTime DateDebut { get; set; }
		public DateTime DateFin { get; set; }
		#endregion

		public Competition()
		{
			this.Groupes = new List<PhaseGroupe>();
			this.PhasesFinales = new List<PhaseFinale>();
		}

		[NotMapped]
		public List<Combinaisons3eme> TableauCombinaisons
        {
            get
            {
                List<Combinaisons3eme> list = new List<Combinaisons3eme>();
                list.Add(new Combinaisons3eme() { Combinaisons = new List<char>() { 'A', 'B', 'C', 'D' }, Adversaire1A = 'C', Adversaire1B = 'D', Adversaire1C = 'A', Adversaire1D = 'B' });
                list.Add(new Combinaisons3eme() { Combinaisons = new List<char>() { 'A', 'B', 'C', 'E' }, Adversaire1A = 'C', Adversaire1B = 'A', Adversaire1C = 'B', Adversaire1D = 'E' });
                list.Add(new Combinaisons3eme() { Combinaisons = new List<char>() { 'A', 'B', 'C', 'F' }, Adversaire1A = 'C', Adversaire1B = 'A', Adversaire1C = 'B', Adversaire1D = 'F' });
                list.Add(new Combinaisons3eme() { Combinaisons = new List<char>() { 'A', 'B', 'D', 'E' }, Adversaire1A = 'D', Adversaire1B = 'A', Adversaire1C = 'B', Adversaire1D = 'E' });
                list.Add(new Combinaisons3eme() { Combinaisons = new List<char>() { 'A', 'B', 'D', 'F' }, Adversaire1A = 'D', Adversaire1B = 'A', Adversaire1C = 'B', Adversaire1D = 'F' });
                list.Add(new Combinaisons3eme() { Combinaisons = new List<char>() { 'A', 'B', 'E', 'F' }, Adversaire1A = 'E', Adversaire1B = 'A', Adversaire1C = 'B', Adversaire1D = 'F' });
                list.Add(new Combinaisons3eme() { Combinaisons = new List<char>() { 'A', 'C', 'D', 'E' }, Adversaire1A = 'C', Adversaire1B = 'D', Adversaire1C = 'A', Adversaire1D = 'E' });
                list.Add(new Combinaisons3eme() { Combinaisons = new List<char>() { 'A', 'C', 'D', 'F' }, Adversaire1A = 'C', Adversaire1B = 'D', Adversaire1C = 'A', Adversaire1D = 'F' });
                list.Add(new Combinaisons3eme() { Combinaisons = new List<char>() { 'A', 'C', 'E', 'F' }, Adversaire1A = 'C', Adversaire1B = 'A', Adversaire1C = 'F', Adversaire1D = 'E' });
                list.Add(new Combinaisons3eme() { Combinaisons = new List<char>() { 'A', 'D', 'E', 'F' }, Adversaire1A = 'D', Adversaire1B = 'A', Adversaire1C = 'F', Adversaire1D = 'E' });
                list.Add(new Combinaisons3eme() { Combinaisons = new List<char>() { 'B', 'C', 'D', 'E' }, Adversaire1A = 'C', Adversaire1B = 'D', Adversaire1C = 'B', Adversaire1D = 'E' });
                list.Add(new Combinaisons3eme() { Combinaisons = new List<char>() { 'B', 'C', 'D', 'F' }, Adversaire1A = 'C', Adversaire1B = 'D', Adversaire1C = 'B', Adversaire1D = 'F' });
                list.Add(new Combinaisons3eme() { Combinaisons = new List<char>() { 'B', 'C', 'E', 'F' }, Adversaire1A = 'E', Adversaire1B = 'C', Adversaire1C = 'B', Adversaire1D = 'F' });
                list.Add(new Combinaisons3eme() { Combinaisons = new List<char>() { 'B', 'D', 'E', 'F' }, Adversaire1A = 'E', Adversaire1B = 'D', Adversaire1C = 'B', Adversaire1D = 'F' });
                list.Add(new Combinaisons3eme() { Combinaisons = new List<char>() { 'C', 'D', 'E', 'F' }, Adversaire1A = 'C', Adversaire1B = 'D', Adversaire1C = 'F', Adversaire1D = 'E' });
                return list;
            }
        }
        public List<PhaseGroupe.ClassementGroupeModel> GetClassement3emesPronostics(List<Pronostic> pPronosticsUser)
        {
            List<PhaseGroupe.ClassementGroupeModel> classement = new List<PhaseGroupe.ClassementGroupeModel>();

            foreach (var groupe in Groupes)
            {
                var equipe3eme = groupe.ClassementWithPronostics(pPronosticsUser).ElementAt(2);
                classement.Add(equipe3eme);
            }

            return classement.OrderByDescending(c => c.ButsMarques).OrderByDescending(c => c.Difference).OrderByDescending(c => c.Points).ToList();
        }
        public List<PhaseGroupe.ClassementGroupeModel> GetClassement3emes()
        {
            List<PhaseGroupe.ClassementGroupeModel> classement = new List<PhaseGroupe.ClassementGroupeModel>();

            foreach (var groupe in Groupes)
            {
                var equipe3eme = groupe.Classement().ElementAt(2);
                classement.Add(equipe3eme);
            }

            return classement.OrderByDescending(c => c.ButsMarques).OrderByDescending(c => c.Difference).OrderByDescending(c => c.Points).ToList();
        }
        public List<char> GetCombinaisonClassementTroisiemes (List<Pronostic> pPronosticsUser)
		{
            var classement = GetClassement3emesPronostics(pPronosticsUser);
			List<char> combinaisons = new List<char>();
			foreach (var cl in classement.Take(4))
			{
				if (cl.IDEquipe == 0)
					return new List<char>();
				var groupe = this.Groupes.Where(g => g.Equipes.Any(e => e.ID == cl.IDEquipe)).FirstOrDefault();
				combinaisons.Add(groupe.Lettre.ToCharArray().First());
            }
			return combinaisons;
		}
		public List<char> GetCombinaisonClassementTroisiemes()
		{
            var classement = GetClassement3emes();
            List<char> combinaisons = new List<char>();
			foreach (var cl in classement.Take(4))
			{
				if (cl.IDEquipe == 0)
					return new List<char>();
				var groupe = this.Groupes.Where(g => g.Equipes.Any(e => e.ID == cl.IDEquipe)).FirstOrDefault();
				combinaisons.Add(groupe.Lettre.ToCharArray().First());
			}
			return combinaisons;
		}

		public List<int> GetEquipesQualifieesHuitiemes()
		{
			List<int> equipes = new List<int>();
			foreach (var groupe in this.Groupes)
			{
				var classement = groupe.Classement();
				if (classement.Count == 4)
				{
					var equipe1 = classement.ElementAt(0);
					var equipe2 = classement.ElementAt(1);
					equipes.Add(equipe1.IDEquipe);
					equipes.Add(equipe2.IDEquipe);
					if (GetCombinaisonClassementTroisiemes().Any(c => c.ToString() == groupe.Lettre))
						equipes.Add(classement.ElementAt(2).IDEquipe);
				}
			}
			return equipes;
		}
        public List<int> GetEquipesQualifieesQuarts()
        {
            List<int> equipes = new List<int>();
            var huitiemes = this.PhasesFinales.Where(pf => pf.TypePhaseFinale == TypePhaseFinale.Huitieme).FirstOrDefault();
            if (huitiemes != null)
                foreach (var match in huitiemes.Matchs)
                    if (match.ButsEquipeDomicile != null && match.ButsEquipeExterieur != null)
                        equipes.Add(match.VainqueurID.Value);
            return equipes;
        }
        public List<int> GetEquipesQualifieesDemis()
        {
            List<int> equipes = new List<int>();
            var quarts = this.PhasesFinales.Where(pf => pf.TypePhaseFinale == TypePhaseFinale.Quart).FirstOrDefault();
            if (quarts != null)
                foreach (var match in quarts.Matchs)
                    if (match.ButsEquipeDomicile != null && match.ButsEquipeExterieur != null)
                        equipes.Add(match.VainqueurID.Value);
            return equipes;
        }
        public List<int> GetEquipesQualifieesFinale()
        {
            List<int> equipes = new List<int>();
            var demis = this.PhasesFinales.Where(pf => pf.TypePhaseFinale == TypePhaseFinale.Demi).FirstOrDefault();
            if (demis != null)
                foreach (var match in demis.Matchs)
                    if (match.ButsEquipeDomicile != null && match.ButsEquipeExterieur != null)
                        equipes.Add(match.VainqueurID.Value);
            return equipes;
        }
        public int? GetVainqueurCompetition()
        {
            var finale = this.PhasesFinales.Where(pf => pf.TypePhaseFinale == TypePhaseFinale.Finale).FirstOrDefault();
            if (finale != null)
                if (finale.Matchs.FirstOrDefault() != null)
                    return finale.Matchs.First().VainqueurID;
            return null;
        }
        public List<int> GetEquipesQualifieesHuitiemes(List<Pronostic> pPronosticsUser)
		{
			List<int> equipes = new List<int>();
			foreach (var groupe in this.Groupes)
			{
				var classement = groupe.ClassementWithPronostics(pPronosticsUser);
				if (classement.Count == 4)
				{
					var equipe1 = classement.ElementAt(0);
					var equipe2 = classement.ElementAt(1);
					equipes.Add(equipe1.IDEquipe);
					equipes.Add(equipe2.IDEquipe);
					if (GetCombinaisonClassementTroisiemes(pPronosticsUser).Any(c => c.ToString() == groupe.Lettre))
						equipes.Add(classement.ElementAt(2).IDEquipe);
				}
			}
			return equipes;
		}

		[NotMapped]
		public List<Equipe> Equipes
		{
			get
			{
				List<Equipe> equipes = new List<Equipe>();
                foreach (var gr in this.Groupes)
					equipes.AddRange(gr.Equipes);
				return equipes;
			}
		}
        [NotMapped]
        public List<Match> AllMatchs
        {
            get
            {
                var listMatchs = new List<Match>();

                foreach (var g in this.Groupes)
                    listMatchs.AddRange(g.Matchs);

                foreach (var pf in this.PhasesFinales)
                    listMatchs.AddRange(pf.Matchs);

                return listMatchs;
            }
        }
        #region Propriétés de navigation
        public virtual ICollection<PhaseGroupe> Groupes { get; set; }
		public virtual ICollection<PhaseFinale> PhasesFinales { get; set; }
		#endregion
    }
    public class Combinaisons3eme
    {
		[Key]
		public int ID { get; set; }
        public List<char> Combinaisons { get; set; }
        public char Adversaire1A { get; set; }
        public char Adversaire1B { get; set; }
        public char Adversaire1C { get; set; }
        public char Adversaire1D { get; set; }
    }
}
