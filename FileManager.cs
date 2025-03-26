using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class FileManager
{
    public static List<Diak> BeolvasDiakok(string fajlnev)
    {
        List<Diak> diakok = new List<Diak>();
        if (!File.Exists(fajlnev))
        {
            Console.WriteLine("Hiba: A fájl nem található!");
            return diakok;
        }

        string[] sorok = File.ReadAllLines(fajlnev);
        for (int i = 1; i < sorok.Length; i++) 
        {
            string[] adatok = sorok[i].Split(';');
            if (adatok.Length == 4)
            {
                diakok.Add(new Diak(
                    adatok[0],
                    int.Parse(adatok[1]),
                    int.Parse(adatok[2]),
                    int.Parse(adatok[3])
                ));
            }
        }
        return diakok;
    }

    public static void MentesEgeszsegesDiakok(string fajlnev, List<Diak> diakok)
    {
        var egeszsegesDiakok = diakok.Where(d => d.Bmi() >= 18.5 && d.Bmi() < 24.9);
        List<string> sorok = new List<string> { "Név;BMI" };
        sorok.AddRange(egeszsegesDiakok.Select(d => $"{d.Nev};{d.Bmi():F1}"));
        File.WriteAllLines(fajlnev, sorok);
        Console.WriteLine($"Egészséges diákok mentve a {fajlnev} fájlba.");
    }
}
