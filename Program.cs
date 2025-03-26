using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string fajlnev = "bmi.txt";
        string kimenetiFajlnev = "egeszseges_diakok.txt";
        List<Diak> diakok = FileManager.BeolvasDiakok(fajlnev);

        if (diakok.Count == 0)
        {
            Console.WriteLine("Nincsenek diák adatok!");
            return;
        }

        // 3. feladat: Kiírás
        Console.WriteLine($"3.a, Feladat: A diákok száma: {diakok.Count}");

        // Legmagasabb diák keresése
        Diak legmagasabb = diakok[0];
        foreach (var diak in diakok)
        {
            if (diak.Magassag > legmagasabb.Magassag)
            {
                legmagasabb = diak;
            }
        }
        Console.WriteLine($"3.b, Feladat: A legmagasabb diák: {legmagasabb.Nev}, magassága: {legmagasabb.Magassag} cm");

        int egeszsegesDiakokSzama = diakok.Count(d => d.Bmi() >= 18.5 && d.Bmi() < 24.9);
        Console.WriteLine($"5.b, Feladat: Egészséges BMI tartományba eső diákok száma: {egeszsegesDiakokSzama}");

        bool vanTothEva = diakok.Any(d => d.Nev == "Tóth Éva");
        Console.WriteLine($"5.c, Feladat: Van-e Tóth Éva a diákok között? {(vanTothEva ? "Igen" : "Nem")}");

        // 6. Egészséges diákok mentése fájlba
        try
        {
            using (StreamWriter sw = new StreamWriter(kimenetiFajlnev))
            {
                foreach (var diak in diakok.Where(d => d.Bmi() >= 18.5 && d.Bmi() < 24.9))
                {
                    sw.WriteLine($"{diak.Nev};{diak.Bmi():F1}");
                }
            }
            Console.WriteLine($"6. Feladat: Az egészséges diákok adatai elmentve a '{kimenetiFajlnev}' fájlba.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hiba történt a fájl mentése közben: {ex.Message}");
        }
    }
}