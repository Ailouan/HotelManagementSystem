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
    public partial class Bookings : Form
    {
        SqlConnection cnx = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Acer\Documents\HotelDbase.mdf;Integrated Security=True;Connect Timeout=30");

        public Bookings()
        {
            InitializeComponent();
            ChargeBooking();
            GetRoom();
            GetCustomer();
        }
        private void ChargeBooking()
        {
            cnx.Open();
            string Query = "Select * from BookingTbl";
            SqlDataAdapter dp = new SqlDataAdapter(Query, cnx);
            SqlCommandBuilder blind = new SqlCommandBuilder(dp);
            var ds = new DataSet();
            dp.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            cnx.Close();
        }

        private void GetRoom()
        {
            cnx.Open();
            SqlCommand cmd = new SqlCommand("Select * from RoomTbl where RStatus = 'Available'",cnx);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("RNum", typeof(int));
            dt.Load(dr);
            Rcb.DisplayMember = "RName";
            Rcb.ValueMember = "RNum";
            Rcb.DataSource = dt;
            cnx.Close();

        }
        private void GetCustomer()
        {
            cnx.Open();
            SqlCommand cmd = new SqlCommand("Select * from CustomerTbl", cnx);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustNum", typeof(int));
            dt.Load(dr);
            Ccb.DisplayMember = "CustName";
            Ccb.ValueMember = "CustNum";
            Ccb.DataSource = dt;
            cnx.Close();

        }
        int price = 1;
        private void fechCost()
        {
            cnx.Open();
            SqlCommand cmd = new SqlCommand("select TypeCost from RoomTbl join TYpeTbl on Rtype = TypeNum where RNum = '"+Rcb.SelectedValue.ToString()+"'",cnx);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                price = Convert.ToInt32(dr["TypeCost"].ToString());
            }
            cnx.Close();
        }
        private void Booking()
        {
            if (Rcb.Text == "" || Ccb.Text == "" || Duration.Text == "" || Amount.Text == "")
            {
                MessageBox.Show("Missing Information !!");
            }
            else
                try
                {
                    cnx.Open();
                    SqlCommand cmd = new SqlCommand("insert into BookingTbl (Room,Customer,BookDate,Duration,Cost) values(@Rom,@Cust,@Dat,@Dure,@Cost)", cnx);
                    cmd.Parameters.AddWithValue("@Rom", Rcb.SelectedValue);
                    cmd.Parameters.AddWithValue("@Cust", Ccb.SelectedValue);
                    cmd.Parameters.AddWithValue("@Dat", dat.Value.Date);
                    cmd.Parameters.AddWithValue("@Dure", Duration.Text);
                    cmd.Parameters.AddWithValue("@Cost", Amount.Text);
                    cmd.ExecuteNonQuery();
                    cnx.Close();
                    MessageBox.Show("Booking Inserted");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Booking();
            ChargeBooking();
            setAvailable();
        }

        private void Rcb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fechCost();
        }

        private void Duration_TextChanged(object sender, EventArgs e)
        {
            if (Amount.Text == "")
            {
                Amount.Text = "Rs 0";
            }
            else
            {
                try
                {
                    int tolatl = price * Convert.ToInt32(Duration.Text);
                    Amount.Text = tolatl.ToString();
                }
                catch(Exception ex)
                {
                    //MessageBox.Show("");
                }
            }
            
        }
        int key = 0;
        private void setAvailable()
        {
            try
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand("Update RoomTbl set RStatus = @RS where RNum = @key", cnx);
                cmd.Parameters.AddWithValue("@RS", "Booked");
                cmd.Parameters.AddWithValue("@Key", key);
                cmd.ExecuteNonQuery();
                cnx.Close();
                ChargeBooking();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                Rcb.Text = row.Cells["Room"].Value.ToString();
                Ccb.Text = row.Cells["Customer"].Value.ToString();
                Duration.Text = row.Cells["Duration"].Value.ToString();
                Amount.Text = row.Cells["Const"].Value.ToString();

                if (Rcb.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(row.Cells["RNum"].Value.ToString());
                }
            }
        }
    }
}
