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
    /// <summary>
    /// Interaction logic for InstruktorWindow.xaml
    /// </summary>
    public partial class InstruktorWindow : Window
    {
        private ICollectionView view;
        private ICollectionView view2;
        public InstruktorWindow()
        {
            InitializeComponent();
            UpdateView();
            view.Filter = CustomFilter;
            UpdateView2();
        }
        private void UpdateView()
        {
            DGTreninzi.ItemsSource = null;
            //https://stackoverflow.com/questions/2160481/wpf-collectionviewsource-multiple-views
            //resenje za problem instanciranja grida
            view = new CollectionViewSource { Source = Util.Instance.Treninzi }.View;
            DGTreninzi.ItemsSource = view;
            DGTreninzi.IsSynchronizedWithCurrentItem = true;
            DGTreninzi.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);


            DGTreninzi.SelectedItems.Clear();


        }
        private void UpdateView2()
        {
            DGPolaznici.ItemsSource = null;
            view2 = new CollectionViewSource { Source = Util.Instance.Korisnici }.View;

            DGPolaznici.IsSynchronizedWithCurrentItem = true;
            DGPolaznici.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            DGPolaznici.ItemsSource = view2;
            view2.Filter = CustomFilter2;
            view2.Refresh();



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


        private bool CustomFilter(Object obj)
        {
            Trening trening = obj as Trening;

            if (trening.InstruktorJmbg.Equals(Util.Instance.jmbgPrijavljen7) && trening.Aktivan)
            {

                if (pretrazi.Text != "")
                {
                    return trening.Datum.ToString().Contains(pretrazi.Text);
                }
                else
                    return true;
            }
            return false;
        }
        private void DGPolaznici_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan"))
            {

            }
            if (e.PropertyName.Equals("Adresa"))
            {
                e.ToString();

            }



        }

        private void DGTreninzi_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
          

        }

        private void Odjava_Click(object sender, RoutedEventArgs e)
        {
            var mainWin = new MainWindow();
            mainWin.Show();
            this.Close();

        }


        private void BrisanjeTreninga_Click(object sender, RoutedEventArgs e)
        {
            Trening treningZaBrisanje = view.CurrentItem as Trening;
            Util.Instance.BrisanjeTreninga(treningZaBrisanje.Id);

            int index = Util.Instance.Treninzi.ToList().FindIndex(tr => tr.Id.Equals(treningZaBrisanje.Id));
            Util.Instance.Treninzi[index].Aktivan = false;

            UpdateView();
            view.Refresh();


        }

        private void Objava_Click(object sender, RoutedEventArgs e)
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

                Util.Instance.Korisnici[index] = korisnikZaIzmenu;

            }
            this.Show();
            view2.Refresh();

        }

        private void DodajTrening_Click(object sender, RoutedEventArgs e)
        {
            DodajTreningWindow treningWindow = new DodajTreningWindow();
            this.Hide();
            treningWindow.Show();

        }
        private void pretrazi_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();

        }
    }
}
