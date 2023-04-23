using RetailBaseManagmentPanel.Pages.Modules;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetailBaseManagmentPanel.Pages.Main
{
    public partial class Order : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dawid\Documents\FRMSDB.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand sqlCommand = new SqlCommand();
        SqlDataReader reader;
        public Order()
        {
            InitializeComponent();
            LoadOrder();
        }

        public void LoadOrder()
        {
            int i = 0;
            dgvUser.Rows.Clear();
            sqlCommand = new SqlCommand("SELECT * FROM tbOrder", con);
            con.Open();
            reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                i++;
                dgvUser.Rows.Add(i, reader[0].ToString(), Convert.ToDateTime(reader[1].ToString()).ToString("dd/MM/yyyy"), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString());
            }
            reader.Close();
            con.Close();
        }

        private void customerButton1_Click(object sender, EventArgs e)
        {
            OrderModules orderModules = new OrderModules();
            orderModules.Insertbtn.Enabled = true;
            orderModules.Updatebtn.Enabled = false;
            orderModules.ShowDialog();
        }
    }
}
