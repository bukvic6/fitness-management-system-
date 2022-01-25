using SR22_2020_POP2021.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private ICollectionView view;
        private ICollectionView view2;
        private ICollectionView view3;

        public AdminWindow()
        {
            InitializeComponent();
            UpdateView();
            UpdateView2();
            UpdateView3();
            PretragaB.Items.Add("Pretrazi po Imenu");
            PretragaB.Items.Add("Pretrazi po Prezimenu");
            PretragaB.Items.Add("Pretrazi po Emailu");
        }
        private void UpdateView()
        {
            DGInstruktori.ItemsSource = null;
            //https://stackoverflow.com/questions/2160481/wpf-collectionviewsource-multiple-views
            //resenje za problem instanciranja grida
            view = new CollectionViewSource { Source = Util.Instance.Korisnici }.View;
            DGInstruktori.ItemsSource = view;
            DGInstruktori.IsSynchronizedWithCurrentItem = true;
            DGInstruktori.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
           
            view.Filter = CustomFilter;
            view.Refresh();


        }
        private void UpdateView2()
        {
            DGPolaznici.ItemsSource = null;
            view2 = new CollectionViewSource { Source = Util.Instance.Korisnici}.View;
            view2.Filter = CustomFilter2;
            view2.Refresh();
            DGPolaznici.IsSynchronizedWithCurrentItem = true;
            DGPolaznici.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            DGPolaznici.ItemsSource = view2;
            


        }
        private void UpdateView3()
        {
            GDTreninzi.ItemsSource = null;
            view3 = new CollectionViewSource { Source = Util.Instance.Treninzi }.View;
            view3.Filter = CustomFilter3;
            view3.Refresh();
            GDTreninzi.IsSynchronizedWithCurrentItem = true;
            GDTreninzi.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            GDTreninzi.ItemsSource = view3;



        }
        private bool CustomFilter2(Object obj)
        {
            RegistrovaniKorisnik korisnik = obj as RegistrovaniKorisnik;

            if ( korisnik.JMBG.Equals(Util.Instance.jmbgPrijavljen7))
            {
                return true;
            }
            return false;
        }
        private bool CustomFilter3(Object obj)
        {
            Trening trening = obj as Trening;

            if (trening.Aktivan)
            {

                return true;
            }
            return false;
        }


        private bool CustomFilter(Object obj)
        {
            RegistrovaniKorisnik korisnik = obj as RegistrovaniKorisnik;

            if (!korisnik.TipKorisnika.Equals(ETipKorisnika.ADMINISTRATOR) && korisnik.Aktivan)
            {

                return true;
            }

            return false;
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
                
            }
            return false;
        }

        private void Izmena_Click(object sender, RoutedEventArgs e)
        {
            RegistrovaniKorisnik korisnikZaIzmenu = view.CurrentItem as RegistrovaniKorisnik;

            RegistrovaniKorisnik stariInstruktor = korisnikZaIzmenu.Clone();

            AddEditInstruktoraWindow addEditInstruktoraWindow = new AddEditInstruktoraWindow(korisnikZaIzmenu, EStatus.IZMENI);
            this.Hide();
            if (!(bool)addEditInstruktoraWindow.ShowDialog()){
                int index = Util.Instance.Korisnici.ToList().FindIndex(k => k.Email.Equals(korisnikZaIzmenu.Email));
                
                Util.Instance.Korisnici[index] = korisnikZaIzmenu;
                
            }
            this.Show();
            view.Refresh();

        }
        
        private void Brisanje_Click(object sender, RoutedEventArgs e)
        {
            RegistrovaniKorisnik korisnikZaBrisanje = view.CurrentItem as RegistrovaniKorisnik;
            Util.Instance.BrisanjeKorisnika(korisnikZaBrisanje.Email);

            int index = Util.Instance.Korisnici.ToList().FindIndex(korisnik => korisnik.Email.Equals(korisnikZaBrisanje.Email));
            Util.Instance.Korisnici[index].Aktivan = false;

            UpdateView();
            view.Refresh();

        }

        private void Dodavanje_Click(object sender, RoutedEventArgs e)
        {
            RegistrovaniKorisnik noviKorisnik = new RegistrovaniKorisnik();
            AddEditInstruktoraWindow addEditInstruktoraWindow = new AddEditInstruktoraWindow(noviKorisnik);
            this.Hide();
            if (!(bool)addEditInstruktoraWindow.ShowDialog())
            {
                

            }
            this.Show();
            view.Refresh();

        }


        private void Pretraga_Click(object sender, RoutedEventArgs e)
        {
            

            view.Refresh();

        }

        private void DGInstruktori_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan"))
            {

            }

        }



        private void IzmenaP_Click(object sender, RoutedEventArgs e)
        {

        }

  
        private void BrisanjeP_Click(object sender, RoutedEventArgs e)
        {

        }
        private void DGPolaznici_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan"))
            {

            }



        }

        private void BP_Click(object sender, RoutedEventArgs e)
        {
            AddEditTrening treningWindow = new AddEditTrening();
            this.Hide();
            treningWindow.Show();
        }

        private void GDTreninzi_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }

        private void ObrisiTrening_Click(object sender, RoutedEventArgs e)
        {
            Trening treningZaBrisanje = view3.CurrentItem as Trening;
            Util.Instance.BrisanjeTreninga(treningZaBrisanje.Id);

            int index = Util.Instance.Treninzi.ToList().FindIndex(tr => tr.Id.Equals(treningZaBrisanje.Id));
            Util.Instance.Treninzi[index].Aktivan = false;

            UpdateView();
            view.Refresh();

        }

        private void Prezime_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PretraziKlik_Click(object sender, RoutedEventArgs e)
        {
            view.Filter = CustomFilterPretraga;
            view.Refresh();


        }
    }
}
