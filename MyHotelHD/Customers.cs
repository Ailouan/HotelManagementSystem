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
    public partial class Customers : Form
    {
        SqlConnection cnx = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Acer\Documents\HotelDbase.mdf;Integrated Security=True;Connect Timeout=30");
        public Customers()
        {
            InitializeComponent();
            chargeCust();
        }
        int key = 0;
        private void chargeCust()
        {
            cnx.Open();
            string Query = "Select * from CustomerTbl";
            SqlDataAdapter dp = new SqlDataAdapter(Query, cnx);
            SqlCommandBuilder blind = new SqlCommandBuilder(dp);
            var ds = new DataSet();
            dp.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            cnx.Close();
        }
        private void InsertCustomers()
        {
            if (CustName.Text == "" || CustPhone.Text == "" || CustGender.SelectedIndex == -1 )
            {
                MessageBox.Show("Missing Information !!");
            }
            else
                try
                {
                    cnx.Open();
                    SqlCommand cmd = new SqlCommand("insert into CustomerTbl (CustName,CustPhone,CustGender) values(@CN,@CPh,@CG)", cnx);
                    cmd.Parameters.AddWithValue("@CN", CustName.Text);
                    cmd.Parameters.AddWithValue("@CPh", CustPhone.Text);
                    cmd.Parameters.AddWithValue("@CG", CustGender.SelectedItem);
                    cmd.ExecuteNonQuery();
                    cnx.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

        }
        private void EditCustpmer()
        {
            if (key == 0)
            {
                MessageBox.Show("Pleas Selectione User From Data");
            }
            else
                try
                {
                    cnx.Open();
                    SqlCommand cmd = new SqlCommand("Update CustomerTbl set CustName = @CN,CustPhone = @CPh , CustGender = @CG where CustNum = @key ", cnx);
                    cmd.Parameters.AddWithValue("@CN", CustName.Text);
                    cmd.Parameters.AddWithValue("@CPh", CustPhone.Text);
                    cmd.Parameters.AddWithValue("@CG", CustGender.Text);
                    cmd.Parameters.AddWithValue("@key", key);
                    cmd.ExecuteNonQuery();
                    cnx.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

        }
        private void DeleteCustomers()
        {
            if (key == 0)
            {
                MessageBox.Show("Pleas Selectione Cunstomer From Data");
            }
            else
            {
                try
                {
                    cnx.Open();
                    SqlCommand cmd = new SqlCommand("Delete CustomerTbl where CustNum = @key ", cnx);
                    cmd.Parameters.AddWithValue("@key", key);
                    cmd.ExecuteNonQuery();
                    cnx.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if(e.ColumnIndex >= 0 && e.RowIndex >= 0)
            //{
            //    CustName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            //    CustGender.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            //    CustPhone.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            //}
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                CustName.Text = row.Cells["CustName"].Value.ToString();
                CustGender.Text = row.Cells["CustGender"].Value.ToString();
                CustPhone.Text = row.Cells["CustPhone"].Value.ToString();
                if (CustName.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(row.Cells["CustNum"].Value.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InsertCustomers();
            chargeCust();
            MessageBox.Show("Customer Inserted");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditCustpmer();
            chargeCust();
            MessageBox.Show("Customer Updatet");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteCustomers();
            chargeCust();
            MessageBox.Show("Customer Deleted");
        }
    }
}
