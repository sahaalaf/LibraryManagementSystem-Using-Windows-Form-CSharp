using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class viewAndSearchStd : Form
    {
        private SqlConnection connection;

        public viewAndSearchStd()
        {
            InitializeComponent();
            connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Madina Computers\\OneDrive\\Documents\\library.mdf\";Integrated Security=True;Connect Timeout=30");
        }

        private void viewAndSearchStd_Load(object sender, EventArgs e)
        {
            PopulateDataGridView();
        }

        private void PopulateDataGridView()
        {
            try
            {
                connection.Open();
                string query = "SELECT StudentID AS [ID], StudentName AS [Name], EnrollNo AS [Enroll No], DepartmentName AS [Department], Semester AS [Semester No], ContactNo AS [Contact] , EmailAddress AS [Email] FROM Students";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading student data: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            string searchValue = searchStudent.Text.Trim();

            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Please enter a search term.");
                return;
            }

            try
            {
                connection.Open();
                string query = "SELECT StudentID, StudentName, EnrollNo, DepartmentName, Semester, ContactNo, EmailAddress FROM Students " +
                               "WHERE StudentName LIKE @SearchValue OR EnrollNo LIKE @SearchValue OR DepartmentName LIKE @SearchValue";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SearchValue", "%" + searchValue + "%");

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while searching for students: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void backToMainMenu_Click(object sender, EventArgs e)
        {
            StudentSection books = new StudentSection();
            books.Show();
            this.Hide();
        }

        private void exitPic_Click(object sender, EventArgs e)
        {
            StudentSection ss = new StudentSection();
            ss.Show();
            this.Hide();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
