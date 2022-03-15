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
    public partial class Users : Form
    {
        SqlConnection cnx = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Acer\Documents\HotelDbase.mdf;Integrated Security=True;Connect Timeout=30");
        public Users()
        {
            InitializeComponent();
            ChargeUsers();
        }
        private void ChargeUsers()
        {
            cnx.Open();
            string Query = "Select * from UserTbl";
            SqlDataAdapter dp = new SqlDataAdapter(Query, cnx);
            SqlCommandBuilder blind = new SqlCommandBuilder(dp);
            var ds = new DataSet();
            dp.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            cnx.Close();
        }
        private void InsertCategorie()
        {
            if (Uname.Text == "" || Uphone.Text == "" || Ugender.SelectedIndex == -1 || Upass.Text == "")
            {
                MessageBox.Show("Missing Information !!");
            }
            else
                try
                {
                    cnx.Open();
                    SqlCommand cmd = new SqlCommand("insert into UserTbl (UName,UPhone,UGender,UPassword) values(@UN,@UPh,@UG,@UP)", cnx);
                    cmd.Parameters.AddWithValue("@UN", Uname.Text);
                    cmd.Parameters.AddWithValue("@UPh", Uphone.Text);
                    cmd.Parameters.AddWithValue("@UG", Ugender.Text);
                    cmd.Parameters.AddWithValue("@UP", Upass.Text);
                    cmd.ExecuteNonQuery();
                    cnx.Close();
                    MessageBox.Show("User Inserted !!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            InsertCategorie();
            ChargeUsers();
            Uname.Clear();
            Uphone.Clear();
            Upass.Clear();
        }
        int key = 0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                Uname.Text = row.Cells["UName"].Value.ToString();
                Uphone.Text = row.Cells["UPhone"].Value.ToString();
                Ugender.Text = row.Cells["UGender"].Value.ToString();
                Upass.Text = row.Cells["UPassword"].Value.ToString();
                if (Uname.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(row.Cells["UNum"].Value.ToString());
                }
            }
        }
        private void DeleteUsers()
        {
            if(key == 0 || Uname.Text == "" || Uphone.Text == "" || Upass.Text == "")
            {
                MessageBox.Show("Pleas Selectione Users From Data");
            }
            else
            {
                try
                {
                    cnx.Open();
                    SqlCommand cmd = new SqlCommand("Delete UserTbl where UNum = @key ", cnx);
                    cmd.Parameters.AddWithValue("@key", key);
                    cmd.ExecuteNonQuery();
                    cnx.Close();
                    MessageBox.Show("User Deleted");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
                

        }
        private void EditUsers()
        {
            if (key == 0)
            {
                MessageBox.Show("Pleas Selectione User From Data");
            }
            else
                try
                {
                    cnx.Open();
                    SqlCommand cmd = new SqlCommand("Update UserTbl set UPhone = @UPh , UPassword = @UP where UNum = @key ", cnx);
                    cmd.Parameters.AddWithValue("@UPh", Uphone.Text);
                    cmd.Parameters.AddWithValue("@UP", Upass.Text);
                    cmd.Parameters.AddWithValue("@key", key);
                    cmd.ExecuteNonQuery();
                    cnx.Close();
                    MessageBox.Show("User Updatet !!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteUsers();
            ChargeUsers();
            Uname.Clear();
            Uphone.Clear();
            Ugender.SelectedIndex = -1;
            Upass.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditUsers();
            ChargeUsers();
            Uname.Clear();
            Uphone.Clear();
            Ugender.SelectedIndex = -1;
            Upass.Clear();
        }
    }
}
