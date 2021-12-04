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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private ICollectionView view;

        public AdminWindow()
        {
            InitializeComponent();
            UpdateView();
            view.Filter = CustomFilter;
        }
        private void UpdateView()
        {
            DGInstruktori.ItemsSource = null;
            view = CollectionViewSource.GetDefaultView(Util.Instance.Korisnici);
            DGInstruktori.ItemsSource = view;
            DGInstruktori.IsSynchronizedWithCurrentItem = true;

            DGInstruktori.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            view.Refresh();
            

        }
        private bool CustomFilter(Object obj)
        {
            RegistrovaniKorisnik korisnik = obj as RegistrovaniKorisnik;

            if(korisnik.TipKorisnika.Equals(ETipKorisnika.INSTRUKTOR) && korisnik.Aktivan)
            {
                return true;
            }
            return false;
        }

        private void Izmena_Click(object sender, RoutedEventArgs e)
        {
            RegistrovaniKorisnik selektedInstruktor = view.CurrentItem as RegistrovaniKorisnik;

            RegistrovaniKorisnik stariInstruktor = selektedInstruktor.Clone();

            AddEditInstruktoraWindow addEditInstruktoraWindow = new AddEditInstruktoraWindow(selektedInstruktor, EStatus.IZMENI);
            this.Hide();
            if (!(bool)addEditInstruktoraWindow.ShowDialog()){
                int index = Util.Instance.Korisnici.ToList().FindIndex(k => k.Email.Equals(stariInstruktor.Email));
                Util.Instance.Korisnici[index] = stariInstruktor;
            }
            this.Show();
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

        private void DGInstruktori_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan"))
            {

            }

        }
    }
}
