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

namespace RetailBaseManagmentPanel.Pages.Modules
{
    public partial class OrderModules : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dawid\Documents\FRMSDB.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand sqlCommand = new SqlCommand();
        SqlDataReader reader;
        public OrderModules()
        {
            InitializeComponent();
            LoadCustomer();
            LoadProduct();
        }

        private void exitlbl_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            sqlCommand = new SqlCommand("SELECT CId,CName FROM tbCustomer WHERE CONCAT(CId,CName) LIKE '%"+SearchCustomertxt.Text+"%'", con);
            con.Open();
            reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, reader[0].ToString(), reader[1].ToString());
            }
            reader.Close();
            con.Close();
        }

        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            sqlCommand = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(pname,pqty,pprice,pdescription,pcategory) LIKE '%" + SearchProducttxt.Text + "%'", con);
            con.Open();
            reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString());
            }
            reader.Close();
            con.Close();
        }

        private void SearchCustomertxt_TextChanged(object sender, EventArgs e)
        {
            LoadCustomer();
        }

        private void SearchProducttxt_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        int quantity = 0;
       
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if(Convert.ToInt16(numericUpDown1.Value) > quantity)
            {
                MessageBox.Show("Not enough items in stock", "Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                numericUpDown1.Value = numericUpDown1.Value - 1;
                return;
            }
            int total = Convert.ToInt16(Pricetxt.Text) * Convert.ToInt16(numericUpDown1.Value);
            TotalTxt.Text = total.ToString();
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cidtxt.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            cnametxt.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            PIDtxt.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            pnametxt.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            Pricetxt.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
            quantity = Convert.ToInt16(dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString());
        }

        private void Insertbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (cidtxt.Text == "")
                {
                    MessageBox.Show("Please select customer!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (PIDtxt.Text == "")
                {
                    MessageBox.Show("Please select product!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Are you sure you want to order?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sqlCommand = new SqlCommand("Insert INTO tbOrder(odate,pid,cid,qty,price,total)VALUES(@odate,@pid,@cid,@qty,@price,@total)", con);
                    sqlCommand.Parameters.AddWithValue("@odate", odatetxt.Text);
                    sqlCommand.Parameters.AddWithValue("@pid", Convert.ToInt16(PIDtxt.Text));
                    sqlCommand.Parameters.AddWithValue("@cid", Convert.ToInt16(PIDtxt.Text));
                    sqlCommand.Parameters.AddWithValue("@qty", Convert.ToInt16(numericUpDown1.Text));
                    sqlCommand.Parameters.AddWithValue("@price", Convert.ToInt16(Pricetxt.Text));
                    sqlCommand.Parameters.AddWithValue("@total", Convert.ToInt16(TotalTxt.Text));
                    
                    con.Open();
                    sqlCommand.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("User has been saved");
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            cidtxt.Clear();
            cnametxt.Clear();
            PIDtxt.Clear();
            pnametxt.Clear();
            Pricetxt.Clear();
            numericUpDown1.Value = 1;
            TotalTxt.Clear();
            odatetxt.Value = DateTime.Now;
        }

        private void Clearbtn_Click(object sender, EventArgs e)
        {
            Clear();
            Insertbtn.Enabled = true;
            Updatebtn.Enabled = false;
        }
    }
}
