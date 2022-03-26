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
    public partial class Dashboard : Form
    {
        SqlConnection cnx = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Acer\Documents\HotelDbase.mdf;Integrated Security=True;Connect Timeout=30");

        public Dashboard()
        {
            InitializeComponent();
            CountRooms();
            CountCustomer();
            SumAmount();
            GetCustomer();
        }
        private void CountRooms()
        {
            cnx.Open();
            SqlDataAdapter da = new SqlDataAdapter("select count(*) from RoomTbl", cnx);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Rlbl.Text = dt.Rows[0][0].ToString() + "   Romms";
            cnx.Close();
        }
        private void CountCustomer()
        {
            cnx.Open();
            SqlDataAdapter da = new SqlDataAdapter("select count(*) from CustomerTbl", cnx);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Clbl.Text = dt.Rows[0][0].ToString() + "   Customers";
            cnx.Close();
        }
        private void SumAmount()
        {
            cnx.Open();
            SqlDataAdapter da = new SqlDataAdapter("select sum(Cost) from BookingTbl", cnx);
            DataTable dt = new DataTable();
            da.Fill(dt);
            BFlbl.Text = "Rs : " + dt.Rows[0][0].ToString() +" $";
            cnx.Close();
        }
        private void SumDaily()
        {
            cnx.Open();
            SqlDataAdapter da = new SqlDataAdapter("select sum(Cost) from BookingTbl where BookDate = '"+datm.Value+"' ", cnx);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Dlbl.Text = "Rs : " + dt.Rows[0][0].ToString() + " $";
            cnx.Close();
        }
        private void GetCustomer()
        {
            //cnx.Open();
            //SqlCommand cmd = new SqlCommand("Select * from CustomerTbl", cnx);
            //SqlDataReader dr;
            //dr = cmd.ExecuteReader();
            //DataTable dt = new DataTable();
            //dt.Columns.Add("CustNum", typeof(int));
            //dt.Load(dr);
            //Ccbx.DisplayMember = "CustName";
            //Ccbx.ValueMember = "CustNum";
            //Ccbx.DataSource = dt;
            //cnx.Close();

        }
        private void SumByCustomer()
        {
            //cnx.Open();
            //SqlDataAdapter da = new SqlDataAdapter("select sum(Cost) from BookingTbl where Customer = '" + Ccbx.SelectedItem.ToString() + "' ", cnx);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //Ilbl.Text = dt.Rows[0][0].ToString();
            //cnx.Close();
        }
        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            SumDaily();
        }

        private void datm_ValueChanged(object sender, EventArgs e)
        {
            SumDaily();
        }

        private void Ccbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            SumByCustomer();
        }
    }
}
