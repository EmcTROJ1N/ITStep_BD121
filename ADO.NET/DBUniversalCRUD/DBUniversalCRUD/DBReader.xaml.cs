using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Remoting.Contexts;
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
using System.Xml.Linq;

namespace DBUniversalCRUD
{
    /// <summary>
    /// Логика взаимодействия для DBReader.xaml
    /// </summary>
    public partial class DBReader : UserControl
    {
        SqlConnection Connection;
        public string ConnectionString;

        public DBReader() =>
            InitializeComponent();
        public DBReader(string connectionString, Dictionary<string, DataGrid> templates = null)
        {
            InitializeComponent();
            Connection = new SqlConnection(connectionString);
            ConnectionString = connectionString;
            Title.Text = Connection.Database;
            
            Connection.Open();
            SqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM INFORMATION_SCHEMA.TABLES";
            SqlDataReader tables = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            if (Connection.State == ConnectionState.Open)
            {
                TabItem otherElementsTab = new TabItem();
                otherElementsTab.Header = "Other";
                DataGrid otherElementsGrid = new DataGrid();
                DataTable otherElementsTable = new DataTable();
                otherElementsTable.Columns.Add("Element name", typeof(string));
                otherElementsTable.Columns.Add("Element type", typeof(string));

                while (tables.Read())
                {
                    if ((string)tables["TABLE_TYPE"] != "BASE TABLE")
                    {
                        DataRow newRow = otherElementsTable.NewRow();
                        newRow["Element name"] = (string)tables["TABLE_NAME"];
                        newRow["Element type"] = (string)tables["TABLE_TYPE"];
                        otherElementsTable.Rows.Add(newRow);
                        continue;
                    }
                    TabItem tableItem = new TabItem();
                    tableItem.Header = (string)tables["TABLE_NAME"];

                    SqlCommand getTable = new SqlCommand($"select * from {(string)tables["TABLE_NAME"]}", Connection);
                    using (SqlDataReader tableRes = getTable.ExecuteReader())
                    {
                        DataGrid tableGrid;

                        if (templates != null && templates.ContainsKey((string)tables["TABLE_NAME"]))
                            tableGrid = templates[(string)tables["TABLE_NAME"]];
                        else tableGrid = new DataGrid();

                        tableGrid.CellEditEnding += TableData_CellEditEnding;
                        tableGrid.Name = (string)tables["TABLE_NAME"];

                        DataTable dataTable = new DataTable();
                        dataTable.TableName = (string)tables["TABLE_NAME"];
                        dataTable.RowDeleted += DataTable_RowDeleted;
                        dataTable.Load(tableRes);
                        tableGrid.ItemsSource = dataTable.DefaultView;

                        tableItem.Content = tableGrid;
                        TablesTabs.Items.Add(tableItem);

                        if (templates == null || templates.ContainsKey((string)tables["TABLE_NAME"]))
                            continue;
                        tableGrid.AutoGenerateColumns = false;

                        foreach (DataColumn column in dataTable.Columns)
                        {
                            DataGridTextColumn textColumn = new DataGridTextColumn();
                            textColumn.Header = column.ColumnName;
                            Binding binding = new Binding(column.ColumnName);
                            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                            textColumn.Binding = binding;

                            tableGrid.Columns.Add(textColumn);
                        }

                    }
                }
                otherElementsGrid.ItemsSource = otherElementsTable.DefaultView;
                otherElementsTab.Content = otherElementsGrid;
                TablesTabs.Items.Add(otherElementsTab);
                Connection.Close();
            }
        }

        private void DataTable_RowDeleted(object sender, DataRowChangeEventArgs e)
        {
            DataTable dt = sender as DataTable;
            Connection.Open();

            new SqlCommand($"delete from {dt.TableName}", Connection).ExecuteNonQuery();

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Connection))
            {
                bulkCopy.DestinationTableName = dt.TableName;
                bulkCopy.WriteToServer(dt);
            }
            Connection.Close();
        }

        private void TableData_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            DataGrid grid = sender as DataGrid;

            DataTable dt = ((DataView)grid.ItemsSource).Table;

            Connection.Open();

            new SqlCommand($"delete from {grid.Name}", Connection).ExecuteNonQuery();

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Connection))
            {
                bulkCopy.DestinationTableName = grid.Name;
                bulkCopy.WriteToServer(dt);
            }
            Connection.Close();
        }
    }
}
