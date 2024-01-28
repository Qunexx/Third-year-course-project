using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

public class DatabaseHelper
{
    private SqlConnection connection;
    private string connectionString = "Server=QunexxPC\\SQLEXPRESS;Database=Office;User ID=root;Password=root;";



    public DatabaseHelper()
    {
        connection = new SqlConnection(connectionString);
    }

    public bool OpenConnection()
    {
        try
        {
            connection.Open();
            return true;
        }
        catch (SqlException ex)
        {
            
            return false;
        }
    }

    public bool CloseConnection()
    {
        try
        {
            if(connection != null) { 
            connection.Close();
            }
            return true;
        }
        catch (SqlException ex)
        {
           
            return false;
        }
    }

    public SqlConnection GetConnection()
    {
        return connection;
    }



    

    public bool ValidateUser(string username, string enteredPassword)
    {
        string passwordQuery = "SELECT Password FROM Users WHERE Username = @username";
        string saltQuery = "SELECT Salt FROM Users WHERE Username = @username";
        
        SqlCommand passwordCmd = new SqlCommand(passwordQuery, GetConnection());
        SqlCommand saltCmd = new SqlCommand(saltQuery, GetConnection());
        passwordCmd.Parameters.AddWithValue("@username", username);
        saltCmd.Parameters.AddWithValue("@username", username);

        try
        {
           

            var passwordResult = passwordCmd.ExecuteScalar();
            var saltResult = saltCmd.ExecuteScalar();

           

            if (passwordResult != null && saltResult != null)
            {
                string storedHash = passwordResult.ToString();
                string storedSalt = saltResult.ToString();

                string enteredPasswordHash = GenerateHash(enteredPassword, storedSalt);

                if (enteredPasswordHash == storedHash)
                {
                    return true; 
                }
            }
        }
        catch (SqlException ex)
        {
            
           
        }
        finally
        {
            
        }
        
        return false; 
    }

    public string GetUserRole(string username)
    {
        string query = @"SELECT Roles.RoleName 
                     FROM Users 
                     JOIN UserRoles ON Users.UserID = UserRoles.UserID 
                     JOIN Roles ON UserRoles.RoleID = Roles.RoleID 
                     WHERE Users.Username = @username"
        ;

        SqlCommand cmd = new SqlCommand(query, GetConnection());
        cmd.Parameters.AddWithValue("@username", username);

        try
        {
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                return result.ToString();
            }
        }
        catch (MySqlException ex)
        {
           
        }
        return string.Empty;
    }


    public bool RegisterUser(string username, string password)
    {
        if (username == null || password == null) { return false; }

       
        string salt = CreateSalt();
        string hashedPassword = GenerateHash(password, salt);

       
        string query = "INSERT INTO Users (Username, Password, Salt) VALUES (@username, @password, @salt)";
        SqlCommand cmd = new SqlCommand(query, GetConnection());

        
        cmd.Parameters.AddWithValue("@username", username);
        cmd.Parameters.AddWithValue("@password", hashedPassword);
        cmd.Parameters.AddWithValue("@salt", salt);

        try
        {
            OpenConnection();
            cmd.ExecuteNonQuery();
            CloseConnection();
            return true;
        }
        catch (SqlException ex)
        {
            if (ex.Number == 2627) 
            {
                CloseConnection();
                return false; // Пользователь уже существует
            }
            else
            {
                CloseConnection();
                return false; 
            }
        }
    }


   




    public static string CreateSalt()
    {
        byte[] randomBytes = new byte[32];
        using (var generator = RandomNumberGenerator.Create())
        {
            generator.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }

    public static string GenerateHash(string password, string salt)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(password + salt);
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }

    


    public bool AssignRoleToUser(string username, string roleName) 
    {
       
        string roleQuery = "SELECT RoleID FROM Roles WHERE RoleName = @roleName";
        SqlCommand roleCmd = new SqlCommand(roleQuery, GetConnection());
        roleCmd.Parameters.AddWithValue("@roleName", roleName);

        try
        {
            OpenConnection();
            int roleId = Convert.ToInt32(roleCmd.ExecuteScalar());

            
            string userQuery = "SELECT UserID FROM Users WHERE Username = @username";
            SqlCommand userCmd = new SqlCommand(userQuery, GetConnection());
            userCmd.Parameters.AddWithValue("@username", username);
            int userId = Convert.ToInt32(userCmd.ExecuteScalar());

            
            string assignQuery = "INSERT INTO UserRoles (UserID, RoleID) VALUES (@userId, @roleId)";
            SqlCommand assignCmd = new SqlCommand(assignQuery, GetConnection());
            assignCmd.Parameters.AddWithValue("@userId", userId);
            assignCmd.Parameters.AddWithValue("@roleId", roleId);

            assignCmd.ExecuteNonQuery();
            CloseConnection();
            return true;
        }
        catch (SqlException ex)
        {
            
            CloseConnection();
            return false;
        }
    }

   



    public void UpdateExpencesDb(DataGridViewRow row) 
    {
            string query = "UPDATE Expenses SET ExpenseTypeID = @ExpenseTypeID, DepartmentID = @DepartmentID, EmployeeID = @EmployeeID, Amount = @Amount, ExpenseDate = @ExpenseDate WHERE ExpenseID = @ExpenseID";

        
            SqlCommand command = new SqlCommand(query, GetConnection());

           
            int ExpenseID = Convert.ToInt32(row.Cells["ExpenseID"].Value);
            int ExpenseTypeID = Convert.ToInt32(row.Cells["ExpenseTypeID"].Value);
            int DepartmentID = Convert.ToInt32(row.Cells["DepartmentID"].Value);
            int EmployeeID = Convert.ToInt32(row.Cells["EmployeeID"].Value);

        if (!IsEmployeeExist(EmployeeID))
        {
            /*throw new InvalidOperationException("Сотрудник с указанным ID не существует.");*/
            MessageBox.Show("Сотрудник с указанным ID не существует.");
            CloseConnection();
            return;
        }
        if (!IsDepartmentExist(DepartmentID))
        {
            /*throw new InvalidOperationException("Отдел с указанным ID не существует.");*/
            MessageBox.Show("Отдел с указанным ID не существует.");
            CloseConnection();
            return;
        }
        if (!IsExpenseTypeExist(ExpenseTypeID))
        {
            /*throw new InvalidOperationException("Тип расхода с указанным ID не существует.");*/
            MessageBox.Show("Тип расхода с указанным ID не существует.");
            CloseConnection();
            return;
        }
        decimal Amount = Convert.ToDecimal(string.Format("{0:F2}", row.Cells["Amount"].Value));
            DateTime ExpenseDate = Convert.ToDateTime(row.Cells["ExpenseDate"].Value);

       
            command.Parameters.AddWithValue("@ExpenseID", ExpenseID);
            command.Parameters.AddWithValue("@ExpenseTypeID", ExpenseTypeID);
            command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
            command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
            command.Parameters.AddWithValue("@Amount", Amount);
            command.Parameters.AddWithValue("@ExpenseDate", ExpenseDate);

            OpenConnection();
            command.ExecuteNonQuery();
            CloseConnection();

    }


    public void AddExpenseToDb(DataRow row)
    {
        string query = "INSERT INTO Expenses (ExpenseID,ExpenseTypeID, DepartmentID, EmployeeID, Amount, ExpenseDate) VALUES (@ExpenseID,@ExpenseTypeID, @DepartmentID, @EmployeeID, @Amount, @ExpenseDate)";

        SqlCommand command = new SqlCommand(query, GetConnection());

       
        command.Parameters.AddWithValue("@ExpenseID", Convert.ToInt32(row["ExpenseID"]));
        command.Parameters.AddWithValue("@ExpenseTypeID", Convert.ToInt32(row["ExpenseTypeID"]));
        command.Parameters.AddWithValue("@DepartmentID", Convert.ToInt32(row["DepartmentID"]));
        command.Parameters.AddWithValue("@EmployeeID", Convert.ToInt32(row["EmployeeID"]));
        command.Parameters.AddWithValue("@Amount", Convert.ToDecimal(row["Amount"]));
        command.Parameters.AddWithValue("@ExpenseDate", Convert.ToDateTime(row["ExpenseDate"]));
        
        int EmployeeID = Convert.ToInt32(row["EmployeeID"]);
        int DepartmentID = Convert.ToInt32(row["DepartmentID"]);
        int ExpenseTypeID = Convert.ToInt32(row["ExpenseTypeID"]);
        if (!IsEmployeeExist(EmployeeID))
        {
            /*throw new InvalidOperationException("Сотрудник с указанным ID не существует.");*/
            MessageBox.Show("Сотрудник с указанным ID не существует.");
            CloseConnection();
            return;
        }
        if (!IsDepartmentExist(DepartmentID))
        {
            /*throw new InvalidOperationException("Отдел с указанным ID не существует.");*/
            MessageBox.Show("Отдел с указанным ID не существует.");
            CloseConnection();
            return;
        }
        if (!IsExpenseTypeExist(ExpenseTypeID))
        {
            /*throw new InvalidOperationException("Тип расхода с указанным ID не существует.");*/
            MessageBox.Show("Тип расхода с указанным ID не существует.");
            CloseConnection();
            return;

        }

        OpenConnection();
        command.ExecuteNonQuery();
        CloseConnection();
    }


    public bool IsExpenseLimit(int departmentId, decimal newExpenseAmount, DateTime expenseDate) // проверка, достигнут ли лимит
    {
        
       
        string query = @"
        SELECT SUM(Amount) 
        FROM Expenses 
        WHERE DepartmentID = @DepartmentID AND 
              MONTH(ExpenseDate) = @Month AND 
              YEAR(ExpenseDate) = @Year";


        SqlCommand command = new SqlCommand(query, GetConnection());
        command.Parameters.AddWithValue("@DepartmentID", departmentId);
        command.Parameters.AddWithValue("@Month", expenseDate.Month);
        command.Parameters.AddWithValue("@Year", expenseDate.Year);
        OpenConnection();
        object result = command.ExecuteScalar();
        decimal currentTotal = (result != DBNull.Value) ? Convert.ToDecimal(result) : 0;

        // Получаем лимит для отдела
        decimal departmentLimit = GetDepartmentLimit(departmentId);
        CloseConnection();
        return currentTotal + newExpenseAmount >= departmentLimit;

    }

    public decimal GetDepartmentLimit(int departmentId)
    {
        string query = "SELECT LimitAmount FROM DepartmentLimits WHERE DepartmentID = @DepartmentID";

            SqlCommand command = new SqlCommand(query, GetConnection());
            command.Parameters.AddWithValue("@DepartmentID", departmentId);
            
            object result = command.ExecuteScalar();
            
            return (result != DBNull.Value) ? Convert.ToDecimal(result) : 0;
        
    }





    public void DeleteLastRowFromExpencesDb(DataGridView dataGridView) //удаляем строку из бд
    {
        
        string query = "DELETE FROM Expenses WHERE ExpenseID = @ExpenseID";

            SqlCommand command = new SqlCommand(query, GetConnection());

            // Получаем ID последней записи
            int lastRowId = GetLastRowExpenseId(dataGridView); 

            command.Parameters.AddWithValue("@ExpenseID", lastRowId);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        
    }

    public int GetLastRowExpenseId(DataGridView dataGridView) //Получаем последнюю строку из датагридвью
    {
        if (dataGridView.Rows.Count > 0)
        {
            int lastIndex = dataGridView.Rows.Count - 1;
            return Convert.ToInt32(dataGridView.Rows[lastIndex].Cells["ExpenseID"].Value);
        }
        return -1; 
    }

    public void DeleteLastRowFromDataGridView(DataGridView dataGridView)
    {
        if (dataGridView.Rows.Count > 0)
        {
            
            int lastIndex = dataGridView.Rows.Count - 1;
            if (!dataGridView.Rows[lastIndex].IsNewRow)
            {
                dataGridView.Rows.RemoveAt(lastIndex);
            }
        }
    }


    public bool IsEmployeeExist(int employeeId)
    {
        string query = "SELECT COUNT(*) FROM Employees WHERE EmployeeID = @EmployeeID";
       
            SqlCommand command = new SqlCommand(query, GetConnection());
            command.Parameters.AddWithValue("@EmployeeID", employeeId);
            OpenConnection();
            int count = Convert.ToInt32(command.ExecuteScalar());
            CloseConnection();
            return count > 0;
        
    }

    public bool IsDepartmentExist(int departmentId)
    {
        string query = "SELECT COUNT(*) FROM Departments WHERE DepartmentID = @DepartmentID";
        
            SqlCommand command = new SqlCommand(query, GetConnection());
            command.Parameters.AddWithValue("@DepartmentID", departmentId);
            OpenConnection();
            int count = Convert.ToInt32(command.ExecuteScalar());
            CloseConnection();
            return count > 0;
        
    }

    public bool IsExpenseTypeExist(int expenseTypeId)
    {
        string query = "SELECT COUNT(*) FROM ExpenseTypes WHERE ExpenseTypeID = @ExpenseTypeID";
        
            SqlCommand command = new SqlCommand(query, GetConnection());
            command.Parameters.AddWithValue("@ExpenseTypeID", expenseTypeId);
            OpenConnection();
            int count = Convert.ToInt32(command.ExecuteScalar());
            CloseConnection();
            return count > 0;
        
    }



    public void UpdateExpencesTypesDb(DataGridViewRow row) //обновляем значения в бд
    {
        string query = "UPDATE ExpenseTypes SET Name = @Name,  Description = @Description, LimitAmount = @LimitAmount WHERE ExpenseTypeID = @ExpenseTypeID";


        SqlCommand command = new SqlCommand(query, GetConnection());

        int ExpenseTypeID = Convert.ToInt32(row.Cells["ExpenseTypeID"].Value);
        string Name = row.Cells["Name"].Value.ToString();
        string Description = row.Cells["Description"].Value.ToString();
        decimal LimitAmount = Convert.ToDecimal(string.Format("{0:F2}", row.Cells["LimitAmount"].Value));
        
        command.Parameters.AddWithValue("@ExpenseTypeID", ExpenseTypeID);
        command.Parameters.AddWithValue("@Name", Name);
        command.Parameters.AddWithValue("@Description", Description);
        command.Parameters.AddWithValue("@LimitAmount", LimitAmount);


        OpenConnection();
        command.ExecuteNonQuery();
        CloseConnection();

    }


    public void AddExpenseTypeToDb(DataRow row)
    {
        string query = "INSERT INTO ExpenseTypes (ExpenseTypeID, Name, Description, LimitAmount) VALUES (@ExpenseTypeID, @Name, @Description, @LimitAmount)";

        SqlCommand command = new SqlCommand(query, GetConnection());


        command.Parameters.AddWithValue("@ExpenseTypeID", Convert.ToInt32(row["ExpenseTypeID"]));
        command.Parameters.AddWithValue("@Name", row["Name"].ToString());
        command.Parameters.AddWithValue("@Description", row["Description"].ToString());
        command.Parameters.AddWithValue("@LimitAmount", Convert.ToDecimal(row["LimitAmount"]));
       

     

        OpenConnection();
        command.ExecuteNonQuery();
        CloseConnection();
    }




    public void DeleteLastRowFromExpenseTypesDb(DataGridView dataGridView)
    {

        int lastRowId = GetLastRowExpenseTypesId(dataGridView);
        
        if (lastRowId == -1) {
            CloseConnection();
            return;
        }  // Нет строк для удаления
        

        if (!IsExpenseTypeUsed(lastRowId))
        {
            string query = "DELETE FROM ExpenseTypes WHERE ExpenseTypeID = @ExpenseTypeID";

                
                SqlCommand command = new SqlCommand(query, GetConnection());
                command.Parameters.AddWithValue("@ExpenseTypeID", lastRowId);
                OpenConnection();
                command.ExecuteNonQuery();
                CloseConnection();
            
        }
        else
        {
            MessageBox.Show("Данная строка используется в Расходах(Expenses), её невозможно удалить, измените вид расхода в ней, прежде чем попробовать ещё раз");
            CloseConnection();
        }
    }


    public int GetLastRowExpenseTypesId(DataGridView dataGridView) //Получаем последнюю строку
    {
        if (dataGridView.Rows.Count > 0)
        {
            int lastIndex = dataGridView.Rows.Count - 1;
            return Convert.ToInt32(dataGridView.Rows[lastIndex].Cells["ExpenseTypeID"].Value);
        }
        return -1;
    }

    public bool IsExpenseTypeUsed(int expenseTypeId)
    {
        string query = "SELECT COUNT(*) FROM Expenses WHERE ExpenseTypeID = @ExpenseTypeID";
            OpenConnection();
            SqlCommand command = new SqlCommand(query, GetConnection());
            command.Parameters.AddWithValue("@ExpenseTypeID", expenseTypeId);
           
            int count = Convert.ToInt32(command.ExecuteScalar());
            CloseConnection();
            return count > 0;
        
    }


    public void UpdateDepLimitsDb(DataGridViewRow row) //обновляем значения в бд
    {
        string query = "UPDATE DepartmentLimits SET DepartmentID = @DepartmentID,  MonthYear = @MonthYear, LimitAmount = @LimitAmount WHERE LimitID = @LimitID";


        SqlCommand command = new SqlCommand(query, GetConnection());

        int LimitID = Convert.ToInt32(row.Cells["LimitID"].Value);
        int DepartmentID = Convert.ToInt32(row.Cells["DepartmentID"].Value);
        DateTime MonthYear = Convert.ToDateTime(row.Cells["MonthYear"].Value);
        decimal LimitAmount = Convert.ToDecimal(string.Format("{0:F2}", row.Cells["LimitAmount"].Value));

        command.Parameters.AddWithValue("@LimitID", LimitID);
        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
        command.Parameters.AddWithValue("@MonthYear", MonthYear);
        command.Parameters.AddWithValue("@LimitAmount", LimitAmount);
        OpenConnection();
        command.ExecuteNonQuery();
        CloseConnection();
        

    }

    public void AddDepLimitToDb(DataRow row)
    {
        string query = "INSERT INTO DepartmentLimits(LimitID,DepartmentID,MonthYear,LimitAmount) VALUES (@LimitID, @DepartmentID,@MonthYear, @LimitAmount)";

        SqlCommand command = new SqlCommand(query, GetConnection());


        int LimitID = Convert.ToInt32(row["LimitID"]);
        int DepartmentID = Convert.ToInt32(row["DepartmentID"]);
        DateTime MonthYear = Convert.ToDateTime(row["MonthYear"]);
        decimal LimitAmount = Convert.ToDecimal(string.Format("{0:F2}", row["LimitAmount"]));

        command.Parameters.AddWithValue("@LimitID", LimitID);
        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
        command.Parameters.AddWithValue("@MonthYear", MonthYear);
        command.Parameters.AddWithValue("@LimitAmount", LimitAmount);



        OpenConnection();
        command.ExecuteNonQuery();
        CloseConnection();
    }




    public void DeleteLastRowFromDepLimitsDb(DataGridView dataGridView)
    {

        int lastRowId = GetLastRowDepLimitsId(dataGridView);

        if (lastRowId == -1)
        {
            CloseConnection();
            return;
        }  


        
            string query = "DELETE FROM DepartmentLimits WHERE LimitID = @LimitID";


            SqlCommand command = new SqlCommand(query, GetConnection());
            command.Parameters.AddWithValue("@LimitID", lastRowId);
            OpenConnection();
            command.ExecuteNonQuery();
            CloseConnection();    
       
    }

    public int GetLastRowDepLimitsId(DataGridView dataGridView) //Получаем последнюю строку
    {
        if (dataGridView.Rows.Count > 0)
        {
            int lastIndex = dataGridView.Rows.Count - 1;
            return Convert.ToInt32(dataGridView.Rows[lastIndex].Cells["LimitID"].Value);
        }
        return -1;
    }


}
