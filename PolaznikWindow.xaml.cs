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

        public PolaznikWindow()
        {
            InitializeComponent();
            UpdateView();
        }
        private void UpdateView()
        {
            DGInstruktori.ItemsSource = null;

            view = new CollectionViewSource { Source = Util.Instance.Korisnici }.View;
            DGInstruktori.ItemsSource = view;
            DGInstruktori.IsSynchronizedWithCurrentItem = true;
            DGInstruktori.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            view.Filter = CustomFilter;
            view.Refresh();


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
            this.Hide();
            if (!(bool)addEditInstruktoraWindow.ShowDialog())
            {
                int index = Util.Instance.Korisnici.ToList().FindIndex(k => k.JMBG.Equals(trenutniInstruktor.JMBG));

                Util.Instance.Korisnici[index] = trenutniInstruktor;

                

            }
            this.Show();
            view.Refresh();

        }
    }
}