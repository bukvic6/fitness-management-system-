using SR22_2020_POP2021.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            PretragaB.Items.Add("Pretrazi po Imenu");
            PretragaB.Items.Add("Pretrazi po Prezimenu");
            PretragaB.Items.Add("Pretrazi po Emailu");
            PretragaB.Items.Add("Pretrazi po Ulici");
            PretragaB.Items.Add("Pretrazi po Gradu");

        }

        private void UpdateView()
        {
            DGInstruktori.ItemsSource = null;
            view = CollectionViewSource.GetDefaultView(Util.Instance.Korisnici);
            DGInstruktori.ItemsSource = view;
            DGInstruktori.IsSynchronizedWithCurrentItem = true;

            DGInstruktori.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            DGInstruktori.SelectedItems.Clear();

        }
        private bool CustomFilter(Object obj)
        {
            RegistrovaniKorisnik korisnik = obj as RegistrovaniKorisnik;

            if (korisnik.TipKorisnika.Equals(ETipKorisnika.INSTRUKTOR) && korisnik.Aktivan)
            {
                if (PretragaB.SelectedIndex == 0)
                {
                    return korisnik.Ime.Contains(pretrazi.Text);
                }
                else if (PretragaB.SelectedIndex == 1)
                {

                    return korisnik.Prezime.Contains(pretrazi.Text);
                }
                else if (PretragaB.SelectedIndex == 2 )
                {

                    return korisnik.Email.Contains(pretrazi.Text);

                }               
                else if (PretragaB.SelectedIndex == 3)
                {

                    return korisnik.Adresa.Ulica.Contains(pretrazi.Text);
                }
                else if (PretragaB.SelectedIndex == 4 )
                {

                    return korisnik.Adresa.Grad.Contains(pretrazi.Text);
                }


                else
                    return true;



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

        private void pretrazi_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();

        }

    }
}
