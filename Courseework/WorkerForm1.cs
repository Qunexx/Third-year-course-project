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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Courseework
{
    public partial class WorkerForm1 : Form
    {

        private DatabaseHelper db;
        public WorkerForm1()
        {
            InitializeComponent();
            db = new DatabaseHelper();

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            if (db.OpenConnection()) { this.Close(); }
            else { this.Close(); }
        }

        private void ExpencesButton_Click(object sender, EventArgs e)
        {
            LoadExpences();
        }


        public void LoadExpences()
        {
            string query = "SELECT * FROM Expenses";
            SqlDataAdapter adapter = new SqlDataAdapter(query, db.GetConnection());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {

            DataTable dataTable = dataGridView1.DataSource as DataTable;
            if (dataTable != null)
            {
                int newId = GetMaxIdFromDataGridView() + 1;
                DataRow newRow = dataTable.NewRow();

                
                newRow["ExpenseID"] = newId;
                newRow["ExpenseTypeID"] = Convert.ToInt32(textBox1.Text); 
                newRow["DepartmentID"] = Convert.ToInt32(textBox2.Text);
                newRow["EmployeeID"] = Convert.ToInt32(textBox3.Text);
                newRow["Amount"] = Convert.ToDecimal(textBox4.Text);
                newRow["ExpenseDate"] = dateTimePicker1.Value;



                int departmentId = Convert.ToInt32(textBox2.Text);
                decimal newExpenseAmount = Convert.ToDecimal(textBox4.Text);
                DateTime expenseDate = dateTimePicker1.Value;

                if (db.IsExpenseLimit(departmentId, newExpenseAmount, expenseDate))
                {
                    MessageBox.Show("Лимит расходов для данного отдела или сотрудника превышен.");
                    return;
                }
                else
                {
                    // Добавляем новую строку в DataTable
                    if (db.IsEmployeeExist(Convert.ToInt32(textBox3.Text)) && (db.IsExpenseTypeExist(Convert.ToInt32(textBox1.Text))) && (db.IsDepartmentExist(Convert.ToInt32(textBox2.Text))))
                    {
                        dataTable.Rows.Add(newRow);
                    }
                    // Обновляем базу данных
                    db.AddExpenseToDb(newRow); 
                }
            }
        }
        

        private int GetMaxIdFromDataGridView()
        {
            int maxId = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["ExpenseID"].Value != null)
                {
                    int currentId = Convert.ToInt32(row.Cells["ExpenseID"].Value);
                    if (currentId > maxId)
                    {
                        maxId = currentId;
                    }
                }
            }
            return maxId;
        }




        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                textBox1.Text = row.Cells["ExpenseTypeID"].Value != null ? row.Cells["ExpenseTypeID"].Value.ToString() : "";
                textBox2.Text = row.Cells["DepartmentID"].Value != null ? row.Cells["DepartmentID"].Value.ToString() : "";
                textBox3.Text = row.Cells["EmployeeID"].Value != null ? row.Cells["EmployeeID"].Value.ToString() : "";
                textBox4.Text = row.Cells["Amount"].Value != null ? row.Cells["Amount"].Value.ToString() : "";

                // Для DateTime, проверка на DBNull
                if (row.Cells["ExpenseDate"].Value != DBNull.Value && row.Cells["ExpenseDate"].Value != null)
                {
                    dateTimePicker1.Value = Convert.ToDateTime(row.Cells["ExpenseDate"].Value);
                }
                else
                {
                    dateTimePicker1.Value = dateTimePicker1.MinDate; 
                }
            }
        }


        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                
                row.Cells["ExpenseTypeID"].Value = textBox1.Text;
                row.Cells["DepartmentID"].Value =  textBox2.Text;
                row.Cells["EmployeeID"].Value = textBox3.Text;
                row.Cells["Amount"].Value = textBox4.Text;
                row.Cells["ExpenseDate"].Value = dateTimePicker1.Value;

                int departmentId = Convert.ToInt32(textBox2.Text);
                decimal newExpenseAmount = Convert.ToDecimal(textBox4.Text);
                DateTime expenseDate = dateTimePicker1.Value;

                if (db.IsExpenseLimit(departmentId, newExpenseAmount, expenseDate))
                {
                    MessageBox.Show("Лимит расходов для данного отдела или сотрудника превышен.");
                    return;
                }
                else
                { 
                db.UpdateExpencesDb(row);
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            db.DeleteLastRowFromExpencesDb(dataGridView1);
            db.DeleteLastRowFromDataGridView(dataGridView1);
        }





    }
}
