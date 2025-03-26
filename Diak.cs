using System;

public class Diak
{
    public string Nev { get; set; }
    public int Eletkor { get; set; }
    public int Magassag { get; set; }  // cm-ben
    public int Testsuly { get; set; }  // kg-ban

    public Diak(string nev, int eletkor, int magassag, int testsuly)
    {
        Nev = nev;
        Eletkor = eletkor;
        Magassag = magassag;
        Testsuly = testsuly;
    }

    public double Bmi()
    {
        double magassagM = Magassag / 100.0;
        return Testsuly / (magassagM * magassagM);
    }

    public string BmiKategoria()
    {
        double bmi = Bmi();
        if (bmi < 18.5) return "Sovány";
        if (bmi < 24.9) return "Normál";
        if (bmi < 29.9) return "Túlsúlyos";
        return "Elhízott";
    }

    public override string ToString()
    {
        return $"{Nev}, {Eletkor} év, {Magassag} cm, {Testsuly} kg, BMI: {Bmi():F1} ({BmiKategoria()})";
    }
}
