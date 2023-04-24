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
    public partial class CategoryModule : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=RetailDB;Integrated Security=True;Pooling=False");
        SqlCommand sqlCommand = new SqlCommand();
        public CategoryModule()
        {
            InitializeComponent();
        }

        private void Savebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this Category?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sqlCommand = new SqlCommand("Insert INTO tbCategory(catname)VALUES(@catname)", con);
                    sqlCommand.Parameters.AddWithValue("@catname", CatNametxt.Text);
                    con.Open();
                    sqlCommand.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Category has been saved");
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
            CatNametxt.Clear();
        }

        private void Clearbtn_Click(object sender, EventArgs e)
        {
            Clear();
            Savebtn.Enabled = true;
            Updatebtn.Enabled = false;
        }

        private void exitlbl_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Updatebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this category?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sqlCommand = new SqlCommand("Update tbCategory SET catname=@catname WHERE catid LIKE '" + CategoryIdlbl.Text + "'", con);
                    sqlCommand.Parameters.AddWithValue("@catname", CatNametxt.Text);
                    con.Open();
                    sqlCommand.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Category has been Updated");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
