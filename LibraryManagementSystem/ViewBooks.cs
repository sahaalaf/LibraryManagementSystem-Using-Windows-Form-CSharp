using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class ViewBooks : Form
    {
        private SqlConnection connection;

        public ViewBooks()
        {
            InitializeComponent();
            connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Madina Computers\\OneDrive\\Documents\\library.mdf\";Integrated Security=True;Connect Timeout=30");
        }

        private void ViewBooks_Load(object sender, EventArgs e)
        {
            PopulateDataGridView();
        }

        private void PopulateDataGridView()
        {
            try
            {
                connection.Open();
                string query = "SELECT book_id AS [BOOK ID], book_name AS [BOOK NAME], book_author AS [AUTHOR], book_pub AS [PUBLICATION], book_pub_date AS [PUBLICATION DATE], book_price AS [PRICE], book_quantity AS [QUANTITY] FROM AddBooks";
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

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            string searchValue = searchBook.Text.Trim();

            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Please enter a search term.");
                return;
            }

            try
            {
                connection.Open();
                string query = "SELECT book_id AS [BOOK ID], book_name AS [BOOK NAME], book_author AS [AUTHOR], book_pub AS [PUBLICATION], book_pub_date AS [PUBLICATION DATE], book_price AS [PRICE], book_quantity AS [QUANTITY] FROM AddBooks " +
                               "WHERE book_name LIKE @SearchValue OR book_author LIKE @SearchValue";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SearchValue", "%" + searchValue + "%");

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while searching for books: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void backToMainMenu_Click(object sender, EventArgs e)
        {
            BookForm bf = new BookForm();
            bf.Show();
            this.Hide();
        }

        private void exitPic_Click(object sender, EventArgs e)
        {
            BookForm bf = new BookForm();
            bf.Show();
            this.Hide();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
