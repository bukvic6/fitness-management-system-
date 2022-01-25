using SR22_2020_POP2021.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for NeregistrovaniWindow.xaml
    /// </summary>
    public partial class NeregistrovaniWindow : Window
    {
        ICollectionView view;
        public NeregistrovaniWindow()
        {
            InitializeComponent();
            UpdateView();
            view.Filter = CustomFilter;
        }
        private bool CustomFilter(object obj)
        {
            RegistrovaniKorisnik korisnik = obj as RegistrovaniKorisnik;

            if(korisnik.TipKorisnika.Equals(ETipKorisnika.INSTRUKTOR) && korisnik.Aktivan)
            {
                if (txtPretraga.Text != "")
                {
                    return korisnik.Ime.Contains(txtPretraga.Text);
                }
                else
                    return true; 
            }
            return false;

        }
        private void UpdateView()
        {
            DGInstruktori.ItemsSource = null;
            view = CollectionViewSource.GetDefaultView(Util.Instance.Korisnici);
            DGInstruktori.ItemsSource = view;
            DGInstruktori.IsSynchronizedWithCurrentItem = true;

            DGInstruktori.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

           

        }
        private bool CustomFilterPretraga(Object obj)
        {
            RegistrovaniKorisnik korisnik = obj as RegistrovaniKorisnik;

            if (korisnik.TipKorisnika.Equals(ETipKorisnika.INSTRUKTOR) && korisnik.Aktivan)
            {
                if (PretragaB.SelectedIndex == 0)
                {

                    using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
                    {
                        conn.Open();

                        SqlCommand command = conn.CreateCommand();
                        command.CommandText = @"select * from korisnici where Ime = @ime";
                        command.Parameters.AddWithValue("@ime", pretrazi.Text);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            DGInstruktori.ItemsSource = dt.DefaultView;

                        }


                    }
                }
                else if (PretragaB.SelectedIndex == 1)
                {

                    using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
                    {
                        conn.Open();

                        SqlCommand command = conn.CreateCommand();
                        command.CommandText = @"select * from korisnici where Prezime = @prezime";
                        command.Parameters.AddWithValue("@prezime", pretrazi.Text);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            DGInstruktori.ItemsSource = dt.DefaultView;

                        }


                    }
                }
                else if (PretragaB.SelectedIndex == 2)
                {

                    using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
                    {
                        conn.Open();

                        SqlCommand command = conn.CreateCommand();
                        command.CommandText = @"select * from korisnici where Email = @email";
                        command.Parameters.AddWithValue("@email", pretrazi.Text);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            DGInstruktori.ItemsSource = dt.DefaultView;

                        }

                    }
                }
                else if (PretragaB.SelectedIndex == 3)
                {

                    using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
                    {
                        conn.Open();

                        SqlCommand command = conn.CreateCommand();
                        command.CommandText = @"select * from korisnici where TipKorisnika = @tipKorisnika";
                        command.Parameters.AddWithValue("@tipKorisnika", pretrazi.Text);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            DGInstruktori.ItemsSource = dt.DefaultView;

                        }

                    }
                }
                else if (PretragaB.SelectedIndex == 4)
                {

                    using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
                    {
                        conn.Open();

                        SqlCommand command = conn.CreateCommand();
                        command.CommandText = @"select * from korisnici where Ulica = @ulica";
                        command.Parameters.AddWithValue("@ulica", pretrazi.Text);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            DGInstruktori.ItemsSource = dt.DefaultView;
                        }

                    }
                }

            }
            return false;
        }

        private void DGInstruktori_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Adresa"))
            {
                e.ToString();

            }


            if (e.PropertyName.Equals("Aktivan"))
            {
                e.Column.Visibility = Visibility.Collapsed;

            }
            if (e.PropertyName.Equals("JMBG"))
            {
                e.Column.Visibility = Visibility.Collapsed;

            }
            if (e.PropertyName.Equals("Lozinka"))
            {
                e.Column.Visibility = Visibility.Collapsed;

            }
            if (e.PropertyName.Equals("TipKorisnika"))
            {
                e.Column.Visibility = Visibility.Collapsed;

            }


        }

        private void txtPretraga_PreviewKeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}
