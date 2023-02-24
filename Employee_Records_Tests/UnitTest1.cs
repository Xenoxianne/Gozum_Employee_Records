using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Data.SqlClient;

namespace Employee_Records_Tests
{
	[TestClass]
	public class UnitTest1
	{
		private SqlConnection connection;

		[TestInitialize]
		public void TestSetup()
		{
			// Create a test database and populate it with sample data
			string connectionString = "Data Source=Xenoxia\\SQLEXPRESS01;Initial Catalog=Employee-Record;Integrated Security=True";
			connection = new SqlConnection(connectionString);
			connection.Open();
			SqlCommand command = new SqlCommand("CREATE TABLE TestTable (id INT PRIMARY KEY IDENTITY(1,1), emp_name VARCHAR(50), emp_age INT NOT NULL, emp_salary decimal(18,2) NOT NULL, join_date DATETIME NOT NULL, phone VARCHAR(50) NOT NULL)", connection);
			command.ExecuteNonQuery();
			command = new SqlCommand("INSERT INTO TestTable VALUES ('John Doe', 35, 60000.00, '2022-03-01 12:00:00', '987-654-3210')", connection);
			command.ExecuteNonQuery();
		}

		[TestMethod]
		public void TestAddEmployee()
		{
			SqlCommand command = new SqlCommand("exec Insert_Info 'John Doe', 25, 50000.00, '2022-02-25', '1234567890'", connection);
			int result = command.ExecuteNonQuery();
			Assert.AreEqual(1, result);
		}

		[TestMethod]
		public void TestUpdateEmployee()
		{
			SqlCommand command = new SqlCommand("exec Update_Info 3, 'Jane Smith', 30, 50000.00, '2022-02-25', '1234567890'", connection);
			int result = command.ExecuteNonQuery();

			Assert.AreEqual(1, result);
		}

		[TestMethod]
		public void TestLoadEmployee()
		{
			SqlDataAdapter adapter = new SqlDataAdapter("exec Employee_List", connection);
			DataTable table = new DataTable();
			adapter.Fill(table);
			Assert.AreEqual(3, table.Rows[0]["id"]);
		}

		[TestMethod]
		public void TestDeleteEmployee()
		{
			SqlCommand command = new SqlCommand("exec Delete_Info 3", connection);
			int result = command.ExecuteNonQuery();
			Assert.AreEqual(1, result);
		}

		[TestCleanup]
		public void TestCleanup()
		{
			SqlCommand command = new SqlCommand("DROP TABLE TestTable", connection);
			command.ExecuteNonQuery();
			connection.Close();
		}
	}
}
