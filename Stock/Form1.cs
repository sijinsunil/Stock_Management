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
using System.IO;

namespace Stock
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\SIJIN S\Source\Repos\Stock\Stock\Database.mdf;Integrated Security=True");
        //SqlCommand cmd;
        //SqlDataAdapter da;
        DataSet ds = new DataSet();
        public int Id;

        

        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            dis_data();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" )
            {

                MessageBox.Show("Enter Product Details");
            }
            else if (textBox2.Text == "")
            {

                MessageBox.Show("Select product");
            }
            else if (textBox3.Text == "")
            {

                MessageBox.Show("Select product");
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch (textBox1.Text,"[^0-9]" ))
            {

                MessageBox.Show("procode must a number");
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, "[^0-9]"))
            {

                MessageBox.Show("price must a number");
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, "[^0-9]"))
            {

                MessageBox.Show("qty must a number");
            }
            else if (textBox4.Text == "")
            {

                MessageBox.Show("Enter Product Details");
            }
            else
            {

                // con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT product from proli where procode = '" + textBox1.Text + "'  ", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                //con.Close();
                int i = dt.Rows.Count;
                if(i>0)
                {
                    MessageBox.Show("procode" + textBox1.Text + "is Alredy Exists");
                    dt.Clear();
                }
                else
                {
                    //con.Open();
                    SqlCommand scmd = new SqlCommand("insert into proli values(@procode,@product,@price,@qty)", con);
                    scmd.CommandType = CommandType.Text;
                    scmd.Parameters.AddWithValue("@procode", textBox1.Text);
                    scmd.Parameters.AddWithValue("@product", textBox2.Text);
                    scmd.Parameters.AddWithValue("@price", textBox3.Text);
                    scmd.Parameters.AddWithValue("@qty", textBox4.Text);
                    con.Open();
                    //cmd.CommandText = "insert into proli values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "')";
                    scmd.ExecuteNonQuery();
                    MessageBox.Show("New Data Is Addedd");
                    con.Close();
                    dis_data();
                    clear();
                    
                }

            }
        }

        public void dis_data()
        {
            
            SqlCommand scmd = new SqlCommand("select * from proli", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = scmd.ExecuteReader();
            dt.Load(sdr);

            con.Close();
            dataGridView1.DataSource = dt;
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Select product");
            }
            else if (textBox3.Text == "")
            {

                MessageBox.Show("Select product");
            }
            else if (textBox4.Text == "")
            {

                MessageBox.Show("Select product");
            }
            else if (textBox2.Text == "")
            {

                MessageBox.Show("Select product");
            }
            else
            {
                SqlCommand scmd = new SqlCommand("delete from proli where procode=@procode", con);
                scmd.CommandType = CommandType.Text;
                scmd.Parameters.AddWithValue("@procode", textBox1.Text);
                con.Open();
                scmd.ExecuteNonQuery();

                clear();
                MessageBox.Show("Your data deleted");
                con.Close();
                dis_data();
            }
                
                
        }

        void clear()
        {
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text= textBox7.Text = "";
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           // Id = Convert.ToInt32( dataGridView1.Rows[0].Cells[0].Value);
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Select product");
            }
            else if (textBox2.Text == "")
            {

                MessageBox.Show("Enter Product Details");
            }
            else if (textBox4.Text == "")
            {

                MessageBox.Show("Enter Product Details");
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[^0-9]"))
            {

                MessageBox.Show("procode must a number");
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, "[^0-9]"))
            {

                MessageBox.Show("price must a number");
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, "[^0-9]"))
            {

                MessageBox.Show("qty must a number");
            }
            else if (textBox3.Text == "")
            {

                MessageBox.Show("Enter Product Details");
            }

            else
            {
                con.Open();
                SqlCommand scmd = new SqlCommand("update proli set product=@product,price=@price,qty=@qty where procode=@procode", con);
                scmd.CommandType = CommandType.Text;
                scmd.Parameters.AddWithValue("@procode", textBox1.Text);
                scmd.Parameters.AddWithValue("@product", textBox2.Text);
                scmd.Parameters.AddWithValue("@price", textBox3.Text);
                scmd.Parameters.AddWithValue("@qty", textBox4.Text);
                //   scmd.Parameters.AddWithValue("@Id", this.Id);

                scmd.ExecuteNonQuery();

                clear();
                MessageBox.Show("Your details updated");
                con.Close();
                dis_data();

            }
                
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlCommand scmd = new SqlCommand("SELECT * from proli where product like '%" + textBox5.Text + "%'  ", con);
            
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = scmd.ExecuteReader();
            dt.Load(sdr);

            con.Close();
            dataGridView1.DataSource = dt;

            clear();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Select product");

            }
            else if (textBox1.Text == "")
            {

                MessageBox.Show("Enter Product Details");
            }
            else if (textBox3.Text == "")
            {

                MessageBox.Show("Enter Product Details");
            }
            else if (textBox4.Text == "")
            {

                MessageBox.Show("Enter Product Details");
            }
            else if (textBox6.Text == "")
            {

                MessageBox.Show("Enter no.of item");
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(textBox6.Text, "[^0-9]"))
            {

                MessageBox.Show("stock qty must a number");
            }
            else
            {
                con.Open();
                int oldq = Convert.ToInt16(textBox4.Text);
                int newq = Convert.ToInt16(textBox6.Text);
                int newqty = (oldq + newq);
                SqlCommand scmd = new SqlCommand("update proli set qty='" + newqty + "' where product=@product", con);
                scmd.CommandType = CommandType.Text;
                scmd.Parameters.AddWithValue("@procode", textBox1.Text);
                scmd.Parameters.AddWithValue("@product", textBox2.Text);
                scmd.Parameters.AddWithValue("@price", textBox3.Text);
                scmd.Parameters.AddWithValue("@qty", textBox4.Text);
                //scmd.Parameters.AddWithValue("@Id", this.Id);
                scmd.Parameters.AddWithValue("@addqty", textBox6.Text);

                scmd.ExecuteNonQuery();

                clear();
                MessageBox.Show("Stock updated");
                con.Close();
                dis_data();
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {

                MessageBox.Show("Select product");
            }
            else if (textBox2.Text == "")
            {

                MessageBox.Show("Select product");
            }
            else if (textBox3.Text == "")
            {

                MessageBox.Show("Select product");
            }
            else if (textBox4.Text == "")
            {

                MessageBox.Show("Select product");
            }
            else if (textBox7.Text == "")
            {

                MessageBox.Show("Enter no.of item");
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(textBox7.Text, "[^0-9]"))
            {

                MessageBox.Show("stock qty must a number");
            }
            else
            {
                con.Open();
                int oldq = Convert.ToInt16(textBox4.Text);
                int newq = Convert.ToInt16(textBox7.Text);
                int newqty = (oldq - newq);
                SqlCommand scmd = new SqlCommand("update proli set qty='" + newqty + "' where product=@product", con);
                scmd.CommandType = CommandType.Text;
                scmd.Parameters.AddWithValue("@procode", textBox1.Text);
                scmd.Parameters.AddWithValue("@product", textBox2.Text);
                scmd.Parameters.AddWithValue("@price", textBox3.Text);
                scmd.Parameters.AddWithValue("@qty", textBox4.Text);
                //    scmd.Parameters.AddWithValue("@Id", this.Id);
                scmd.Parameters.AddWithValue("@addqty", textBox7.Text);

                scmd.ExecuteNonQuery();

                clear();
                MessageBox.Show("Stock updated");
                con.Close();
                dis_data();

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dis_data();
        }
    }
}
