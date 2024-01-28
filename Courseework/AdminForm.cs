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
using MySql.Data.MySqlClient;

namespace Courseework
{
    public partial class AdminForm : Form
    {

        private DatabaseHelper db;
        public AdminForm()
        {
            InitializeComponent();
            db = new DatabaseHelper();
        }

     

        private void ExitButton_Click(object sender, EventArgs e)
        {
            if (db.OpenConnection()) { db.CloseConnection(); Application.Exit(); }
            else { Application.Exit(); } 
        }

        private void LoadDepsButton_Click(object sender, EventArgs e)
        {
            LoadDepartments();
        }

        public void LoadDepartments()
        {
            string query = "SELECT * FROM Departments";
            SqlDataAdapter adapter = new SqlDataAdapter(query, db.GetConnection());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        public void LoadUsersOfSystem()
        {
            string query = "SELECT UserID,Username FROM Users";
            SqlDataAdapter adapter = new SqlDataAdapter(query, db.GetConnection());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        public void LoadEmployees()
        {
            string query = "SELECT * FROM Employees";
            SqlDataAdapter adapter = new SqlDataAdapter(query, db.GetConnection());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void RegisterNewUser_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
        }

        private void LoadEmployeesButton_Click(object sender, EventArgs e)
        {
            LoadEmployees();
            
        }

        private void LoadUsersButton_Click(object sender, EventArgs e)
        {
            LoadUsersOfSystem();
        }
    }
}
