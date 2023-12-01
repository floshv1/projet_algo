using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_algo
{
    public class Deplacement
    {
        int ligne ; 
        int colonne; 

        public int Ligne{
            get{return this.ligne;}
            set{this.ligne = value;}
        }
        public Deplacement(int ligne , int colonne){
            this.ligne = ligne ; 
            this.colonne = colonne; 
        }
        public void Gauche()
        {
            ligne = ligne ;
            colonne = colonne -1;
        }
        public void Droite(){
            ligne = ligne; 
            colonne = colonne +1; 
        }

        public void DiagGauche(){
            ligne = ligne -1;
            colonne = colonne -1; 
        }

        public void DiagDroite(){
            ligne = ligne -1;
            colonne = colonne +1;
        }
        public void Haut(){
            ligne = ligne -1;
            colonne = colonne; 
        }

        public string toString(){
            return ("Ligne : "+this.ligne+"\nColonne : "+this.colonne);
        }

    }
}