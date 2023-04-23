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
    public partial class Category : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dawid\Documents\FRMSDB.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand sqlCommand = new SqlCommand();
        SqlDataReader reader;
        public Category()
        {
            InitializeComponent();
            LoadCategory();
        }

        public void LoadCategory()
        {
            int i = 0;
            dgvCategory.Rows.Clear();
            sqlCommand = new SqlCommand("SELECT * FROM tbCategory", con);
            con.Open();
            reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                i++;
                dgvCategory.Rows.Add(i, reader[0].ToString(), reader[1].ToString());
            }
            reader.Close();
            con.Close();
        }

        private void customerButton1_Click(object sender, EventArgs e)
        {
            CategoryModule categoryModule = new CategoryModule();
            categoryModule.Savebtn.Enabled = true;
            categoryModule.Updatebtn.Enabled = false;
            categoryModule.ShowDialog();
            LoadCategory();
        }

        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCategory.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                CategoryModule categoryModule = new CategoryModule();
                categoryModule.CategoryIdlbl.Text = dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();
                categoryModule.CatNametxt.Text = dgvCategory.Rows[e.RowIndex].Cells[2].Value.ToString();

                categoryModule.Savebtn.Enabled = false;
                categoryModule.Updatebtn.Enabled = true;
                categoryModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this category?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    sqlCommand = new SqlCommand("DELETE FROM tbCategory WHERE CatId LIKE  '" + dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    sqlCommand.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been deleted");
                }
            }
            LoadCategory();
        }
    }
}
