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
		public List<char> GetCombinaisonClassementTroisiemes (List<Pronostic> pPronosticsUser)
		{
			List<PhaseGroupe.ClassementGroupeModel> classement = new List<PhaseGroupe.ClassementGroupeModel>();

			foreach (var groupe in Groupes)
			{
				var equipe3eme = groupe.ClassementWithPronostics(pPronosticsUser).ElementAt(2);
				classement.Add(equipe3eme);
			}

			classement = classement.OrderByDescending(c => c.ButsMarques).OrderByDescending(c => c.Difference).OrderByDescending(c => c.Points).ToList();

			List<char> combinaisons = new List<char>();
			foreach (var cl in classement.Take(4))
			{
				var groupe = this.Groupes.Where(g => g.Equipes.Any(e => e.ID == cl.IDEquipe)).FirstOrDefault();
				combinaisons.Add(groupe.Lettre.ToCharArray().First());
            }
			return combinaisons;
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
