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

namespace PizzaOrder
{
    /// <summary>
    /// Interaction logic for CheifLogin.xaml
    /// </summary>
    public partial class CheifLogin : Window
    {
        public CheifLogin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cheifTb_1.Text == "" || cheifpassTb_1.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else if (cheifTb_1.Text == "chief" && cheifpassTb_1.Text == "12345")
            {
                Settings obj = new Settings();
                obj.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Missing Data!!!");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window2 obj = new Window2();
            obj.Show();
            this.Close();
        }

        private void cheifpassTb_1_TextChanged(object sender, TextChangedEventArgs e)
        {
            /// just a simple text change no use.
        }
    }
}
