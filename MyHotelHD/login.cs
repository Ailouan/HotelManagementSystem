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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        SqlConnection cnx = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Acer\Documents\HotelDbase.mdf;Integrated Security=True;Connect Timeout=30");

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtname.Text == "admin" && txtpass.Text == "admin")
            {
                Rooms r = new Rooms();
                r.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Missing Information !!");
            }
            //cnx.Open();
            //SqlCommand cmd = new SqlCommand();
            //cmd.CommandText = "select UName, UPassword from UserTbl";
            //cmd.Connection = cnx;
            //SqlDataReader dr;
            //dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //    if (dr[1].ToString().Equals(txtname.Text) && dr[4].ToString().Equals(txtpass.Text))
            //    {
            //        Rooms r = new Rooms();
            //        r.Show();
            //        this.Hide();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Missing information");
            //    }
            //}
            //cnx.Close();
        }
    }
}
