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
        //Plateforme à différente hauteur
        const int Y_MAX = 550, Y_MAX_DIFFERENCE = 200;
        //Plateforme de différente épaisseur
        const int HAUTEUR_MAX = 200, HAUTEUR_MIN = 50;
        //Plateforme de différente taille 
        const int LONGUEUR_MAX = 300, LONGUEUR_MIN = 100;
        //Plateforme avec espacement varié
        const int ESPACEMENT_X_MIN = 50, ESPACEMENT_X_MAX = 180;

        private Panel pnlPersonnage;
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
            int iY = r.Next(ListPlateformes.Last().Top - Y_MAX_DIFFERENCE, Y_MAX);
            int iLongueur = r.Next(LONGUEUR_MIN, LONGUEUR_MAX);
            int iEpaisseur = r.Next(HAUTEUR_MIN, HAUTEUR_MAX);
            int iEspacement = r.Next(ESPACEMENT_X_MIN, ESPACEMENT_X_MAX);


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
        public Panel getPersonnage()
        {
            return pnlPersonnage;
        }
        public void setPersonnage(Panel personnage)
        {
            pnlPersonnage = personnage;
        }
    }
}

