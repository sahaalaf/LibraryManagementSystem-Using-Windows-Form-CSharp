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
    public partial class ReturnBooks : Form
    {
        public ReturnBooks()
        {
            InitializeComponent();
        }

        private void listIssueBooks_Click(object sender, EventArgs e)
        {
            IssuedBooksList ibl = new IssuedBooksList();
            ibl.Show();
            this.Hide();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            MainForm main = new MainForm();
            main.Show();
            this.Hide();
        }

        private void returnIssueBooks_Click(object sender, EventArgs e)
        {
            ReturnIssuedBooks rib = new ReturnIssuedBooks();
            rib.Show();
            this.Hide();
        }

        private void updateDeleteStdBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
