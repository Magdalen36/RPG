using RPG.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.Equipements
{
    public class Potion: Equipement
    {
        public int Soin { get; set; }

        public Potion() { }
        public Potion(string n, int cuir, int fer, int or, int soin, int rare)
        {
            Nom = n;
            Cuir = cuir;
            Fer = fer;
            Or = or;
            Soin = soin;
            Rarete = rare;
        }

        public override string ToString()
        {
            return Nom + $"  (S + {Soin})";
        }
    }
}

