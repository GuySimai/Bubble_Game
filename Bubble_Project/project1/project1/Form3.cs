//Guy Simai
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace project1
{
    public partial class Form3 : Form
    {

        public Form3()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            ReadOrderData();
            tblDataGridView1.DataSource = tblBindingSource1;
            tblBindingNavigator1.BindingSource = tblBindingSource1;
        }

        private void ReadOrderData()
        {
            string queryString = "SELECT * FROM dbo.TblProducts";

            using (SqlConnection connection = new SqlConnection(Form1.connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                DataTable dataTable = new DataTable();
                dataTable.Load(reader);   

                reader.Close();

                tblBindingSource1.DataSource = dataTable;
            }
        }

        private void AdjustColumnOrder()
        {
            tblDataGridView1.Columns["ID"].DisplayIndex = 0;
            tblDataGridView1.Columns["Name"].DisplayIndex = 1;
            tblDataGridView1.Columns["Length"].DisplayIndex = 2;
        }

        // Q1 - Show who is the player who played the longest of all.
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string queryString = "SELECT * FROM dbo.TblProducts WHERE Length = (SELECT MAX(Length) FROM dbo.TblProducts)";

            using (SqlConnection connection = new SqlConnection(Form1.connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                tblBindingSource1.DataSource = dataTable;

                tblDataGridView1.DataSource = tblBindingSource1;

                AdjustColumnOrder();
                reader.Close();
            }
        }

        // Q2 - For each player, what is the longest game they have played.
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string queryString = "SELECT Name, MAX(Length) AS Max FROM dbo.TblProducts GROUP BY Name ORDER BY Max DESC";

            using (SqlConnection connection = new SqlConnection(Form1.connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                tblBindingSource1.DataSource = dataTable;

                tblDataGridView1.DataSource = tblBindingSource1;
                reader.Close();
            }
        }

        // Q3 - All records are displayed again as usual.
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ReadOrderData();
            AdjustColumnOrder();
        }
    }

}

