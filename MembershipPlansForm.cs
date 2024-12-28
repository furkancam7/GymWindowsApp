using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace sali
{
    public partial class MembershipPlansForm : Form
    {
        private int userId;

        public MembershipPlansForm(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadMembershipPlans();
            LoadUserCurrentPlan();
        }

        private void MembershipPlansForm_Load(object sender, EventArgs e)
        {

            LoadMembershipPlans();
            LoadUserCurrentPlan();
            CustomizeDataGridView();
            AddImageColumn();
            AddImagesToPlans();
            dgvPlans.AllowUserToAddRows = false;
            dgvPlans.CellFormatting += dgvPlans_CellFormatting;
        }
        private void LoadUserCurrentPlan()
        {
            try
            {
                using (var context = new GymDatabaseEntitiess())
                {
                    var currentPlan = context.Members
                        .Where(m => m.member_id == userId)
                        .Select(m => m.Membership_Plans.plan_name)
                        .FirstOrDefault();

                    if (!string.IsNullOrEmpty(currentPlan))
                    {
                        lblCurrentPlan.Text = "Current Plan: " + currentPlan;
                    }
                    else
                    {
                        lblCurrentPlan.Text = "No membership plan assigned.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading current plan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadMembershipPlans()
        {
            try
            {
                using (var context = new GymDatabaseEntitiess())
                {
                    var plans = context.Membership_Plans
                        .Select(p => new
                        {
                            p.membership_plan_id,
                            PlanName = p.plan_name,
                            Duration = p.duration,
                            Price = p.price
                        })
                        .ToList();

                    dgvPlans.DataSource = plans;
                    dgvPlans.Columns["membership_plan_id"].Visible = false; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading plans: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomizeDataGridView()
        {
             dgvPlans.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvPlans.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dgvPlans.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dgvPlans.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvPlans.DefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Italic);
            dgvPlans.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvPlans.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            dgvPlans.GridColor = Color.Gray;

            dgvPlans.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dgvPlans.ScrollBars = ScrollBars.Both;

            dgvPlans.Columns["PlanName"].HeaderText = "Plan Name";
            dgvPlans.Columns["duration"].HeaderText = "Duration (Days)";
            dgvPlans.Columns["price"].HeaderText = "Price/Month ($)";
            

        }
        private void AddImageColumn()
        {
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn
            {
                Name = "ImageColumn",
                HeaderText = "",
                ImageLayout = DataGridViewImageCellLayout.Zoom
            };

            if (!dgvPlans.Columns.Contains("ImageColumn"))
            {
                dgvPlans.Columns.Insert(0, imageColumn);
            }
        }

        private void dgvPlans_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvPlans.Columns[e.ColumnIndex].Name == "plan_name" && e.Value != null)
            {
                e.CellStyle.Font = new Font("Segoe UI", 12, FontStyle.Italic);
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }
        private void AddImagesToPlans()
        {
            foreach (DataGridViewRow row in dgvPlans.Rows)
            {
                if (row.Cells["PlanName"].Value != null)
                {
                    string planName = row.Cells["PlanName"].Value.ToString();

                    if (planName == "Basic Plan")
                        row.Cells["ImageColumn"].Value = Image.FromFile(@"C:\Users\Furkan\Desktop\gym\sali\sali\Images2\basic.png");
                    else if (planName == "Standard Plan")
                        row.Cells["ImageColumn"].Value = Image.FromFile(@"C:\Users\Furkan\Desktop\gym\sali\sali\Images2\basic.png");
                    else if (planName == "Premium Plan")
                        row.Cells["ImageColumn"].Value = Image.FromFile(@"C:\Users\Furkan\Desktop\gym\sali\sali\Images2\basic.png");
                }
            }
        }

        private void btnChangePlan_Click(object sender, EventArgs e)
        {
            if (dgvPlans.SelectedRows.Count > 0)
            {
                int selectedPlanId = Convert.ToInt32(dgvPlans.SelectedRows[0].Cells["membership_plan_id"].Value);
                string selectedPlanName = dgvPlans.SelectedRows[0].Cells["PlanName"].Value.ToString();
                decimal selectedPlanPrice = Convert.ToDecimal(dgvPlans.SelectedRows[0].Cells["price"].Value);

                DialogResult dialogResult = MessageBox.Show(
                    $"You are about to purchase the {selectedPlanName} plan for {selectedPlanPrice:C}. Do you want to proceed?",
                    "Confirm Purchase",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    UpdateMembershipPlan(selectedPlanId);
                }
            }
            else
            {
                MessageBox.Show("Please select a membership plan from the table.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateMembershipPlan(int planId)
        {
            try
            {
                using (var context = new GymDatabaseEntitiess())
                {
                    var member = context.Members.FirstOrDefault(m => m.member_id == userId);

                    if (member != null)
                    {
                        member.membership_plan_id = planId;

                        context.SaveChanges();

                        MessageBox.Show("Membership plan updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadUserCurrentPlan();
                    }
                    else
                    {
                        MessageBox.Show("Member not found. Please try again.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            
            MainForm mainForm = new MainForm(userId); 
            mainForm.Show();
            this.Close();
        }

        private void dgvPlans_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblCurrentPlan_Click(object sender, EventArgs e)
        {

        }
    }
}
