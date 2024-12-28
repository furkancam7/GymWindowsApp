using System;
using System.Windows.Forms;

namespace sali
{
    public partial class MainForm : Form
    {
        private int userId;
        public MainForm(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }
      


        private void MainForm_Load(object sender, EventArgs e)
        {
            


            this.ActiveControl = null; 

        }
        public MainForm()
        {
            InitializeComponent();
        }
        private void btnClasses_Click(object sender, EventArgs e)
        {
            ClassesForm classesForm = new ClassesForm(userId);
            classesForm.Show();
            this.Hide();
        }


        private void btnTrainers_Click(object sender, EventArgs e)
        {
           
            TrainerForm trainersForm = new TrainerForm(userId);
            trainersForm.Show();
            this.Hide();

        }


        private void btnEquipments_Click(object sender, EventArgs e)
        {
            
            EquipmentsForm equipmentsForm = new EquipmentsForm(userId);
            equipmentsForm.Show();
            this.Hide();
        }

        private void btnVisits_Click(object sender, EventArgs e)
        {
            
            VisitsForm visitsForm = new VisitsForm(userId);
            visitsForm.Show();
            this.Hide();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            int currentUserId = LoginForm.CurrentUserId; 
            ProfileForm profileForm = new ProfileForm(userId);
            profileForm.Show();
            this.Hide();
        }




        private void btnMembershipPlans_Click(object sender, EventArgs e)
        {
            MembershipPlansForm plansForm = new MembershipPlansForm(userId);
            plansForm.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close(); 
        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            HomeForm homeForm = new HomeForm();
            homeForm.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
