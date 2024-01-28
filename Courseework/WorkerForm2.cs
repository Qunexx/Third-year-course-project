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
    public partial class WorkerForm2 : Form
    {


        private DatabaseHelper db;
        public WorkerForm2()
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
            LoadExpencesTypes();


        }



        public void LoadExpencesTypes()
        {
            string query = "SELECT * FROM ExpenseTypes";
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


                newRow["ExpenseTypeID"] = newId;
                newRow["Name"] = textBox1.Text.ToString();
                newRow["Description"] = textBox2.Text.ToString();
                newRow["LimitAmount"] = Convert.ToDecimal(textBox3.Text);
         


                dataTable.Rows.Add(newRow);
                db.AddExpenseTypeToDb(newRow);

            }
        }
        


        private int GetMaxIdFromDataGridView()
        {
            int maxId = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["ExpenseTypeID"].Value != null)
                {
                    int currentId = Convert.ToInt32(row.Cells["ExpenseTypeID"].Value);
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

                textBox1.Text = row.Cells["Name"].Value != null ? row.Cells["Name"].Value.ToString() : "";
                textBox2.Text = row.Cells["Description"].Value != null ? row.Cells["Description"].Value.ToString() : "";
                textBox3.Text = row.Cells["LimitAmount"].Value != null ? row.Cells["LimitAmount"].Value.ToString() : "";
                

               
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];


                row.Cells["Name"].Value = textBox1.Text;
                row.Cells["Description"].Value = textBox2.Text;
                row.Cells["LimitAmount"].Value = Convert.ToDecimal(textBox3.Text);
               

                db.UpdateExpencesTypesDb(row);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            db.DeleteLastRowFromExpenseTypesDb(dataGridView1);
            int lastrowid = db.GetLastRowExpenseTypesId(dataGridView1);
            if (!db.IsExpenseTypeUsed(lastrowid)) { 
            db.DeleteLastRowFromDataGridView(dataGridView1); //проверка на использование вида в других таблицах, перед удалением из datagridview
            }
            else { return; }
        }
    }
}
