using RetailBaseManagmentPanel.Pages.Modules;
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

namespace RetailBaseManagmentPanel.Pages.Main
{
    public partial class Product : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dawid\Documents\FRMSDB.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand sqlCommand = new SqlCommand();
        SqlDataReader reader;
        public Product()
        {
            InitializeComponent();
            LoadProduct();
        }

        private void customerButton1_Click(object sender, EventArgs e)
        {
            ProductModule productModule = new ProductModule();
            productModule.Savebtn.Enabled = true;
            productModule.Updatebtn.Enabled = false;
            productModule.ShowDialog();
            LoadProduct();
        }
        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            sqlCommand = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(pname,pqty,pprice,pdescription,pcategory) LIKE '%"+Searchtxt.Text+"%'", con);
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

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                ProductModule productModule = new ProductModule();
                productModule.PIDlbl.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                productModule.ProductNameTxt.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                productModule.Quantitytxt.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                productModule.PriceTxt.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                productModule.DescriptionTxt.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
                productModule.CategoryCombo.Text = dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString();
                productModule.Savebtn.Enabled = false;
                productModule.Updatebtn.Enabled = true;
                productModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this Product?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    sqlCommand = new SqlCommand("DELETE FROM tbProduct WHERE pid LIKE  '" + dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    sqlCommand.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product has been deleted");
                }
            }
            LoadProduct();
        }

        private void Searchtxt_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }
    }


}
