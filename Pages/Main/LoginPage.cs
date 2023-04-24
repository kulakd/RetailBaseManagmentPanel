using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetailBaseManagmentPanel
{
    public partial class LoginPage : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=RetailDB;Integrated Security=True;Pooling=False");
        SqlCommand sqlCommand = new SqlCommand();
        SqlDataReader reader;
        public LoginPage()
        {
            InitializeComponent();
        }

        private void checkboxPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkboxPassword.Checked)
                Passwordtxt.UseSystemPasswordChar = true;
            else
                Passwordtxt.UseSystemPasswordChar = false;
        }

        private void clearlbl_Click(object sender, EventArgs e)
        {
            Passwordtxt.Clear();
            UserNametxt.Clear();
        }

        private void exitlbl_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to close this app?", "Exit Application", MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes) 
                Application.Exit();
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            try 
            {
                sqlCommand=new SqlCommand("SELECT * FROM tbUSER WHERE username=@username AND password=@password",con); ;
                sqlCommand.Parameters.AddWithValue("@username", UserNametxt.Text);
                sqlCommand.Parameters.AddWithValue("@password", Passwordtxt.Text);
                con.Open();
                reader = sqlCommand.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    MessageBox.Show("Welcome " + reader["fullname"].ToString() + "|", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Main main = new Main();
                    this.Hide();
                    main.ShowDialog();
                }
                else
                    MessageBox.Show("Invalid Password or username", "ACCESS DENIAD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
