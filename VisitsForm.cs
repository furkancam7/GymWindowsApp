using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;


namespace sali
{
    public partial class VisitsForm : Form
    {
        private int userId;

        public VisitsForm(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void VisitsForm_Load(object sender, EventArgs e)
        {
           
            CustomizeDataGridViews();
            LoadEnrollmentsData();
            LoadPrivateLessonsData();
            LoadEquipmentRentalsData();
            AddTabDescriptions();
        }
        private void AddTabDescriptions()
        {
            Label lblClasses = new Label
            {
                Text = "Classes you have joined:",
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                Location = new Point(10, tabControl1.Top + 10),
                AutoSize = true
            };
            tabPages1.Controls.Add(lblClasses);

            Label lblTrainers = new Label
            {
                Text = "Trainers you have worked with:",
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                Location = new Point(10, tabControl1.Top + 10),
                AutoSize = true
            };
            tabPages2.Controls.Add(lblTrainers);

            Label lblEquipments = new Label
            {
                Text = "Equipments you have rented:",
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                Location = new Point(10, tabControl1.Top + 10),
                AutoSize = true
            };
            tabPages3.Controls.Add(lblEquipments);
        }

        private void LoadEnrollmentsData()
        {
            try
            {
                using (var context = new GymDatabaseEntitiess())
                {
                    var enrollments = context.Enrollments
                        .Where(e => e.member_id == userId)
                        .Select(e => new
                        {
                            ClassName = e.Class.class_name,
                            Schedule = e.Class.schedule,
                            Capacity = e.Class.capacity
                        })
                        .ToList();

                    dgvClasses.DataSource = enrollments;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading enrollments data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadPrivateLessonsData()
        {
            try
            {
                using (var context = new GymDatabaseEntitiess())
                {
                    var privateLessons = context.Private_Lessons
                        .Where(pl => pl.member_id == userId) 
                        .Join(
                            context.Staffs, 
                            pl => pl.staff_id, 
                            s => s.staff_id,   
                            (pl, s) => new 
                            {
                                TrainerName = s.first_name + " " + s.last_name,
                                LessonDate = pl.lesson_date,
                                Notes = pl.notes
                            })
                        .ToList();

                    dgvTrainers.DataSource = privateLessons;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading private lessons data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void LoadEquipmentRentalsData()
        {
            try
            {
                using (var context = new GymDatabaseEntitiess())
                {
                    var equipmentRentals = context.Equipment_Rentals
                        .Where(er => er.member_id == userId)
                        .Select(er => new
                        {
                            Equipment = er.Equipment.equipment_name,
                            RentalDate = er.rental_date,
                            ReturnDate = er.return_date
                        })
                        .ToList();

                    dgvEquipments.DataSource = equipmentRentals;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading equipment rentals data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomizeDataGridViews()
        {
            DataGridView[] dataGrids = { dgvClasses, dgvTrainers, dgvEquipments }; 

            foreach (var grid in dataGrids)
            {
                grid.Size = new Size(tabControl1.Width - 20, tabControl1.Height - 30); 
                grid.Location = new Point(10, 10);

                grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(63, 81, 181); 
                grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                grid.EnableHeadersVisualStyles = false;

                grid.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Italic);
                grid.DefaultCellStyle.ForeColor = Color.Black;

                grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                grid.DefaultCellStyle.BackColor = Color.White;

                grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                grid.GridColor = Color.LightGray;
                grid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grid.RowTemplate.Height = 40;

                
                grid.ScrollBars = ScrollBars.Both;

               
                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }





        private void LoadDataToGrid(Func<GymDatabaseEntitiess, IQueryable<object>> queryFunction, DataGridView gridView)
        {
            try
            {
                using (var context = new GymDatabaseEntitiess())
                {
                    var data = queryFunction(context).ToList();

                    gridView.DataSource = data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
           
            MainForm mainForm = new MainForm(userId); 
            mainForm.Show();
            this.Close();
        }
    }
}
