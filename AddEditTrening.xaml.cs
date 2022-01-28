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
    /// Interaction logic for AddEditTrening.xaml
    /// </summary>
    public partial class AddEditTrening : Window
    {
         private ICollectionView view;
        public AddEditTrening()
        {

            InitializeComponent();
            UpdateView();
            view.Filter = CustomFilter;

        }
        private void UpdateView()
        {
            instruktoriZaRez.ItemsSource = null;
            //https://stackoverflow.com/questions/2160481/wpf-collectionviewsource-multiple-views
            //resenje za problem instanciranja grida
            view = new CollectionViewSource { Source = Util.Instance.Korisnici }.View;
            instruktoriZaRez.ItemsSource = view;
            instruktoriZaRez.IsSynchronizedWithCurrentItem = true;
            instruktoriZaRez.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            instruktoriZaRez.SelectedItems.Clear();


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



        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();

        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            RegistrovaniKorisnik instruktor = view.CurrentItem as RegistrovaniKorisnik;
            if (instruktor == null) {
                MessageBox.Show("Morate odabrati instruktora");
                return;
            }

            if (vreme.Text == "" || Trajanje.Text == "" )
            {
                MessageBox.Show("Morate uneti sve podatke");
                return;
            }
            Trening trening = new Trening(DatePicker.SelectedDate.Value.Date,vreme.Text,Trajanje.Text, instruktor.JMBG,EStatusTreninga.SLOBODAN);

            Util.Instance.Treninzi.Add(trening);
            Util.Instance.SacuvajEntitet(trening);
            this.Close();




        }

        private void instruktoriZaRez_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
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
            if (e.PropertyName.Equals("Email"))
            {
                e.Column.Visibility = Visibility.Collapsed;

            }
            if (e.PropertyName.Equals("Aktivan"))
            {
                e.Column.Visibility = Visibility.Collapsed;

            }

        }
    }
}
