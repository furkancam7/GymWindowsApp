using sali;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using System;
using System.Linq;


namespace sali { 
public partial class ProfileForm : Form
{
    private int userId;

    public ProfileForm(int userId)
    {
        InitializeComponent();
        this.userId = userId;
        LoadUserData();
    }
        private void ProfileForm_Load(object sender, EventArgs e)
        {
            
        }



        private void LoadUserData()
        {
            try
            {
                using (var context = new GymDatabaseEntitiess())
                {
                    var member = context.Members.FirstOrDefault(m => m.member_id == userId);

                    if (member != null)
                    {
                        txtFirstName.Text = member.first_name ?? string.Empty;
                        txtLastName.Text = member.last_name ?? string.Empty;
                        txtEmail.Text = member.email ?? string.Empty;
                        txtPhone.Text = member.phone ?? string.Empty;
                        txtAddress.Text = member.address ?? string.Empty;
                        cmbGender.SelectedItem = member.gender ?? "Not Specified";
                        dtpBirthDate.Value = member.birth_date ?? DateTime.Now;
                    }
                    else
                    {
                        MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




  private void btnUpdate_Click(object sender, EventArgs e)
{
    try
    {
        using (var context = new GymDatabaseEntitiess())
        {
            var member = context.Members.FirstOrDefault(m => m.member_id == userId);

            if (member != null)
            {
                member.first_name = txtFirstName.Text.Trim();
                member.last_name = txtLastName.Text.Trim();
                member.email = txtEmail.Text.Trim();
                member.phone = txtPhone.Text.Trim();
                member.address = txtAddress.Text.Trim();
                member.gender = cmbGender.SelectedItem?.ToString();
                member.birth_date = dtpBirthDate.Value;

                context.SaveChanges();

                MessageBox.Show("User information updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("User not found. Unable to update information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error updating user data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}


    private void btnBack_Click(object sender, EventArgs e)
    {
        MainForm mainForm = new MainForm(userId);
        mainForm.Show();
        this.Close();
    }
        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}