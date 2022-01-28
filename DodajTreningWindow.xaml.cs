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
using SR22_2020_POP2021.Model;

namespace SR22_2020_POP2021
{
    /// <summary>
    /// Interaction logic for DodajTreningWindow.xaml
    /// </summary>
    public partial class DodajTreningWindow : Window
    {
        public DodajTreningWindow()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();

        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {

            Trening trening = new Trening(DatePicker.SelectedDate.Value.Date, vreme.Text, Trajanje.Text, Util.Instance.jmbgPrijavljen7, EStatusTreninga.SLOBODAN);


            if (vreme.Text == "" || Trajanje.Text == "")
            {
                MessageBox.Show("Morate uneti sve podatke");
                return;
            }
            Util.Instance.Treninzi.Add(trening);
            Util.Instance.SacuvajEntitet(trening);
            this.Close();

        }
    }
}
