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
    public partial class ReturnIssuedBooks : Form
    {
        private SqlConnection connection;
        public ReturnIssuedBooks()
        {
            InitializeComponent();
            connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Madina Computers\\OneDrive\\Documents\\library.mdf\";Integrated Security=True;Connect Timeout=30");


        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {

        }

        private void searchStdBtn_Click(object sender, EventArgs e)
{
        string studentEnrollmentNumber = enrollNoTxt.Text.Trim();

        string query = "SELECT bookName, bookIssueDate FROM IssueReturnBooks WHERE studentEnroll = @enrollmentNumber AND (bookReturnDate IS NULL OR bookReturnDate = '')";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@enrollmentNumber", studentEnrollmentNumber);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    // Clear any previous data
                    bookComboBox.Items.Clear();
                    issueDateTxt.Text = "";

                    while (reader.Read())
                    {
                        string bookName = reader.GetString(0);
                        string issueDate = reader.GetString(1); // Assuming IssueDate is stored as DateTime
                        bookComboBox.Items.Add(bookName); // Add book to ComboBox
                    }
                }
                else
                {
                    MessageBox.Show("No records found for the provided enrollment number.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
}

    private void bookComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        // When a book is selected from ComboBox, update issue date
        string selectedBook = bookComboBox.SelectedItem.ToString();
        issueDateTxt.Text = GetIssueDate(selectedBook);
    }

    private string GetIssueDate(string bookName)
    {
        string issueDate = "";

        // Query to retrieve issue date of the selected book
        string query = "SELECT bookIssueDate FROM IssueReturnBooks WHERE bookName = @bookName AND (bookReturnDate IS NULL OR bookReturnDate = '')";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@bookName", bookName);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    issueDate = Convert.ToDateTime(result).ToString(); // Assuming IssueDate is stored as DateTime
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        return issueDate;
    }


        private void returnBtn_Click(object sender, EventArgs e)
        {
            string studentEnrollmentNumber = enrollNoTxt.Text.Trim();
            string bookName = bookComboBox.Text.Trim();

            
            DateTime returnDate = DateTime.Now; 

            string updateQuery = "UPDATE IssueReturnBooks SET bookReturnDate = @returnDate WHERE studentEnroll = @enrollmentNumber AND bookName = @bookName AND (bookReturnDate IS NULL OR bookReturnDate = '')";

            using (SqlCommand command = new SqlCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@returnDate", returnDate);
                command.Parameters.AddWithValue("@enrollmentNumber", studentEnrollmentNumber);
                command.Parameters.AddWithValue("@bookName", bookName);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Book returned successfully!");
                        // Clear the form after successful return
                        bookComboBox.Text = "";
                        issueDateTxt.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Failed to return the book. Please check the provided information.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            ReturnBooks rb = new ReturnBooks();
            rb.Show();
            this.Hide();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
