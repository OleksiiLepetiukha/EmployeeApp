using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQLConstants;

namespace ConnectData
{
    public partial class Employee_info : Form
    {
        
        String connectionString = SQLQueries.CONNSTRING;

        public Employee_info()
        {
            InitializeComponent();
        }

        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtEmployeeId.Enabled = false;
            loadData();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            
               
                MySqlConnection con = new MySqlConnection(connectionString);

                try
                {
                    con.Open();
                                   
                    string employeeName = employeeNameField.Text;

                    // Check employee name and gender are filled
                    if (string.IsNullOrEmpty(employeeName) || genderSelect.SelectedItem == null)
                    {
                        lblError.Text = "Please provide both Employee Name and Gender.";
                        return;
                    }

                    string gender = genderSelect.SelectedItem.ToString();
                   
                    // Insert the new employee into the database

                    //string insertQuery = SQLQueries.INSERT;

                    MySqlCommand cmd = new MySqlCommand("insertEmployees", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("emName", employeeName);
                    cmd.Parameters.AddWithValue("emGender", gender);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        lblError.Text = "New employee added successfully!";
                        clearForm();
                        loadData();
                    }
                    else
                    {
                        lblError.Text = "Error adding the new employee.";
                    }

                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cannot open connection!" + ex.Message);
                }
            
        }

        private void loadData()
        {
            
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                //String sqlQuery = SQLQueries.SELECTALL;

                MySqlCommand cmd = new MySqlCommand("selectAllEmployees", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                grdManagementData.ReadOnly = true;
                grdManagementData.DataSource = dataSet.Tables[0];
                lblRowsCount.Text = "Total Records: " + dataSet.Tables[0].Rows.Count.ToString();
                connection.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Connection unsuccessful" + ex.Message);
            }
        }


        private void clearForm()
        {
            employeeNameField.Text = string.Empty;
            txtEmployeeId.Text = string.Empty;
            genderSelect.SelectedItem = null;
            employeeNameField.Enabled = true;
            genderSelect.Enabled = true;
            btnConnect.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            MySqlConnection connection  = new MySqlConnection(connectionString);

          

            try
            {
                connection.Open();
                //string deleteQuery = SQLQueries.DELETE;
                MySqlCommand command = new MySqlCommand("deleteEmployees", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@employee_id_param", txtEmployeeId.Text);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    lblError.Text = "Row deleted successfully";
                    loadData();
                    clearForm();
                } else
                {
                    lblError.Text = "Row has not been deleted";
                }
                connection.Close();
            }
            catch(Exception ex) 
            {
                lblError.Text = ex.Message;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            employeeNameField.Enabled = true;
            genderSelect.Enabled = true;
        }

        private void grdManagementData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = grdManagementData.Rows[e.RowIndex];
                txtEmployeeId.Text = row.Cells["Employee_ID"].Value.ToString();
                employeeNameField.Text = row.Cells["Employee_Surname"].Value.ToString();
                string gender = row.Cells["Gender"].Value.ToString();
                genderSelect.SelectedItem = gender;
                employeeNameField.Enabled = false;
                genderSelect.Enabled = false;
                btnConnect.Enabled = false;
               
            }
        }




        private void txtEmployeeId_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                //string updateEmployeesQuery = SQLQueries.UPDATE;

                MySqlCommand command = new MySqlCommand("updateEmployees", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                command.Parameters.AddWithValue("@emId", txtEmployeeId.Text);
                command.Parameters.AddWithValue("@emName", employeeNameField.Text);
                command.Parameters.AddWithValue("@emGender", genderSelect.SelectedItem);
                if (!string.IsNullOrEmpty(employeeNameField.Text)) {
                command.ExecuteNonQuery();
                connection.Close();
                loadData();
                clearForm();
                lblError.Text = "Updated successfully";
                } else
                {
                    lblError.Text = "Employee name is empty. Pleas type employee name";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.Hide();

            SearchEmployees searchForm = new SearchEmployees();
            searchForm.ShowDialog();

            this.Show();
        }

        private void grdManagementData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

