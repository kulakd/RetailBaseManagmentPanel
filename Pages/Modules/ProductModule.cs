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
    public partial class ProductModule : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dawid\Documents\FRMSDB.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand sqlCommand = new SqlCommand();
        SqlDataReader reader;
        public ProductModule()
        {
            InitializeComponent();
            LoadCategory();
        }

        public void LoadCategory()
        {
            CategoryCombo.Items.Clear();
            sqlCommand = new SqlCommand("SELECT CategoryName FROM tbCategory",con);
            con.Open();
            reader = sqlCommand.ExecuteReader();
            while(reader.Read())
            {
                CategoryCombo.Items.Add(reader[0].ToString());
            }
            reader.Close();
            con.Close();
        }

        private void exitlbl_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Savebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to add this product?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sqlCommand = new SqlCommand("Insert INTO tbProduct(pname,pqty,pprice,pdescription,pcategory)VALUES(@pname,@pqty,@pprice,@pdescription,@pcategory)", con);
                    sqlCommand.Parameters.AddWithValue("@pname", ProductNameTxt.Text);
                    sqlCommand.Parameters.AddWithValue("@pqty", Convert.ToInt16(Quantitytxt.Text));
                    sqlCommand.Parameters.AddWithValue("@pprice", Convert.ToInt16(PriceTxt.Text));
                    sqlCommand.Parameters.AddWithValue("@pdescription", DescriptionTxt.Text);
                    sqlCommand.Parameters.AddWithValue("@pcategory", CategoryCombo.Text);

                    con.Open();
                    sqlCommand.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product has been added");
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
            ProductNameTxt.Clear();
            Quantitytxt.Clear();
            DescriptionTxt.Clear();
            PriceTxt.Clear();
            CategoryCombo.Text = "";
        }

        private void Clearbtn_Click(object sender, EventArgs e)
        {
            Clear();
            Savebtn.Enabled = true;
            Updatebtn.Enabled = false;
        }

        private void Updatebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this product?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sqlCommand = new SqlCommand("Update tbproduct SET pname=@pname,pqty=@pqty,pprice=@pprice,pdescription=@pdescription,pcategory=@pcategory WHERE pid LIKE '" + PIDlbl.Text + "'", con);
                    sqlCommand.Parameters.AddWithValue("@pname", ProductNameTxt.Text);
                    sqlCommand.Parameters.AddWithValue("@pqty", Convert.ToInt16(Quantitytxt.Text));
                    sqlCommand.Parameters.AddWithValue("@pprice", Convert.ToInt16(PriceTxt.Text));
                    sqlCommand.Parameters.AddWithValue("@pdescription", DescriptionTxt.Text);
                    sqlCommand.Parameters.AddWithValue("@pcategory", CategoryCombo.Text);
                    con.Open();
                    sqlCommand.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product has been Updated");
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
