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
using SR22_2020_POP2021.Model;

namespace SR22_2020_POP2021
{

    public partial class PolaznikWindow : Window

    {
        private ICollectionView view;
        private ICollectionView view2;
        private ICollectionView view3;
        public PolaznikWindow()
        {
            InitializeComponent();
            UpdateView();
            view.Filter = CustomFilter;
            UpdateView2();
            view2.Filter = CustomFilter2;
            UpdateView3();
            view3.Filter = CustomFilter3;
        }
        private void UpdateView()
        {
            DGInstruktori.ItemsSource = null;

            view = new CollectionViewSource { Source = Util.Instance.Korisnici }.View;
            DGInstruktori.ItemsSource = view;
            DGInstruktori.IsSynchronizedWithCurrentItem = true;
            DGInstruktori.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);


            DGPolaznici.SelectedItems.Clear();
        }

        private bool CustomFilter(Object obj)
        {
            RegistrovaniKorisnik korisnik = obj as RegistrovaniKorisnik;

            if (korisnik.TipKorisnika.Equals(ETipKorisnika.INSTRUKTOR) && korisnik.Aktivan)
            {

                return true;
            }

            return false;
        }
        private void UpdateView2()
        {
            DGPolaznici.ItemsSource = null;
            view2 = new CollectionViewSource { Source = Util.Instance.Korisnici }.View;
            DGPolaznici.IsSynchronizedWithCurrentItem = true;
            DGPolaznici.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            DGPolaznici.ItemsSource = view2;
            DGPolaznici.SelectedItems.Clear();



        }
        private bool CustomFilter2(Object obj)
        {
            RegistrovaniKorisnik korisnik = obj as RegistrovaniKorisnik;

            if (korisnik.JMBG.Equals(Util.Instance.jmbgPrijavljen7))
            {
                return true;
            }
            return false;
        }
        private void UpdateView3()
        {
            DGTreninzi.ItemsSource = null;

            view3 = new CollectionViewSource { Source = Util.Instance.Treninzi }.View;
            
            DGTreninzi.IsSynchronizedWithCurrentItem = true;
            DGTreninzi.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            DGTreninzi.ItemsSource = view3;

            DGTreninzi.SelectedItems.Clear();


        }
        private bool CustomFilter3(Object obj)
        {
            Trening trening = obj as Trening;

            if (trening.PolaznikJmbg.Equals(Util.Instance.jmbgPrijavljen7))
            {
                return true;
            }
            return false;
        }



        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

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
            if (e.PropertyName.Equals("Id"))
            {
                e.Column.Visibility = Visibility.Collapsed;

            }
            if (e.PropertyName.Equals("Pol"))
            {
                e.Column.Visibility = Visibility.Collapsed;

            }
            if (e.PropertyName.Equals("Adresa"))
            {
                e.Column.Visibility = Visibility.Collapsed;

            }

        }

        private void RezervisiTrening_Click(object sender, RoutedEventArgs e)
        {
            RegistrovaniKorisnik trenutniInstruktor = view.CurrentItem as RegistrovaniKorisnik;


            RezervisiTrening addEditInstruktoraWindow = new RezervisiTrening(trenutniInstruktor);
            
            if (!(bool)addEditInstruktoraWindow.ShowDialog())
            {
                int index = Util.Instance.Korisnici.ToList().FindIndex(k => k.JMBG.Equals(trenutniInstruktor.JMBG));

                Util.Instance.Korisnici[index] = trenutniInstruktor;

                

            }
            this.Show();
            view.Refresh();

        }

        private void Odjava_Click(object sender, RoutedEventArgs e)
        {
            var mainWin = new MainWindow();
            mainWin.Show();
            this.Close();

        }
        private void IzmenaLicnihInfo_Click(object sender, RoutedEventArgs e)
        {
            RegistrovaniKorisnik korisnikZaIzmenu = view2.CurrentItem as RegistrovaniKorisnik;

            RegistrovaniKorisnik stariKorisnik = korisnikZaIzmenu.Clone();

            IzmenaLicnihInfoWindow izmenaLicnihInfo = new IzmenaLicnihInfoWindow(korisnikZaIzmenu);
            this.Hide();
            if (!(bool)izmenaLicnihInfo.ShowDialog())
            {
                int index = Util.Instance.Korisnici.ToList().FindIndex(k => k.Email.Equals(korisnikZaIzmenu.Email));

                Util.Instance.Korisnici[index] = stariKorisnik;

            }
            this.Show();
            view2.Refresh();


        }
        private void OtkaziTrening_Click(object sender, RoutedEventArgs e)
        {
            Trening treningZaOtkazivanje = view3.CurrentItem as Trening;
            Util.Instance.OtkazivanjeTreninga(treningZaOtkazivanje.Id);

            int index = Util.Instance.Treninzi.ToList().FindIndex(tr => tr.Id.Equals(treningZaOtkazivanje.Id));
            Util.Instance.Treninzi[index].StatusTreninga = EStatusTreninga.SLOBODAN;
            Util.Instance.Treninzi[index].PolaznikJmbg = " ";
            view3.Refresh();

        }

        private void DGPolaznici_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }
        private void DGTreninzi_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("PolaznikJmbg"))
            {
                e.Column.Visibility = Visibility.Collapsed;

            }

        }


    }
}