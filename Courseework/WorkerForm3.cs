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
    public partial class WorkerForm3 : Form
    {

        private DatabaseHelper db;
        public WorkerForm3()
        {
            InitializeComponent();
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            db = new DatabaseHelper();

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            if (db.OpenConnection()) { this.Close(); }
            else { this.Close(); }
        }

        private void ExpencesButton_Click(object sender, EventArgs e)
        {
            LoadDepsLimits();
        }


        public void LoadDepsLimits()
        {
            string query = "SELECT * FROM DepartmentLimits";
            SqlDataAdapter adapter = new SqlDataAdapter(query, db.GetConnection());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                int DepartmentID = Convert.ToInt32(textBox2.Text);
                if (db.IsDepartmentExist(DepartmentID))
                {
                    row.Cells["DepartmentID"].Value = Convert.ToInt32(textBox2.Text);
                    row.Cells["MonthYear"].Value = Convert.ToDateTime(dateTimePicker1.Value);
                    row.Cells["LimitAmount"].Value = Convert.ToDecimal(textBox4.Text);
                    db.UpdateDepLimitsDb(row);

                }
                else
                {
                    MessageBox.Show("Отдел с представленным айди не существует");
                }

            }
        }




        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                textBox2.Text = row.Cells["DepartmentID"].Value != null ? row.Cells["DepartmentID"].Value.ToString() : "";
                textBox4.Text = row.Cells["LimitAmount"].Value != null ? row.Cells["LimitAmount"].Value.ToString() : "";
                if (row.Cells["MonthYear"].Value != DBNull.Value && row.Cells["MonthYear"].Value != null)
                {
                    dateTimePicker1.Value = Convert.ToDateTime(row.Cells["MonthYear"].Value);
                }
                else
                {
                    dateTimePicker1.Value = dateTimePicker1.MinDate;
                }


            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {

            DataTable dataTable = dataGridView1.DataSource as DataTable;
            if (dataTable != null)
            {
                int newId = GetMaxIdFromDataGridView() + 1;
                DataRow newRow = dataTable.NewRow();

                int DepartmentID = Convert.ToInt32(textBox2.Text);
                if(db.IsDepartmentExist(DepartmentID)) 
                { 
                newRow["LimitID"] = newId;
                newRow["DepartmentID"] = Convert.ToInt32(textBox2.Text);
                newRow["MonthYear"] = Convert.ToDateTime(dateTimePicker1.Value);
                newRow["LimitAmount"] = Convert.ToDecimal(textBox4.Text);

                dataTable.Rows.Add(newRow);
                db.AddDepLimitToDb(newRow);

                }
                else
                {
                    MessageBox.Show("Отдел с представленным айди не существует");
                    return;
                }

            }
        }



        private int GetMaxIdFromDataGridView()
        {
            int maxId = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["LimitID"].Value != null)
                {
                    int currentId = Convert.ToInt32(row.Cells["LimitID"].Value);
                    if (currentId > maxId)
                    {
                        maxId = currentId;
                    }
                }
            }
            return maxId;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            db.DeleteLastRowFromDepLimitsDb(dataGridView1);
          
            db.DeleteLastRowFromDataGridView(dataGridView1); //проверка на использование вида в других таблицах, перед удалением из datagridview
            
            
        }
    }
}
