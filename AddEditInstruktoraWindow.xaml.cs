using SR22_2020_POP2021.Model;
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

namespace SR22_2020_POP2021
{
    /// <summary>
    /// Interaction logic for AddEditInstruktoraWindow.xaml
    /// </summary>
    public partial class AddEditInstruktoraWindow : Window
    {
        private EStatus odabraniStatus;
        private RegistrovaniKorisnik odabraniInstruktor;



        public AddEditInstruktoraWindow(RegistrovaniKorisnik instruktor, EStatus status = EStatus.DODAJ)
        {
            InitializeComponent();

            this.DataContext = instruktor;

            ComboTip.ItemsSource = Enum.GetValues(typeof(ETipKorisnika)).Cast<ETipKorisnika>();
            ComboPol.ItemsSource = Enum.GetValues(typeof(EPol)).Cast<EPol>();
            

            
            odabraniInstruktor = instruktor;
            odabraniStatus = status;

            if(status.Equals(EStatus.IZMENI) && instruktor != null)
            {
                this.Title = "izmeni instruktora";
            }
            else
            {
                this.Title = "Dodaj instruktora";
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();

        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (odabraniStatus.Equals(EStatus.DODAJ))
            {
                Adresa adresa = new Adresa
                {

                    Ulica = txtUlica.Text,
                    Broj = txtBroj.Text,
                    Drzava = txtDrzava.Text,
                    Grad = txtGrad.Text,
                };

                RegistrovaniKorisnik k = new RegistrovaniKorisnik(txtIme.Text, txtPrezime.Text, txtJmbg.Text, txtEmail.Text, txtLozinka.Text, ETipKorisnika.INSTRUKTOR,EPol.DRUGO, adresa);


                Util.Instance.Korisnici.Add(k);

                Util.Instance.SacuvajEntitet(k);
                this.Close();

            }
            else
            {


                iZmena(odabraniInstruktor);

            }




            this.Close();

        }
        public int iZmena(object obj)
        {
            RegistrovaniKorisnik korisnik = obj as RegistrovaniKorisnik;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update dbo.Korisnici set  Ime= '" + this.txtIme.Text + "' ,Prezime= '" + this.txtPrezime.Text + "',Lozinka= '" + this.txtLozinka.Text + "' where Id=" + korisnik.Id + ";";
                // command.ExecuteScalar();

                return (int)command.ExecuteNonQuery();
            }


        }



    }
}
