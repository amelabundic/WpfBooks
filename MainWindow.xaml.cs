using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;

namespace WpfBooks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Knjiga> listaKnjiga = new List<Knjiga>();

       private string putanja = "Knjiga.json";

        private int id = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private ComboBoxItem NadjiStavku(string zanr, ComboBox ComboBox1)
        {
            foreach (ComboBoxItem stavka in ComboBox1.Items)
            {
                if (stavka.Content.ToString() == zanr)
                {
                    return stavka;
                }
            }
            return null;
        }

        private void Stampaj()
        {
            ListBox1.Items.Clear();

            foreach (Knjiga k in listaKnjiga)
            {
                ListBox1.Items.Add(k);
            }
        }

        private void Sacuvaj()
        {
            if (listaKnjiga.Count > 0)
            {
                string jsonString = JsonConvert.SerializeObject(listaKnjiga);
                File.WriteAllText(putanja,jsonString);
            }
            else
            {
                File.Delete(putanja);
            }
            MessageBox.Show("Podaci sacuvani");
        }

        private void Citaj()
        {
            if (File.Exists(putanja))
            {
                string jsonString = File.ReadAllText(putanja);
                listaKnjiga.Clear();
                listaKnjiga = JsonConvert.DeserializeObject<List<Knjiga>>(jsonString);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Citaj();

            if (listaKnjiga.Count > 0)
            {
                Knjiga k1 = listaKnjiga[listaKnjiga.Count - 1];
                id = k1.KnjigaId;

                Stampaj();

            }
        }

        private void ButtonUbaci_Click(object sender, RoutedEventArgs e)
        {
            Window1 w1 = new Window1();
            w1.Title = "Unesi knjigu";
            id++;
            w1.TextBoxId.Text = id.ToString();

            if (w1.ShowDialog() == true)
            {
                Knjiga k1 = new Knjiga { 
                KnjigaId = id,
                Naslov = w1.TextBoxNaslov.Text,
                Autor = w1.TextBoxAutor.Text
                };

                ComboBoxItem selektovano = (ComboBoxItem)w1.ComboBox1.SelectedItem;
                k1.Zanr = selektovano.Content.ToString();

                listaKnjiga.Add(k1);
                Stampaj();
                Sacuvaj();
            }
            else
            {
                id--;
            }
        }

        private void ButtonPromijeni_Click(object sender, RoutedEventArgs e)
        {
            int indeks = ListBox1.SelectedIndex;

            if (indeks > -1)
            {
                Knjiga k1 = listaKnjiga[indeks];
                Window1 w1 = new Window1();
                w1.Title = "Promijeni knjigu: " + k1.Naslov;
                w1.TextBoxId.Text = k1.KnjigaId.ToString();
                w1.TextBoxNaslov.Text = k1.Naslov;
                w1.TextBoxAutor.Text = k1.Autor;

                w1.ComboBox1.SelectedItem = NadjiStavku(k1.Zanr, w1.ComboBox1);


                if (w1.ShowDialog() == true)
                {
                    k1.Naslov = w1.TextBoxNaslov.Text;
                    k1.Autor = w1.TextBoxAutor.Text;

                    ComboBoxItem selektovano = (ComboBoxItem)w1.ComboBox1.SelectedItem;
                    k1.Zanr = selektovano.Content.ToString();

                    Stampaj();
                    Sacuvaj();

                    ListBox1.SelectedIndex = indeks;
                }
            }
            else
            {
                MessageBox.Show("Odaberi knjigu");
            }
        }

        private void ButtonObrisi_Click(object sender, RoutedEventArgs e)
        {
            int indeks = ListBox1.SelectedIndex;

            if (indeks > -1)
            {
                Window2 w2 = new Window2();
                w2.Title = "Brisanje knjige: " + listaKnjiga[indeks].Naslov;

                if (w2.ShowDialog() == true)
                {
                    listaKnjiga.RemoveAt(indeks);
                    Stampaj();
                    Sacuvaj();
                }
                else
                {
                    MessageBox.Show("Odaberi knjigu");
                }
            }
        }
    }
}
