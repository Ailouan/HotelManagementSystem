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
    public partial class Rooms : Form
    {
        SqlConnection cnx = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Acer\Documents\HotelDbase.mdf;Integrated Security=True;Connect Timeout=30");
        public Rooms()
        {
            InitializeComponent();
            ChargeRooms();
        }
        private void ChargeRooms()
        {
            cnx.Open();
            string Query = "Select * from RoomTbl";
            SqlDataAdapter dp = new SqlDataAdapter(Query, cnx);
            SqlCommandBuilder blind = new SqlCommandBuilder(dp);
            var ds = new DataSet();
            dp.Fill(ds);
            RoomsDgd.DataSource = ds.Tables[0];
            cnx.Close();
        }
        private void EditRoom()
        {
            if (key == 0)
            {
                MessageBox.Show("Pleas Selectione The Room");
            }
            else
                try
                {
                    cnx.Open();
                    SqlCommand cmd = new SqlCommand("Update RoomTbl set RStatus = @RS where RNum = @key", cnx);
                    //cmd.Parameters.AddWithValue("@RN", RnameTb.Text);
                    //cmd.Parameters.AddWithValue("@RT", RtypeCb.SelectedIndex.ToString());
                    cmd.Parameters.AddWithValue("@RS", StatusCb.Text);
                    cmd.Parameters.AddWithValue("@Key", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Updated");
                    cnx.Close();
                    ChargeRooms();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }
        private void InsertRoom()
        {
            if (RnameTb.Text=="" || StatusCb.SelectedIndex == -1 || RtypeCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information !!");
            }
            else
            try
            {
                    cnx.Open();
                    SqlCommand cmd = new SqlCommand("insert into RoomTbl (RName,Rtype,Rstatus) values(@RN,@RT,@RS)", cnx);
                    cmd.Parameters.AddWithValue("@RN", RnameTb.Text);
                    cmd.Parameters.AddWithValue("@RT", RtypeCb.SelectedIndex.ToString());
                    cmd.Parameters.AddWithValue("@RS", "Available");
                    cmd.ExecuteNonQuery();
                    cnx.Close();
                    ChargeRooms();
                    MessageBox.Show("Room Inserted !!");
                }
                catch(Exception ex)
            {
                    MessageBox.Show(ex.Message);
            }
            
        }
        private void DeleteRoom()
        {
            if (key == 0)
            {
                MessageBox.Show("Pleas Selectione The Room");
            }
            else
                try
                {
                    cnx.Open();
                    SqlCommand cmd = new SqlCommand("Delete RoomTbl where RNum = @key", cnx);
                    cmd.Parameters.AddWithValue("@Key", key);
                    cmd.ExecuteNonQuery();
                    cnx.Close();
                    ChargeRooms();
                    MessageBox.Show("Room Delete");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            InsertRoom();
            RnameTb.Clear();
            StatusCb.Text= "Rooms Types";
            RtypeCb.Text = "Status";
            ChargeRooms();
        }
        int key = 0;
        private void RoomsDgd_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = this.RoomsDgd.Rows[e.RowIndex];

                RnameTb.Text = row.Cells["RName"].Value.ToString();
                RtypeCb.Text = row.Cells["Rtype"].Value.ToString();
                StatusCb.Text = row.Cells["RStatus"].Value.ToString();

                if (RnameTb.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(row.Cells["RNum"].Value.ToString());
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            EditRoom();
            ChargeRooms();
        }

        private void Rooms_Load(object sender, EventArgs e)
        {

        }

        private void DeletBtn_Click(object sender, EventArgs e)
        {
            DeleteRoom();
            RnameTb.Clear();
            RtypeCb.Text = "Rooms Types";
            StatusCb.Text = "Status";
            ChargeRooms();
        }
    }
}
