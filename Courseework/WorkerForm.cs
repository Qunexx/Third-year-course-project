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
using System.Collections;

namespace Courseework
{
    public partial class WorkerForm : Form
    {

        private DatabaseHelper db;
        public WorkerForm()
        {
            InitializeComponent();
            db = new DatabaseHelper();
        }

        private void WorkerForm_Load(object sender, EventArgs e)
        {

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            if (db.OpenConnection()) { db.CloseConnection(); Application.Exit(); }
            else { Application.Exit(); }
        }

        private void LoadExpencesButton_Click(object sender, EventArgs e)
        {
            LoadExpences();
        }

        private void LoadExpencesTypesButton_Click(object sender, EventArgs e)
        {
            LoadExpencesTypes();
        }

        private void LoadDepsLimitsButton_Click(object sender, EventArgs e)
        {
            LoadDepsLimits();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            DataRow newRow = ((DataTable)dataGridView1.DataSource).NewRow();
            ((DataTable)dataGridView1.DataSource).Rows.Add(newRow);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                // Удаление текущей выбранной строки
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            }
        }


        private void SaveButton_Click(object sender, EventArgs e)
        {

        }

        public void LoadExpences()
        {
            WorkerForm1 workerForm1 = new WorkerForm1();
            workerForm1.Show();
        }
        public void LoadExpencesTypes()
        {
            WorkerForm2 workerForm2 = new WorkerForm2();
            workerForm2.Show();
        }
        public void LoadDepsLimits()
        {
            WorkerForm3 workerForm3 = new WorkerForm3();
            workerForm3.Show();
        }

        private void LoadDepsButton_Click(object sender, EventArgs e)
        {
            LoadDepartments();
        }

        private void LoadEmployeeButton_Click(object sender, EventArgs e)
        {
            LoadEmployees();
        }


        public void LoadEmployees()
        {
            string query = "SELECT * FROM Employees";
            SqlDataAdapter adapter = new SqlDataAdapter(query, db.GetConnection());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        public void LoadDepartments()
        {
            string query = "SELECT * FROM Departments";
            SqlDataAdapter adapter = new SqlDataAdapter(query, db.GetConnection());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void TaskButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("17.Учет внутриофисных расходов\r\nОписание предметной области\r\nВы работаете в бухгалтерии частной фирмы. Сотрудники фирмы имеют возможность осуществлять мелкие покупки для нужд фирмы, предоставляя в бухгалтерию товарный чек.\r\n Вашей задачей является отслеживание внутриофисных расходов. Ваша фирма состоит из отделов. Каждый отдел имеет название. В каждом отделе работает определенное количество сотрудников. Сотрудники могут осуществлять покупки в соответствии с видами расходов.\r\n Каждый вид расходов имеет название, некоторое описание и предельную сумму средств, которые могут быть потрачены по данному виду расходов в месяц. \r\nПри каждой покупке сотрудник оформляет документ, где указывает вид расхода, дату, сумму и отдел.\r\nСписок обязательных полей:\r\nОтделы (Код отдела, Название, Количество сотрудников).\r\nВиды расходов (Код вида, Название, Описание, Предельная норма).\r\nРасходы (Код расхода, Код вида, Код отдела, Сумма, Дата).\r\nРазвитие постановки задачи\r\nТеперь ситуация изменилась. Оказалось, что нужно хранить данные о расходах не только в целом по отделу, но и по отдельным сотрудникам.\r\n Нормативы по расходованию средств устанавливаются не в целом, а по каждому отделу за каждый месяц. Неиспользованные в текущем месяце деньги могут быть использованы позже. Внести в структуру таблиц изменения, учитывающие этот факт.\r\n");
        }
    }
}
