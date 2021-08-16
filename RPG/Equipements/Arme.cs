using RPG.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.Equipements
{
    public class Arme : Equipement
    {
        public int DegatsSupp { get; set; }

        public Arme() { }
        public Arme(string n, int cuir, int fer, int or, int degats, int rare)
        {
            Nom = n;
            Cuir = cuir;
            Fer = fer;
            Or = or;
            DegatsSupp  = degats;
            Rarete = rare;
        }

        public override string ToString()
        {
            return Nom + $"  (D + {DegatsSupp})";
        }
    }
}

 
