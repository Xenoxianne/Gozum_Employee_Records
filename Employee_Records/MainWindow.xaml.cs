/*
	* Name: Denise Julianne S. Gozum
	* Section: BSCS 3-1N
	* Employee Records System using SQL database and WPF
*/

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Employee_Records
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		// Show/Refresh Database
		void GetEmpList()
		{
			SqlConnection con = new SqlConnection("Data Source=Xenoxia\\SQLEXPRESS01;Initial Catalog=Employee-Record;Integrated Security=True");
			SqlCommand c = new SqlCommand("exec Employee_List", con);
			SqlDataAdapter da = new SqlDataAdapter();
			da.SelectCommand = c;
			DataTable dt = new DataTable();
			da.Fill(dt);
			DataGrid1.ItemsSource = (IEnumerable)dt.DefaultView;
		}

		// Add Employee Info
		private void add_Click(object sender, RoutedEventArgs e)
		{
			SqlConnection con = new SqlConnection("Data Source=Xenoxia\\SQLEXPRESS01;Initial Catalog=Employee-Record;Integrated Security=True");
			string emp_name = TB_Name.Text, phone = TB_Phone.Text;
			int emp_age = int.Parse(TB_Age.Text);
			double emp_salary = double.Parse(TB_Salary.Text);
			DateTime join_date = DateTime.Parse(TB_JoinDate.Text);
			string FDateTime = join_date.ToString("yyyy-MM-dd HH:mm:ss");

			SqlDataReader dr;
			SqlCommand cmd = new SqlCommand("exec Insert_Info '" + emp_name + "','" + emp_age + "','" + emp_salary + "','" + FDateTime + "','" + phone + "'", con);
			con.Open();
			dr = cmd.ExecuteReader();
			MessageBox.Show("Succesfully saved!");
			con.Close();
			GetEmpList();
		}

		// Update Employee Info
		private void upd_Click(object sender, RoutedEventArgs e)
		{
			SqlConnection con = new SqlConnection("Data Source=Xenoxia\\SQLEXPRESS01;Initial Catalog=Employee-Record;Integrated Security=True");
			string emp_name = TB_Name.Text, phone = TB_Phone.Text;
			int emp_age = int.Parse(TB_Age.Text), id = int.Parse(TB_ID.Text);
			double emp_salary = double.Parse(TB_Salary.Text);
			DateTime join_date = DateTime.Parse(TB_JoinDate.Text);
			string FDateTime = join_date.ToString("yyyy-MM-dd HH:mm:ss");

			SqlDataReader dr;
			SqlCommand cmd = new SqlCommand("exec Update_Info '" + id + "','" + emp_name + "','" + emp_age + "','" + emp_salary + "','" + FDateTime + "','" + phone + "'", con);
			con.Open();
			dr = cmd.ExecuteReader();
			MessageBox.Show("Succesfully updated!");
			con.Close();
			GetEmpList();
		}

		// Delete Employee Info
		private void delete_click(object sender, RoutedEventArgs e)
		{
			SqlConnection con = new SqlConnection("Data Source=Xenoxia\\SQLEXPRESS01;Initial Catalog=Employee-Record;Integrated Security=True");
			int id = int.Parse(TB_ID.Text);

			SqlDataReader dr;
			SqlCommand cmd = new SqlCommand("exec Delete_Info'" + id + "'", con);
			con.Open();
			dr = cmd.ExecuteReader();
			MessageBox.Show("Succesfully deleted!");
			con.Close();
			GetEmpList();
		}

		// Upon opening form
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			GetEmpList();
		}

	}
}
