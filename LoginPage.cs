using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

    }
}
