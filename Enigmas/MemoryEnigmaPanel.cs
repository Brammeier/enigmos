using Cpln.Enigmos.Enigmas.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Cpln.Enigmos.Enigmas
{
    class MemoryEnigmaPanel : EnigmaPanel
    {
        // Initialisation de divers variables
        Panel[] tpnlCarte = new Panel[16];
        FlowLayoutPanel centerLayout = new FlowLayoutPanel();
        Bitmap[] tImage = new Bitmap[8] { Properties.Resources.memory0, Properties.Resources.memory1, Properties.Resources.memory2, Properties.Resources.memory3, Properties.Resources.memory4, Properties.Resources.memory5, Properties.Resources.memory6, Properties.Resources.memory7};
                
        public MemoryEnigmaPanel()
        {
            //Mise en forme du panel
            Width = 800;
            Height = 800;

            //Proriétés de la disposition du jeu
            centerLayout.Dock = DockStyle.Fill;

            //ajout d'un contrôle sur la disposition
            Controls.Add(centerLayout);

            Random rndCarte = new Random();
            int iNumCarte;
            int[] tSaveNum = new int[8];
            for (int i = 0; i < tSaveNum.Length; i++)
                tSaveNum[i] = 0;

            //création de carte dans le carré             
            for (int i = 0; i < tpnlCarte.Length; i++)
            {
                iNumCarte = rndCarte.Next(0, 7);
                tpnlCarte[i] = new Panel();
                tpnlCarte[i].Margin = new Padding(2);
                tpnlCarte[i].Size = new Size(196, 196);
                tpnlCarte[i].AccessibleDescription = iNumCarte.ToString();
                tpnlCarte[i].BackColor = Color.Gray;
                centerLayout.Controls.Add(tpnlCarte[i]);
                tpnlCarte[i].Click += new EventHandler(ClickOnCarte);
            }
        }
        private void ClickOnCarte(object sender, EventArgs e)
        {
            Panel pnl = (Panel)sender;
            int iRefCarte = Convert.ToInt32(pnl.AccessibleDescription);
            pnl.BackgroundImage = tImage[iRefCarte];
            MessageBox.Show(iRefCarte.ToString());
        }

    }
}