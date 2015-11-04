﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cpln.Enigmos
{
    partial class Enigmos
    {
        private void ReferenceEnigmas()
        {
            enigmas.Add(new Enigma("Démo", new Panel(), "1234"));
        }

        private Enigma NextEnigma()
        {
            #if DEBUG
            // Retournez votre enigme ici

            // ---
            #endif

            Random random = new Random();
            enigmas.OrderBy(item => random.Next());
            foreach (Enigma enigma in enigmas)
            {
                if (enigma.IsPlayable(solved))
                {
                    lblId.Text = enigma.Id;
                    return enigma;
                }
            }
            throw new Exception("Vous avez terminé le jeu !");
        }
    }
}
