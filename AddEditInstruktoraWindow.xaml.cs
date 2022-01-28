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
            //ComboBoxItem selektovaniItem = (ComboBoxItem)ComboPol.SelectedItem;
            //string value = selektovaniItem.Content.ToString();
            
            Enum.TryParse(ComboPol.Text, out EPol pol);
            if (odabraniStatus.Equals(EStatus.DODAJ))
            {
                if (txtIme.Text == "" || txtPrezime.Text == "" || txtJmbg.Text == "" || txtGrad.Text == ""
|| txtBroj.Text == "" || txtLozinka.Text == "" || txtDrzava.Text == "" || txtEmail.Text == "" || txtUlica.Text == "")
                {
                    MessageBox.Show("Morate uneti sve podatke");
                    return;
                }
                if (!txtEmail.Text.Contains("@") && !txtEmail.Text.EndsWith(".com"))
                {
                    MessageBox.Show("Neispravan format emaila");
                    return;
                }
                Adresa adresa = new Adresa
                {

                    Ulica = txtUlica.Text,
                    Broj = txtBroj.Text,
                    Drzava = txtDrzava.Text,
                    Grad = txtGrad.Text,
                };

                RegistrovaniKorisnik k = new RegistrovaniKorisnik(txtIme.Text, txtPrezime.Text, txtJmbg.Text, txtEmail.Text, txtLozinka.Text, ETipKorisnika.INSTRUKTOR,pol, adresa);


                Util.Instance.Korisnici.Add(k);

                Util.Instance.SacuvajEntitet(k);
                

            }
            else
            {


                iZmena(odabraniInstruktor);

            }



            this.DialogResult = true;
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
