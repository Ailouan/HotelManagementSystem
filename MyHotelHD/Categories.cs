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

namespace MyHotelHD
{
    public partial class Categories : Form
    {
        SqlConnection cnx = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Acer\Documents\HotelDbase.mdf;Integrated Security=True;Connect Timeout=30");
        public Categories()
        {
            InitializeComponent();
            ChargeData();
        }

        private void ChargeData()
        {
            cnx.Open();
            string Query = "Select * from TYpeTbl";
            SqlDataAdapter dp = new SqlDataAdapter(Query, cnx);
            SqlCommandBuilder blind = new SqlCommandBuilder(dp);
            var ds = new DataSet();
            dp.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            cnx.Close();
        }
        private void InsertCategorie()
        {
            if (TNTb.Text == "" || TCCb.Text == "")
            {
                MessageBox.Show("Missing Information !!");
            }
            else
                try
                {
                    cnx.Open();
                    SqlCommand cmd = new SqlCommand("insert into TYpeTbl (TypeName,TypeCost) values(@TN,@TC)", cnx);
                    cmd.Parameters.AddWithValue("@TN", TNTb.Text);
                    cmd.Parameters.AddWithValue("@TC", TCCb.Text);
                    cmd.ExecuteNonQuery();
                    cnx.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

        }
        private void EditCategory()
        {
            if (TNTb.Text == "" || TCCb.Text == "")
            {
                MessageBox.Show("Missing Information !!");
            }
            else
                try
                {
                    cnx.Open();
                    SqlCommand cmd = new SqlCommand("Update TYpeTbl set TypeName = @TN,TypeCost = @TC where TypeNum = @key ", cnx);
                    cmd.Parameters.AddWithValue("@TN", TNTb.Text);
                    cmd.Parameters.AddWithValue("@TC", TCCb.Text);
                    cmd.Parameters.AddWithValue("@key", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Upditet");
                    cnx.Close();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

        }
        private void DeleteCategory()
        {
            if (TNTb.Text == "" || TCCb.Text == "")
            {
                MessageBox.Show("Missing Information !!");
            }
            else
                try
                {
                    cnx.Open();
                    SqlCommand cmd = new SqlCommand("Delete TYpeTbl where TypeNum = @key ", cnx);
                    cmd.Parameters.AddWithValue("@key", key);
                    cmd.ExecuteNonQuery();
                    cnx.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            InsertCategorie();
            ChargeData();
            TNTb.Clear();
            TCCb.Clear();
        }
        int key = 0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                TNTb.Text = row.Cells["TypeName"].Value.ToString();
                TCCb.Text = row.Cells["TypeCost"].Value.ToString();
                if (TNTb.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(row.Cells["TypeNum"].Value.ToString());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditCategory();
            ChargeData();
            TNTb.Clear();
            TCCb.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteCategory();
            ChargeData();
            TNTb.Clear();
            TCCb.Clear();
        }
    }
}
