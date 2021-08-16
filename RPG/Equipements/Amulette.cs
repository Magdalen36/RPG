using RPG.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.Equipements
{
    public class Amulette : Equipement
    {
        public int ProtectionArmure { get; set; }
        public int DegatsSupp { get; set; }

        public Amulette() { }
        public Amulette(string n, int cuir, int fer, int or, int protect, int degats, int rare)
        {
            Nom = n;
            Cuir = cuir;
            Fer = fer;
            Or = or;
            ProtectionArmure = protect;
            DegatsSupp = degats;
            Rarete = rare;
        }

        public override string ToString()
        {
            return Nom + $"  (D + {DegatsSupp}) (P + {ProtectionArmure})";
        }
    }
}

