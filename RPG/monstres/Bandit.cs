using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.monstres
{
    public class Bandit : Monstre, IOr
    {
        public int Or { get; set; }

        public Bandit()
        {
            De d = new De();
            Or = d.Lance(5, 13) + NiveauPuissance()*2;
            AugmenterFor(2);
            AugmenterEndu(2);
            AugmenterPv(3);
            DonneExp = Experience();
        }

        public override int Experience()
        {
            return base.Experience() + 30;
        }

        public override string ToString()
        {
            return "bandit";
        }
    }
}
