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
using Bunifu.UI.WinForms.Helpers.Transitions;
using System.Web.UI.WebControls;

namespace SuperMarket_Management_System
{
    public partial class Category_Form : Form
    {
        public Category_Form()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Abubakar\source\repos\SuperMarket Management System\SuperMarket Management System\SuperMarket Management System\SuperMarket Management System\SMMSD.mdf"";Integrated Security=True");
        private void Populate()
        {
            Con.Open();
            string query = "select * from CategoriesTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CategoriesDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string query = "insert into CategoriesTbl values(" + txtCategoryID.Text + ",'" + txtCategoryName.Text + "','" + txtCategoryDescription.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Categgory Added Succesfully");
                Con.Close();
                Populate();
                txtCategoryID.Text = "";
                txtCategoryName.Text = "";
                txtCategoryDescription.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Category_Form_Load(object sender, EventArgs e)
        {
            Populate();
        }

        private void CategoriesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCategoryID.Text = CategoriesDGV.SelectedRows[0].Cells[0].Value.ToString();
            txtCategoryName.Text = CategoriesDGV.SelectedRows[0].Cells[1].Value.ToString();
            txtCategoryDescription.Text = CategoriesDGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try 
            {
                if (txtCategoryID.Text == "" || txtCategoryName.Text == "" || txtCategoryDescription.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    Con.Open();
                    string query = "Update CategoriesTbl set CatName='" + txtCategoryName.Text + "',CatDesc='" + txtCategoryDescription.Text + "' Where CatId=" + txtCategoryID.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category has been updated successfully");
                    Con.Close();
                    Populate();
                    txtCategoryID.Text = "";
                    txtCategoryName.Text = "";
                    txtCategoryDescription.Text = "";
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
                if (txtCategoryID.Text == "")
                {
                    MessageBox.Show("Select Category id to Delete");
                }
                else
                {
                    Con.Open();
                    string query = "delete from CategoriesTbl where CatId=" + txtCategoryID.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category has been deleted successfully");
                    Con.Close();
                    Populate();
                    txtCategoryID.Text = "";
                    txtCategoryName.Text = "";
                    txtCategoryDescription.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void btnSellers_Click(object sender, EventArgs e)
        {
            Seller_Form sell = new Seller_Form();
            sell.Show();
            this.Hide();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            Product_Form prod = new Product_Form();
            prod.Show();
            this.Hide();
        }

        private void btnSelling_Click(object sender, EventArgs e)
        {
            Selling_Form sell = new Selling_Form();
            sell.Show();
            this.Hide();
        }
    }
}
    

