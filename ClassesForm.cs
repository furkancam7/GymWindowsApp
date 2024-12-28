using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace sali
{
    public partial class ClassesForm : Form
    {
        private int userId;


        public ClassesForm(int userId)
        {


            InitializeComponent();
            this.userId = userId;


        }


        private void AdjustRowHeight()
        {
            int totalRows = dataGridViewClasses.RowCount;
            if (totalRows > 0)
            {
                int totalHeight = dataGridViewClasses.ClientSize.Height;
                int rowHeight = totalHeight / totalRows;

                dataGridViewClasses.RowTemplate.Height = rowHeight;
                dataGridViewClasses.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            }
        }
        private void CustomizeColumnColors()
        {
            dataGridViewClasses.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            dataGridViewClasses.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void ClassesForm_Load(object sender, EventArgs e)
        {



            dataGridViewClasses.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);


            dataGridViewClasses.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dataGridViewClasses.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridViewClasses.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;


            dataGridViewClasses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewClasses.ColumnHeadersHeight = 60;


            dataGridViewClasses.GridColor = Color.Gray;

            dataGridViewClasses.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewClasses.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Italic);

            dataGridViewClasses.DefaultCellStyle.ForeColor = Color.Black;

            dataGridViewClasses.DefaultCellStyle.BackColor = Color.White;

            dataGridViewClasses.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridViewClasses.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            dataGridViewClasses.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            LoadClassesData();
            foreach (DataGridViewColumn column in dataGridViewClasses.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }

            if (dataGridViewClasses.Columns.Count > 0)
            {
                dataGridViewClasses.Columns[0].Frozen = true;
            }

            AdjustRowHeight();



            LoadClassesData();
            CustomizeColumnColors();
            AdjustRowHeight();


        }


        private void LoadClassesData()
        {
            try
            {
                using (var context = new GymDatabaseEntitiess())
                {
                    var classesData = context.Classes
                        .Select(c => new
                        {
                            ClassID = c.class_id,
                            ClassName = c.class_name,
                            Description = c.description,
                            Schedule = c.schedule,
                            Capacity = c.capacity
                        })
                        .ToList();

                    dataGridViewClasses.DataSource = classesData;

                    dataGridViewClasses.Columns["ClassID"].Visible = true; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }







        private void dataGridViewClasses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {

            MainForm mainForm = new MainForm(userId);
            mainForm.Show();
            this.Close();
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            if (dataGridViewClasses.SelectedRows.Count > 0)
            {
                int selectedClassId = GetSelectedClassId();
                RegisterUserToClass(this.userId, selectedClassId);
            }
            else
            {
                MessageBox.Show("Please select a class to join.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private int GetSelectedClassId()
        {
            int selectedClassId = 0;
            if (dataGridViewClasses.SelectedRows.Count > 0)
            {
                selectedClassId = Convert.ToInt32(dataGridViewClasses.SelectedRows[0].Cells["ClassID"].Value);
            }
            return selectedClassId;
        }



        private void RegisterUserToClass(int userId, int classId)
        {
            try
            {
                using (var context = new GymDatabaseEntitiess())
                {
                   
                    var memberExists = context.Members.Any(m => m.member_id == userId);
                    if (!memberExists)
                    {
                        MessageBox.Show("Member not found. Please ensure the user ID is valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                 
                    var classExists = context.Classes.Any(c => c.class_id == classId);
                    if (!classExists)
                    {
                        MessageBox.Show("Class not found. Please ensure the class ID is valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

            
                    var enrollment = new Enrollment
                    {
                        member_id = userId,
                        class_id = classId,
                        enrollment_date = DateTime.Now
                    };

                    context.Enrollments.Add(enrollment);
                    context.SaveChanges();

                    MessageBox.Show("Successfully joined the class!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }

}

