using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        Function Con;
        int GrdTotal = 0;
        private string initialGrandTotal; // variable to store original value

        public Window2()
        {
            Con = new Function();
            InitializeComponent();
            initialGrandTotal = Grandtotal.Content.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //this is a setting button used to set price
            CheifLogin obj = new CheifLogin();
            obj.Show();
            this.Close();
        }
        int key = 0;
        int Price = 0;
        private void Getprice(int key)
        {
            string Pizza = "";
            if (key == 1)
            {
                Pizza = "Small";
            }
            else if (key == 2)
            {
                Pizza = "Medium";
            }
            else if (key == 3)
            {
                Pizza = "Large";
            }
            else if (key == 4)
            {
                Pizza = "Extra Large";
            }
            string Query = "Select * from Pizzatable where item ='{0}'";
            Query = string.Format(Query, Pizza);
            DataTable dt = Con.GetData(Query);
            if (dt.Rows.Count > 0)
            {
                Price = Convert.ToInt32(dt.Rows[0][1].ToString());
            }
            else
            {
                // Handle the case where the query did not return any results
            }
        }
        public string Item;
        public int n = 0;

        public void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // this is a Add to cart button
            int Qty = 0;

            if (radiobutton1.IsChecked == true)
            {
                key = 1;
                Item = "Small Pizza";
            }
            else if (radiobutton2.IsChecked == true)
            {
                key = 2;
                Item = "Medium Pizza";
            }
            else if (radiobutton3.IsChecked == true)
            {
                key = 3;
                Item = "Large Pizza";
            }
            else if (radiobutton4.IsChecked == true)
            {
                key = 4;
                Item = "Extra Large Pizza";
            }
            else
            {
                MessageBox.Show("Please select a pizza size.");
            }

            Getprice(key);

            if (!int.TryParse(textBox3.Text, out Qty) || Qty == 0)
            {
                MessageBox.Show("Invalid quantity value. Please enter a valid integer that is not equal to zero.");
                return;
            }

            int total = Qty * Price;

            if (Datagrid1.ItemsSource == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Item", typeof(string));
                dt.Columns.Add("Price", typeof(int));
                dt.Columns.Add("Quantity", typeof(int));
                dt.Columns.Add("Total", typeof(int));
                Datagrid1.ItemsSource = dt.AsDataView();
            }
            DataRow newRow = ((DataView)Datagrid1.ItemsSource).Table.NewRow();
            newRow["ID"] = n + 1;
            n++;
            newRow["Item"] = Item;
            newRow["Price"] = Price;
            newRow["Quantity"] = Qty;
            newRow["Total"] = total;
            ((DataView)Datagrid1.ItemsSource).Table.Rows.Add(newRow);

            foreach (DataRowView rowView in ((DataView)Datagrid1.ItemsSource))
            {
                
                DataRow row = rowView.Row;

                GrdTotal = ((DataView)Datagrid1.ItemsSource)
                            .Table.AsEnumerable()
                            .Sum(r => r.Field<int>("Total"));

            }
            Grandtotal.Visibility = Visibility.Visible;
            Grandtotal.Content = Convert.ToString("Rs" + GrdTotal);
            Datagrid1.Items.Refresh();


        }
        private void DataGrid_SelectionChanged_3(object sender, SelectionChangedEventArgs e)
        {
            //this is a Data grid.
        }
        private void button786_Click(object sender, RoutedEventArgs e)
        {
            if (Datagrid1.ItemsSource != null)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to confirm order?", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    //Code to execute if the user clicks Yes
                    //MessageBox.Show("Thankyou for ordering YUMM YUMMs PIZZA");
                    Print1 pr = new Print1();
                    pr.Show();
                    PrintBill(pr.reader);
                    Datagrid1.ItemsSource = null;

                    // Reset grand total
                    Grandtotal.Content = initialGrandTotal;
                }
                else
                {
                    // Code to execute if the user clicks No
                }
            }
            else
            {
                MessageBox.Show("Please Order First!!!");
            }
        }
        public void PrintBill(FlowDocumentReader reader)
        {
            // Create a FlowDocument
            FlowDocument doc = new FlowDocument();
            doc.PagePadding = new Thickness(50);

            // Add a title to the FlowDocument
            Paragraph title = new Paragraph(new Run("**PIZZA BILL RECEIPT**"));
            Paragraph Love_message = new Paragraph(new Run("**THANK YOU FOR HAVING MEAL AT YUMM YUMM PIZZA**"));
            Paragraph manage_By = new Paragraph(new Run("**Program designed and managed by \n Sudais, Anas, Saad and Usman **"));

            title.FontSize = 24;
            title.TextAlignment = TextAlignment.Center;
            doc.Blocks.Add(title);

            // Create a Table
            Table table = new Table();
            table.CellSpacing = 0;
            table.BorderBrush = Brushes.Black;
            table.BorderThickness = new Thickness(1);

            // Add columns to the table
            for (int i = 0; i < Datagrid1.Columns.Count; i++)
            {
                table.Columns.Add(new TableColumn());
            }

            // Create a TableRowGroup and add it to the table
            TableRowGroup group = new TableRowGroup();
            table.RowGroups.Add(group);

            // Create a header row and add it to the group
            TableRow headerRow = new TableRow();
            group.Rows.Add(headerRow);
            for (int i = 0; i < Datagrid1.Columns.Count; i++)
            {
                headerRow.Cells.Add(new TableCell(new Paragraph(new Run(Datagrid1.Columns[i].Header.ToString()))));
                headerRow.Cells[i].BorderBrush = Brushes.Black;
                headerRow.Cells[i].BorderThickness = new Thickness(1);
                headerRow.Cells[i].FontWeight = FontWeights.Bold;
                headerRow.Cells[i].TextAlignment = TextAlignment.Center;
                headerRow.Cells[i].Background = Brushes.LightGray;
            }

            // Add data rows to the group
            for (int i = 0; i < Datagrid1.Items.Count; i++)
            {
                DataRowView rowView = Datagrid1.Items[i] as DataRowView;
                if (rowView != null)
                {
                    TableRow dataRow = new TableRow();
                    group.Rows.Add(dataRow);
                    for (int j = 0; j < Datagrid1.Columns.Count; j++)
                    {
                        dataRow.Cells.Add(new TableCell(new Paragraph(new Run(rowView[j].ToString()))));
                        dataRow.Cells[j].BorderBrush = Brushes.Black;
                        dataRow.Cells[j].BorderThickness = new Thickness(1);
                        dataRow.Cells[j].TextAlignment = TextAlignment.Center;
                    }
                }
                else
                {
                    // Handle the case where the cast failed
                }
            }
            // Add a row to display the grand total
            TableRow totalRow = new TableRow();
            group.Rows.Add(totalRow);
            totalRow.Cells.Add(new TableCell(new Paragraph(new Run("Grand Total:"))));
            totalRow.Cells[0].ColumnSpan = Datagrid1.Columns.Count - 1;
            totalRow.Cells[0].BorderBrush = Brushes.Black;
            totalRow.Cells[0].BorderThickness = new Thickness(1);
            totalRow.Cells[0].FontWeight = FontWeights.Bold;
            totalRow.Cells[0].TextAlignment = TextAlignment.Right;
            totalRow.Cells.Add(new TableCell(new Paragraph(new Run(GrdTotal.ToString()))));
            totalRow.Cells[1].BorderBrush = Brushes.Black;
            totalRow.Cells[1].BorderThickness = new Thickness(1);
            totalRow.Cells[1].TextAlignment = TextAlignment.Center;

            // Add the table to the FlowDocument
            doc.Blocks.Add(table);

            // Display the FlowDocument in the FlowDocumentReader control
            reader.Document = doc;
            doc.Blocks.Add(Love_message);
            doc.Blocks.Add(manage_By);
        }

        private void logoutbutton1_Click(object sender, RoutedEventArgs e)
        {
            // this is a logout button 
            MainWindow obj = new MainWindow();
            obj.Show();
            this.Close();
        }
    }
}