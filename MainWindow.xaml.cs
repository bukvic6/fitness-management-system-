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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Util.Instance.CitanjeEntiteta();
            InitializeComponent();
        }
        private void BtnFitnesCentar_Click(object sender, RoutedEventArgs e)
        {
            NeregistrovaniWindow neregistrovaniWindow = new NeregistrovaniWindow();
            neregistrovaniWindow.Show();
        }

        private void BtnRegistracija_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow1 registrationWindow = new RegistrationWindow1();
            registrationWindow.Show();
                
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            this.Hide();
            loginWindow.Show();

        }
    }
}
