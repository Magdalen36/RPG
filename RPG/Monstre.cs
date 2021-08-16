using RPG.Equipements;
using RPG.Interfaces;
using RPG.monstres;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public abstract class Monstre : Personnage
    {

        public int DonneExp { get; set; }
        public int Puissance { get; set; }
        public Equipement equi { get; set; } = null;

        protected Monstre()
        {
        }

        public int NiveauPuissance()
        {
            int totalCarac = End + For;

            if (totalCarac < 26) return -1;
            else if (totalCarac >= 32) return 1;
            else if (totalCarac >= 48) return 2;
            else if (totalCarac >= 60) return 3;
            else return 0;
            
        }

        public void AffichageNiveauMonstre() {

            int totalCarac = For + End;
            if (NiveauPuissance() == -1)
                Console.WriteLine("\t Il a l'air malade. Facile ! (" + totalCarac + ")\n");
            else if (NiveauPuissance() == 1)
                Console.WriteLine("\tIl a l'air furieux. Faites attention ! (" + totalCarac + ")\n");
            else if (NiveauPuissance() == 2)
                Console.WriteLine("\tIl a l'air vraiment très furieux. Attention ! (" + totalCarac + ")\n");
            else if (NiveauPuissance() == 3)
                Console.WriteLine("\tOh Oh ! Un monstre élite !(" + totalCarac + ")\n");
            else
                Console.WriteLine("\tIl a l'air en forme ! (" + totalCarac + ")\n");

        }

        public virtual int Experience()
        {
            De d = new De();
            int niveau = NiveauPuissance();

            DonneExp = 30 + d.Lance(1, 20) + (niveau*15);

            return DonneExp;
        }


        public override string ToString() 
        {
            return "le monstre";
        }

    }
}
