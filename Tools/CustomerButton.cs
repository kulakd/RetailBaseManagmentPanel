﻿using System;
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
    public partial class CustomerButton : PictureBox
    {
        public CustomerButton()
        {
            InitializeComponent();
        }

        private Image Normal;
        private Image Hover;
        
        public Image ImageNormal { get { return Normal; } set {Normal=value; } }
        public Image ImageHover { get { return Hover; } set { Hover=value; } }


        private void CustomerButton_MouseHover(object sender, EventArgs e)
        {
            this.Image = Hover;
        }

        private void CustomerButton_MouseLeave(object sender, EventArgs e)
        {
            this.Image = Normal;
        }
    }
}
