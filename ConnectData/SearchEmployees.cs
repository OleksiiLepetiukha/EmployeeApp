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
using BindingDataControls;
using ValidateControls;
using System.Xml.Linq;

namespace ConnectData
{
    public partial class SearchEmployees : Form
    {
        string connectionString = SQLQueries.CONNSTRING;
        
        public SearchEmployees()
        {
            InitializeComponent();
        }

        private void SearchEmployees_Load(object sender, EventArgs e)
        {
            //loadData(SQLQueries.SELECTALL);
            DataBinding dataBindingObj = new DataBinding();
            dataBindingObj.LoadData(connectionString, SQLQueries.SELECTALL, dgvSearch);
        }
   
      //  private void loadData(String sqlQuery)
       // {

         //   MySqlConnection connection = new MySqlConnection(connectionString);

           // try
            //{
              //  connection.Open();
                //String employeesQuery = sqlQuery;      
                //DataTable dt = new DataTable();
                //MySqlDataAdapter adapter = new MySqlDataAdapter(employeesQuery, connection);
                //adapter.Fill(dt);
                //dgvSearch.DataSource = dt;
                //lblFeedback.Text = dt.Rows.Count.ToString() + " row(s)";
                //connection.Close();
           // }

           // catch (Exception ex)
           // {
           //     MessageBox.Show("Connection unsuccessful" + ex.Message);
           // }
       // }

        private void buildQuery()
        {
            String searchCriteria = "";
            String fieldName = (string)ddlField.SelectedItem;
            String operatorQuery = (string)ddlOperator.SelectedItem;
            String criteria = txtSearch.Text;
            searchCriteria = fieldName + " " + operatorQuery + " " + "'" + criteria + "'";
            String mainQuery = "Select * from employees where " + searchCriteria;
            DataBinding dataBindingObj = new DataBinding();
            dataBindingObj.LoadData(connectionString, mainQuery, dgvSearch);
            //loadData(mainQuery);
            //lblFeedback.Text = mainQuery;
        }   

        private void dgvSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if ( Validation.ValidateControl(ddlField, "Field") && 
                 Validation.ValidateControl(ddlOperator, "Operator") && 
                 Validation.ValidateControl(txtSearch, "Criteria"))
            {
            buildQuery();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           // Employee_info employeesForm = new Employee_info();
            this.Close();
           // employeesForm.Show();
        }
    }
}
