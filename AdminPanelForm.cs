using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sali
{
    public partial class AdminPanelForm : Form
    {
        public AdminPanelForm()
        {
            InitializeComponent();
        }

        private void AdminPanelForm_Load(object sender, EventArgs e)
        {
            LoadMembers();
            LoadEnrollments();
            LoadEquipments();
            LoadTrainers();
            LoadClasses();
            LoadTrainerss();
            LoadEquipmentss();
        }
        private void LoadEquipmentss()
        {
            using (var context = new GymDatabaseEntitiess())
            {
                var equipmentList = context.Equipments.ToList(); 
                dgvEquipmentss.DataSource = null;
               
                dgvEquipmentss.DataSource = equipmentList;
                dgvEquipmentss.Refresh();

            }
        }



        private void btnAddEquipmentt_Click(object sender, EventArgs e)
        {
            using (var context = new GymDatabaseEntitiess())
            {
                var newEquipment = new Equipment
                {
                    equipment_name = txtEquipmentName.Text,
                    type = txtEquipmentType.Text,
                    purchase_date = dtpPurchaseDate.Value,
                    condition = cmbCondition.SelectedItem?.ToString()
                };

                try
                {
                    context.Equipments.Add(newEquipment); 
                    context.SaveChanges(); 
                    LoadEquipmentss();
                    MessageBox.Show("Equipment added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding equipment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDeleteEquipmentt_Click(object sender, EventArgs e)
        {
            if (dgvEquipmentss.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select equipment to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int equipmentId = Convert.ToInt32(dgvEquipmentss.SelectedRows[0].Cells["equipment_id"].Value);

            using (var context = new GymDatabaseEntitiess())
            {
                try
                {
                    var equipmentToDelete = context.Equipments.FirstOrDefault(equipment => equipment.equipment_id == equipmentId);

                    if (equipmentToDelete != null)
                    {
                        context.Equipments.Remove(equipmentToDelete);
                        context.SaveChanges(); 
                        LoadEquipmentss(); 
                        MessageBox.Show("Equipment deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting equipment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void LoadTrainerss()
        {
            using (var context = new GymDatabaseEntitiess())
            {
                context.Configuration.LazyLoadingEnabled = false;
                var trainersList = context.Staffs.Select(s => new
                {
                    s.staff_id,
                    s.first_name,
                    s.last_name,
                    s.position,
                    s.phone,
                    s.email,
                    s.salary
                }).ToList();

                dgvTrainerss.DataSource = trainersList;
            }
        }


        private void btnAddTrainerr_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTrainerFirstName.Text.Trim()) ||
                    string.IsNullOrWhiteSpace(txtTrainerLastName.Text.Trim()) ||
                    string.IsNullOrWhiteSpace(txtTrainerPosition.Text.Trim()) ||
                    string.IsNullOrWhiteSpace(txtTrainerPhone.Text.Trim()) ||
                    string.IsNullOrWhiteSpace(txtTrainerEmail.Text.Trim()) ||
                    string.IsNullOrWhiteSpace(txtSalary.Text.Trim()))
                {
                    MessageBox.Show("All fields must be filled.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

               
                if (!decimal.TryParse(txtSalary.Text.Trim(), out decimal salary))
                {
                    MessageBox.Show("Please enter a valid numeric value for salary.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

               
                var newTrainer = new Staff
                {
                    first_name = txtTrainerFirstName.Text.Trim(),
                    last_name = txtTrainerLastName.Text.Trim(),
                    position = txtTrainerPosition.Text.Trim(),
                    phone = txtTrainerPhone.Text.Trim(),
                    email = txtTrainerEmail.Text.Trim(),
                    salary = salary.ToString() 
                };

                using (var context = new GymDatabaseEntitiess())
                {
                    context.Staffs.Add(newTrainer);
                    context.SaveChanges();
                    MessageBox.Show("Trainer added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTrainerss();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    MessageBox.Show($"Inner Exception: {ex.InnerException.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }








        private void btnDeleteTrainerr_Click(object sender, EventArgs e)
        {
            if (dgvTrainerss.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a trainer to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int staffId = Convert.ToInt32(dgvTrainerss.SelectedRows[0].Cells["staff_id"].Value);

            using (var context = new GymDatabaseEntitiess())
            {
                var trainerToDelete = context.Staffs.FirstOrDefault(s => s.staff_id == staffId);
                if (trainerToDelete != null)
                {
                    context.Staffs.Remove(trainerToDelete);
                    context.SaveChanges();
                    LoadTrainerss();
                    MessageBox.Show("Trainer deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void LoadClasses()
        {
            try
            {
                using (var context = new GymDatabaseEntitiess()) 
                {
                    var classesList = context.Classes
                        .Select(c => new
                        {
                            c.class_id,
                            c.class_name,
                            c.description,
                            c.schedule
                        })
                        .ToList();

                    dgvClassess.DataSource = classesList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading classes: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnAddClass_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new GymDatabaseEntitiess())
                {
                    var newClass = new Class
                    {
                        class_name = txtClassName.Text.Trim(),
                        description = txtClassDescription.Text.Trim(),
                        schedule = txtClassSchedule.Text.Trim()
                    };

                    context.Classes.Add(newClass);
                    context.SaveChanges(); 

                    LoadClasses();
                    MessageBox.Show("Class added successfully!", "Success");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding class: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnDeleteClass_Click(object sender, EventArgs e)
        {
            if (dgvClasses.SelectedRows.Count > 0)
            {
                int classId = Convert.ToInt32(dgvClasses.SelectedRows[0].Cells["class_id"].Value);

                try
                {
                    using (var context = new GymDatabaseEntitiess())
                    {
                        var classToDelete = context.Classes.FirstOrDefault(c => c.class_id == classId);
                        if (classToDelete != null)
                        {
                            context.Classes.Remove(classToDelete);
                            context.SaveChanges(); 

                            LoadClasses();
                            MessageBox.Show("Class deleted successfully!", "Success");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting class: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a class to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        private void btnUpdateClass_Click(object sender, EventArgs e)
        {
            if (dgvClasses.SelectedRows.Count > 0)
            {
                int classId = Convert.ToInt32(dgvClasses.SelectedRows[0].Cells["class_id"].Value);

                try
                {
                    using (var context = new GymDatabaseEntitiess())
                    {
                        var classToUpdate = context.Classes.FirstOrDefault(c => c.class_id == classId);
                        if (classToUpdate != null)
                        {
                            classToUpdate.class_name = txtClassName.Text.Trim();
                            classToUpdate.description = txtClassDescription.Text.Trim();
                            classToUpdate.schedule = txtClassSchedule.Text.Trim();

                            context.SaveChanges(); 

                            LoadClasses();
                            MessageBox.Show("Class updated successfully!", "Success");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating class: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a class to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadMembers()
        {
            try
            {
                using (var context = new GymDatabaseEntitiess())
                {
                    var members = context.Members
                        .Select(m => new
                        {
                            m.member_id,
                            m.first_name,
                            m.last_name,
                            m.email,
                            m.phone,
                            m.address,
                            m.birth_date,
                            m.gender
                        })
                        .ToList();

                    dgvMembers.DataSource = members;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading members: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadEnrollments()
        {
            try
            {
                using (var context = new GymDatabaseEntitiess())
                {
                    var enrollments = context.Enrollments.ToList();
                    dgvClasses.DataSource = enrollments;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading enrollments: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadTrainers()
        {
            try
            {
                using (var context = new GymDatabaseEntitiess())
                {
                    var trainers = context.Private_Lessons.ToList();
                    dgvTrainers.DataSource = trainers;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading trainers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void LoadEquipments()
        {
            try
            {
                using (var context = new GymDatabaseEntitiess())
                {
                    var equipments = context.Equipment_Rentals.ToList();
                    dgvEquipments.DataSource = equipments;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading equipments: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                    string.IsNullOrWhiteSpace(txtLastName.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("First Name, Last Name, Email ve Password alanları gereklidir.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var context = new GymDatabaseEntitiess())
                {
                    var newMember = new Member
                    {
                        first_name = txtFirstName.Text.Trim(),
                        last_name = txtLastName.Text.Trim(),
                        email = txtEmail.Text.Trim(),
                        phone = txtPhone.Text.Trim(),
                        address = txtAddress.Text.Trim(),
                        birth_date = dtpBirthDate.Value,
                        gender = cmbGender.SelectedItem?.ToString(),
                        password = txtPassword.Text.Trim(), 
                        membership_plan_id = 1 
                    };

                    context.Members.Add(newMember);
                    context.SaveChanges();

                    LoadMembers();
                    ClearMemberInputFields();

                    MessageBox.Show("Member added successfully!", "Success");
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        MessageBox.Show($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding member: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void ClearMemberInputFields()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            dtpBirthDate.Value = DateTime.Now;
            cmbGender.SelectedIndex = -1; 
        }

        private void btnDeleteMember_Click(object sender, EventArgs e)
        {
            if (dgvMembers.SelectedRows.Count > 0)
            {
                int memberId = Convert.ToInt32(dgvMembers.SelectedRows[0].Cells["member_id"].Value);

                try
                {
                    using (var context = new GymDatabaseEntitiess()) 
                    {
                        var enrollmentsToDelete = context.Enrollments
                            .Where(member => member.member_id == memberId)
                            .ToList();

                        context.Enrollments.RemoveRange(enrollmentsToDelete);

                        var memberToDelete = context.Members
                            .FirstOrDefault(m => m.member_id == memberId);

                        if (memberToDelete != null)
                        {
                            context.Members.Remove(memberToDelete);
                            context.SaveChanges(); 
                        }

                        LoadMembers();

                        MessageBox.Show("Member deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting member: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a member to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnAddClasss_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new GymDatabaseEntitiess()) 
                {
                    var newClass = new Class
                    {
                        class_name = txtClassName.Text.Trim(),
                        description = txtClassDescription.Text.Trim(),
                        schedule = txtClassSchedule.Text.Trim(),
                        instructor_id = Convert.ToInt32(txtInstructorId.Text.Trim()),
                        capacity = Convert.ToInt32(txtCapacity.Text.Trim())
                    };

                    context.Classes.Add(newClass);
                    context.SaveChanges(); 

                    LoadClasses();

                    MessageBox.Show("Class added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearClassInputFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding class: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ClearClassInputFields()
        {
            txtClassName.Clear();
            txtClassDescription.Clear();
            txtClassSchedule.Clear();
            txtInstructorId.Clear();
            txtCapacity.Clear();
        }

        private void btnDeleteClasss_Click(object sender, EventArgs e)
        {
            if (dgvClassess.SelectedRows.Count > 0)
            {
                int classId = Convert.ToInt32(dgvClassess.SelectedRows[0].Cells["class_id"].Value);

                try
                {
                    using (var context = new GymDatabaseEntitiess()) 
                    {
                        var classToDelete = context.Classes.FirstOrDefault(c => c.class_id == classId);
                        if (classToDelete != null)
                        {
                            context.Classes.Remove(classToDelete); 
                            context.SaveChanges(); 

                          
                            LoadClasses();

                            
                            MessageBox.Show("Class deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Class not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show("Error deleting class: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a class to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAddTrainer_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new GymDatabaseEntitiess()) 
                {
                    var newLesson = new Private_Lessons
                    {
                        
                        lesson_date = DateTime.Now,
                        notes = "No notes"
                    };

                    context.Private_Lessons.Add(newLesson);
                    context.SaveChanges(); 

                    LoadTrainers();

                    
                    MessageBox.Show("Trainer added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding trainer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteTrainer_Click(object sender, EventArgs e)
        {
            if (dgvTrainers.SelectedRows.Count > 0)
            {
                int trainerId = Convert.ToInt32(dgvTrainers.SelectedRows[0].Cells["staff_id"].Value);

                try
                {
                    using (var context = new GymDatabaseEntitiess())
                    {
                        var trainerToDelete = context.Private_Lessons.FirstOrDefault(t => t.staff_id == trainerId);

                        if (trainerToDelete != null)
                        {
                            context.Private_Lessons.Remove(trainerToDelete);
                            context.SaveChanges(); 

                            LoadTrainers();

                            MessageBox.Show("Private Lesson deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Trainer not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting trainer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a Private Lesson to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnAddEquipment_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new GymDatabaseEntitiess()) 
                {
                    var newEquipment = new Equipment_Rentals
                    {
                        
                        rental_date = DateTime.Now,
                        return_date = DateTime.Now.AddDays(7)
                    };

                    context.Equipment_Rentals.Add(newEquipment);
                    context.SaveChanges(); 

                    LoadEquipments();

                    MessageBox.Show("Equipment added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding equipment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteEquipment_Click(object sender, EventArgs e)
        {
            if (dgvEquipments.SelectedRows.Count > 0)
            {
                int equipmentId = Convert.ToInt32(dgvEquipments.SelectedRows[0].Cells["equipment_id"].Value);

                try
                {
                    using (var context = new GymDatabaseEntitiess()) 
                    {
                        var equipmentToDelete = context.Equipment_Rentals.FirstOrDefault(eq => eq.equipment_id == equipmentId);

                        if (equipmentToDelete != null)
                        {
                            context.Equipment_Rentals.Remove(equipmentToDelete);
                            context.SaveChanges(); 

                            
                            LoadEquipments();

                            
                            MessageBox.Show("Equipment deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Equipment not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting equipment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an equipment to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void dgvMembers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow row = dgvMembers.Rows[e.RowIndex];

                txtFirstName.Text = row.Cells["first_name"].Value?.ToString();
                txtLastName.Text = row.Cells["last_name"].Value?.ToString();
                txtPassword.Text = ""; 
                txtEmail.Text = row.Cells["email"].Value?.ToString();
                txtPhone.Text = row.Cells["phone"].Value?.ToString();
                txtAddress.Text = row.Cells["address"].Value?.ToString();
                dtpBirthDate.Value = Convert.ToDateTime(row.Cells["birth_date"].Value);
                cmbGender.SelectedItem = row.Cells["gender"].Value?.ToString();
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMembers.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Lütfen güncellemek için bir üye seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int memberId = Convert.ToInt32(dgvMembers.SelectedRows[0].Cells["member_id"].Value);

                using (var context = new GymDatabaseEntitiess())
                {
                    
                    var member = context.Members.FirstOrDefault(m => m.member_id == memberId);
                    if (member != null)
                    {
                        
                        member.first_name = txtFirstName.Text.Trim();
                        member.last_name = txtLastName.Text.Trim();
                        member.email = txtEmail.Text.Trim();
                        member.phone = txtPhone.Text.Trim();
                        member.address = txtAddress.Text.Trim();
                        member.birth_date = dtpBirthDate.Value;
                        member.gender = cmbGender.SelectedItem?.ToString();
                        if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                        {
                            member.password = txtPassword.Text.Trim(); 
                        }

                        context.SaveChanges(); 
                        LoadMembers(); 
                        ClearMemberInputFields(); 

                        MessageBox.Show("Üye başarıyla güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Seçilen üye bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Güncelleme sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnUpdateEquipmentt_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvEquipmentss.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an equipment to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int equipmentId = Convert.ToInt32(dgvEquipmentss.SelectedRows[0].Cells["equipment_id"].Value);

                using (var context = new GymDatabaseEntitiess())
                {
                    var equipmentToUpdate = context.Equipments.FirstOrDefault(eq => eq.equipment_id == equipmentId);

                    if (equipmentToUpdate == null)
                    {
                        MessageBox.Show("Equipment not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    equipmentToUpdate.equipment_name = txtEquipmentName.Text.Trim();
                    equipmentToUpdate.type = txtEquipmentType.Text.Trim();
                    equipmentToUpdate.purchase_date = dtpPurchaseDate.Value;
                    equipmentToUpdate.condition = cmbCondition.SelectedItem?.ToString();

                    context.SaveChanges();
                    MessageBox.Show("Equipment updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    
                    LoadEquipmentss();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtCapacity_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        

        private void tabPageMembers_Click(object sender, EventArgs e)
        {

        }

        private void dgvClassess_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow row = dgvClasses.Rows[e.RowIndex];

                txtClassName.Text = row.Cells["class_name"].Value?.ToString();
                txtInstructorId.Text = row.Cells["instructor"].Value?.ToString();
                txtCapacity.Text = row.Cells["capacity"].Value?.ToString();
                txtClassSchedule.Text = row.Cells["schedule"].Value?.ToString();
                txtClassDescription.Text = row.Cells["description"].Value?.ToString();
            }
        }
        private void btnUpdatee_Click(object sender, EventArgs e)
        {
            if (dgvClassess.CurrentRow == null || dgvClassess.CurrentRow.Index < 0)
            {
                MessageBox.Show("Please select a class to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int classId = Convert.ToInt32(dgvClassess.CurrentRow.Cells["class_id"].Value);

            using (var context = new GymDatabaseEntitiess())
            {
                var classToUpdate = context.Classes.FirstOrDefault(c => c.class_id == classId);
                if (classToUpdate != null)
                {
                    classToUpdate.class_name = txtClassName.Text.Trim();
                    classToUpdate.instructor_id = int.Parse(txtInstructorId.Text.Trim());
                    classToUpdate.capacity = int.Parse(txtCapacity.Text.Trim());
                    classToUpdate.schedule = txtClassSchedule.Text.Trim();
                    classToUpdate.description = txtClassDescription.Text.Trim();

                    context.SaveChanges(); 
                    LoadClasses(); 
                    ClearClassInputFields(); 

                    MessageBox.Show("Class updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Selected class not found.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void tabPageClassess_Click(object sender, EventArgs e)
        {

        }

        private void dgvTrainerss_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtTrainerPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdateeee_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTrainerss.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a trainer to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int staffId = Convert.ToInt32(dgvTrainerss.SelectedRows[0].Cells["staff_id"].Value);

                using (var context = new GymDatabaseEntitiess())
                {
                    var trainerToUpdate = context.Staffs.FirstOrDefault(t => t.staff_id == staffId);

                    if (trainerToUpdate == null)
                    {
                        MessageBox.Show("Trainer not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    trainerToUpdate.first_name = txtTrainerFirstName.Text.Trim();
                    trainerToUpdate.last_name = txtTrainerLastName.Text.Trim();
                    trainerToUpdate.position = txtTrainerPosition.Text.Trim();
                    trainerToUpdate.phone = txtTrainerPhone.Text.Trim();
                    trainerToUpdate.email = txtTrainerEmail.Text.Trim();
                    trainerToUpdate.salary = txtSalary.Text.Trim();

                    context.SaveChanges();
                    MessageBox.Show("Trainer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTrainerss();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    } }
