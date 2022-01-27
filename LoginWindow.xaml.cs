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

            InitializeComponent();
            Util.Instance.jmbgPrijavljen7 = "";

        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (txtJMBG.Text == "" || txtLozinka.Password.Length == 0)
            {
                MessageBox.Show("Niste uneli JMBG ili Lozinku");
                return;

            }
            foreach(RegistrovaniKorisnik k in Util.Instance.Korisnici)
            {
                if(k.JMBG==txtJMBG.Text && k.Lozinka == txtLozinka.Password && k.TipKorisnika == ETipKorisnika.ADMINISTRATOR)
                {
                    Util.Instance.jmbgPrijavljen7 = k.JMBG;
                   
                    AdminWindow adminWindow = new AdminWindow();
                    this.Hide();
                    adminWindow.Show();

                    break;
                }
                else if (k.JMBG == txtJMBG.Text && k.Lozinka == txtLozinka.Password && k.TipKorisnika == ETipKorisnika.INSTRUKTOR)
                {
                    Util.Instance.jmbgPrijavljen7 = k.JMBG;

                    InstruktorWindow instruktWindow = new InstruktorWindow();
                    this.Hide();
                    instruktWindow.Show();

                    break;
                }
                else if (k.JMBG == txtJMBG.Text && k.Lozinka == txtLozinka.Password && k.TipKorisnika == ETipKorisnika.POLAZNIK)
                {
                    Util.Instance.jmbgPrijavljen7 = k.JMBG;

                    PolaznikWindow polaznikWindow = new PolaznikWindow();
                    this.Hide();
                    polaznikWindow.Show();

                    break;
                }
            }  
        }
    }
}
