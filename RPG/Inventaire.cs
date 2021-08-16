using RPG.Equipements;
using RPG.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{ 

    public class Inventaire
    {
        //pour stocker les objets non utilisés
        public static List<Equipement> listE = new List<Equipement>();

        //pour stocker les objets équipés
        public Armure ArmureEquipee { get; set; }
        public Arme ArmeEquipee { get; set; }
        public Amulette AmuletteEquipee { get; set; }
        public Potion PotionEquipee { get; set; }

        public void BoirePotion()
        {
            PotionEquipee = null;
        }

        public int NbObjetsInventaire()
        {
            return listE.Count;
        }

        public Equipement GetEquipement(int choixInv)
        {
            return listE[choixInv - 1];
        }

        public void LootEquipement(Equipement equi) 
        {
            listE.Add(equi);
        }

        public void CleanInventaire()
        {
            listE.RemoveRange(0, listE.Count);
        }

        public void ChangerEquipement(int choix, Hero h)
        {
            Equipement tempEqui;
            Equipement equi = listE[choix];
            if (equi is Armure)
            {
                tempEqui = ArmureEquipee;
                ArmureEquipee = (Armure)equi;
                LootEquipement(tempEqui);
            }
            else if (equi is Arme)
            {
                tempEqui = ArmeEquipee;
                ArmeEquipee = (Arme)equi;
                LootEquipement(tempEqui);
            }
            else if (equi is Amulette)
            {
                tempEqui = AmuletteEquipee;
                AmuletteEquipee = (Amulette)equi;
                LootEquipement(tempEqui);
            }
            else if (equi is Potion)
            {
                if(!(PotionEquipee == null))
                {
                    tempEqui = PotionEquipee;
                    LootEquipement(tempEqui);
                }
                PotionEquipee = (Potion)equi;
            }
            listE.Remove(equi);   

            h.SetBonusDegats();
            h.SetBonusProtect();
        }
   
        public void RemoveEquipement(int remove)
        {
            Console.Clear();
            Console.WriteLine("Suppression de " + listE[remove].Nom + ".");
            listE.RemoveAt(remove);
        }


        public void AfficherInventaire()
        {
            Console.Clear();

            Console.WriteLine("\nObjets équipés : ");
            Console.WriteLine("\tArmure = " + ArmureEquipee.Nom + " (Protection + " + ArmureEquipee.ProtectionArmure + ")" + 
                            "\n\tArme = " + ArmeEquipee.Nom + " (Dégats + " + ArmeEquipee.DegatsSupp + ")" );

            Console.WriteLine("\tAmulette = " + AmuletteEquipee.Nom + " (Protection + " + AmuletteEquipee.ProtectionArmure + ")" + " (Dégats + " + AmuletteEquipee.DegatsSupp + ")");

            if (!(PotionEquipee == null)) Console.WriteLine("\tPotion = " + PotionEquipee.Nom + " (Soins = " + PotionEquipee.Soin + ")" + "\n\n");
            else Console.WriteLine("\tPotion = --- \n\n");
            
            Console.WriteLine("Dans l'inventaire : ");

            ConsoleColor saveColor = Console.ForegroundColor;
                for (int i = 0; i < listE.Count; i++)
                {
                    Graphisme.ColorRarete(listE[i].Rarete);
                    Console.WriteLine("\t" + (i+1) + ". " + listE[i].ToString());
                }
            Console.ForegroundColor = saveColor;

        }


        
    }

}
