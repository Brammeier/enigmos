using Cpln.Enigmos.Enigmas.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cpln.Enigmos.Enigmas
{
    class NiveauPiegeEnigmaPanel : EnigmaPanel
    {
        private Personnage pnlPersonnage = new Personnage();
        private Timer timerMario;

        List<Panel> ListPlateformes = new List<Panel>(); //Création d'une liste de panel
        Panel Plateforme;

        public NiveauPiegeEnigmaPanel()
        {
            //Trouvé sur trucs et astuces
            DoubleBuffered = true; //Gère le double buffering de la Form.
            System.Reflection.PropertyInfo aProp = typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance); // Crée la propriété qui sera utilisée dans la/les ligne(s) suivante(s).
            aProp.SetValue(pnlPersonnage, true, null); // pnlImage est le nom du panel qui scintille
        }
        private void Supprimer_plateforme(int limite)
        {
            if (ListPlateformes.First().Right < limite)
            {
                Controls.Remove(ListPlateformes.First());
                ListPlateformes.RemoveAt(0); //Efface la première plateforme si elle est plus petit que 0
            }
        }
        private void Ajoute_Plateforme()
        {
            //Initialisation des randoms
            Random r = new Random(); //Donne un chiffre aléatoire au random
            //Les variables prennent les valeurs aléatoires
            int iY = r.Next(ListPlateformes.Last().Top - PlateformesAleatoire.Y_MAX_DIFFERENCE, PlateformesAleatoire.Y_MAX);
            int iLongueur = r.Next(PlateformesAleatoire.LONGUEUR_MIN, PlateformesAleatoire.LONGUEUR_MAX);
            int iEpaisseur = r.Next(PlateformesAleatoire.HAUTEUR_MIN, PlateformesAleatoire.HAUTEUR_MAX);
            int iEspacement = r.Next(PlateformesAleatoire.ESPACEMENT_X_MIN, PlateformesAleatoire.ESPACEMENT_X_MAX);

            //Plateforme = new Panel(); //Crée un nouveau panel à la liste
            Plateforme.Location = new Point(ListPlateformes.Last().Right + iEspacement, iY); //Met le panel au coordonnées indiqué
            Plateforme.Size = new Size(iEpaisseur, 10); //le panel prend la taille du random
            Plateforme.BackColor = Color.White; //Le panel prend la couleur voulue
            Controls.Add(Plateforme); //Ajoute le panel à la liste
            Controls.SetChildIndex(Plateforme, 0); //Met la plateforme en première possition (tout devant)
            int CoordonneeDeLaDernierePlateforme = ListPlateformes.Last().Right;
            ListPlateformes.Add(Plateforme); //Ajoute une plateforme
            Supprimer_plateforme(0); //Supprime la platefomres en première position de la liste si elle sort de l'écran
        }
        private void FirstPlateforme()
        {
            Panel Plateforme = new Panel(); //crée un nouveau panel
            Plateforme.Location = new Point(0, 355); //Met le panel au coordonnées indiqué
            Plateforme.Size = new Size(234, 21); //Met le panel à la taille indiqué
            Plateforme.BackColor = Color.White; //Le panel prend la couleur voulue
            Controls.Add(Plateforme);//Ajoute le panel à la liste
            Controls.SetChildIndex(Plateforme, 0); //Met la plateforme en première possition (tout devant)
            ListPlateformes.Add(Plateforme); //Ajoute une plateforme
        }
        private void timerMario_Tick(object sender, EventArgs e)
        {
            if (pnlPersonnage.Collision(ListPlateformes)) //Teste les collision
            {
                if (Plateforme.Top <= pnlPersonnage.Bottom)
                {
                    pnlPersonnage.setVitesse(0);
                    pnlPersonnage.Location = new Point(pnlPersonnage.Location.X, Plateforme.Top - pnlPersonnage.Height);
                }
            }
            //Aide fourni par arthur
            //Passe le bool à vrai si le personnage à dépacé la position 355
            if (!pnlPersonnage.getDepart())
            {
                if (pnlPersonnage.Right > 355)
                    pnlPersonnage.setDepart(true);
            }
            else Plateforme.Location = new Point(Plateforme.Location.X - PlateformesAleatoire.VITESSE_PLATEFORMES, Plateforme.Location.Y); //Fait bouger les plateformes
            if (pnlPersonnage.getDroit() & Collision())
                pnlPersonnage.setVitesse(-15);
            positionMario = Position(); //reprends la formule de graviter
            if (pnlPersonnage.getGauche())
            {
                pnlPersonnage.setPosition.X(-20);   //Permet le délacement du mario à gauche
                pnlPersonnage.BackgroundImage = Image.FromFile(@".\images\mario2Gauche.png"); //Image du mario regardant à gauche
            }
            if (pnlPersonnage.getDroit())
            {
                if (pnlPersonnage.Right < EnigmaPanel.Right)
                    positionMario.X += 20;   //Permet le délacement du mario à droite
                pnlPersonnage.BackgroundImage = Image.FromFile(@".\images\mario2Droite.png"); //Image du mario regardant à droite
            }
            pnlPersonnage.Location = positionMario; //le panel prend les valeurs de la variable "positionMario"
            if (ListPlateformes.Last().Right <= this.Width)
                Ajoute_Plateforme(); //Reprends la formule Ajoute_Plateforme
            positionMario = Position(); //reprend la formule de graviter
            if (pnlPersonnage.Top > EnigmaPanel.Bottom)
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
                    pnlPersonnage.Location = new Point(10, -100); //Place le personnage au position indiquée
                    timerMario.Enabled = true;
                    pnlPersonnage.BackgroundImage = Image.FromFile(@".\images\mario2Droite.png"); //Image du mario regardant à droite
                    pnlPersonnage.setDroit(false);
                    pnlPersonnage.setGauche(false);
                    pnlPersonnage.setSauter(false);
                    pnlPersonnage.setDepart(false);
                }
                else
                    Application.Exit(); //Quitte l'application
            }
            if (pnlPersonnage.Right < NiveauPiegeEnigmaPanel.Left)
            {
                timerMario.Enabled = false;
                DialogResult dlg = MessageBox.Show("Le mot mystère est : gauche", "Bravo !!!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        public Panel getPersonnage()
        {
            return pnlPersonnage;
        }
        public void setPersonnage(Panel personnage)
        {
            pnlPersonnage = personnage;
        }
        public void PersonnageMouvement()
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
                    pnlPersonnage.setDroit(false);
                    break;
                case Keys.A:
                    pnlPersonnage.setGauche(false);
                    break;
                case Keys.Space:
                    pnlPersonnage.setSauter(false);
                    break;
            }
        }
        private void ToucheEnfoncer(object sender, KeyEventArgs e)
        {
            //assigne les touche si elles sont appuyées
            switch (e.KeyCode)
            {
                case Keys.D:
                    pnlPersonnage.setDroit(true);
                    break;
                case Keys.A:
                    pnlPersonnage.setGauche(true);
                    break;
                case Keys.Space:
                    if (e.KeyCode == Keys.Space && !pnlPersonnage.getSauter())
                    {
                        pnlPersonnage.setSauter(true);
                    }
                    break;
            }
        }
    }
}

