using SR22_2020_POP2021.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddEditInstruktoraWindow.xaml
    /// </summary>
    public partial class AddEditInstruktoraWindow : Window
    {
        private RegistrovaniKorisnik odabraniInstruktor;
        private EStatus odabraniStatus;
        public AddEditInstruktoraWindow(RegistrovaniKorisnik instruktor,EStatus status = EStatus.DODAJ)
        {
            InitializeComponent();

            this.DataContext = instruktor;
            
            odabraniInstruktor = instruktor;
            odabraniStatus = status;

            if(status.Equals(EStatus.IZMENI) && instruktor != null)
            {
                this.Title = "izmeni instruktora";
            }
            else
            {
                this.Title = "Dodaj instruktora";
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (odabraniInstruktor.Equals(EStatus.DODAJ))
            {
                odabraniInstruktor.Aktivan = true;
                Instruktor instruktor = new Instruktor
                {
                    Korisnik = odabraniInstruktor
                };
                Util.Instance.Korisnici.Add(odabraniInstruktor);
                Util.Instance.Instruktori.Add(instruktor);
            }
            Util.Instance.SacuvajEntitet("korisnici.txt");

            this.DialogResult = true;
            this.Close();

        }
    }
}
