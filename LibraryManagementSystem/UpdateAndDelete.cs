using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class UpdateAndDelete : Form
    {
        private SqlConnection connection;

        public UpdateAndDelete()
        {
            InitializeComponent();
            connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Madina Computers\\OneDrive\\Documents\\library.mdf\";Integrated Security=True;Connect Timeout=30");
        }

        private void UpdateAndDelete_Load(object sender, EventArgs e)
        {
            PopulateBookIDs();
        }

        private void PopulateBookIDs()
        {
            List<int> bookIDs = new List<int>();
            try
            {
                connection.Open();
                string query = "SELECT book_id FROM AddBooks";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    bookIDs.Add(Convert.ToInt32(reader["book_id"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while populating book IDs: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            // Clear any existing bindings
            bookIDComboBox.DataSource = null;

            // Bind the list of book IDs to the ComboBox
            bookIDComboBox.DataSource = bookIDs;
        }

        private void LoadBookInfos_Click(object sender, EventArgs e)
        {
            int selectedBookID;
            if (!int.TryParse(bookIDComboBox.SelectedValue?.ToString(), out selectedBookID))
            {
                MessageBox.Show("Please select a valid Book ID.");
                return;
            }

            try
            {
                connection.Open();
                string query = "SELECT book_name, book_author, book_pub, book_pub_date, book_quantity, book_price FROM AddBooks WHERE book_id = @BookID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookID", selectedBookID);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    bookName.Text = reader["book_name"].ToString();
                    author_name.Text = reader["book_author"].ToString();
                    book_publication.Text = reader["book_pub"].ToString();
                    bookQuantity.Text = reader["book_quantity"].ToString();
                    pubDate.Text = reader["book_pub_date"].ToString();
                    bookprice.Text = reader["book_price"].ToString();
                }
                else
                {
                    MessageBox.Show("Book with the provided ID not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading book information: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            int selectedBookID;
            if (!int.TryParse(bookIDComboBox.SelectedValue?.ToString(), out selectedBookID))
            {
                MessageBox.Show("Please select a valid Book ID.");
                return;
            }

            try
            {
                connection.Open();
                string query = "DELETE FROM AddBooks WHERE book_id = @BookID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookID", selectedBookID);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Book deleted successfully.");
                    PopulateBookIDs();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Book with the provided ID not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the book: " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }



        private void updateBtn_Click(object sender, EventArgs e)
        {
            int selectedBookID;
            if (!int.TryParse(bookIDComboBox.SelectedValue?.ToString(), out selectedBookID))
            {
                MessageBox.Show("Please select a valid Book ID.");
                return;
            }

            string newBookName = bookName.Text;
            string newAuthorName = author_name.Text;
            string newPublication = book_publication.Text;
            string newPubDate = pubDate.Text;
            int newQuantity;
            decimal newPrice;

            if (string.IsNullOrWhiteSpace(newBookName) ||
                string.IsNullOrWhiteSpace(newAuthorName) ||
                string.IsNullOrWhiteSpace(newPublication) ||
                string.IsNullOrWhiteSpace(newPubDate) ||
                !int.TryParse(bookQuantity.Text, out newQuantity) ||
                !decimal.TryParse(bookprice.Text, out newPrice))
            {
                MessageBox.Show("Please enter valid book details.");
                return;
            }

            try
            {
                connection.Open();
                string query = "UPDATE AddBooks SET book_name = @BookName, book_author = @BookAuthor, book_pub = @BookPublication, book_pub_date = @BookPubDate, book_quantity = @BookQuantity, book_price = @BookPrice WHERE book_id = @BookID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookID", selectedBookID);
                command.Parameters.AddWithValue("@BookName", newBookName);
                command.Parameters.AddWithValue("@BookAuthor", newAuthorName);
                command.Parameters.AddWithValue("@BookPublication", newPublication);
                command.Parameters.AddWithValue("@BookPubDate", newPubDate);
                command.Parameters.AddWithValue("@BookQuantity", newQuantity);
                command.Parameters.AddWithValue("@BookPrice", newPrice);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Book updated successfully.");
                    PopulateBookIDs();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Book with the provided ID not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating the book: " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }



        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
        private void ClearFields()
        {
            bookName.Text = "";
            author_name.Text = "";
            book_publication.Text = "";
            bookQuantity.Text = "";
            pubDate.Text = "";
            bookprice.Text = "";
        }
    }
}
