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
        private Dictionary<string, int> scoresPlateau;


        public string Nom{
            get{return this.nom ;}
            set{ this.nom = value;}
        } 

        public List<string> MotsTrouves{
            get{return this.MotsTrouves;}
            set{this.MotsTrouves = value;}
        }

        public Dictionary<string, int> ScoresPlateau{
            get{return this.scoresPlateau;}
            set{this.scoresPlateau = value;}
        }

        public Joueur(string nom){
            if(nom != null){
                this.nom  = nom ; 
                this.motsTrouves = new List<string>();
                this.scoresPlateau = new Dictionary<string, int>();
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
            string strScoresPlateaux = "";
            foreach(KeyValuePair<string,int> score in scoresPlateau){
                strScoresPlateaux += score.Key +": "+score.Value + "; ";
            }
            return("Nom : "+this.nom+"\nMots Trouvés : "+strMotsTrouves+"\nScores Plateaux : "+strScoresPlateaux);
        }

        public void AddScore(string nomPlateau, int val){
            if(scoresPlateau.ContainsKey(nomPlateau)){
                scoresPlateau[nomPlateau]+= val; 
            }
            else{
                scoresPlateau.Add(nomPlateau, val);
            }
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