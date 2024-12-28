using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace sali
{
    public partial class EquipmentsForm : Form
    {
        private int userId;

        public EquipmentsForm(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void EquipmentsForm_Load(object sender, EventArgs e)
        {
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn
            {
                Name = "ImageColumn",
                HeaderText = "",
                ImageLayout = DataGridViewImageCellLayout.Zoom
            };

            if (!dgvEquipments.Columns.Contains("ImageColumn"))
            {
                dgvEquipments.Columns.Insert(0, imageColumn);
            }


            LoadEquipments();


            AddImagesToRows();

            dgvEquipments.DefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Italic);
            dgvEquipments.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dgvEquipments.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEquipments.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvEquipments.Dock = DockStyle.None;
            dgvEquipments.Location = new System.Drawing.Point(50, 50);
            dgvEquipments.Size = new System.Drawing.Size(750, 400);
            dgvEquipments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEquipments.ScrollBars = ScrollBars.Both;
        }

        private void AddImagesToRows()
        {
            string imagesDirectory = @"C:\Images2\";

            foreach (DataGridViewRow row in dgvEquipments.Rows)
            {
                if (row.Cells["Type"].Value != null)
                {
                    string equipmentType = row.Cells["Type"].Value.ToString();
                    string imagePath = string.Empty;

                    if (equipmentType.Contains("Cardio"))
                        imagePath = $"{imagesDirectory}cardio.png";
                    else if (equipmentType.Contains("Weightlifting"))
                        imagePath = $"{imagesDirectory}weightlifting.png";
                    else if (equipmentType.Contains("Strength Training"))
                        imagePath = $"{imagesDirectory}strength.png";
                    else if (equipmentType.Contains("Functional Training"))
                        imagePath = $"{imagesDirectory}functional.png";
                    else if (equipmentType.Contains("Core Training"))
                        imagePath = $"{imagesDirectory}core.png";

                    if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                    {
                        row.Cells["ImageColumn"].Value = Image.FromFile(imagePath);
                    }
                    else
                    {
                        row.Cells["ImageColumn"].Value = Properties.Resources.defaultImage;
                    }
                }
            }
        }

        private void LoadEquipments()
        {
            try
            {
                using (var context = new GymDatabaseEntitiess())
                {
                    var equipments = context.Equipments
                        .Select(e => new
                        {
                            e.equipment_id,
                            EquipmentName = e.equipment_name,
                            Type = e.type,
                            PurchaseDate = e.purchase_date,
                            Condition = e.condition
                        })
                        .ToList();

                    dgvEquipments.DataSource = equipments;
                    dgvEquipments.Columns["equipment_id"].Visible = false; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnRent_Click(object sender, EventArgs e)
        {
            if (dgvEquipments.SelectedRows.Count > 0)
            {
                try
                {
                    int selectedEquipmentId = Convert.ToInt32(dgvEquipments.SelectedRows[0].Cells["equipment_id"].Value);
                    string equipmentName = dgvEquipments.SelectedRows[0].Cells["EquipmentName"].Value.ToString();
                    string condition = dgvEquipments.SelectedRows[0].Cells["Condition"].Value.ToString();

                    DateTime rentalDate = DateTime.Now;
                    DateTime returnDate = rentalDate.AddDays(7);

                    RentEquipment(this.userId, selectedEquipmentId, rentalDate, returnDate);

                    MessageBox.Show($"You have rented the equipment: {equipmentName}\nCondition: {condition}\nReturn Date: {returnDate:yyyy-MM-dd}",
                        "Equipment Rented", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an equipment to rent.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void RentEquipment(int memberId, int equipmentId, DateTime rentalDate, DateTime returnDate)
        {
            try
            {
                using (var context = new GymDatabaseEntitiess())
                {
                    var rental = new Equipment_Rentals
                    {
                        member_id = memberId,
                        equipment_id = equipmentId,
                        rental_date = rentalDate,
                        return_date = returnDate,
                        rental_status = "Active"
                    };

                    context.Equipment_Rentals.Add(rental);
                    context.SaveChanges();
                    MessageBox.Show("Equipment rented successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm(this.userId);
            mainForm.Show();
            this.Close();
        }
    }
}
