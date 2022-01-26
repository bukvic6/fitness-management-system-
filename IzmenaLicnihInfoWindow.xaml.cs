using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for IzmenaLicnihInfoWindow.xaml
    /// </summary>
    public partial class IzmenaLicnihInfoWindow : Window
    {
        private RegistrovaniKorisnik korisnikZaIzmenu;
        public IzmenaLicnihInfoWindow(RegistrovaniKorisnik korisnik)
        {
            InitializeComponent();
            this.DataContext = korisnik;

            korisnikZaIzmenu = korisnik;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();

        }
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {



            iZmena(korisnikZaIzmenu);

            this.Close();

        }
        public int iZmena(object obj)
        {
            RegistrovaniKorisnik korisnik = obj as RegistrovaniKorisnik;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update dbo.Korisnici set  Ime= '" + this.txtIme.Text + "' ,Prezime= '" + this.txtPrezime.Text + "',Lozinka= '" +
                    this.txtLozinka.Text + "' ,Email= '" + this.txtEmail.Text + "' ,Ulica= '" + this.txtUlica.Text +
                    "' ,Broj= '" + this.txtBroj.Text + "' ,Drzava= '" + this.txtDrzava.Text + "' ,Grad= '" + this.txtGrad.Text + "' where Id=" + korisnik.Id + ";";
                // command.ExecuteScalar();

                return (int)command.ExecuteNonQuery();
            }


        }
    }
}
