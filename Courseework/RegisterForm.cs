using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Courseework
{
    public partial class RegisterForm : Form
    {
        private DatabaseHelper db;
        public RegisterForm()
        {
            InitializeComponent();
            db = new DatabaseHelper();
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            bool check1 = string.IsNullOrEmpty(LoginTextBox.Text);
            bool check2 = string.IsNullOrEmpty(PassTextBox.Text);
            bool check3 = string.IsNullOrEmpty(roleComboBox.Text);


            if (!check1 && !check2 && !check3)
            { 
             
            string username = LoginTextBox.Text;
            string password = PassTextBox.Text;
            string selectedRole = roleComboBox.SelectedItem.ToString(); 
            bool registrationResult = db.RegisterUser(username, password);
            if (registrationResult)
            {
                bool roleAssignmentResult = db.AssignRoleToUser(username, selectedRole);
                if (roleAssignmentResult)
                {
                    MessageBox.Show("Пользователь зарегестрирован и роль назначена успешно");
                }
       
            }
            else
            {
                MessageBox.Show("Что-то пошло не так, данный логин уже зарегестрирован в системе");
            }
            }
            else
            {
                MessageBox.Show("Перепроверьте все поля. Должен быть несуществующий логин, пароль и выбрана одна из ролей");
            }
           
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close(); 
            
        }

       
    }
}
