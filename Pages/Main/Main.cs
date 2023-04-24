using RetailBaseManagmentPanel.Pages.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetailBaseManagmentPanel
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null) 
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            Mainpanel.Controls.Add(childForm);
            Mainpanel.Tag = childForm; ;
            childForm.BringToFront();
            childForm.Show();
        }

        private void Userbtn_Click(object sender, EventArgs e)
        {
            openChildForm(new User());
        }

        private void Customersbtn_Click(object sender, EventArgs e)
        {
            openChildForm(new Customer());
        }

        private void Categoriesbtn_Click(object sender, EventArgs e)
        {
            openChildForm(new Category());
        }

        private void Productbtn_Click(object sender, EventArgs e)
        {
            openChildForm(new Product());
        }

        private void Ordersbtn_Click(object sender, EventArgs e)
        {
            openChildForm(new Order());
        }

    }
}
