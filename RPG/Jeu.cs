using RPG.Equipements;
using RPG.Interfaces;
using RPG.monstres;
using System;
using System.Collections.Generic;

namespace RPG
{
   public class Jeu
    {
        public static Hero h;
        public static int cptVictoire = 0;
        public static Menu menu = new Menu();
        public static Jeu jeu = new Jeu();
        public static bool debugMode = true;
        public static List<Equipement> listMarchandise = new List<Equipement>();
        public static List<Equipement> listMarchandiseAmbulant = new List<Equipement>();

        public void Start()
        {
            Console.WriteLine("\n\n(--) Bienvenue à Stormwall (--)\n\n");

            menu.ConstruireMenuPrincipal();           

            Console.WriteLine("\n Créer votre héros !");
            Console.WriteLine("<Enter pour continuer>");
            Console.ReadLine(); Console.Clear();
            h = CreateHero();

            while (true)
            {                
                h.AffichageHero(h);
                menu.MenuPrincipal(cptVictoire, h, debugMode);
                
                //CHOIX DANS LE MENU
                int.TryParse(Console.ReadLine(), out int choix);
                bool boss = false;

                switch (choix)
                {
                    case 1: // CREATION D'UN NOUVEL HEROS
                        Console.Clear();
                        
                        if (h != null) h.Invent.CleanInventaire(); //Pour réinitialiser l'inventaire entre 2 héros
                        if (listMarchandise != null)
                        {
                            listMarchandise.RemoveRange(0, listMarchandise.Count);
                            listMarchandise = ListeEquipement.GenererListeMarchand();
                        }
                        if (cptVictoire != 0) cptVictoire = 0;
                        
                        h = CreateHero();                        
                        break;
                    case 2: //ACCEDER INVENTAIRE
                        GestionInventaire(h, menu);
                        break;
                    case 3: //AVENTURE
                        Aventure(h, boss);                        
                        break;
                    case 4: //MONTER NIVEAU
                        if (h.Experience >= h.XpPourUp()) h.MonterNiveau(h);
                        break;
                    case 5: //SHOPPING
                        if(listMarchandise.Count==0) listMarchandise = ListeEquipement.GenererListeMarchand();
                        Marchand(h, false);
                        break;
                    case 6: //FORGE
                        if (h.Niveau >= 10) Forge(h);
                        break;

                    case 7: //INVENTAIRE FULL (DEBUG)
                        for (int i = 0; i < 10; i++)
                        {
                            h.Invent.LootEquipement(ListeEquipement.GenererLootEquipement());
                        }                       
                        break;
                    case 8: //COMBAT BOSS (DEBUG)
                        if (debugMode == true)
                        {
                            boss = true;
                            Combat(h, boss);
                        }
                        break;
                    case 9: // HEROS SURPUISSANT (DEBUG)
                        if (debugMode == true)
                        {
                            h.AugmenterEndu(50);
                            h.AugmenterFor(50);
                            h.AugmenterPv(100);
                        }
                        break;
                    default:
                        Console.WriteLine("Vous n'avez pas fait le bon choix !");
                        break;
                }
            }
        }

        private void Forge(Hero h)
        {
            Console.Clear();
            Console.WriteLine("\n\nAvez vous tout ce qu'il faut pour la création de la clé ouvrant les portes du grand mechant?\n");
            Console.WriteLine(" - " + h.Emeraude + " émeraude /1");
            Console.WriteLine(" - " + h.Rubis + " rubis /1");
            Console.WriteLine(" - " + h.Saphir + " saphir /1");
            Console.WriteLine(" - " + h.Amethyste + " améthyste /1");
            Console.WriteLine(" - " + h.Fer + " fers /50");
            Console.WriteLine(" - " + h.Or + " pièces d'or /100");

            if(h.Emeraude >=1 && h.Rubis >= 1 && h.Saphir >= 1 && h.Amethyste >= 1 && h.Fer >=50 && h.Or >= 100)
            {
                Console.WriteLine("\n1. Parfait ! <Forger la clé>");
                Console.WriteLine("<Enter pour annuler>");
                int.TryParse(Console.ReadLine(), out int choix);
                if(choix == 1)
                {
                    h.Cle++;
                    h.Emeraude--; h.Rubis--; h.Saphir--; h.Amethyste--; h.Fer -= 50; h.Or -= 100;
                    Console.Clear();
                    Console.WriteLine("\n\n Félicitations, vous pouvez maintenant tenter de détruire le grand méchant !");
                    Console.WriteLine("<Enter pour continuer>");
                    Console.ReadLine();
                }
                else Console.ReadLine();
            }
            else
            {
                Console.WriteLine("\nRevenez quand vous aurez tous les composants");
                Console.WriteLine("<Enter pour annuler>");
                Console.ReadLine();
            }
            
        }

        private static void Aventure(Hero h, bool boss)
        {
            Random rand = new Random();
            int roll = rand.Next(1, 11);
            switch (roll)
            {
                case 1: case 2: case 3: case 4: case 5: case 6: case 7: case 8: Combat(h, boss); break;
                case 9: Marchand(h, true); break;
                case 10: Coffre(h); break;
            }
            
        }

        private static void Coffre(Hero h)
        {
            Console.Clear();

            int or = 0; int cuir = 0; int fer = 0; int s = 0; int a = 0; int r = 0; int e = 0;

            Random rand = new Random();
            int rareteCoffre = rand.Next(1, 4);

            bool c2 = false; bool c3 = false;
            Equipement coffreOuvert1 = ListeEquipement.GenererLootEquipement();
            Equipement coffreOuvert2 = ListeEquipement.GenererLootEquipement();
            Equipement coffreOuvert3 = ListeEquipement.GenererLootEquipement();

            string rarete = "";
            
            switch (rareteCoffre)
            {
                case 1: 
                    rarete = "ordinaire"; 
                    or = rand.Next(1, 5); cuir = rand.Next(1, 3);
                    break;
                case 2: 
                    rarete = "rare";
                    c2 = true;
                    or = rand.Next(3, 10); cuir = rand.Next(2, 5); fer = rand.Next(1, 3);
                    break;
                case 3: 
                    rarete = "épique";
                    c2 = true;
                    c3 = true;
                    or = rand.Next(10, 15); cuir = rand.Next(5, 8); fer = rand.Next(3, 6);

                    int randGemme = rand.Next(1, 8);
                    switch (randGemme)
                    {
                        case 1: s++; break;
                        case 2: e++; break;
                        case 3: a++; break;
                        case 4: r++; break;
                        default: break;
                    }
                    break;
            }
            Console.WriteLine("\n\n Après des heures de recherche,\n vous trouvez un coffre " + rarete + " caché dans la forêt");
            Console.WriteLine("\n<Enter pour l'ouvrir>");
            Console.ReadLine();

            ConsoleColor saveColor = Console.ForegroundColor;
            Console.WriteLine("\n\n Vous avez trouvé :");

            if (!(coffreOuvert1 is null))
            {
                Graphisme.ColorRarete(coffreOuvert1.Rarete);
                Console.WriteLine("\t" + coffreOuvert1.ToString() + ",");
                h.Invent.LootEquipement(coffreOuvert1);
            }
            if (c2 == true)
            {
                Graphisme.ColorRarete(coffreOuvert2.Rarete);
                Console.WriteLine("\t" + coffreOuvert2.ToString() + ",");
                h.Invent.LootEquipement(coffreOuvert2);
            }
            if (c3 == true)
            {
                Graphisme.ColorRarete(coffreOuvert3.Rarete);
                Console.WriteLine("\t" + coffreOuvert3.ToString() + ",");
                h.Invent.LootEquipement(coffreOuvert3);
            }

            Console.ForegroundColor = saveColor;

            if (cuir > 0) { Console.WriteLine("\t" + cuir + " morceau(x) de cuir,"); }
            if (fer > 0) { Console.WriteLine("\t" + fer + " lingot(s) de fer,"); }
            if (s > 0) { Console.WriteLine("\t" + s + " saphir,"); }
            if (a > 0) { Console.WriteLine("\t" + a + " améthyste,"); }
            if (e > 0) { Console.WriteLine("\t" + e + " émeraude,"); }
            if (r > 0) { Console.WriteLine("\t" + r + " rubis,"); }
            if (or > 0) { Console.WriteLine("\t" + or + " pièce(s) d'or.\n"); }

            h.Depouiller(cuir, or, fer, 0, s, r, a, e);
            Console.ReadLine();
        }

        private static void Marchand(Hero h, bool ambulant)
        {

            if (listMarchandiseAmbulant != null) listMarchandiseAmbulant.RemoveRange(0, listMarchandiseAmbulant.Count);  //Si un marchand ambulant a déjà été rencontré, clean la liste                                                                                                                         
            if (ambulant == true) listMarchandiseAmbulant = ListeEquipement.GenererListeMarchandAmbulant(); 
            
            void AffichageObjetsMarchand()
            {
                Console.Clear();

                if (ambulant == false) Console.WriteLine("\n\nBienvenue au bazar !\nJe vous proposerai de nouveaux objets une fois que vous aurez vidé mon stock !");
                else Console.WriteLine("\n\n< Vous croisez un marchand ambulant! >\nEspérons que vous avez assez d'or.");

                Console.WriteLine("Or disponible : " + h.Or);
                Console.WriteLine("\n\nObjets à vendre :\n");

                ConsoleColor saveColor = Console.ForegroundColor;

                if (ambulant == false)
                {
                    for (int i = 0; i < listMarchandise.Count; i++)
                    {
                        Graphisme.ColorRarete(listMarchandise[i].Rarete);
                        Console.WriteLine((i + 1) + ". " + listMarchandise[i].ToString() + "\n\t[Prix : " + listMarchandise[i].Or + " pièce(s) d'or]\n");
                    }
                }
                else
                { 
                    for (int i = 0; i < listMarchandiseAmbulant.Count; i++)
                    {
                        Graphisme.ColorRarete(listMarchandiseAmbulant[i].Rarete);
                        Console.WriteLine((i + 1) + ". " + listMarchandiseAmbulant[i].ToString() + "\n\t[Prix : " + listMarchandiseAmbulant[i].Or + " pièce(s) d'or]\n");
                    }
                }
                Console.ForegroundColor = saveColor;
            }

            AffichageObjetsMarchand();

            Console.WriteLine("<Choisir un objet dans la liste par son numéro pour l'acheter>");

            //cuir
            Console.WriteLine("\n---------------\nJe rachète également vos paquets de cuir. (8 pièces le paquet de 10 cuirs)");
            Console.WriteLine($"Vous avez {h.Cuir} cuir(s) en stock!");
            if(h.Cuir >=10) Console.WriteLine("\n9.<Vendre un paquet>");

            Console.WriteLine("\n<Enter pour continuer l'aventure>");
            int.TryParse(Console.ReadLine(), out int choixAchat);

            if (ambulant == false)
            { 
                if(choixAchat == 9 && h.Cuir >=10)
                {
                    h.Cuir -= 10; h.Or += 8;
                    Marchand(h, false);
                }

                if (choixAchat > 0 && choixAchat <= listMarchandise.Count)
                {
                    if (h.Or >= listMarchandise[choixAchat-1].Or)
                    {
                        Inventaire.listE.Add(listMarchandise[choixAchat - 1]);
                        h.Or -= listMarchandise[choixAchat - 1].Or;
                        string tempNom = listMarchandise[choixAchat - 1].Nom;
                        listMarchandise.RemoveAt(choixAchat - 1);

                        Console.Clear();
                        AffichageObjetsMarchand();

                        Console.WriteLine("\n\nVous avez acheté " + tempNom + "\n <Enter pour continuer>");
                        Console.ReadLine();

                        Marchand(h, false);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n\n\nLa maison ne fait pas crédit! \n Aurevoir.");
                        Console.ReadLine();
                    }
                }
            }
            else
            {
                if (choixAchat > 0 && choixAchat <= listMarchandise.Count)
                {
                    if (h.Or >= listMarchandiseAmbulant[choixAchat - 1].Or)
                    {
                        Inventaire.listE.Add(listMarchandiseAmbulant[choixAchat - 1]);
                        h.Or -= listMarchandiseAmbulant[choixAchat - 1].Or;
                        string tempNom = listMarchandiseAmbulant[choixAchat - 1].Nom;
                        listMarchandiseAmbulant.RemoveAt(choixAchat - 1);

                        Console.Clear();
                        AffichageObjetsMarchand();

                        Console.WriteLine("\n\nVous avez acheté " + tempNom + "\n <Enter pour continuer>");
                        Console.ReadLine();

                        Marchand(h, false);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n\n\nLa maison ne fait pas crédit! \n Aurevoir.");
                        Console.ReadLine();
                    }
                }
                listMarchandiseAmbulant.RemoveRange(0, listMarchandiseAmbulant.Count);
            }
        }

        private static void GestionInventaire(Hero h, Menu menu)
        {
            h.Invent.AfficherInventaire();

            menu.MenuInventaire1(h.Invent);
            int.TryParse(Console.ReadLine(), out int choixInv);

            if (choixInv > 0 && choixInv <= h.Invent.NbObjetsInventaire())
            {
                Console.Clear();
                menu.MenuInventaire2(choixInv);
                int.TryParse(Console.ReadLine(), out int actionInv);

                switch (actionInv)
                {
                    case 1: Equiper(choixInv, h); break;
                    case 2: Recyclage(choixInv, h, false); break; //recyclage
                    case 3: Recyclage(choixInv, h, true); break; //vente
                    default: GestionInventaire(h, menu);  break;                   
                }
            }
            else if (choixInv>0) GestionInventaire(h, menu);
            else  menu.MenuPrincipal(cptVictoire, h, debugMode); 
        }

        private static void Recyclage(int choixInv, Hero h, bool vente)
        {
            int or = 0;

            Console.WriteLine("\n Choix définitif ! Etes vous sûr ?");
            Console.WriteLine("1. <Oui, je suis sur !>");
            Console.WriteLine("2. <Non, revenir à l'inventaire>");
            int.TryParse(Console.ReadLine(), out int choix);          
            if (choix == 1)
            {
                if (vente == false)  //RECYCLAGE
                {
                    GenererLootRecyclage(choixInv, h);
                    h.Invent.RemoveEquipement(choixInv - 1);
                    GestionInventaire(h, menu);
                }
                else //VENTE
                {
                    or = h.Invent.GetEquipement(choixInv).Or / 3;
                    h.Depouiller(0, or, 0, 0, 0, 0, 0, 0);

                    Console.WriteLine("\nEn vendant cet objet vous recevez " + or + "pièce(s) d'or");
                    Console.ReadLine();
                    Console.WriteLine("\n\t <Enter pour continuer");

                    h.Invent.RemoveEquipement(choixInv - 1);
                    GestionInventaire(h, menu);
                }  
            }
            else  GestionInventaire(h, menu); 

        }

        public static void GenererLootRecyclage(int choixInv, Hero h)
        {
            int fer = 0; int cuir = 0;  int s = 0; int r = 0; int a = 0; int e = 0;

            Random rand = new Random();

            if (h.Invent.GetEquipement(choixInv).Fer > 0) fer = (h.Invent.GetEquipement(choixInv).Fer / 4) + rand.Next(1,3);
            if (h.Invent.GetEquipement(choixInv).Cuir > 0) cuir = (h.Invent.GetEquipement(choixInv).Cuir / 4) + rand.Next(1, 3);

            int rollGemme = rand.Next(1, 101); bool lootGemme = false;
            int tempRarete = h.Invent.GetEquipement(choixInv).Rarete;
            if (h.Invent.GetEquipement(choixInv) is Potion) tempRarete +=10;
            switch (tempRarete)
            {
                case 3: if (rollGemme > 50) lootGemme = true;
                    break;
                case 4: if (rollGemme > 25) lootGemme = true;
                    break;
                case 5: if (rollGemme > 10) lootGemme = true;
                    break;
            }
            if(lootGemme == true)
            {
                int quelleGemme = rand.Next(1, 5);
                switch (quelleGemme)
                {
                    case 1: s++; break; 
                    case 2: r++; break; 
                    case 3: a++; break; 
                    case 4: e++; break; 
                }
            }
            if (cuir > 0 || fer > 0 || s > 0 || r > 0 || a > 0 || e > 0)
            {
                Console.WriteLine("\nEn recyclant cet objet vous recevez :");
                if (cuir > 0) { Console.WriteLine("\t" + cuir + " morceau(x) de cuir"); }
                if (fer > 0) { Console.WriteLine("\t" + fer + " lingot(s) de fer"); }
                if (s > 0) { Console.WriteLine("\tun saphir!"); }
                if (r > 0) { Console.WriteLine("\tun rubis!"); }
                if (a > 0) { Console.WriteLine("\tune améthyste!"); }
                if (e > 0) { Console.WriteLine("\tune émeraude!"); }
            }
            else Console.WriteLine("\nMalheureusement vous n'arrivez pas à extraire quoi que ce soit de l'objet");

            Console.ReadLine();
            Console.WriteLine("\n\t <Enter pour continuer");

            h.Depouiller(cuir, 0 , fer, 0 , s, r, a, e);
        }

        private static void Equiper(int choixInv, Hero h)
        {

            h.Invent.ChangerEquipement(choixInv - 1, h);
            GestionInventaire(h, menu);
        }

        private static Hero CreateHero()
        {
            menu.MenuRaceHero();
            int.TryParse(Console.ReadLine(), out int typeHero);

            switch (typeHero)
            {
                case 1:
                    h = new Humain();
                    break;
                default:
                    h = new Nain();
                    break;
            }

            //h.Experience = 2500;
            //h.Or = 200;

            return h;
        }

        private static void Combat(Hero h, bool boss)
        {
            Console.Clear();
            Monstre m;

            if (boss == true) m = GenererBoss(h);
            else m = GenererMonstre(h);

            menu.MenuCombat();
            int.TryParse(Console.ReadLine(), out int menuCombat);
            if (menuCombat == 2)
            {
                h.Or = Fuir(h);
                Console.ReadLine();
            }
            else
            {
                bool combat = true;
                int heroPv = h.Pv;
                int monstrePv = m.Pv + h.BonusEquiDegats;

                Console.Clear(); Console.WriteLine("\n\t(--)  COMBAT  (--)\n");

                while (combat == true)
                {

                    //ATTAQUE DU HEROS
                    int degatsHero = h.Frappe(1,5);
                    Console.WriteLine($"Votre héros inflige {degatsHero} (+{h.BonusEquiDegats}) points de dégats au monstre.");
                    monstrePv = monstrePv - degatsHero - h.BonusEquiDegats;
                    if (monstrePv > 0)
                    {
                        Console.WriteLine("Il lui reste " + monstrePv + " points de vie.\n");
                    }
                    else
                    {
                        ConsoleColor couleurG = Graphisme.ColorGreen();
                        Console.WriteLine("\n\t(--) Le monstre " + m.ToString() + " est mort (--)\n");
                        Console.ForegroundColor = couleurG;

                        combat = false;
                        GenererLoot(m);
                        h.SeReposer();
                        heroPv = h.Pv;
                        cptVictoire++;

                        Console.WriteLine("<Enter pour la suite>");
                        Console.ReadLine();
                    }

                    //ATTAQUE DU MONSTRE
                    if (heroPv > 0 && monstrePv > 0)
                    {
                        int tempDegatsMax = 5 + (h.Niveau/3);
                        int tempDegatsMin = 1 + tempDegatsMax/3; if (tempDegatsMax <= 5) tempDegatsMin = 1; 


                        int degatsMonstre = m.Frappe(tempDegatsMin,tempDegatsMax) + (h.BonusEquiProtect /2);
                        Console.WriteLine("Le monstre " + m.ToString() + " inflige " + degatsMonstre + " (-" + h.BonusEquiProtect + ")" + " points de dégats à votre héros.");
                        heroPv = heroPv - degatsMonstre + h.BonusEquiProtect;
                        if (heroPv > 0)
                        {                          
                            if (heroPv < (h.Pv /3) || heroPv < 6)
                            {
                                ConsoleColor couleurR = Graphisme.ColorRed();
                                Console.WriteLine("Il vous reste " + heroPv + " points de vie.\n");
                                Console.ForegroundColor = couleurR;
                                if (!(h.Invent.PotionEquipee == null)) heroPv = UtilisationPotion(h,heroPv);
                            }
                            else Console.WriteLine("Il vous reste " + heroPv + " points de vie.\n");
                        }
                        else
                        {

                            //GESTION DE LA MORT

                            ConsoleColor couleurR = Graphisme.ColorRed();
                            Console.WriteLine("\n(--) Vous êtes mort après " + cptVictoire + " victoires ! (--)\n");
                            Console.ForegroundColor = couleurR;

                            if (h.Emeraude > 0 || h.Rubis > 0 || h.Saphir > 0 || h.Amethyste > 0)
                            {
                                Console.WriteLine("\nVous pouvez sacrifier l'une de vos gemmes pour revenir à la vie.");
                                heroPv = h.Pv;

                                if (h.Emeraude > 0) Console.WriteLine($"1. <Détruire une émeraude> ({h.Emeraude} en réserve)");
                                if (h.Rubis > 0) Console.WriteLine($"2. <Détruire un rubis> ({h.Rubis} en réserve)");
                                if (h.Saphir > 0) Console.WriteLine($"3. <Détruire un saphir> ({h.Saphir} en réserve)");
                                if (h.Amethyste > 0) Console.WriteLine($"4. <Détruire une améthyste> ({h.Amethyste} en réserve)");
                                Console.WriteLine("Ne vous trompez pas, ou vous mourrez !");

                                int.TryParse(Console.ReadLine(), out int choix);
                                if (h.Emeraude > 0 && choix == 1) h.Emeraude--;
                                else if (h.Rubis > 0 && choix == 2) h.Rubis--;
                                else if (h.Saphir > 0 && choix == 3) h.Saphir--;
                                else if (h.Amethyste > 0 && choix == 4) h.Amethyste--;
                                else {
                                    Console.Clear();
                                    jeu.Start();
                                }
                                
                                Console.Clear();
                                combat = false;
                            }
                            else
                            {
                                combat = false;
                                cptVictoire = 0;
                                h.Invent.CleanInventaire();
                                Console.WriteLine("<Enter pour Rejouer!>");
                                Console.ReadLine();
                                Console.Clear();
                                jeu.Start();
                            }

                        }
                    }
                }
            }
        }

        private static int UtilisationPotion(Hero h, int heroPv)
        {
            Console.WriteLine("\nVos points de vie sont bas ! Voulez vous boire votre potion ?");
            Console.WriteLine("1. Non, je vais tenter ma chance !");
            Console.WriteLine("2. Oui ! <Récupérer " + h.Invent.PotionEquipee.Soin + " points de vie.");
            //Console.WriteLine("3. Je pense plutôt à fuir ce combat ! (Perte de toute l'or)");
            int.TryParse(Console.ReadLine(), out int choix);
            switch (choix)
            {
                case 1: break;
                case 2: 
                    heroPv += h.Invent.PotionEquipee.Soin;
                    h.Invent.BoirePotion();
                    Console.Clear();
                    Console.WriteLine("\n Vous avez maintenant " + heroPv + " points de vie.");
                    Console.WriteLine("<Enter pour continuer le combat>");
                    Console.ReadLine();
                    break;
               // case 3: fuir(h); break;
            }
            return heroPv;

        }

        private static int Fuir(Hero h)
        {

            int temp = (h.Or / 5);

            if (h.Or == 0)
            {
                Console.WriteLine("\nVous fuyez ! Par chance vous n'avez pas d'or à perdre !");
            }
            else
            {
                if (temp > h.Or)
                {
                    temp = h.Or;
                    h.Or = h.Or - temp;
                }
                else if (temp == h.Or)
                {
                    h.Or = 0;
                }
                else
                {
                    temp++;
                    h.Or = h.Or - temp;
                }

                Console.WriteLine("\nDans votre fuite vous avez perdu " + (temp) + " pièce(s) d'or !");
            }

            Console.WriteLine("\n<Enter pour continuer>");
            return h.Or;
        }

        private static Equipement GenererLoot(Monstre m)
        {
            int or = 0;
            int cuir = 0;
            int fer = 0;
            int xp = 0;
            
            Equipement equi = null; 
            Random randLootRare = new Random();
            int lootRare = randLootRare.Next(1, 7);
            if (lootRare == 5 || lootRare == 6) equi = ListeEquipement.GenererLootEquipement();

            if (m is Orque)
            {
                Orque o = (Orque)m;
                or = o.Or;
                xp = o.DonneExp;
            }
            else if (m is Loup)
            {
                Loup l = (Loup)m;
                cuir = l.Cuir;
                xp = l.DonneExp;
            }
            else if (m is Dragonnet)
            {
                Dragonnet d = (Dragonnet)m;
                or = d.Or;
                cuir = d.Cuir;
                xp = d.DonneExp;
            }
            else if (m is Araignee)
            {
                Araignee a = (Araignee)m;
                cuir = a.Cuir;
                xp = a.DonneExp;
            }
            else if (m is Bandit)
            {
                Bandit b = (Bandit)m;
                or = b.Or;
                xp = b.DonneExp;
            }

            Console.WriteLine("Vos récompenses : ");
            if (!(equi is null))
            {
                ConsoleColor saveColor = Console.ForegroundColor;
                Graphisme.ColorRarete(equi.Rarete);
                    Console.WriteLine("\t" + equi.ToString() + ",");
                Console.ForegroundColor = saveColor;
  
                h.Invent.LootEquipement(equi);
            }
            if (cuir > 0) { Console.WriteLine("\t" + cuir + " morceau(x) de cuir,"); }
            if (fer > 0) { Console.WriteLine("\t" + fer + " lingot(s) de fer,"); }
            if (or > 0) { Console.WriteLine("\t" + or + " pièce(s) d'or,"); }
            if (xp > 0) { Console.WriteLine("\t" + xp + " points d'expérience.\n"); }

            h.Depouiller(cuir, or, fer, xp, 0, 0, 0, 0);
            return equi;

        }

        private static Monstre GenererBoss(Hero h)
        {
            Monstre m = new Bandit();
            m.AugmenterPv(500); m.AugmenterFor(500);
            m.AffichageNiveauMonstre();
            return m;
        }

        private static Monstre GenererMonstre(Hero h)
        {
            Random rand = new Random();
            int quelMonstre = rand.Next(1, 8);

            if (h.Niveau > 4 && h.Niveau < 10) { quelMonstre++; }
            else if (h.Niveau >= 10 && h.Niveau < 15) { quelMonstre += 2; }
            else if (h.Niveau >= 15 && h.Niveau < 20) { quelMonstre += 3; }

            Console.WriteLine("");

            Monstre m;
            switch (quelMonstre)
            {
                case 1:
                case 2:
                    m = new Loup();
                    break;
                case 3:
                case 4:
                    m = new Orque();
                    break;
                case 5:
                case 6:
                    m = new Dragonnet();
                    break;
                case 7:
                    m = new Araignee();
                    break;
                default:
                    m = new Bandit();
                    break;
            }

            if (h.Niveau > 4 && h.Niveau < 10) { m.AugmenterEndu(5); m.AugmenterFor(4); }
            else if (h.Niveau >= 10 && h.Niveau < 15) { m.AugmenterEndu(8); m.AugmenterFor(6); }
            else if (h.Niveau >= 15 && h.Niveau < 20) { m.AugmenterEndu(16); m.AugmenterFor(12); }
            else if (h.Niveau >= 20 && h.Niveau < 25) { m.AugmenterEndu(25); m.AugmenterFor(20); }
            else if (h.Niveau >= 25) { m.AugmenterEndu(40); m.AugmenterFor(40); }

            Console.WriteLine("(--) Vous rencontrez un monstre " + m.ToString() + " ! (--)");

            m.AffichageNiveauMonstre();

            return m;
        }


    }
}

