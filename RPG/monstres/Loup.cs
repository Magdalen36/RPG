using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class Loup: Monstre, ICuir
    {

        public int Cuir { get; set; }

        public Loup()
        {
            De d = new De();
            Cuir = d.Lance(1, 5);
            DonneExp = Experience();
        }

        public override string ToString()
        {
            return "loup";
        }
    }
}
