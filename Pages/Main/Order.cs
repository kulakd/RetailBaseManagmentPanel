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
            dgvOrder.Rows.Clear();
            sqlCommand = new SqlCommand("SELECT orderid, O.pid, P.pname, O.cid, C.cname, qty, price, total FROM tbOrder AS O JOIN tbCustomer AS C ON O.cid=C.cid JOIN tbProduct AS P ON O.pid=P.pid WHERE CONCAT(orderid, O.pid, P.pname, O.cid, C.cname, qty) LIKE '%"+Searchtxt.Text+"%'", con);
            con.Open();
            reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                i++;
                dgvOrder.Rows.Add(i, reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString());
            }
            reader.Close();
            con.Close();
        }

        private void customerButton1_Click(object sender, EventArgs e)
        {
            OrderModules orderModules = new OrderModules();
            orderModules.Insertbtn.Enabled = true;
            orderModules.ShowDialog();
            LoadOrder();
        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvOrder.Columns[e.ColumnIndex].Name;
     
            if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this order?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    sqlCommand = new SqlCommand("DELETE FROM tbOrder WHERE orderid LIKE  '" + dgvOrder.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    sqlCommand.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been deleted");
                }
            }
            LoadOrder();
        }

        private void Searchtxt_TextChanged(object sender, EventArgs e)
        {
            LoadOrder();
        }
    }
}
