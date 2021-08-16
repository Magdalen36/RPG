using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class Orque : Monstre, IOr
    {

        public int Or { get; set; }

        public Orque()
        {
            De d = new De();
            Or = d.Lance(1, 7) + NiveauPuissance();
            AugmenterFor(1);
            DonneExp = Experience();
        }

        public override string ToString()
        {
            return "orque";
        }

    }
}
