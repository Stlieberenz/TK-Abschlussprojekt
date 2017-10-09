using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using static Abschlussprojekt.Klassen.Statische_Variablen;

// Namenskonvention: --------------------------------------+
//                                                         |
// Alle Wörter eines Namens werden mit einem "_" getrennt. |
// Klassen     = Klasse_Bsp    => erster Buchstabe groß    |
// Methoden    = Methode_Bsp   => erster Buchstabe groß    |
// Variable    = variable_Bsp  => erster Buchstabe klein   |
// ENUM        = ENUM_BSP      => alle Buchstaben groß     |
//---------------------------------------------------------+

namespace Abschlussprojekt.Klassen
{
    static class Statische_Methoden
    {
        public static void Initialisiere_alle_Felder(Grid spielwiese_grid)
        {
            try
            {
                // Für jedes Image das ein Feld ist wird ein Feld Objeckt erzeugt.
                foreach (System.Windows.Controls.Image control_element in spielwiese_grid.Children)
                {
                    Point image_point = new Point(control_element.Margin.Left, control_element.Margin.Top);
                    if (control_element.Uid.Contains("Start_rot"))
                    {
                        Feld start_feld = new Feld(FARBE.ROT, FELD_EIGENSCHAFT.STARTPOSITION, image_point);
                    }
                    else if (control_element.Uid.Contains("Start_gelb"))
                    {
                        Feld start_feld = new Feld(FARBE.GELB, FELD_EIGENSCHAFT.STARTPOSITION, image_point);
                    }
                    else if (control_element.Uid.Contains("Start_gruen"))
                    {
                        Feld start_feld = new Feld(FARBE.GRUEN, FELD_EIGENSCHAFT.STARTPOSITION, image_point);
                    }
                    else if (control_element.Uid.Contains("Start_blau"))
                    {
                        Feld start_feld = new Feld(FARBE.BLAU, FELD_EIGENSCHAFT.STARTPOSITION, image_point);
                    }
                    else if (control_element.Uid.Contains("Feld_"))
                    {
                        Feld feld = new Feld(FARBE.LEER, FELD_EIGENSCHAFT.SPIELFELD, image_point);
                    }
                    else if (control_element.Uid.Contains("Ziel_rot"))
                    {
                        Feld ziel_feld = new Feld(FARBE.ROT, FELD_EIGENSCHAFT.ZIEL, image_point);
                    }
                    else if (control_element.Uid.Contains("Ziel_gelb"))
                    {
                        Feld ziel_feld = new Feld(FARBE.GELB, FELD_EIGENSCHAFT.ZIEL, image_point);
                    }
                    else if (control_element.Uid.Contains("Ziel_gruen"))
                    {
                        Feld ziel_feld = new Feld(FARBE.GRUEN, FELD_EIGENSCHAFT.ZIEL, image_point);
                    }
                    else if (control_element.Uid.Contains("Ziel_blau"))
                    {
                        Feld ziel_feld = new Feld(FARBE.BLAU, FELD_EIGENSCHAFT.ZIEL, image_point);
                    }
                }
            }
            catch { }
        }

        public static void Initialisiere_Spiel()
        {
            foreach(Spieler spieler in alle_Spieler)
            {
                spieler.Initialisiere_Figuren();
            }
        }

        public static void Initialisiere_Figuren(FARBE farbe)
        {
            for (int i = 0; i < 4; i++)
            {
                Figur figur = new Figur(farbe);
            }
        }

        public static void Initialisiere_Images_für_Figuren()
        {
            string pfad = Erzeuge_Dateipfad();
            Figur_rot.BeginInit();
            Figur_rot.UriSource = new Uri(pfad + "/Bilder/Figur_rot.bmp");
            Figur_rot.EndInit();
            Figur_gelb.BeginInit();
            Figur_gelb.UriSource = new Uri(pfad + "/Bilder/Figur_gelb.bmp");
            Figur_gelb.EndInit();
            Figur_gruen.BeginInit();
            Figur_gruen.UriSource = new Uri(pfad + "/Bilder/Figur_gruen.bmp");
            Figur_gruen.EndInit();
            Figur_blau.BeginInit();
            Figur_blau.UriSource = new Uri(pfad + "/Bilder/Figur_blau.bmp");
            Figur_blau.EndInit();
        }

        public static string Erzeuge_Dateipfad()
        {
            string temp = Directory.GetCurrentDirectory();
            string result = temp.Replace("bin\\Debug", ""); 
            return result;
        }

        
    }
}