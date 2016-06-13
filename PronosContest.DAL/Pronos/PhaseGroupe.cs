using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PronosContest.DAL.Pronos
{
	public class PhaseGroupe
	{
		#region Propriétés primitives
		[Key]
		public int ID { get; set; }
		
		public string Lettre { get; set; }

		[ForeignKey("Competition")]
		public int CompetitionID { get; set; }
		#endregion

		public PhaseGroupe()
		{
			this.Equipes = new List<Equipe>();
			this.Matchs = new List<Match>();
		}

		#region Propriétés de navigation
		public virtual ICollection<Equipe> Equipes { get; set; }
		public virtual ICollection<Match> Matchs { get; set; }
		public virtual Competition Competition { get; set; }
        #endregion

        public List<ClassementGroupeModel> Classement()
        {
            var classementFinal = new List<ClassementGroupeModel>();

            foreach (var match in this.Matchs)
            {
                if (!classementFinal.Where(c => c.Nom == match.EquipeA.Libelle).Any())
                    classementFinal.Add(new ClassementGroupeModel(match.EquipeA.Libelle, match.EquipeA.ShortName));
                if (!classementFinal.Where(c => c.Nom == match.EquipeB.Libelle).Any())
                    classementFinal.Add(new ClassementGroupeModel(match.EquipeB.Libelle, match.EquipeB.ShortName));

                var equipeDom = classementFinal.Where(c => c.Nom == match.EquipeA.Libelle).First();
                var equipeExt = classementFinal.Where(c => c.Nom == match.EquipeB.Libelle).First();

                equipeDom.MatchsJoues += 1;
                equipeExt.MatchsJoues += 1;
                if (match.ButsEquipeDomicile > match.ButsEquipeExterieur)
                {
                    equipeDom.MatchsGagnes += 1;
                    equipeExt.MatchsPerdus += 1;
                }
                else if (match.ButsEquipeExterieur > match.ButsEquipeDomicile)
                {
                    equipeDom.MatchsPerdus += 1;
                    equipeExt.MatchsGagnes += 1;
                }
                else
                {
                    equipeDom.MatchsNuls += 1;
                    equipeExt.MatchsNuls += 1;
                }

                equipeDom.ButsMarques += match.ButsEquipeDomicile.Value;
                equipeDom.ButsEncaisses += match.ButsEquipeExterieur.Value;
                equipeExt.ButsMarques += match.ButsEquipeExterieur.Value;
                equipeExt.ButsEncaisses += match.ButsEquipeDomicile.Value;
            }
			classementFinal = classementFinal.OrderByDescending(c => c.ButsMarques).OrderByDescending(c => c.Difference).OrderByDescending(c => c.Points).ToList();
			// ordre du classement
			if (classementFinal.GroupBy(g => g.Points).Count() != classementFinal.Count)
			{
				List<ClassementGroupeModel> classementProvisoire = new List<ClassementGroupeModel>();

				foreach (var c in classementFinal)
				{
					if (!classementProvisoire.Any())
					{
						classementProvisoire.Add(c);
						continue;
					}
					if (classementProvisoire.Any(cl => cl.Points == c.Points))
					{
						// Equipes avec le meme nombre de points
						var index = 0;
						foreach (var cp in classementProvisoire.Where(cl => cl.Points == c.Points && cl.IDEquipe != c.IDEquipe))
						{
							var equipeA = cp.IDEquipe;
							var equipeB = c.IDEquipe;

							var matchJoue = this.Matchs.Where(pipcg => (pipcg.EquipeAID == equipeA || pipcg.EquipeBID == equipeA) && (pipcg.EquipeAID == equipeB || pipcg.EquipeBID == equipeB)).FirstOrDefault();
							if (matchJoue.VainqueurID != null)
							{
                                if (matchJoue.VainqueurID == equipeB)
                                {
                                    index = classementProvisoire.IndexOf(cp);
                                }
                                else
                                {
                                    index = classementProvisoire.IndexOf(cp) + 1;
                                }
                            }
						}
						classementProvisoire.Insert(index, c);
					}
					else
					{
						var index = 0;
						foreach (var cp in classementProvisoire.Where(cl => cl.IDEquipe != c.IDEquipe))
						{
							if (cp.Points > c.Points)
							{
								index = classementProvisoire.IndexOf(cp) + 1;
							}
							else
							{
								index = classementProvisoire.IndexOf(cp);
							}
						}
						classementProvisoire.Insert(index, c);
					}
				}

				classementFinal = classementProvisoire;
			}

			return classementFinal;
        }
        public List<ClassementGroupeModel> ClassementWithPronostics(List<Pronostic> pPronosticsIDPourCeGroupe)
        {
            var classementFinal = new List<ClassementGroupeModel>();
            var pronostics = pPronosticsIDPourCeGroupe.Where(p => p.Concours.Competition.Groupes.Where(g => g.ID == this.ID).Any());
            foreach (var match in this.Matchs)
            {
                if (!classementFinal.Where(c => c.Nom == match.EquipeA.Libelle).Any())
                    classementFinal.Add(new ClassementGroupeModel(match.EquipeA.Libelle, match.EquipeA.ShortName));
                if (!classementFinal.Where(c => c.Nom == match.EquipeB.Libelle).Any())
                    classementFinal.Add(new ClassementGroupeModel(match.EquipeB.Libelle, match.EquipeB.ShortName));

                var prono = pronostics.Where(p => p.MatchID == match.ID).FirstOrDefault();

                if (prono != null)
                {
                    var equipeDom = classementFinal.Where(c => c.Nom == match.EquipeA.Libelle).First();
                    var equipeExt = classementFinal.Where(c => c.Nom == match.EquipeB.Libelle).First();

                    equipeDom.IDEquipe = match.EquipeAID.Value;
                    equipeExt.IDEquipe = match.EquipeBID.Value;
                    equipeDom.MatchsJoues += 1;
                    equipeExt.MatchsJoues += 1;
                    if (prono.ButsEquipeDomicile > prono.ButsEquipeExterieur)
                    {
                        equipeDom.MatchsGagnes += 1;
                        equipeExt.MatchsPerdus += 1;
                    }
                    else if (prono.ButsEquipeExterieur > prono.ButsEquipeDomicile)
                    {
                        equipeDom.MatchsPerdus += 1;
                        equipeExt.MatchsGagnes += 1;
                    }
                    else
                    {
                        equipeDom.MatchsNuls += 1;
                        equipeExt.MatchsNuls += 1;
                    }

                    equipeDom.ButsMarques += prono.ButsEquipeDomicile;
                    equipeDom.ButsEncaisses += prono.ButsEquipeExterieur;
                    equipeExt.ButsMarques += prono.ButsEquipeExterieur;
                    equipeExt.ButsEncaisses += prono.ButsEquipeDomicile;
                }
            }
			classementFinal = classementFinal.OrderByDescending(c => c.ButsMarques).OrderByDescending(c => c.Difference).OrderByDescending(c => c.Points).ToList();

            if (pronostics.Count() == 6)
            {
                // ordre du classement
                if (classementFinal.GroupBy(g => g.Points).Count() != classementFinal.Count)
                {
                    List<ClassementGroupeModel> classementProvisoire = new List<ClassementGroupeModel>();

                    foreach (var c in classementFinal)
                    {
                        if (!classementProvisoire.Any())
                        {
                            classementProvisoire.Add(c);
                            continue;
                        }
                        if (classementProvisoire.Any(cl => cl.Points == c.Points))
                        {
                            // Equipes avec le meme nombre de points
                            var index = 0;
                            foreach (var cp in classementProvisoire.Where(cl => cl.Points == c.Points && cl.IDEquipe != c.IDEquipe))
                            {
                                var equipeA = cp.IDEquipe;
                                var equipeB = c.IDEquipe;

                                var pronoJoue = pPronosticsIDPourCeGroupe.Where(pipcg => (pipcg.EquipeAID == equipeA || pipcg.EquipeBID == equipeA) && (pipcg.EquipeAID == equipeB || pipcg.EquipeBID == equipeB)).FirstOrDefault();
                                if (pronoJoue != null && pronoJoue.VainqueurID != null)
                                {
                                    if (pronoJoue.VainqueurID == equipeB)
                                    {
                                        index = classementProvisoire.IndexOf(cp);
                                    }
                                    else
                                    {
                                        index = classementProvisoire.IndexOf(cp) + 1;
                                    }
                                }
                            }
                            classementProvisoire.Insert(index, c);
                        }
                        else
                        {
                            var index = 0;
                            foreach (var cp in classementProvisoire.Where(cl => cl.IDEquipe != c.IDEquipe))
                            {
                                if (cp.Points > c.Points)
                                {
                                    index = classementProvisoire.IndexOf(cp) + 1;
                                }
                                else
                                {
                                    index = classementProvisoire.IndexOf(cp);
                                }
                            }
                            classementProvisoire.Insert(index, c);
                        }
                    }

                    classementFinal = classementProvisoire;
                }
            }
			
			return classementFinal;
        }
        public class ClassementGroupeModel
        {
            public int IDEquipe { get; set; }
            public string Nom { get; set; }
            public string ShortName { get; set; }
            public int Points { get { return this.MatchsGagnes * 3 + this.MatchsNuls;  } }
            public int MatchsJoues { get; set; }
            public int MatchsGagnes { get; set; }
            public int MatchsNuls { get; set; }
            public int MatchsPerdus { get; set; }
            public int ButsMarques { get; set; }
            public int ButsEncaisses { get; set; }
            public int Difference { get { return this.ButsMarques - this.ButsEncaisses; } }
            public char Groupe { get; set; }

            public ClassementGroupeModel(string pNom, string pShortName)
            {
                this.Nom = pNom;
                this.ShortName = pShortName;
                this.MatchsGagnes = this.MatchsJoues = this.MatchsNuls = this.MatchsPerdus = this.ButsEncaisses = this.ButsMarques = 0;
            }
        }
    }
}
