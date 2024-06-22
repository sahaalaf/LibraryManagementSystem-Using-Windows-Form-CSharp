using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class IssuedBooksList : Form
    {
        private SqlConnection connection;

        public IssuedBooksList()
        {
            InitializeComponent();
            connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Madina Computers\\OneDrive\\Documents\\library.mdf\";Integrated Security=True;Connect Timeout=30");
        }

        private void PopulateDataGridView()
        {
            try
            {
                connection.Open();
                string query = "SELECT studentEnroll AS [Enroll NO], stduentName AS [Student],studentDep AS [Department], studentSemester AS [Semester], bookName AS [Book Name], bookIssueDate AS [Issue Date] FROM IssueReturnBooks WHERE (bookReturnDate IS NULL OR bookReturnDate='')";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable(); 
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void IssuedBooksList_Load(object sender, EventArgs e)
        {
            PopulateDataGridView();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            ReturnBooks rb = new ReturnBooks();
            rb.Show();
            this.Hide();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            string searchTerm = searchBook.Text.Trim();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT studentEnroll AS [Enroll NO], stduentName AS [Student], studentSemester AS [Semester], bookName AS [Book Name], bookIssueDate AS [Issue Date] FROM IssueReturnBooks WHERE studentEnroll = @searchTerm OR stduentName LIKE @searchTerm";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter a search term.");
            }
        }
    }
}
