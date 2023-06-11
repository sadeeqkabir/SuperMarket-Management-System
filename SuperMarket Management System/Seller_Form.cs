using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SuperMarket_Management_System
{
    public partial class Seller_Form : Form
    {
        public Seller_Form()
        {
            InitializeComponent();
        }
        readonly SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Abubakar\source\repos\SuperMarket Management System\SuperMarket Management System\SuperMarket Management System\SuperMarket Management System\SMMSD.mdf"";Integrated Security=True");

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string query = "insert into SellersTbl values(" + txtSellerID.Text + ",'" + txtSellerName.Text + "'," + txtSellerAge.Text + "," + txtSellerMobileNo.Text + ",'" + txtSellerPassword.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Seller Added Successfully");
                Con.Close();
                Populate();
                txtSellerID.Text = "";
                txtSellerName.Text = "";
                txtSellerAge.Text = "";
                txtSellerMobileNo.Text = "";
                txtSellerPassword.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSellerID.Text == "" || txtSellerName.Text == "" || txtSellerAge.Text == "" || txtSellerMobileNo.Text == "" || txtSellerPassword.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    Con.Open();
                    String query = "update SellersTbl set SellerName='" + txtSellerName.Text + "',SellerAge=" + txtSellerAge.Text + ",SellerMbileNo=" + txtSellerMobileNo.Text + ",SellerPassword=" + txtSellerPassword.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Successfully Updated");
                    Con.Close();
                    Populate();
                    txtSellerID.Text = "";
                    txtSellerName.Text = "";
                    txtSellerAge.Text = "";
                    txtSellerMobileNo.Text = "";
                    txtSellerPassword.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtSellerID.Text == "")
                {
                    MessageBox.Show("Select the Seller to Delete");
                }
                else
                {
                    Con.Open();
                    string query = "delete from SellersTbl where SellerId=" + txtSellerID.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Deleted Successfuully");
                    Con.Close();
                    Populate();
                    txtSellerID.Text = "";
                    txtSellerName.Text = "";
                    txtSellerAge.Text = "";
                    txtSellerMobileNo.Text = "";
                    txtSellerPassword.Text = "";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Populate()
        {
            Con.Open();
            string query = "select * from SellersTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SellersDGV.DataSource = ds.Tables[0];
            Con.Close(); 
        }
        private void Seller_Form_Load(object sender, EventArgs e)
        {
            Populate();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void btnSelling_Click(object sender, EventArgs e)
        {
            Seller_Form sell = new Seller_Form();
            sell.Show();
            this.Hide();
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            Category_Form cat = new Category_Form();
            cat.Show();
            this.Hide();
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            Product_Form prod = new Product_Form();
            prod.Show();
            this .Hide();
        }
    }
}
