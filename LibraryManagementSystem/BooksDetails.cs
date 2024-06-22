using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace LibraryManagementSystem
{
    public partial class BooksDetails : Form
    {
        private SqlConnection connection;

        public BooksDetails()
        {
            InitializeComponent();
            connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Madina Computers\\OneDrive\\Documents\\library.mdf\";Integrated Security=True;Connect Timeout=30");
        }

        private void BooksDetails_Load(object sender, EventArgs e)
        {
            LoadIssuedBooks();
            LoadReturnedBooks();
        }

        private void LoadIssuedBooks()
        {
            try
            {
                connection.Open();
                string query = "SELECT studentEnroll AS [Enroll No], stduentName AS [Name], studentDep AS [Department], studentSemester AS [Semester], studentContact AS [Contact], bookName AS [Book Name], bookIssueDate AS [Issue Date] FROM IssueReturnBooks WHERE (bookReturnDate IS NULL OR bookReturnDate = '')";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                issueBooksGrid.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading issued books: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void LoadReturnedBooks()
        {
            try
            {
                connection.Open();
                string query = "SELECT studentEnroll AS [Enroll No], stduentName AS [Name], studentDep AS [Department], studentSemester AS [Semester], studentContact AS [Contact], bookName AS [Book Name], bookIssueDate AS [Issue Date], bookReturnDate AS [Return Date] FROM IssueReturnBooks WHERE bookReturnDate != ''";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                returnBooksGrid.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading returned books: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void exitPic_Click(object sender, EventArgs e)
        {
            MainForm main = new MainForm();
            main.Show();
            this.Hide();
        }
    }
}
