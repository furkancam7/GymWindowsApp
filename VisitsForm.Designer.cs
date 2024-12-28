namespace sali
{
    partial class VisitsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisitsForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPages1 = new System.Windows.Forms.TabPage();
            this.dgvClasses = new System.Windows.Forms.DataGridView();
            this.tabPages2 = new System.Windows.Forms.TabPage();
            this.dgvTrainers = new System.Windows.Forms.DataGridView();
            this.tabPages3 = new System.Windows.Forms.TabPage();
            this.dgvEquipments = new System.Windows.Forms.DataGridView();
            this.btnBack = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPages1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClasses)).BeginInit();
            this.tabPages2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrainers)).BeginInit();
            this.tabPages3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipments)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPages1);
            this.tabControl1.Controls.Add(this.tabPages2);
            this.tabControl1.Controls.Add(this.tabPages3);
            this.tabControl1.Location = new System.Drawing.Point(12, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(479, 212);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Tag = "";
            // 
            // tabPages1
            // 
            this.tabPages1.Controls.Add(this.dgvClasses);
            this.tabPages1.Location = new System.Drawing.Point(4, 22);
            this.tabPages1.Name = "tabPages1";
            this.tabPages1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPages1.Size = new System.Drawing.Size(348, 240);
            this.tabPages1.TabIndex = 0;
            this.tabPages1.Text = "Classes";
            this.tabPages1.UseVisualStyleBackColor = true;
            // 
            // dgvClasses
            // 
            this.dgvClasses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClasses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvClasses.Location = new System.Drawing.Point(3, 3);
            this.dgvClasses.Name = "dgvClasses";
            this.dgvClasses.Size = new System.Drawing.Size(342, 234);
            this.dgvClasses.TabIndex = 0;
            // 
            // tabPages2
            // 
            this.tabPages2.Controls.Add(this.dgvTrainers);
            this.tabPages2.Location = new System.Drawing.Point(4, 22);
            this.tabPages2.Name = "tabPages2";
            this.tabPages2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPages2.Size = new System.Drawing.Size(348, 240);
            this.tabPages2.TabIndex = 1;
            this.tabPages2.Text = "Trainers";
            this.tabPages2.UseVisualStyleBackColor = true;
            // 
            // dgvTrainers
            // 
            this.dgvTrainers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTrainers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTrainers.Location = new System.Drawing.Point(3, 3);
            this.dgvTrainers.Name = "dgvTrainers";
            this.dgvTrainers.Size = new System.Drawing.Size(342, 234);
            this.dgvTrainers.TabIndex = 0;
            // 
            // tabPages3
            // 
            this.tabPages3.Controls.Add(this.dgvEquipments);
            this.tabPages3.Location = new System.Drawing.Point(4, 22);
            this.tabPages3.Name = "tabPages3";
            this.tabPages3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPages3.Size = new System.Drawing.Size(471, 186);
            this.tabPages3.TabIndex = 2;
            this.tabPages3.Text = "Equipments";
            this.tabPages3.UseVisualStyleBackColor = true;
            // 
            // dgvEquipments
            // 
            this.dgvEquipments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEquipments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEquipments.Location = new System.Drawing.Point(3, 3);
            this.dgvEquipments.Name = "dgvEquipments";
            this.dgvEquipments.Size = new System.Drawing.Size(465, 180);
            this.dgvEquipments.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBack.Image = ((System.Drawing.Image)(resources.GetObject("btnBack.Image")));
            this.btnBack.Location = new System.Drawing.Point(473, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(43, 40);
            this.btnBack.TabIndex = 1;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(194, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Check your Activities!";
            // 
            // VisitsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(528, 267);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.tabControl1);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VisitsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visits";
            this.Load += new System.EventHandler(this.VisitsForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPages1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClasses)).EndInit();
            this.tabPages2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrainers)).EndInit();
            this.tabPages3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPages1;
        private System.Windows.Forms.TabPage tabPages2;
        private System.Windows.Forms.TabPage tabPages3;
        private System.Windows.Forms.DataGridView dgvClasses;
        private System.Windows.Forms.DataGridView dgvTrainers;
        private System.Windows.Forms.DataGridView dgvEquipments;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label label1;
    }
}