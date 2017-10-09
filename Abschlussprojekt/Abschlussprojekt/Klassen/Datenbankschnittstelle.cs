using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Abschlussprojekt.Klassen
{
    class Datenbankschnittstelle
    {
        public static OleDbConnection Datenbankverbindung = new OleDbConnection();
        public static void init() // Einrichten der Datenbankverbindung
        {
            string[] directoryfiles = Directory.GetFiles(Klassen.Statische_Methoden.Erzeuge_Dateipfad() + "/Datenbank");
            string DatabasePath = "Provider=Microsoft.Jet.OLEDB.4.0;" + @"data source=";
            foreach (string filename in directoryfiles)
            {
                if (filename.Contains("Datenbank.mdb")) // Wenn es schon eine Datenbank gibt, danbn benutze sie.
                {
                    DatabasePath += filename;
                    break;
                }
            }
            Datenbankverbindung = new OleDbConnection(DatabasePath);
            try {
                Datenbankverbindung.Open();
                DataTable dataTable = Datenbankverbindung.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, null });

                //Lösche alle Tabellen
                for (int i = 0; i < dataTable.Rows.Count; i++) {
                    if (dataTable.Rows[i]["TABLE_TYPE"].ToString() == "TABLE")
                    { 
                        OleDbCommand Cmd = new OleDbCommand("DROP TABLE " + dataTable.Rows[i]["TABLE_NAME"].ToString(), Datenbankverbindung);
                        Cmd.ExecuteNonQuery();
                    }
                }

                //Füge neue leere Tabellen hinzu
                OleDbCommand cmd1 = new OleDbCommand("CREATE TABLE Spieler ([Name] text, [Kategorie] text,[Farbe] text,[Status] text)", Datenbankverbindung);
                cmd1.ExecuteNonQuery();
                OleDbCommand cmd2 = new OleDbCommand("CREATE TABLE Figuren_rot ([id] int,[Position] int,[Farbe] text)", Datenbankverbindung);
                cmd2.ExecuteNonQuery();
                Datenbankverbindung.Close();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
