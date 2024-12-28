using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Linq;

namespace sali
{
    public partial class AdminLoginForm : Form
    {
        public AdminLoginForm()
        {
            InitializeComponent();
        }
        private void AdminLoginForm_Load(object sender, EventArgs e)
        {
            
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
                    var admin = context.Admins
                        .FirstOrDefault(a => a.username == username && a.password == password);

                    if (admin != null)
                    {
                        MessageBox.Show("Admin login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                      
                        AdminPanelForm adminPanel = new AdminPanelForm();
                        adminPanel.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid credentials.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
    

