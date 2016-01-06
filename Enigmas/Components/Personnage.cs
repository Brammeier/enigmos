using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cpln.Enigmos.Enigmas.Components
{
    class Personnage : Panel
    {
        //Déclaration des variales
        private int iVitesse = 0;

        //La position du point ci-dessous
        private Point positionMario = new Point(10, -100);
        
        private bool bDroite = false, bGauche = false, bSauter, bDepart = false;
        
        private bool Collision(Panel plateforme)
        {
            //Détecte la colision
            if (plateforme.Bottom < this.Top)
                return false;
            if (plateforme.Right < this.Left)
                return false;
            if (plateforme.Top > this.Bottom)
                return false;
            if (plateforme.Left > this.Right)
                return false;
            return true;
        }
        public bool Collision(List<Panel> ListPlateformes)
        {
            //Prend les plateformes une à une
            foreach (Panel Plateforme in ListPlateformes)
            {
                if (Collision(Plateforme))
                {
                    Plateforme.BackColor = Color.Black; // Si le personnage touche une plateformes sa couleur devient noir
                    //Code fourni par le prof (consiste à détecter si la plateforme à déjà été tagé)
                    return true;
                }
            }
            return false;
        }
        public Point gravite()
        {
            //formule de gravité (aide du prof)
            Point p = this.Location;
            p.Y = Math.Min(p.Y + 3 * iVitesse, 1000);
            iVitesse += 1;
            return p;
        }
        public int getVitesse()
        {
            return iVitesse;
        }
        public void setVitesse(int vitesse)
        {
            iVitesse = vitesse;
        }
        public bool getDroit()
        {
            return bDroite;
        }
        public void setDroit(bool droit)
        {
            bDroite = droit;
        }
        public bool getGauche()
        {
            return bGauche;
        }
        public void setGauche(bool gauche)
        {
            bGauche = gauche;
        }
        public bool getSauter()
        {
            return bSauter;
        }
        public void setSauter(bool sauter)
        {
            bSauter = sauter;
        }
        public bool getDepart()
        {
            return bDepart;
        }
        public void setDepart(bool depart)
        {
            bDepart = depart;
        }
        public Point getPosition()
        {
            return positionMario;
        }
        public void setPosition(Point position)
        {
            positionMario = position;
        }
    }
}
