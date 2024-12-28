using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace sali
{
    public partial class LoginForm : Form
    {
        public static int CurrentUserId;


        public LoginForm()
        {

            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var context = new GymDatabaseEntitiess()) 
                {
                    var user = context.Members
                        .FirstOrDefault(m => m.email == username && m.password == password);

                    if (user != null)
                    {
                        LoginForm.CurrentUserId = user.member_id;
                        MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        MainForm mainForm = new MainForm(user.member_id);
                        mainForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistrationForm registrationForm = new RegistrationForm();
            registrationForm.Show();
            this.Hide();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void admin_Click(object sender, EventArgs e)
        {

        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {

            {
                AdminLoginForm adminForm = new AdminLoginForm();
                adminForm.Show();
                this.Hide();

            }
        }
    }
}
    


