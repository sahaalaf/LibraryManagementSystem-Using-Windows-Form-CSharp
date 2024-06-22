using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class UpdateAndDeleteStd : Form
    {
        private SqlConnection connection;

        public UpdateAndDeleteStd()
        {
            InitializeComponent();
            connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Madina Computers\\OneDrive\\Documents\\library.mdf\";Integrated Security=True;Connect Timeout=30");
        }

        private void UpdateAndDeleteStd_Load(object sender, EventArgs e)
        {
            PopulateStudentIDs();
        }

        private void PopulateStudentIDs()
        {
            List<int> studentIDs = new List<int>();
            try
            {
                connection.Open();
                string query = "SELECT StudentID FROM Students";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    studentIDs.Add(Convert.ToInt32(reader["StudentID"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while populating student IDs: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            // Clear any existing bindings
            studentIDComboBox.DataSource = null;

            // Bind the list of student IDs to the ComboBox
            studentIDComboBox.DataSource = studentIDs;
        }

        private void LoadStudentInfos_Click(object sender, EventArgs e)
        {
            int selectedStudentID;
            if (!int.TryParse(studentIDComboBox.SelectedValue?.ToString(), out selectedStudentID))
            {
                MessageBox.Show("Please select a valid Student ID.");
                return;
            }

            try
            {
                connection.Open();
                string query = "SELECT StudentName, EnrollNo, DepartmentName, Semester, ContactNo, EmailAddress FROM Students WHERE StudentID = @StudentID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentID", selectedStudentID);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    StudentName.Text = reader["StudentName"].ToString();
                    EntrollNo.Text = reader["EnrollNo"].ToString();
                    DepName.Text = reader["DepartmentName"].ToString();
                    StdSemester.Text = reader["Semester"].ToString();
                    StdContact.Text = reader["ContactNo"].ToString();
                    StdEmail.Text = reader["EmailAddress"].ToString();
                }
                else
                {
                    MessageBox.Show("Student with the provided ID not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading student information: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            int selectedStudentID;
            if (!int.TryParse(studentIDComboBox.SelectedValue?.ToString(), out selectedStudentID))
            {
                MessageBox.Show("Please select a valid Student ID.");
                return;
            }

            try
            {
                connection.Open();
                string query = "DELETE FROM Students WHERE StudentID = @StudentID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentID", selectedStudentID);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Student deleted successfully.");
                    PopulateStudentIDs();
                }
                else
                {
                    MessageBox.Show("Student with the provided ID not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the student: " + ex.Message);
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
            int selectedStudentID;
            if (!int.TryParse(studentIDComboBox.SelectedValue?.ToString(), out selectedStudentID))
            {
                MessageBox.Show("Please select a valid Student ID.");
                return;
            }

            string newName = StudentName.Text;
            string newEnrollNo = EntrollNo.Text;
            string newDepartment = DepName.Text;
            string newSemester = StudentName.Text;
            string newContactNo = StdContact.Text;
            string newEmailAddress = StdEmail.Text;

            if (string.IsNullOrWhiteSpace(newName) ||
                string.IsNullOrWhiteSpace(newEnrollNo) ||
                string.IsNullOrWhiteSpace(newDepartment) ||
                string.IsNullOrWhiteSpace(newSemester) ||
                string.IsNullOrWhiteSpace(newContactNo) ||
                string.IsNullOrWhiteSpace(newEmailAddress))
            {
                MessageBox.Show("Please enter valid student details.");
                return;
            }

            try
            {
                connection.Open();
                string query = "UPDATE Students SET StudentName = @Name, EnrollNo = @EnrollNo, DepartmentName = @Department, Semester = @Semester, ContactNo = @ContactNo, EmailAddress = @EmailAddress WHERE StudentID = @StudentID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentID", selectedStudentID);
                command.Parameters.AddWithValue("@Name", newName);
                command.Parameters.AddWithValue("@EnrollNo", newEnrollNo);
                command.Parameters.AddWithValue("@Department", newDepartment);
                command.Parameters.AddWithValue("@Semester", newSemester);
                command.Parameters.AddWithValue("@ContactNo", newContactNo);
                command.Parameters.AddWithValue("@EmailAddress", newEmailAddress);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Student updated successfully.");
                    PopulateStudentIDs();
                }
                else
                {
                    MessageBox.Show("Student with the provided ID not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating the student: " + ex.Message);
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
            StudentSection ss = new StudentSection();
            ss.Show();
            this.Hide();
        }
    }
}
