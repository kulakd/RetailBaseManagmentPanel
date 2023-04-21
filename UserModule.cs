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
    public partial class UserModule : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dawid\Documents\FRMSDB.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand sqlCommand = new SqlCommand();
        public UserModule()
        {
            InitializeComponent();
        }

        private void exitlbl_Click(object sender, EventArgs e)
        {
           this.Dispose();
        }

        private void Savebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if(ConPasstxt!=Passwordtxt)
                {
                    MessageBox.Show("Password did not match!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Are you sure you want to save this user?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sqlCommand = new SqlCommand("Insert INTO tbUser(username,fullname,password,phone)VALUES(@username,@fullname,@password,@phone)", con);
                    sqlCommand.Parameters.AddWithValue("@username", UserNametxt.Text);
                    sqlCommand.Parameters.AddWithValue("@fullname", FullNametxt.Text);
                    sqlCommand.Parameters.AddWithValue("@password", Passwordtxt.Text);
                    sqlCommand.Parameters.AddWithValue("@phone", Phonetxt.Text);
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

        private void Clearbtn_Click(object sender, EventArgs e)
        {
            Clear();
            Savebtn.Enabled = true;
            Updatebtn.Enabled = false;
        }

        public void Clear()
        {
            UserNametxt.Clear();
            FullNametxt.Clear();
            ConPasstxt.Clear();
            Passwordtxt.Clear();
            Phonetxt.Clear();
        }

        private void Updatebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConPasstxt != Passwordtxt)
                {
                    MessageBox.Show("Password did not match!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Are you sure you want to update this user?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sqlCommand = new SqlCommand("Update tbUser SET fullname=@fullname,password=@password,phone=@phone WHERE username LIKE '"+UserNametxt.Text+"'", con);
                    sqlCommand.Parameters.AddWithValue("@fullname", FullNametxt.Text);
                    sqlCommand.Parameters.AddWithValue("@password", Passwordtxt.Text);
                    sqlCommand.Parameters.AddWithValue("@phone", Phonetxt.Text);
                    con.Open();
                    sqlCommand.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("User has been Updated");
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
