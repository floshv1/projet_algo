using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace projet_algo
{
    public class Joueur
    {
        private string nom ; 
        private List<string> motsTrouves; 
        private int scoresPlateau;


        public string Nom{
            get{return this.nom ;}
            set{ this.nom = value;}
        } 

        public List<string> MotsTrouves{
            get{return this.MotsTrouves;}
            set{this.MotsTrouves = value;}
        }

        public int ScoresPlateau{
            get{return this.scoresPlateau;}
            set{this.scoresPlateau = value;}
        }

        public Joueur(string nom){
            if(nom != null){
                this.nom  = nom ; 
                this.motsTrouves = new List<string>();
                this.scoresPlateau = 0;
            }
            else{
                Console.WriteLine("Attention aucun nom d'utilisateur entré");
            }
        }

        public void AddMot(string mot){
            motsTrouves.Add(mot);
        }

        public string toString(){
            string strMotsTrouves =""; 
            foreach(string mot in motsTrouves){
                strMotsTrouves += mot + "; "; 
            }
            return("Nom : "+this.nom+"\nMots Trouvés : "+strMotsTrouves+"\nScores Plateaux : "+ scoresPlateau);
        }

        public string toFile(){
            string strMotsTrouves =""; 
            foreach(string mot in motsTrouves){
                strMotsTrouves += mot + ","; 
            }
            return($"{this.nom};{scoresPlateau};{strMotsTrouves}" );
        }

        public Joueur StringToJoueur(string ligne){
            string[] lignes = ligne.Split(';');
            Joueur joueur = new Joueur(lignes[0]);
            
            joueur.scoresPlateau = int.Parse(lignes[1]);

            string[] strMotsTrouves = lignes[2].Split(',');
            foreach(string mot in strMotsTrouves){
                joueur.motsTrouves.Add(mot);
            }
            return joueur;
        }

        public void AddScore(int val){
                scoresPlateau += val;
        }

        public bool Contient(string mot){
            if(motsTrouves.Contains(mot)){
                return true ; 
            }
            else{
                return false;
            } 

            
        }


    }
}
