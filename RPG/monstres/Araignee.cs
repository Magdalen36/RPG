using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class Araignee : Monstre, ICuir
    {
        public int Cuir { get; set; }

        public Araignee()
        {
            De d = new De();
            Cuir = d.Lance(2, 7);
            DonneExp = Experience();
            AugmenterEndu(1);
            AugmenterFor(1);
            AugmenterPv(3);
        }

        public override string ToString()
        {
            return "araignée";
        }

        public override int Experience()
        {
            return base.Experience() + 20;
        }

    }
}
