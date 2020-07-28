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
using System.Windows.Shapes;

namespace WpfBooks
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void ButtonPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxNaslov.Text))
            {
                MessageBox.Show("Unesite naslov knjige");
                TextBoxNaslov.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(TextBoxAutor.Text))
            {
                MessageBox.Show("Unesite autora knjige");
                TextBoxAutor.Focus();
                return;
            }
            if (ComboBox1.SelectedIndex  < 0)
            {
                MessageBox.Show("Odaberite zanr");
                return;
            }
            DialogResult = true;
        }

        private void ButtonOdustani_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
