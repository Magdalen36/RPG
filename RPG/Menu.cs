using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class Menu
    {

        public List<string> listMenu = new List<string>();

        public void ConstruireMenuPrincipal()
        {
            listMenu.Add("Que voulez vous faire ?\n");
            listMenu.Add("1. <Créer un nouveau héros> (Vous perdrez l'ancien)");
            listMenu.Add("2. <Accéder à l'inventaire>");

            listMenu.Add("3. <Partir à l'aventure avec votre héros>");
            listMenu.Add("4. <Aller vous entrainer (Monter niveau)>");          
            listMenu.Add("5. <Faire du shopping au bazar> (EN COURS)");

            listMenu.Add("6. <Aller à la forge>");
            listMenu.Add("7. <Inventaire full> (DEBUG MODE)");
            listMenu.Add("8. <Combattre un boss impossible> (DEBUG MODE)");
            listMenu.Add("9. <Transformer en Superhéros> (DEBUG MODE)");
        }

        public void MenuPrincipal(int cptVictoire, Hero h, bool debug)
        {

            Console.WriteLine(listMenu[0]);
            Console.WriteLine(listMenu[1]);

            //2 Inventaire
            Console.WriteLine(listMenu[2] + "\n");

            //3 Combat
            /*if (cptVictoire == 0)  
                Console.WriteLine(listMenu[3]);
            else
                Console.WriteLine("3. <Continuer à combattre (Victoires : " + cptVictoire + ")>");*/
            //3 Aventure 
            Console.WriteLine(listMenu[3]);

            //4 UpNiveau
            if (h.Experience >= h.XpPourUp()){ Console.WriteLine(listMenu[4]); }


            //5 Achat/Vente
            Console.WriteLine(listMenu[5]);

            //6 Forge
            if(h.Niveau >= 10) Console.WriteLine(listMenu[6]);

            //DEBUG 7 Equi 8 Boss et 9 SuperHero
            if (debug == true)
            {
                Console.WriteLine("\n");
                Console.WriteLine(listMenu[7]);
                Console.WriteLine(listMenu[8]);
                Console.WriteLine(listMenu[9]);
            }
        }

        public void MenuCombat()
        {
            Console.WriteLine("Que voulez vous faire ?");
            Console.WriteLine("1. <Attaquer>");
            Console.WriteLine("2. <Fuir> (Vous risquez de perdre quelques pièces dans votre course)");
        }

        public void MenuRaceHero()
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("1. <Créer un humain>");
            Console.WriteLine("2. <Créer un nain>");
        }

        public void MenuInventaire1(Inventaire i)
        {
            Console.WriteLine("\n");
            if(i.NbObjetsInventaire() != 0) Console.WriteLine("<Choisir un objet dans l'inventaire par son numéro>");
            Console.WriteLine("<Enter pour sortir de l'inventaire>");
        }

        public void MenuInventaire2(int choix)
        {
                Console.WriteLine("\nQue faire avec votre equipement " + choix + " ?");
                Console.WriteLine("1. <Equiper>");
                Console.WriteLine("2. <Recycler>");
                Console.WriteLine("3. <Vendre>");
                Console.WriteLine("4. <Ne rien faire>");
        }

    }


}
