using SR22_2020_POP2021.Model;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            Util.Instance.CitanjeEntiteta("korisnici.txt");
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (txtJMBG.Text == "" || txtLozinka.Password.Length == 0)
            {
                MessageBox.Show("Niste uneli JMBG ili Lozinku");
                return;

            }
            using (StreamReader file = new StreamReader(@"../../Resources/korisnici.txt"))
            {
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    string[] korisnikIzFajla = line.Split(';');

                    Enum.TryParse(korisnikIzFajla[6], out ETipKorisnika tip);

                    string Lozinka = korisnikIzFajla[3];
                    string JMBG = korisnikIzFajla[4];
                    string TipKorisnika = korisnikIzFajla[6];

                    if (txtJMBG.Text.Trim() == JMBG && txtLozinka.Password.ToString() == Lozinka && TipKorisnika == ETipKorisnika.INSTRUKTOR.ToString())
                    {
                        MessageBox.Show("Instruktor");
                        break;
                    }
                    else if (txtJMBG.Text.Trim() == JMBG && txtLozinka.Password.ToString() == Lozinka && TipKorisnika == ETipKorisnika.ADMINISTRATOR.ToString())
                    {
                        AdminWindow adminWindow = new AdminWindow();
                        this.Hide();
                        adminWindow.Show();

                    }
                }
            }
        }
    }
}
