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

namespace crud_application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int Id;
        SqlConnection conn=new SqlConnection("Data Source=DESKTOP-0K8IABK\\SQLEXPRESS;Initial Catalog=Employee-details;Integrated Security=True");

        private void Form1_Load(object sender, EventArgs e)
        {
            Getuserdetails();
        }
        //FORM SESSION-to view the entire details of users
        private void Getuserdetails()
        {
            // string query = "Select * From Userdetailstable";
            string query = "Exec SP_crudapplicationcsharp @type='Select'";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            userdetailsdataGridView.DataSource = dt;
        }

      

       
        //INSERT SESSION
        private void button1_Click(object sender, EventArgs e)
        {
            string query = "Insert Into Userdetailstable (Firstname,Lastname,Date_Of_Birth,Age,Gender,Phone,Email,Username,State,District,Address,Password) values (@Firstname,@Lastname,@dateofbirth,@age,@gender,@phone,@email,@username,@state,@district,@address,@password)";
            SqlCommand cmd = new SqlCommand(query, conn);
            //gender:to add the details of gender on to the database
            string gender = radioButton2.Checked ? "Male" : "Female";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Firstname",textBox7.Text);
            cmd.Parameters.AddWithValue("@Lastname",textBox5.Text);
            cmd.Parameters.AddWithValue("@dateofbirth",dateTimePicker1.Text);
            cmd.Parameters.AddWithValue("@age",textBox10.Text);
            cmd.Parameters.AddWithValue("@gender",gender);
            cmd.Parameters.AddWithValue("@phone",textBox4.Text);
            cmd.Parameters.AddWithValue("@email",textBox6.Text);
            cmd.Parameters.AddWithValue("@username",textBox9.Text);
            cmd.Parameters.AddWithValue("@state",textBox2.Text);
            cmd.Parameters.AddWithValue("@district",textBox11.Text);
            cmd.Parameters.AddWithValue("@address",textBox12.Text);
            cmd.Parameters.AddWithValue("@password",textBox13.Text);
            conn.Open();
            //used to execute a SQL command that does not return any result set, such as an INSERT, UPDATE, DELETE, or other action queries.
            cmd.ExecuteNonQuery();
            conn.Close() ;
            MessageBox.Show("New user details is successfully saved", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Getuserdetails();
            ClearTextBoxes();

        }

        

      

     
      
        //UPDATE SESSION
        private void button4_Click(object sender, EventArgs e)
        {
            if (Id > 0)
            {
                string query = "Update Userdetailstable Set Firstname=@Firstname,Lastname=@Lastname,Date_Of_Birth=@dateofbirth,Age=@Age,Gender=@gender,Phone=@phone,Email=@email,Username=@username,State=@state,District=@district,Address=@Address,Password=@password where Id=@Id";
                string gender = radioButton2.Checked ? "Male" : "Female";
                SqlCommand cmd= new SqlCommand(query,conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Firstname", textBox7.Text);
                cmd.Parameters.AddWithValue("@Lastname", textBox5.Text);
                cmd.Parameters.AddWithValue("@dateofbirth", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@age", textBox10.Text);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@phone", textBox4.Text);
                cmd.Parameters.AddWithValue("@email", textBox6.Text);
                cmd.Parameters.AddWithValue("@username", textBox9.Text);
                cmd.Parameters.AddWithValue("@state", textBox2.Text);
                cmd.Parameters.AddWithValue("@district", textBox11.Text);
                cmd.Parameters.AddWithValue("@address", textBox12.Text);
                cmd.Parameters.AddWithValue("@password", textBox13.Text);
                cmd.Parameters.AddWithValue("@id", Id);
                conn.Open();
                //used to execute a SQL command that does not return any result set, such as an INSERT, UPDATE, DELETE, or other action queries.
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Updated successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Getuserdetails();
                ClearTextBoxes();

            }

            else
            {
                MessageBox.Show("Please select from the below details", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

      
        //DATAGRID SESSION
        private void userdetailsdataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }
        //DELETE SESSION
        private void button3_Click(object sender, EventArgs e)
        {
            
                string query = "Delete From Userdetailstable  where Id=@Id";
                string gender = radioButton2.Checked ? "Male" : "Female";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", Id);
            conn.Open();
                //used to execute a SQL command that does not return any result set, such as an INSERT, UPDATE, DELETE, or other action queries.
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Getuserdetails();
            ClearTextBoxes();


        }
        //CANCEL BUTTON
        private void button2_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }

     
        //this.controls is the collection of all the controls in the form
        private void ClearTextBoxes()
        {
            foreach (Control item in this.Controls)
                //here controls means the userinterface elements /components present in the forms
            {
                if (item is TextBox textBox)
                   // to both check the type of the control(item) and have a strongly-typed reference to the control(textBox) if it is indeed a TextBox.
                {
                    textBox.Clear();
                }
                else if (item is RadioButton radioButton)
                {
                    radioButton.Checked = false; // Clear selection
                }
                /*DateTimePicker is the name of the control you are referencing.DateTimePicker dateTimePicker, dateTimePicker is the variable name that you are using to refer to an instance of the DateTimePicker control. It allows you to access properties and methods of the DateTimePicker control within the scope of the loop.*/

                else if (item is DateTimePicker dateTimePicker)
                {
                    dateTimePicker.Value = DateTime.Today; // Set to current date
                }
            }
        }

        private void userdetailsdataGridView_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //rowindex:index of the row
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = userdetailsdataGridView.Rows[e.RowIndex];

                //how to display the value of gender in the table
                string gender = row.Cells["Gender"].Value.ToString();
                if (gender == "Male")
                {
                    radioButton2.Checked = true;
                    radioButton1.Checked = false;
                }
                else
                {
                    radioButton1.Checked = true;
                    radioButton2.Checked = false;

                }
                //============
                Id = Convert.ToInt32(row.Cells[0].Value);
                textBox7.Text = row.Cells[1].Value.ToString();
                textBox5.Text = row.Cells[2].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(row.Cells[3].Value); // Convert to DateTime other wise we will get error
                textBox10.Text = row.Cells[4].Value.ToString();
                textBox4.Text = row.Cells[6].Value.ToString();
                textBox6.Text = row.Cells[7].Value.ToString();
                textBox9.Text = row.Cells[8].Value.ToString();
                textBox2.Text = row.Cells[9].Value.ToString();
                textBox11.Text = row.Cells[10].Value.ToString();
                textBox12.Text = row.Cells[11].Value.ToString();
                textBox13.Text = row.Cells[12].Value.ToString();
            }
        }
    }
}
