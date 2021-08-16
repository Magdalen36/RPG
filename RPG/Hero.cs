using RPG.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{

    public abstract class Hero : Personnage, ICuir, IOr, IFer, ISaphir, IRubis, IAmethyste, IEmeraude
    {

        public int Cuir { get; set; }
        public int Or { get; set; } = 100;
        public int Fer { get; set; } = 50;
        public int Saphir { get; set; } = 2;
        public int Rubis { get; set; } = 3;
        public int Amethyste { get; set; } = 1;
        public int Emeraude { get; set; } = 1;

        public int Cle { get; set; }

        public int Experience { get; set; } = 2000;

        public Inventaire Invent { get; set; }

        public int BonusEquiDegats { get; set; }
        public int BonusEquiProtect { get; set; }

        public Hero()
        {
            Invent = ListeEquipement.CreateInventaireBase();
            SetBonusDegats();
            SetBonusProtect();
        }
        
        public void SetBonusDegats()
        {
            BonusEquiDegats = Invent.ArmeEquipee.DegatsSupp + Invent.AmuletteEquipee.DegatsSupp;
        }
        public void SetBonusProtect()
        {
            BonusEquiProtect = Invent.ArmureEquipee.ProtectionArmure + Invent.AmuletteEquipee.ProtectionArmure;
        }

        public void AffichageHero(Hero h)
        {
            Console.Clear();
            ConsoleColor couleur = Graphisme.ColorCyan();

            //affichage race
                string type = null;
                if (QuelPersonnage(h) == 11) { type = "Humain"; }
                if (QuelPersonnage(h) == 12) { type = "Nain"; }

            Console.WriteLine("\nVotre héros " + type + "\tNiveau " + Niveau + " (" + Experience + " / " + XpPourUp() + ")\n");
        
            Console.WriteLine("Endurance = " + End + "\t Force = " + For + "\t PV = " + Pv);
            Console.WriteLine("Bonus Dégats = " + BonusEquiDegats + "\tBonus Armure = " + BonusEquiProtect + "\n");

            Console.WriteLine("Composants : \tCuir = " + Cuir + "\t Fer = " + Fer + "\t Or = " + Or);
            Console.WriteLine($"Gemmes : \tEmeraude = {Emeraude} \t Rubis = {Rubis} \t Saphir = {Saphir} \t Améthyste = {Amethyste}");
            Console.WriteLine("\nEquipement : \tArmure = " + Invent.ArmureEquipee.Nom + "\n\t\tArme = " + Invent.ArmeEquipee.Nom);
            Console.WriteLine("\t\tAmulette = " + Invent.AmuletteEquipee.Nom);
            
            if(Invent.PotionEquipee == null) Console.WriteLine("\t\tPotion =  --- \n");
            else Console.WriteLine("\t\tPotion = " + Invent.PotionEquipee.Nom + "\n");

            if (Cle > 0) Console.WriteLine("Vous possédez la clé pour ouvrir la porte du grand méchant!\n");

            Console.ForegroundColor = couleur;
        }



        public void Depouiller(int cuir, int or, int fer, int xp, int s, int r, int a, int e)
        {
            Cuir += cuir;
            Or += or;
            Fer += fer;
            Experience += xp;
            Saphir += s;
            Rubis += r;
            Amethyste += a;
            Emeraude += e;
        }

        public void SeReposer()
        {
            Console.WriteLine("Votre héros se repose et récupère tous ses points de vie.\n");
        }

        public void MonterNiveau(Hero h)
        {
            int cpt = 0;
            int tempXp = XpPourUp();

            for (int i = 0; Experience >= tempXp; i++)
            {
                tempXp = XpPourUp();
                cpt++;
                Experience = Experience - tempXp;
                AugmenterNiveau(1);
            }

            Console.Clear();           
            AffichageHero(h);
            Console.WriteLine("Votre héros a gagné " + cpt + " niveau(x).");

            //Augmenter compétence aléatoire
            for (int i = 0; i < cpt; i++)
            {
                Random rand = new Random();
                int roll = rand.Next(1,3);
                int temp = rand.Next(1, 3);
                switch (roll)
                {
                    case 1:
                        AugmenterEndu(temp);
                        Console.WriteLine("Endurance augmentée de " + temp + ".");
                        break;
                    case 2:
                        AugmenterFor(1);
                        Console.WriteLine("Force augmentée de 1.");
                        break;
                   /*case 3:
                        AugmenterPv(temp);
                        Console.WriteLine("Points de vie augmentés de " + temp + ".");
                        break; */
                    default:
                        break;
                }
            }

            Console.WriteLine("\n<Enter pour continuer>");
            Console.ReadLine();

        }

        public int XpPourUp()
        {
            int tempXp;
            if (Niveau <= 4) { tempXp = 100; }
            else if (Niveau > 4 && Niveau <= 10) { tempXp = 150; }
            else if (Niveau > 10 && Niveau <= 15) { tempXp = 200; }
            else if (Niveau > 15 && Niveau <= 20) { tempXp = 300; }
            else if (Niveau > 20 && Niveau <= 25) { tempXp = 400; }
            else if (Niveau > 25 && Niveau <= 30) { tempXp = 500; }
            else { tempXp = 1000; }
            return tempXp;
        }

    }
}
