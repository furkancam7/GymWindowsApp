using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Data;
using System.Linq;

namespace sali
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }
        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            try
            {
                using (var context = new GymDatabaseEntitiess())
                {
                    var membershipPlans = context.Membership_Plans
                                                  .Select(plan => new
                                                  {
                                                      plan.membership_plan_id,
                                                      plan.plan_name
                                                  })
                                                  .ToList();

                    cmbMembershipPlans.DataSource = membershipPlans;
                    cmbMembershipPlans.DisplayMember = "plan_name"; 
                    cmbMembershipPlans.ValueMember = "membership_plan_id"; 
                    cmbMembershipPlans.SelectedIndex = -1; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading membership plans: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void btnRegister_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            DateTime birthDate = dtpBirthDate.Value; 
            string gender = cmbGender.SelectedItem?.ToString();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string address = txtAddress.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone) ||
                string.IsNullOrEmpty(address) || string.IsNullOrEmpty(password) || gender == null || cmbMembershipPlans.SelectedValue == null)
            {
                MessageBox.Show("Please fill in all fields, including password and membership plan.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int membershipPlanID = Convert.ToInt32(cmbMembershipPlans.SelectedValue);

            try
            {
                using (var context = new GymDatabaseEntitiess())
                {
                    var newMember = new Member
                    {
                        first_name = firstName,
                        last_name = lastName,
                        birth_date = birthDate,
                        gender = gender,
                        email = email,
                        phone = phone,
                        address = address,
                        password = password,
                        membership_plan_id = membershipPlanID
                    };

                    context.Members.Add(newMember);
                    context.SaveChanges();

                    MessageBox.Show("Registration successful! You can now log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearFields();

                    this.Hide();
                    LoginForm loginForm = new LoginForm();
                    loginForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void ClearFields()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            cmbGender.SelectedIndex = -1;
            dtpBirthDate.Value = DateTime.Now;
        }

        private void cmbMembershipPlans_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
