using RetailBaseManagmentPanel.Pages.Modules;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetailBaseManagmentPanel.Pages.Main
{
    public partial class Order : Form
    {
        public Order()
        {
            InitializeComponent();
        }

        private void customerButton1_Click(object sender, EventArgs e)
        {
            OrderModules orderModules = new OrderModules();
            orderModules.Insertbtn.Enabled = true;
            orderModules.Updatebtn.Enabled = false;
            orderModules.ShowDialog();
        }
    }
}
