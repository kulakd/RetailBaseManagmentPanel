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
    public partial class CustomerModule : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=RetailDB;Integrated Security=True;Pooling=False");
        SqlCommand sqlCommand = new SqlCommand();
        public CustomerModule()
        {
            InitializeComponent();
        }

        private void Savebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this customer?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sqlCommand = new SqlCommand("Insert INTO tbCustomer(cname,cphone)VALUES(@cname,@cphone)", con);
                    sqlCommand.Parameters.AddWithValue("@cname", CusNametxt.Text);
                    sqlCommand.Parameters.AddWithValue("@cphone", CusPhonetxt.Text);
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
            CusNametxt.Clear();
            CusPhonetxt.Clear();
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
                if (MessageBox.Show("Are you sure you want to update this customer?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sqlCommand = new SqlCommand("Update tbCustomer SET cname=@cname,cphone=@cphone WHERE cid LIKE '" + ClClbl.Text + "'", con);
                    sqlCommand.Parameters.AddWithValue("@cname", CusNametxt.Text);
                    sqlCommand.Parameters.AddWithValue("@cphone", CusPhonetxt.Text);
                    con.Open();
                    sqlCommand.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Customer has been Updated");
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
