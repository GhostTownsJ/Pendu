using System;
using System.Collections.Generic; 


namespace PenduA
{
    public class Mot //définit le plan de construction et sert a créer plusieurs objets
    {
        //méthode avec un niveau de protection privé ou public
        private string _mot; 
        public string mots => _mot; // equivalent du get/set , mais ici que GET

        private int _tailleDuMot;
        public int tailleMot => _tailleDuMot;

        public Mot(string val) //valeur du mot 
        {
            _mot = val;
            _tailleDuMot = val.Length;
        }
    }
    class Program
    {
        //Déclarer les variables dont je vais me servir dans mon programme, type : int, bool, tableaux ...
        private static string[] tableauxMots = { "PLANTE", "ROSE", "NATURE", "RACINE", "PURIFICATEUR", "ILE", "DILIGENCE", "DIPLOME", "PEINTRE", "COBRA", "WEB", "REINE", "PROTECTION" }; // tableau contenant plusieurs mots
        static List<string> historiqueLettres = new List<string>(); //liste pour stocker les lettres utilisé par le joueur
        static string lettreTeste = ""; //variables string pour recuperer les lettres tester du joueur
        private static Mot choisirMot; // choisirMot fait partie de la Class Mot, elle contient le mot aléatoire
        private static char[] lettres; // un tableau de charactères qui contient les lettres du mot choisi présent dans la variable choisirMot
        private static int vie; // nombre de vie du joueur 
        static bool Gagner; //booléan gagner pour la fonction gameover 
        static DessinPendu dessin = new DessinPendu(); //on accède a la classe dessin
        static void Main(string[] args) // Equivalent fonction Start 
        {
            Pendu();
        }

        static void Pendu() // Affichage nom du Jeu & règles du Jeu 
        {
            //Règles du Jeu
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("*                          Bienvenue dans le Jeu du Pendu !                                              *");
            Console.WriteLine("*  Les règles sont simples, vous devez trouver toutes les lettres du mot a deviner avant d'être Pendu... *");
            Console.WriteLine("*               Il n'y a que des majuscules et aucun accent ni caratère spéciale.                        *");
            Console.WriteLine("*            Vous avez un nombre d'essais limité ! Alors attention réfléchissez bien !                   *");
            Console.WriteLine("*                    Bonne chance et que le sort puisse vous être favorable...                           *");
            Console.WriteLine("**********************************************************************************************************");
            Console.ResetColor();

            vie = 7; // le joueur dispose de 7 vie
            ChoisirMot(); // Démarrer la fonction qui choisi le mot
            Console.WriteLine("Le mot contient : " + lettres.Length + " lettres."); // Fonction de debug qui nous indique combien il y a de lettres dans le mot
            
            while (GameOver() == false) //tant que il n'y a pas GAMEOVER alors 
            { 
              Console.WriteLine(AfficherMot()); //on continue d'afficher le mot avec les tiret necessaires et les lettres trouvé 
              VerificationLettre(LettreDuJoueur()); //on continue de demander au joueur de jouer donc de donner une lettre
            }
            ReponseResultat(); //si le joueur a trouver le mot alors il a gagné
            Restart(); //on lui demande si il veut rejouer

        }

        static void ChoisirMot() // Fonction qui permet de choisir aléatoirement un mot dans mon tableau de mots 
        {
            choisirMot = new Mot(tableauxMots[new Random().Next(0, tableauxMots.Length)]); // variable qui contient un mot choisi de manière aléatoire dans le tableau

            // permet de remplacer toutes les lettres du mot par des Tirets
            lettres = new char[choisirMot.tailleMot]; // on récupère la taille du mot
            for (int i = 0; i < lettres.Length; i++) // pour toutes les lettres dans le mot on inscrit un "_"
            {
                lettres[i] = '_';
            }

        }

        static string AfficherMot() // fonction qui permet d'afficher le mot 
        {
            string mot = new string(lettres); // nouvelle chaine de charactère qui contient toutes les lettres du mot à deviner
            return mot; // on renvoi la valeur mot 
        }

        static string LettreDuJoueur() //fonction qui va demander la lettre du joueur
        {
            string input;
            do
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Entrez votre lettre !");
                Console.ResetColor();
                input = Console.ReadLine();

                lettreTeste += input + " "; 
                historiqueLettres.Add(input);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Lettres utilisés: " +lettreTeste);
                Console.ResetColor();

            } while (input.Length != 1);
            return input;
        }

        static void VerificationLettre(string input) //fonction qui va verifier si la lettre est ou non dans le mot 
        {
            bool decouverteLettre = false; // booléan de l'état de base de la verif lettre 
            for (int i = 0; i < choisirMot.tailleMot; i++)
            {
                if (choisirMot.mots[i] == input[0])  // si le joueur decouvre une lettre alors:   
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Bravo la lettre est bien dans le mot !"); //dire au joueur qu'il a bon 
                    Console.ResetColor();
                    decouverteLettre = true;  //alors je passe le boolean a true 
                    lettres[i] = input[0]; //la lettre i est bien la meme que l'input du joueur
                }
            }

            if (decouverteLettre == false) // si la lettre est fausse alors: //alors le boolean reste a false
            {
                vie--; //il faut enlever une vie
                Console.WriteLine(dessin.penduDessin[vie]); //on accède au tableau et on affiche le dessin correspondant au niveau de vie

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERREUR !! Une vie en moins ! " +vie + " vies restantes"); //dire qu'il a une vie en moins et lui dire combien 
                Console.ResetColor();
            }
        }
        static void ReponseResultat() //fonction qui permet de dire au joueur si il a gagné ou non
        {
            if (Gagner == true) //si il a trouver le bon mot 
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Félicitations vous avez gagné ! Le mot a deviner était " + choisirMot.mots); //alors on lui dit qu'il a gagné + le mot afficher
                Console.ResetColor();
            }

            else //sinon il perd 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Dommage le mot était " + choisirMot.mots); //et le bon mot s'affiche
                Console.ResetColor();
            }
        }

        private static bool GameOver() //fonction qui si le joueur n'a plus de vie alors il a perdu 
        {
            if (vie <= 0 ) //si la vie est inferieur ou égal a 0 alors il perd
            {
                Gagner = false; //ce qui nous renvoie dans la fonction ReponseResultat et dans la boucle else
                return true; 
            }

            foreach (char lettre in lettres) //pour chaque character de lettre dans lettres 
            {
                if (lettre == '_') //si la lettre est egal a la bonne lettre caché 
                {
                    return false;  
                }
            }

            Gagner = true; 
            return true; 
        }

        private static void Restart() //fonction restart, si trop d'erreur alors plus de vie donc demande au joueur s'il veut rejouer et si il gagne egalement
        {
            ConsoleKey choix; //Spécifie les touches standard d’une console.
            do 
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Voulez vous rejouez ? Q = Quittez et R = Rejouez"); 
                Console.ResetColor();
                choix = Console.ReadKey(false).Key; 
            } while (choix != ConsoleKey.Q && choix != ConsoleKey.R); //tant que le choix n'est pas egal a Q ou R 


            if (choix == ConsoleKey.R) 
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(" Nouvelle Partie"); //recommencer le jeu
                Console.ResetColor();
                Pendu(); 
            }
            if (choix == ConsoleKey.Q )
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(" Fin du Jeu."); //fermer le jeu 
                Console.ResetColor();
            }
        }
    }
}