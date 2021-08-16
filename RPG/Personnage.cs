using RPG.monstres;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public abstract class Personnage
    {

        public int End { get; private set; }
        public int For { get; private set; }
        public int Pv { get; private set; }

        public int Niveau { get; set; }

        public int Nom { get; private set; }

        //constructeur
        protected Personnage()
        {
            End = Carac();
            For = Carac();
            Pv = End + GetModificateur(End);
            Niveau = 1;
        }

        //Methode pour calculer les caractéristiques au lancer de dés (4x dés 6, prendre les 3 premiers)
        public int Carac()
        {
            int[] tabCarac = new int[4];
            int nbPetit = 6; int index = 0; int lancer = 0;
            for (int i = 0; i < 4; i++)
            {
                De d = new De();
                int nb = d.Lance(1, 7);
                tabCarac[i] = nb;
                if (tabCarac[i] < nbPetit)
                {
                    nbPetit = tabCarac[i];
                    index = i;
                }
            }
            tabCarac[index] = 0;
            for (int i = 0; i < 4; i++)
            {
                lancer += tabCarac[i];
            }

            if (lancer < 10) lancer = 12;
            return lancer;
        }

        public int GetModificateur(int carac)
        {
            int modificateur = 0;
            if(carac < 5) modificateur = -1;
            else if(carac < 10) modificateur = 0;
            else if(carac < 20) modificateur = +1;
            else if(carac < 25) modificateur = +2;
            else if(carac < 30) modificateur = +3;
            else if(carac < 34) modificateur = +4;
            else modificateur = +5;
            
            return modificateur;
        }

        public int Frappe(int diffMin, int diffMax)
        {
            De d = new De();
            int lancer = d.Lance(diffMin, diffMax);
            int frappe = lancer + GetModificateur(For);
            return frappe;
        }

        public void AugmenterNiveau(int nb)
        {
            Niveau += nb;
        }
        public void AugmenterEndu(int nb)
        {
            End += nb;
            Pv = End + GetModificateur(End);
        }
        public void AugmenterFor(int nb)
        {
            For += nb;
        }
        public void AugmenterPv(int nb)
        {
            Pv += nb;
        }

        public int QuelPersonnage(Personnage m)
        {
            switch (m)
            {
                case Orque o:
                    return 1;
                    
                case Loup p:
                    return 2;
                    
                case Dragonnet d:
                    return 3;
                    
                case Araignee a:
                    return 4;
                    
                case Bandit b:
                    return 5;
                    
                case Humain h:
                    return 11;
                    
                case Nain n:
                    return 12;
                    
                default:
                    return 0;
                    
            }
        }
    }
}
