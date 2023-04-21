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
    public partial class User : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dawid\Documents\FRMSDB.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand sqlCommand = new SqlCommand();
        SqlDataReader reader;
        public User()
        {
            InitializeComponent();
            LoadUser();
        }

        public void LoadUser()
        {
            int i = 0;
            dgvUser.Rows.Clear();
            sqlCommand= new SqlCommand("SELECT * FROM tbUser", con);
            con.Open();
            reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                i++;
                dgvUser.Rows.Add(i,reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString());
            }
            reader.Close();
            con.Close();
        }

        private void customerButton1_Click(object sender, EventArgs e)
        {
            UserModule userModule = new UserModule();
            userModule.Savebtn.Enabled = true;
            userModule.Updatebtn.Enabled = false;
            userModule.ShowDialog();
            LoadUser();

        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvUser.Columns[e.ColumnIndex].Name;
            if(colName == "Edit")
            {
                UserModule userModule= new UserModule();
                userModule.UserNametxt.Text = dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString();
                userModule.FullNametxt.Text = dgvUser.Rows[e.RowIndex].Cells[2].Value.ToString();
                userModule.Passwordtxt.Text = dgvUser.Rows[e.RowIndex].Cells[3].Value.ToString();
                userModule.Phonetxt.Text = dgvUser.Rows[e.RowIndex].Cells[4].Value.ToString();
                userModule.Savebtn.Enabled = false;
                userModule.Updatebtn.Enabled = true;
                userModule.UserNametxt.Enabled = false;
                userModule.ShowDialog();
            }
            else if(colName=="Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this user?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    sqlCommand = new SqlCommand("DELETE FROM tbUSER WHERE username LIKE  '" + dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    sqlCommand.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been deleted");
                }
            }
            LoadUser();
        }
    }
}
