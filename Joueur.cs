using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace projet_algo
{
    public class Joueur
    {
        #region Attributs

        /// <summary> Nom du joueur </summary>
        private string nom ; 

        /// <summary> Booléen qui indique si le joueur est en jeu </summary>
        bool enJeu;

        /// <summary> Liste des mots trouvés par le joueur </summary>
        private List<string> motsTrouves; 

        /// <summary> Scores du joueur sur le plateau </summary>
        private int scoresPlateau;

        /// <summary> Booléen qui indique si le joueur a passé son tour </summary>
        private bool skip;
        #endregion

        #region Propriétés

        /// <summary> Propriété du nom du joueur </summary>
        public string Nom{
            get{return this.nom ;}
            set{ this.nom = value;}
        } 
        
        /// <summary> Propriété du booléen qui indique si le joueur est en jeu </summary>
        public bool EnJeu{
            get{return this.enJeu;}
            set{this.enJeu = value;}
        }

        /// <summary> Propriété de la liste des mots trouvés par le joueur </summary>
        public List<string> MotsTrouves{
            get{return this.motsTrouves;}
            set{this.MotsTrouves = value;}
        }

        /// <summary> Propriété des scores du joueur sur le plateau </summary>
        public int ScoresPlateau{
            get{return this.scoresPlateau;}
            set{this.scoresPlateau = value;}
        }
        
        /// <summary> Propriété du booléen qui indique si le joueur a passé son tour </summary>
        public bool Skip{
            get{return this.skip;}
            set{this.skip = value;}
        }
        #endregion

        #region Constructeurs
        
        /// <summary> Constructeur de la classe Joueur </summary>
        /// <param name="nom"> Nom du joueur </param>
        /// <returns> Un nouveau joueur </returns>
        public Joueur(string nom){
            if(nom != null){
                this.nom  = nom ; 
                this.motsTrouves = new List<string>();
                this.enJeu = false;
                this.skip = false;
                this.scoresPlateau = 0;
            }
            else{
                Console.WriteLine("Attention aucun nom d'utilisateur entré");
            }
        }
        #endregion

        #region Méthodes
        
        /// <summary> Méthode qui ajoute un mot à la liste des mots trouvés </summary>
        /// <param name="mot"> Mot à ajouter </param>
        /// <returns> La liste des mots trouvés </returns>
        public void AddMot(string mot){
            motsTrouves.Add(mot);
        }

        /// <summary> Méthode qui affiche les caracteristiques d'un joueur </summary>
        /// <returns> Caractéristiques d'un joueur </returns>
        public string toString(){
            string strMotsTrouves =""; 
            foreach(string mot in motsTrouves){
                strMotsTrouves += mot + "; "; 
            }
            return("Nom : "+this.nom+"\nMots Trouvés : "+strMotsTrouves+"\nScores Plateaux : "+ scoresPlateau);
        }

        /// <summary> Méthode qui ecrit une ligne de facon à etre mis dans un fichier </summary>
        /// <returns> Ligne contenant les informations du joueur </returns>
        public string toFile(){
            string strMotsTrouves =""; 
            foreach(string mot in motsTrouves)
            {
                if(mot != motsTrouves.Last())
                    strMotsTrouves += mot + ",";
                else strMotsTrouves += mot; 
            }
            return($"{this.nom};{this.enJeu};{skip};{scoresPlateau};{strMotsTrouves}" );
        }

        /// <summary> Méthode qui convertit une ligne de fichier en joueur </summary>
        /// <param name="ligne"> Ligne du fichier </param>
        /// <returns> Le joueur </returns>
        public static Joueur StringToJoueur(string ligne){
            string[] lignes = ligne.Split(';');
            Joueur joueur = new Joueur(lignes[0]);
            joueur.enJeu = bool.Parse(lignes[1]);
            joueur.skip = bool.Parse(lignes[2]);
            joueur.scoresPlateau = int.Parse(lignes[3]);

            string[] strMotsTrouves = lignes[4].Split(',');
            foreach(string mot in strMotsTrouves){
                joueur.motsTrouves.Add(mot);
            }
            return joueur;
        }
        
        /// <summary> Méthode qui ajoute les scores d'un mot au score du joueur </summary>
        /// <param name="mot"> Mot à ajouter </param>
        /// <returns> Les scores du joueur </returns>
        public void AddScore(string mot){
                scoresPlateau += PointsMot(mot);
        }
        
        /// <summary> Méthode qui calcule les points d'un mot </summary>
        /// <param name="mot"> Mot à calculer </param>
        /// <returns> Les points du mot </returns>
        public static int PointsMot(string mot)
        {
            try 
            {
                int scores  = 0 ; 
                int i = 0;
                mot = mot.ToUpper();
                string[]  lignes = File.ReadAllLines("Lettre.txt");
                for (i = 0; i < mot.Length; i++)
                {
                    foreach(string ligne in lignes)
                    {
                        string[] mots = ligne.Split(',');
                        if(mot[i] == char.Parse(mots[0]))
                        {
                            scores += int.Parse(mots[2]);
                        }
                    }
                }
                return scores;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur : " + e.Message);
                return 0;
            }
            
        }
        
        /// <summary> Méthode qui vérifie si un mot est dans la liste des mots trouvés </summary>
        /// <param name="mot"> Mot à vérifier </param>
        /// <returns> Si le mot est dans la liste des mots trouvés </returns>
        public bool Contient(string mot){
            if(motsTrouves.Contains(mot)){
                return true ; 
            }
            else{
                return false;
            } 
        }
        #endregion
    }
}
