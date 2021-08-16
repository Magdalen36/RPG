using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class Dragonnet : Monstre, ICuir, IOr
    {

        public int Or { get; set; }
        public int Cuir { get; set; }

        public Dragonnet()
        {
            De d = new De();
            Or = d.Lance(1, 7) + NiveauPuissance();
            Cuir = d.Lance(1, 5);
            DonneExp = Experience();

            AugmenterEndu(1);  
            
        }

        public override int Experience()
        {
            return base.Experience() + 10;   
        }

        public override string ToString()
        {
            return "dragonnet";
        }



    }
}
