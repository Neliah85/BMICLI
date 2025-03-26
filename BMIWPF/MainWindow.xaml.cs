using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BMIWPF
{
    public partial class MainWindow : Window
    {
        private List<Diak> diakok;
        private const string FajlNev = "bmi.txt";

        public MainWindow()
        {
            InitializeComponent();
            diakok = BetoltesDiakok(FajlNev);
            diakDataGrid.ItemsSource = diakok;
            BetoltesKategoriaComboBox();
            
        }

        private List<Diak> BetoltesDiakok(string fajlnev)
        {
            try
            {
                return FileManager.BeolvasDiakok(fajlnev);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show($"Hiba: A fájl '{fajlnev}' nem található!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Diak>(); // Visszaadunk egy üres listát
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a fájl beolvasása közben: {ex.Message}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Diak>(); // Visszaadunk egy üres listát
            }
        }

        private void BetoltesKategoriaComboBox()
        {
            kategoriaComboBox.SelectedIndex = 0; // "Minden kategória" legyen kiválasztva induláskor
        }

        private void KategoriaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (diakok == null)
            {
                MessageBox.Show("A diákok lista nem töltőd", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string kivalasztottKategoria = kategoriaComboBox.SelectedItem.ToString();
            if (kivalasztottKategoria == "Minden kategória")
            {
                diakDataGrid.ItemsSource = diakok;
            }
            else
            {
                diakDataGrid.ItemsSource = diakok.Where(d => d.BmiBesorolas == kivalasztottKategoria).ToList();
            }
        }

        private void MentesButton_Click(object sender, RoutedEventArgs e)
        {
            string kivalasztottKategoria = kategoriaComboBox.SelectedItem.ToString();
            if (kivalasztottKategoria == "Minden kategória")
            {
                MessageBox.Show("Válassz ki egy kategóriát a mentéshez!", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string mentendoFajlNev = $"{kivalasztottKategoria}.txt";
            var mentendoDiakok = diakok.Where(d => d.BmiBesorolas == kivalasztottKategoria).ToList();

            try
            {
                using (StreamWriter sw = new StreamWriter(mentendoFajlNev))
                {
                    foreach (var diak in mentendoDiakok)
                    {
                        sw.WriteLine($"{diak.Nev};{diak.Eletkor};{diak.Magassag};{diak.Testsuly}");
                    }
                }
                MessageBox.Show($"A '{kivalasztottKategoria}' kategóriájú diákok adatai elmentve a '{mentendoFajlNev}' fájlba.", "Sikeres mentés", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a fájl mentése közben: {ex.Message}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}