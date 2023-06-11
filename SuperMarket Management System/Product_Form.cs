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
using System.Linq.Expressions;
using System.Web.UI.WebControls;

namespace SuperMarket_Management_System
{
    public partial class Product_Form : Form
    {
        public Product_Form()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Abubakar\source\repos\SuperMarket Management System\SuperMarket Management System\SuperMarket Management System\SuperMarket Management System\SMMSD.mdf"";Integrated Security=True");
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string query = "insert into ProductsTbl values(" + txtProductID.Text + ",'" + txtProductName.Text + "'," + txtProductQuantity.Text + "," + txtProductPrice.Text + ",'" + cbSelectCategory.SelectedValue.ToString() + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Added Successfully");
                Con.Close();
                Populate();
                txtProductID.Text = "";
                txtProductName.Text = "";
                txtProductQuantity.Text = "";
                txtProductPrice.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FillCategory()
        {
            // This message will blind the Combobox with the Database
            Con.Open();
            SqlCommand cmd = new SqlCommand("select CatName from CategoriesTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            cbSearchCategory.ValueMember = "CatName";
            cbSearchCategory.DataSource = dt;
            cbSelectCategory.ValueMember = "CatName";
            cbSelectCategory.DataSource = dt;
            Con.Close();
        }
        private void Populate()
        {
            Con.Open();
            string query = "Select * from ProductsTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Product_Form_Load(object sender, EventArgs e)
        {
            FillCategory();
            Populate();
        }

        private void ProductsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtProductID.Text = ProductsDGV.SelectedRows[0].Cells[0].Value.ToString();
            txtProductName.Text = ProductsDGV.SelectedRows[0].Cells[1].Value.ToString();
            txtProductQuantity.Text = ProductsDGV.SelectedRows[0].Cells[2].Value.ToString();
            txtProductPrice.Text = ProductsDGV.SelectedRows[0].Cells[3].Value.ToString();
            cbSelectCategory.SelectedValue = ProductsDGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProductID.Text == "" || txtProductName.Text == "" || txtProductQuantity.Text == "" || txtProductPrice.Text == "")
                {
                    MessageBox.Show("missing information");
                }
                else
                {
                    Con.Open();
                    string query = "update ProductsTbl set ProdName='" + txtProductName.Text + "',ProdQty=" + txtProductQuantity.Text + ",ProdPrice=" + txtProductPrice.Text + ",ProdCat='" + cbSelectCategory.SelectedValue.ToString() + "' where ProdId=" + txtProductID.Text + "; ";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Successfully Updated");
                    Con.Close();
                    Populate();
                    txtProductID.Text = "";
                    txtProductName.Text = "";
                    txtProductQuantity.Text = "";
                    txtProductPrice.Text = "";
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
                if(txtProductID.Text == "")
                {
                    MessageBox.Show("select the product to Delete");
                }
                else
                {
                    Con.Open();
                    string query = "delete from ProductsTbl where ProdId=" + txtProductID.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product deleted Successfully");
                    Con.Close();
                    Populate();
                    txtProductID.Text = "";
                    txtProductName.Text = "";
                    txtProductQuantity.Text = "";
                    txtProductPrice.Text = "";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbSearchCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Con.Open();
            string query = "select * from ProductsTbl where ProdCat='" + cbSearchCategory.SelectedValue.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder bulder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void btnSelling_Click(object sender, EventArgs e)
        {
            Selling_Form sell = new Selling_Form();
            sell.Show();
            this.Hide();

        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            Category_Form cat = new Category_Form();
            cat.Show();
            this.Hide();
        }
    }
}

