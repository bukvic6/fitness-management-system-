using SR22_2020_POP2021.Model;
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

namespace SR22_2020_POP2021
{
    /// <summary>
    /// Interaction logic for RegistrationWindow1.xaml
    /// </summary>
    public partial class RegistrationWindow1 : Window
    {
        
        public RegistrationWindow1()
        {
            InitializeComponent();
        }

        private void cmbPol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selektovaniItem = (ComboBoxItem)cmbPol.SelectedItem;
            string value = selektovaniItem.Content.ToString();
            Enum.TryParse(value, out EPol pol);

            RegistrovaniKorisnik k = new RegistrovaniKorisnik
            {
                Ime = txtIme.Text,
                Prezime = txtPrezime.Text,
                JMBG = txtJMBG.Text,
                Email = txtEmail.Text,
                TipKorisnika = ETipKorisnika.ADMINISTRATOR,
                Aktivan = true,
                Lozinka = txtPassword.Text,
                Pol = pol,
            };
            Util.Instance.Korisnici.Add(k);
            Util.Instance.SacuvajEntitet("korisnici.txt");
            this.Close();

            

        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {

        }

   
    }
}
