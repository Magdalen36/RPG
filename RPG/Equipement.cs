
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.Interfaces
{
    public abstract class Equipement: IFer, IOr, ICuir
    {

        public string Nom { get; set; }
        public int Fer { get; set; }
        public int Cuir { get; set; }
        public int Or { get; set; }

        public int Rarete { get; set; } = 1;

    }
}
