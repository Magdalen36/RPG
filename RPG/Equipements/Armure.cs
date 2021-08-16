using RPG.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.Equipements
{
    public class Armure : Equipement
    {
        public int ProtectionArmure { get; set; }

        public Armure() { }
        public Armure(string n, int cuir, int fer, int or, int protect, int rare)
        {
            Nom = n;
            Cuir = cuir;
            Fer = fer;
            Or = or;
            ProtectionArmure = protect;
            Rarete = rare;
        }

        public override string ToString() 
        {
            return Nom + $"  (P + {ProtectionArmure})";
        } 

        
    }
}
