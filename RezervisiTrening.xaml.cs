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
    /// Interaction logic for RezervisiTrening.xaml
    /// </summary>
    public partial class RezervisiTrening : Window
    {
        private RegistrovaniKorisnik odabraniInstruktor;
        

        private ICollectionView view;

        public RezervisiTrening(RegistrovaniKorisnik instruktor)
        {
            InitializeComponent();
            this.DataContext = instruktor;
            this.odabraniInstruktor = instruktor;
            
            UpdateView();
            view.Filter = CustomFilter;

        }
        
        private void UpdateView()
        {
            GDTreninzi.ItemsSource = null;
            //resenje za problem instanciranja grida
            view = new CollectionViewSource { Source = Util.Instance.Treninzi }.View;
            GDTreninzi.ItemsSource = view;
            GDTreninzi.IsSynchronizedWithCurrentItem = true;
            GDTreninzi.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            GDTreninzi.SelectedItems.Clear();
        }
        private bool CustomFilter(Object obj)

        {
            
            Trening trening = obj as Trening;

            if (trening.InstruktorJmbg == odabraniInstruktor.JMBG )
            {
                return true;
            }
            return false;
        }

        private void GDTreninzi_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }


        private void Rezervacija_Click(object sender, RoutedEventArgs e)
        {
            Trening treningZaRezervaciju = view.CurrentItem as Trening;
            if(treningZaRezervaciju.StatusTreninga == EStatusTreninga.REZERVISAN)
            {
                MessageBox.Show("Trening je vec rezervisan");
                return;
            }
            Util.Instance.RezervacijaTreninga(treningZaRezervaciju.Id);

            int index = Util.Instance.Treninzi.ToList().FindIndex(tr => tr.Id.Equals(treningZaRezervaciju.Id));
            Util.Instance.Treninzi[index].StatusTreninga = EStatusTreninga.REZERVISAN;
            Util.Instance.Treninzi[index].PolaznikJmbg = Util.Instance.jmbgPrijavljen7;
            view.Refresh();
            GDTreninzi.SelectedItems.Clear();


        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
