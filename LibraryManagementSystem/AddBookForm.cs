using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class AddBookForm : Form
    {
        public AddBookForm()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
 
            try
            {
                // Retrieve data from the form fields
                string bName = bookName.Text;
                string bAuthor = author_name.Text;
                string bookPublication = book_publication.Text;
                string publicationDate = pubDate.Text;
                int bookPrice = int.Parse(bookprice.Text);
                int bookQuan = int.Parse(bookQuantity.Text);

                // Establish connection
                SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Madina Computers\\OneDrive\\Documents\\library.mdf\";Integrated Security=True;Connect Timeout=30");
                connection.Open();

                // Define the query with parameters
                string query = @"INSERT INTO AddBooks (book_name, book_author, book_pub, book_pub_date, book_price, book_quantity)
                         VALUES (@book_name, @book_author, @book_pub, @book_pub_date, @book_price, @book_quantity)";

                // Create a command with the query and connection
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameters
                // Add parameters
                command.Parameters.AddWithValue("@book_name", bName);
                command.Parameters.AddWithValue("@book_author", bAuthor);
                command.Parameters.AddWithValue("@book_pub", bookPublication);
                command.Parameters.AddWithValue("@book_pub_date", publicationDate);
                command.Parameters.AddWithValue("@book_price", bookPrice);
                command.Parameters.AddWithValue("@book_quantity", bookQuan);


                // Execute the command
                int rowsAffected = command.ExecuteNonQuery();

                // Check if any rows were affected
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Book added successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to add book!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void exitPic_Click(object sender, EventArgs e)
        {
            BookForm bf = new BookForm();
            bf.Show();
            this.Hide();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
