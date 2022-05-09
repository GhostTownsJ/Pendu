using System;
using System.Collections.Generic;
using System.Text;

namespace PenduA
{
    public class DessinPendu
    {
          //dessin classé par ordre décroissant car vie commence a 7 et on utilise le int vie pour charger la bonne cellule du tableau
        private string[] _PenduDessin = {@" 
  +---+
  |   |
  O   |
 /|\  |
 / \  |
      |
=========",
            @"
  +---+
  |   |
  O   |
 /|\  |
 /    |
      |
=========", @"
  +---+
  |   |
  O   |
 /|\  |
      |
      |
=========",
            @"
  +---+
  |   |
  O   |
 /|   |
      |
      |
=========",
             @"
  +---+
  |   |
  O   |
  |   |
      |
      |
=========", @"
  +---+
  |   |
  O   |
      |
      |
      |
=========",@"
  +---+
  |   |
      |
      |
      |
      |
========="};
        public string[] penduDessin => _PenduDessin; 
    }
}