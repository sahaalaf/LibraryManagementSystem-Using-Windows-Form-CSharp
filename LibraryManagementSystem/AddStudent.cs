using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class AddStudent : Form
    {
        // Connection string for your database
        public AddStudent()
        {
            InitializeComponent();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            StudentName.Clear();
            EntrollNo.Clear();
            DepName.Clear();
            StdSemester.Clear();
            StdContact.Clear();
            StdEmail.Clear();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            // Get the values from the textboxes
            string name = StudentName.Text;
            string enroll = EntrollNo.Text;
            string dep = DepName.Text;
            string sem = StdSemester.Text;
            string contact = StdContact.Text;
            string email = StdEmail.Text;

            // Connection string for your database
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Madina Computers\\OneDrive\\Documents\\library.mdf\";Integrated Security=True;Connect Timeout=30";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // SQL query to insert data into the database
                    string query = "INSERT INTO Students (StudentName, EnrollNo, DepartmentName, Semester, ContactNo, EmailAddress) " +
                                   "VALUES (@Name, @Enroll, @Dep, @Sem, @Contact, @Email)";

                    // Create command object
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the command
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Enroll", enroll);
                        command.Parameters.AddWithValue("@Dep", dep);
                        command.Parameters.AddWithValue("@Sem", sem);
                        command.Parameters.AddWithValue("@Contact", contact);
                        command.Parameters.AddWithValue("@Email", email);

                        // Open the connection
                        connection.Open();

                        // Execute the command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if any rows were affected
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Student added successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to add student!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
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
