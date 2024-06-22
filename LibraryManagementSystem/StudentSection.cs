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
    public partial class StudentSection : Form
    {
        public StudentSection()
        {
            InitializeComponent();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void addStd_Click(object sender, EventArgs e)
        {
            AddStudent add = new AddStudent();
            add.Show();
            this.Hide();
        }

        private void viewSearchStudent_Click(object sender, EventArgs e)
        {
            viewAndSearchStd viewSearch = new viewAndSearchStd();
            viewSearch.Show();
            this.Hide();
        }

        private void updateDeleteStdBtn_Click(object sender, EventArgs e)
        {
            UpdateAndDeleteStd ud = new UpdateAndDeleteStd();
            ud.Show();
            this.Hide();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void exitPic_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            MainForm main = new MainForm();
            main.Show();
            this.Hide();
        }
    }
}
