using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cpln.Enigmos.Enigmas.Components
{
    class Personnage
    {
        //Déclaration des constantes
        const int VITESSE_PLATEFORMES = 10;
        //La position du point ci-dessous
        Point positionMario = new Point(10, -100);
        //Déclaration des variales
        int iVitesse = 0;
        bool bDroite = false, bGauche = false, bSauter, bDepart = false;
        public void Personnage()
        {
            //Création des KeyUp et KeyDown
            KeyDown += new KeyEventHandler(ToucheEnfoncer);
            KeyUp += new KeyEventHandler(ToucheRelacher);
        }
        private void ToucheRelacher(object sender, KeyEventArgs e)
        {
            //Passent les bool en faux si les touches sont relachées
            switch (e.KeyCode)
            {
                case Keys.D:
                    bDroite = false;
                    break;
                case Keys.A:
                    bGauche = false;
                    break;
                case Keys.Space:
                    bSauter = false;
                    break;
            }
        }
        private void ToucheEnfoncer(object sender, KeyEventArgs e)
        {
            //assigne les touche si elles sont appuyées
            switch (e.KeyCode)
            {
                case Keys.D:
                    bDroite = true;
                    break;
                case Keys.A:
                    bGauche = true;
                    break;
                case Keys.Space:
                    if (e.KeyCode == Keys.Space && !bSauter)
                    {
                        bSauter = true;
                    }
                    break;
            }
        }
        private void timerMario_Tick(object sender, EventArgs e)
        {
            foreach (Panel Plateforme in ListPlateformes)
            {
                if (Collision(Plateforme)) //Teste les collision
                {
                    if (Plateforme.Top <= pnlImage.Bottom)
                    {
                        iVitesse = 0;
                        pnlImage.Location = new Point(pnlImage.Location.X, Plateforme.Top - pnlImage.Height);
                    }
                }
                //Aide fourni par arthur
                //Passe le bool à vrai si le personnage à dépacé la position 355
                if (!bDepart)
                {
                    if (pnlImage.Right > 355)
                        bDepart = true;
                }
                else Plateforme.Location = new Point(Plateforme.Location.X - VITESSE_PLATEFORMES, Plateforme.Location.Y); //Fait bouger les plateformes
            }
            if (bSauter == true & Collision())
                iVitesse = -15;
            positionMario = Position(); //reprends la formule de graviter
            if (bGauche == true)
            {
                positionMario.X -= 20;   //Permet le délacement du mario à gauche
                pnlImage.BackgroundImage = Image.FromFile(@".\images\mario2Gauche.png"); //Image du mario regardant à gauche
            }
            if (bDroite == true)
            {
                if (pnlImage.Right < EnigmaPanel.Right)
                    positionMario.X += 20;   //Permet le délacement du mario à droite
                pnlImage.BackgroundImage = Image.FromFile(@".\images\mario2Droite.png"); //Image du mario regardant à droite
            }
            pnlImage.Location = positionMario; //le panel prend les valeurs de la variable "positionMario"
            if (ListPlateformes.Last().Right <= this.Width)
                Ajoute_Plateforme(); //Reprends la formule Ajoute_Plateforme
            positionMario = Position(); //reprend la formule de graviter
            if (pnlImage.Top > EnigmaPanel.Bottom)
            {
                timerMario.Enabled = false;
                DialogResult dlg = MessageBox.Show("Dommage vous avez Perdu... Voulez-vous recommencer ?", "Perdu...", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dlg == DialogResult.Yes)
                {
                    while (ListPlateformes.Count > 0) //Teste si le nombres de plateformes est plus élevé que 0
                    {
                        Supprimer_plateforme(Int32.MaxValue); //Supprime toutes les platefomres affichées
                    }
                    PlateformeAleatoire(); //reprend la création des platefomres aléatoires
                    pnlImage.Location = new Point(10, -100); //Place le personnage au position indiquée
                    timerMario.Enabled = true;
                    pnlImage.BackgroundImage = Image.FromFile(@".\images\mario2Droite.png"); //Image du mario regardant à droite
                    bDroite = false;
                    bGauche = false;
                    bSauter = false;
                    bDepart = false;
                }
                else
                    Application.Exit(); //Quitte l'application
            }
            if (pnlImage.Right < NiveauPiegeEnigmaPanel.Left)
            {
                timerMario.Enabled = false;
                DialogResult dlg = MessageBox.Show("Le mot mystère est : gauche", "Bravo !!!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private bool Collision(Panel plateforme)
        {
            //Détecte la colision
            if (plateforme.Bottom < pnlImage.Top)
                return false;
            if (plateforme.Right < pnlImage.Left)
                return false;
            if (plateforme.Top > pnlImage.Bottom)
                return false;
            if (plateforme.Left > pnlImage.Right)
                return false;
            return true;
        }

        private bool Collision()
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
        Point Position()
        {
            //formule de gravité (aide du prof)
            Point p = pnlImage.Location;
            p.Y = Math.Min(p.Y + 3 * iVitesse, 1000);
            iVitesse += 1;
            return p;
        }
    }
}
