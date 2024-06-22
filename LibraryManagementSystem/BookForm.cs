using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class BookForm : Form
    {
        public BookForm()
        {
            InitializeComponent();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bookBtn_Click(object sender, EventArgs e)
        {
            AddBookForm addBook = new AddBookForm();
            addBook.Show();
            this.Hide();
        }

        private void viewBookBtn_Click(object sender, EventArgs e)
        {
            ViewBooks viewBook = new ViewBooks();
            viewBook.Show();
            this.Hide();
        }

        private void updateDeleteBtn_Click(object sender, EventArgs e)
        {

            UpdateAndDelete ud = new UpdateAndDelete();
            ud.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
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
