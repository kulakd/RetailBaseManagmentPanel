using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetailBaseManagmentPanel
{
    public partial class Customer : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dawid\Documents\FRMSDB.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand sqlCommand = new SqlCommand();
        SqlDataReader reader;
        public Customer()
        {
            InitializeComponent();
            LoadCustomer();
        }

        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            sqlCommand = new SqlCommand("SELECT * FROM tbCustomer", con);
            con.Open();
            reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, reader[0].ToString(), reader[1].ToString(), reader[2].ToString());
            }
            reader.Close();
            con.Close();
        }

        private void customerButton1_Click(object sender, EventArgs e)
        {
            CustomerModule customerModule = new CustomerModule();
            customerModule.Savebtn.Enabled = true;
            customerModule.Updatebtn.Enabled = false;
            customerModule.ShowDialog();
            LoadCustomer();
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCustomer.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                CustomerModule cusotmerModule = new CustomerModule();
                cusotmerModule.ClClbl.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                cusotmerModule.CusNametxt.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
                cusotmerModule.CusPhonetxt.Text = dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();
                
                cusotmerModule.Savebtn.Enabled = false;
                cusotmerModule.Updatebtn.Enabled = true;
                cusotmerModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this customer?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    sqlCommand = new SqlCommand("DELETE FROM tbCustomer WHERE CId LIKE  '" + dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    sqlCommand.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been deleted");
                }
            }
            LoadCustomer();
        }
    }
}
