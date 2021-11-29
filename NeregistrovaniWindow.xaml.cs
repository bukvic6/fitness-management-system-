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

        private void DGInstruktori_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan"))
            {

            }


        }

        private void txtPretraga_PreviewKeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}
