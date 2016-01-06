using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cpln.Enigmos.Enigmas.Components
{
    class PlateformesAleatoire
    {
        //Déclaration des constantes
        public const int VITESSE_PLATEFORMES = 10;
        //Plateforme à différente hauteur
        public const int Y_MAX = 550, Y_MAX_DIFFERENCE = 200;
        //Plateforme de différente épaisseur
        public const int HAUTEUR_MAX = 200, HAUTEUR_MIN = 50;
        //Plateforme de différente taille 
        public const int LONGUEUR_MAX = 300, LONGUEUR_MIN = 100;
        //Plateforme avec espacement varié
        public const int ESPACEMENT_X_MIN = 50, ESPACEMENT_X_MAX = 180;
        
    }
}
