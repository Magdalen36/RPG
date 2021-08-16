using RPG.Equipements;
using RPG.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class ListeEquipement
    {
        private static List<Equipement> listEquipement = new List<Equipement>();
        private static List<Equipement> listMarchandise = new List<Equipement>();

        private static List<Armure> listArmure = new List<Armure>()
        {
            // nom, cuir, fer, or, protect, rarete
            new Armure("Armure en tissu", 1, 0, 5, 0, 1),
            new Armure("Armure en cuir", 5, 0, 30, 1, 2),
            new Armure("Armure en fer", 5, 2, 50, 2, 3),
            new Armure("Armure en plaque", 10, 7, 100, 3, 4)
        };

        private static List<Arme> listArme = new List<Arme>()
        {
            // nom, cuir, fer, or, degats, rarete
            new Arme("Couteau en bois",0,1,5,0,1),
            new Arme("Dague en fer",0,5,30,1,2),
            new Arme("Epée en fer",5,10,50,2,3),
            new Arme("Epée en fer trempé",5,15,100,3,4)
        };
        private static List<Amulette> listAmulette = new List<Amulette>()
        {
            // nom, cuir, fer, or, protect, degats, rarete
            new Amulette("Pendentif solide", 1,0,5,0,0,1),
            new Amulette("Collier en cuir", 5,2,40,1,0,2),
            new Amulette("Sautoir ouvragé", 2,5,40,0,1,2),
            new Amulette("Pendentif magique", 10,10,70,1,1,3),
            new Amulette("Collier lunaire", 10,10,120,2,2,4)
        };
        private static List<Potion> listPotion = new List<Potion>()
        {
            // nom, cuir, fer, or, soin, rarete
            new Potion("Gourde d'eau",0,0,2,4,1),
            new Potion("Potion de soin mineur",0,0,10,8,2),
            new Potion("Potion de soin",0,0,20,12,3),
            new Potion("Potion de soin majeure",0,0,40,16,4)
        };

      
        public static void CreateListeFull()
        {
            for (int i = 0; i < listArmure.Count; i++) listEquipement.Add(listArmure[i]);
            for (int i = 0; i < listArme.Count; i++) listEquipement.Add(listArme[i]);
            for (int i = 0; i < listAmulette.Count; i++) listEquipement.Add(listAmulette[i]);
            for (int i = 0; i < listPotion.Count; i++) listEquipement.Add(listPotion[i]);
        }
 
        public static Inventaire CreateInventaireBase()
        {
            Inventaire inv = new Inventaire();

            inv.ArmureEquipee = listArmure[0];
            inv.ArmeEquipee = listArme[0];
            inv.AmuletteEquipee = listAmulette[0];
            inv.PotionEquipee = listPotion[0];

            return inv;
        }

        public static Equipement GenererLootEquipement()
        {       
            CreateListeFull();
            Equipement e;
            Random rand = new Random();           
            int rarete = rand.Next(1,11); int tempRare = 0;

            switch (rarete)
            {
                case 1: case 2: case 3: case 4:  tempRare = 1; break;
                case 5: case 6: case 7:  tempRare = 2; break;
                case 8: case 9:  tempRare = 3; break;
                case 10:  tempRare = 4; break;
            }

            List<Equipement> tempListRare = new List<Equipement>();

            for (int i = 0; i < listEquipement.Count; i++)
            {
                if(listEquipement[i].Rarete == tempRare)
                    tempListRare.Add(listEquipement[i]);
            }

            int randInvent = rand.Next(1,tempListRare.Count+1);

            e = tempListRare[randInvent-1];
            return e;
        }

        public static List<Equipement> GenererListeMarchand()
        {
            listMarchandise.Add(listPotion[0]);
            for (int i = 0; i < 4; i++)
            {
                listMarchandise.Add(GenererLootEquipement());               
            }
            return listMarchandise;
        }

        public static List<Equipement> GenererListeMarchandAmbulant()
        {
            for (int i = 0; i < 3; i++)
            {
                listMarchandise.Add(GenererLootEquipement());
            }
            return listMarchandise;
        }

    }
}
