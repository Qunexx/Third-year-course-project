using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Courseework
{
    public partial class LoginForm : Form
    {
        private DatabaseHelper db;
        public LoginForm()
        {
            InitializeComponent();
            db = new DatabaseHelper();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void loginButton_Click(object sender, EventArgs e)
        {

            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text; 

            if (db.OpenConnection())
            {
                if (db.ValidateUser(username, password))
                {
                    string userRole = db.GetUserRole(username);
                    this.Hide();

                    if (userRole == "ADMIN")
                    {
                        AdminForm adminForm = new AdminForm(); 
                        adminForm.Show();
                    }
                    else
                    {
                        WorkerForm workerForm = new WorkerForm(); 
                        workerForm.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                    

                }

                db.CloseConnection();
                
                
            }
            else
            {
                MessageBox.Show("Error connecting to database.");
            }
        }

        

        private void ExitButton_Click(object sender, EventArgs e)
        {
            if (db.OpenConnection()) { db.CloseConnection(); Application.Exit(); }
            else { Application.Exit(); }
        }
    }
}
