using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtUsername_MouseClick(object sender, MouseEventArgs e)
        {
            // Clear default text when the username textbox is clicked
            if (txtUsername.Text == "Username")
            {
                txtUsername.Clear();
            }
        }

        private void txtPass_MouseClick(object sender, MouseEventArgs e)
        {
            // Clear default text and set password character when the password textbox is clicked
            if (txtPass.Text == "Password")
            {
                txtPass.Clear();
                txtPass.PasswordChar = '•';
            }
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            try {
                SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Madina Computers\\OneDrive\\Documents\\library.mdf\";Integrated Security=True;Connect Timeout=30");
                connection.Open();
                string query = "SELECT * FROM loginTable WHERE Username = @username AND Password = @password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", txtUsername.Text);
                command.Parameters.AddWithValue("@password", txtPass.Text);
                SqlDataReader reader = command.ExecuteReader();

                // Check if login credentials are valid
                if (reader.Read())
                {

                    // Navigate to the next form
                    MainForm mainForm = new MainForm();
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password!");
                }
                reader.Close();
                connection.Close(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void signUpBtn_Click(object sender, EventArgs e)
        {
            SignUp signup = new SignUp();
            signup.Show();
            this.Hide();
        }
    }
}
