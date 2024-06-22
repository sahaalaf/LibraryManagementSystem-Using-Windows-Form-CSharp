using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class IssueBook : Form
    {
        public IssueBook()
        {
            InitializeComponent();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void IssueBook_Load(object sender, EventArgs e)
        {
            LoadBooksFromDatabase();
        }

        private void LoadBooksFromDatabase()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Madina Computers\\OneDrive\\Documents\\library.mdf\";Integrated Security=True;Connect Timeout=30"))
                {
                    string query = "SELECT book_name FROM AddBooks";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        BooksCombo.Items.Add(reader.GetString(0));
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading books: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void searchStdBtn_Click(object sender, EventArgs e)
        {
            // Get the enrollment number entered by the user
            string enrollmentNumber = enrollNoTxt.Text.Trim();

            try
            {
                // Connect to the database
                using (SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Madina Computers\\OneDrive\\Documents\\library.mdf\";Integrated Security=True;Connect Timeout=30"))
                {
                    connection.Open();

                    // Create a parameterized query to select the student based on enrollment number
                    string selectStudentQuery = "SELECT * FROM Students WHERE EnrollNo = @EnrollNo";
                    SqlCommand selectStudentCmd = new SqlCommand(selectStudentQuery, connection);
                    selectStudentCmd.Parameters.Add("@EnrollNo", SqlDbType.VarChar).Value = enrollmentNumber;

                    // Execute the query and retrieve the student data
                    SqlDataReader reader = selectStudentCmd.ExecuteReader();

                    // Check if a student with the provided enrollment number exists
                    if (reader.Read())
                    {
                        // Fill the text fields with the retrieved student data
                        StudentName.Text = reader["StudentName"].ToString();
                        EntrollNo.Text = reader["EnrollNo"].ToString();
                        DepName.Text = reader["DepartmentName"].ToString();
                        StdSemester.Text = reader["Semester"].ToString();
                        StdContact.Text = reader["ContactNo"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Student not found with the provided enrollment number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // Close the reader
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching for student: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void refreshBtn_Click(object sender, EventArgs e)
        {
            enrollNoTxt.Clear();
            BooksCombo.Items.Clear();
            LoadBooksFromDatabase();
        }

        private void clearFields()
        {
            StudentName.Clear();
            EntrollNo.Clear();
            DepName.Clear();
            StdSemester.Clear();
            StdContact.Clear();
            BooksCombo.Items.Clear();
            enrollNoTxt.Clear();
            LoadBooksFromDatabase();
        }

        private void IssueBtn_Click(object sender, EventArgs e)
        {
            // Get selected student and book information
            string studentEnroll = enrollNoTxt.Text.Trim();
            string studentName = StudentName.Text.Trim();
            string studentDep = DepName.Text.Trim();
            string studentSemester = StdSemester.Text.Trim();
            long studentContact = long.Parse(StdContact.Text.Trim());
            string selectedBook = BooksCombo.SelectedItem as string;

            if (string.IsNullOrEmpty(studentEnroll) || string.IsNullOrEmpty(studentName) || string.IsNullOrEmpty(studentDep) ||
                string.IsNullOrEmpty(studentSemester) || studentContact == 0 || string.IsNullOrEmpty(selectedBook))
            {
                MessageBox.Show("Please fill in all student details and select a book to issue.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Madina Computers\\OneDrive\\Documents\\library.mdf\";Integrated Security=True;Connect Timeout=30"))
                {
                    connection.Open();

                    // Check the number of books already issued to the student
                    string countBooksQuery = "SELECT COUNT(*) FROM IssueReturnBooks WHERE (studentEnroll = @StudentEnroll AND bookReturnDate = '')";
                    SqlCommand countBooksCmd = new SqlCommand(countBooksQuery, connection);
                    countBooksCmd.Parameters.AddWithValue("@StudentEnroll", studentEnroll);
                    int issuedBooksCount = Convert.ToInt32(countBooksCmd.ExecuteScalar());

                    // If the student has already issued three books, prevent issuance of a new book
                    if (issuedBooksCount >= 3)
                    {
                        MessageBox.Show("This student has already issued three books. Cannot issue more books.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        clearFields();
                        return;
                    }

                    // Retrieve the book ID based on the selected book name
                    string selectBookQuery = "SELECT book_id FROM AddBooks WHERE book_name = @BookName";
                    SqlCommand selectBookCmd = new SqlCommand(selectBookQuery, connection);
                    selectBookCmd.Parameters.AddWithValue("@BookName", selectedBook);
                    int bookID = Convert.ToInt32(selectBookCmd.ExecuteScalar());

                    // Insert a new record into the IssueReturnBooks table
                    string insertQuery = "INSERT INTO IssueReturnBooks (studentEnroll, stduentName, studentDep, studentSemester, studentContact, studentEmail, bookName, bookIssueDate, bookReturnDate) " +
                                         "VALUES (@StudentEnroll, @StudentName, @StudentDep, @StudentSemester, @StudentContact, @StudentEmail, @BookName, @IssueDate, @ReturnDate)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, connection);
                    insertCmd.Parameters.AddWithValue("@StudentEnroll", studentEnroll);
                    insertCmd.Parameters.AddWithValue("@StudentName", studentName);
                    insertCmd.Parameters.AddWithValue("@StudentDep", studentDep);
                    insertCmd.Parameters.AddWithValue("@StudentSemester", studentSemester);
                    insertCmd.Parameters.AddWithValue("@StudentContact", studentContact);
                    // Assuming student email is not provided in your form
                    insertCmd.Parameters.AddWithValue("@StudentEmail", "");
                    insertCmd.Parameters.AddWithValue("@BookName", selectedBook);
                    insertCmd.Parameters.AddWithValue("@IssueDate", DateTime.Now);
                    // Assuming book return date is initially empty
                    insertCmd.Parameters.AddWithValue("@ReturnDate", "");
                    insertCmd.ExecuteNonQuery();

                    MessageBox.Show($"Book '{selectedBook}' has been issued to '{studentName}' successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error issuing book: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exitPic_Click(object sender, EventArgs e)
        {
            MainForm main = new MainForm();
            main.Show();
            this.Hide();
        }
    }
}
