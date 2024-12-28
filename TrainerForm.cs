using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace sali
{
    public partial class TrainerForm : Form
    {
        private int memberId; 

        public TrainerForm(int memberId) 
        {
            InitializeComponent();
            this.memberId = memberId;
        }

        private void TrainersForm_Load(object sender, EventArgs e)
        {
            dataGridViewTrainers.Dock = DockStyle.None;

            AddImageColumn();
            LoadTrainersData();
            AddImagesToRows();
            CustomizeDataGridViewHeaders();
        }

        private void AddImageColumn()
        {
           
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn
            {
                Name = "ImageColumn",
                HeaderText = "",
                ImageLayout = DataGridViewImageCellLayout.Zoom
            };

            if (!dataGridViewTrainers.Columns.Contains("ImageColumn"))
            {
                dataGridViewTrainers.Columns.Insert(0, imageColumn);
            }
        }

        private void AddImagesToRows()
        {
            string imagePath = @"C:\Users\Furkan\Desktop\gym\sali\sali\Images2\classes.png";

            if (!System.IO.File.Exists(imagePath))
            {
                MessageBox.Show($"Image file not found at: {imagePath}. Please check the file name and path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (DataGridViewRow row in dataGridViewTrainers.Rows)
            {
                if (row.Cells["ImageColumn"] != null)
                {
                    row.Cells["ImageColumn"].Value = Image.FromFile(imagePath); 
                }
            }
        }

        private void LoadTrainersData()
        {
            try
            {
                using (var context = new GymDatabaseEntitiess())
                {
                    var trainersData = context.Staffs
                        .Select(s => new
                        {
                            s.staff_id,
                            FirstName = s.first_name,
                            LastName = s.last_name,
                            Position = s.position,
                            Phone = s.phone,
                            Email = s.email,
                            Salary = s.salary
                        })
                        .ToList();

                    dataGridViewTrainers.DataSource = trainersData;

                    if (dataGridViewTrainers.Columns["staff_id"] != null)
                    {
                        dataGridViewTrainers.Columns["staff_id"].Visible = false;
                    }

                    dataGridViewTrainers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CustomizeDataGridViewHeaders()
        {
            dataGridViewTrainers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewTrainers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewTrainers.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridViewTrainers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewTrainers.ColumnHeadersHeight = 60;
            dataGridViewTrainers.GridColor = Color.Gray;
            dataGridViewTrainers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewTrainers.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Italic);
            dataGridViewTrainers.DefaultCellStyle.ForeColor = Color.Black;
            dataGridViewTrainers.DefaultCellStyle.BackColor = Color.White;
            dataGridViewTrainers.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewTrainers.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
        }

        private int GetSelectedTrainerId()
        {
            if (dataGridViewTrainers.SelectedRows.Count > 0)
            {
                return Convert.ToInt32(dataGridViewTrainers.SelectedRows[0].Cells["staff_id"].Value);
            }
            else
            {
                MessageBox.Show("Please select a trainer.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }
        }

        private void btnBookLesson_Click(object sender, EventArgs e)
        {
            int selectedTrainerId = GetSelectedTrainerId();
            if (selectedTrainerId == -1)
            {
                MessageBox.Show("Please select a trainer.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime lessonDate = dateTimePickerLesson.Value; 
            string notes = txtNotes.Text;

            try
            {
                using (var context = new GymDatabaseEntitiess())
                {
                    var privateLesson = new Private_Lessons
                    {
                        member_id = memberId,
                        staff_id = selectedTrainerId,
                        lesson_date = lessonDate,
                        notes = notes
                    };

                    context.Private_Lessons.Add(privateLesson);
                    context.SaveChanges();

                    MessageBox.Show("Lesson booked successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm(memberId);
            mainForm.Show();
            this.Close();
        }
        private void txtNotes_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
