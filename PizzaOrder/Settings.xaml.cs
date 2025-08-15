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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        Function Con;
        public Settings()
        {
            InitializeComponent();
            Con = new Function();
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            Window2 obj = new Window2();
            obj.Show();
            this.Close();
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            //This a submit button used to submit price if we want to change the prize.
            string key;
            try
            {
                int Pr = Convert.ToInt32(textBox4.Text);
                if (combobox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Please Select a Pizza!!!");
                }
                else if (combobox1.SelectedIndex == 0)
                {
                    key = "Extra Large";
                    string Query = "Update Pizzatable set Price = {0} where Item = '{1}' ";
                    Query = string.Format(Query, Pr, key);
                    Con.SetData(Query);
                    MessageBox.Show("Price Updated!!!");
                }
                else if(combobox1.SelectedIndex == 1) 
                {
                    key = "Large";
                    string Query = "Update Pizzatable set Price = {0} where Item = '{1}' ";
                    Query = string.Format(Query, Pr, key);
                    Con.SetData(Query);
                    MessageBox.Show("Price Updated!!!");
                }
                else if(combobox1.SelectedIndex == 2)
                {
                    key = "Medium";
                    string Query = "Update Pizzatable set Price = {0} where Item = '{1}' ";
                    Query = string.Format(Query, Pr, key);
                    Con.SetData(Query);
                    MessageBox.Show("Price Updated!!!");
                }
                else if(combobox1.SelectedIndex == 3)
                {
                    key = "Small";
                    string Query = "Update Pizzatable set Price = {0} where Item = '{1}' ";
                    Query = string.Format(Query, Pr, key);
                    Con.SetData(Query);
                    MessageBox.Show("Price Updated!!!");
                }
            }
            catch (Exception Ex) 
            {
                MessageBox.Show(Ex.Message);
            }
        }
    }
}
