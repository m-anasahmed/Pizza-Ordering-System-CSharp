using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PizzaOrder
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if(textBox1.Text == ""  || textBox2.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else if (textBox1.Text == "user" && textBox2.Text == "pass")
            {
                Window2 obj = new Window2();
                obj.Show();
                this.Close();
            }
            else if (textBox1.Text == "chief" && textBox2.Text == "12345")
            {
                Window2 obj = new Window2();
                obj.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Missing Data!!!");
            }
        }

        private void closebutton1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            // this is a Text box for Taking Id
        }
    }

    class Function
    {
        private SqlConnection Con;
        private SqlCommand cmd;
        private DataTable dt;
        private SqlDataAdapter sda;
        private string ConStr;
        public Function()
        {
            ConStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\CodeBase\Sem-02-Project\Pizza-Ordering-System-CSharp\PizzaOrder\Database.mdf;Integrated Security=True";
            Con = new SqlConnection(ConStr);
            cmd = new SqlCommand();
            cmd.Connection = Con;
        }
        public DataTable GetData(string Query)
        {
            dt = new DataTable();
            sda = new SqlDataAdapter(Query, ConStr);
            sda.Fill(dt);
            return dt;
        }
        public int SetData(string Query) 
        {
            int Cnt = 0;
            if(Con.State == ConnectionState.Closed)
            {
                Con.Open();
            }
            cmd.CommandText = Query;
            Cnt = cmd.ExecuteNonQuery();
            Con.Close();
            return Cnt;
        }

    }
       

}
