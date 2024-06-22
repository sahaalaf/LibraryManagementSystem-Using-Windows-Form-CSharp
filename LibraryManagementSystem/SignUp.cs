using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class SignUp : Form
    {
        private SqlConnection connection;

        public SignUp()
        {
            InitializeComponent();
            connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Madina Computers\\OneDrive\\Documents\\library.mdf\";Integrated Security=True;Connect Timeout=30");
        }

        private void signUpBtn_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = pass.Text.Trim();
            string confirmPassword = confirmPass.Text.Trim();

            // Validate if password and confirm password match
            if (password != confirmPassword)
            {
                MessageBox.Show("Password and Confirm Password do not match.");
                return;
            }

            // Insert user into the database
            try
            {
                connection.Open();
                string query = "INSERT INTO loginTable (username, password) VALUES (@Username, @Password)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("User signed up successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to sign up user.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void txtUsername_MouseClick(object sender, MouseEventArgs e)
        {
            if(txtUsername.Text == "Username")
            {
                txtUsername.Clear();
            }
        }

        private void pass_MouseClick(object sender, MouseEventArgs e)
        {
            if(pass.Text == "Password")
            {
                pass.Clear();
                pass.PasswordChar = '•';
            }
        }

        private void confirmPass_MouseClick(object sender, MouseEventArgs e)
        {
            if(confirmPass.Text == "Confirm Password")
            {
                confirmPass.Clear();
                confirmPass.PasswordChar = '•';
            }
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
