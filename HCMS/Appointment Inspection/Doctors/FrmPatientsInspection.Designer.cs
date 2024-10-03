namespace HCMS.Appointment_Inspection.Doctors
{
    partial class FrmPatientsInspection
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvListPatientInspection = new System.Windows.Forms.DataGridView();
            this.cmsPatientInspection = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showDoctorDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.CancelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.InspectionToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.txtFindBy = new System.Windows.Forms.TextBox();
            this.cbFindBy = new System.Windows.Forms.ComboBox();
            this.lblFindBy = new System.Windows.Forms.Label();
            this.lblRecordsCount = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.makeMedicalRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListPatientInspection)).BeginInit();
            this.cmsPatientInspection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvListPatientInspection
            // 
            this.dgvListPatientInspection.AllowUserToAddRows = false;
            this.dgvListPatientInspection.AllowUserToDeleteRows = false;
            this.dgvListPatientInspection.BackgroundColor = System.Drawing.Color.White;
            this.dgvListPatientInspection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListPatientInspection.ContextMenuStrip = this.cmsPatientInspection;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvListPatientInspection.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvListPatientInspection.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvListPatientInspection.GridColor = System.Drawing.Color.White;
            this.dgvListPatientInspection.Location = new System.Drawing.Point(41, 338);
            this.dgvListPatientInspection.MultiSelect = false;
            this.dgvListPatientInspection.Name = "dgvListPatientInspection";
            this.dgvListPatientInspection.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvListPatientInspection.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvListPatientInspection.RowHeadersWidth = 51;
            this.dgvListPatientInspection.RowTemplate.Height = 24;
            this.dgvListPatientInspection.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListPatientInspection.Size = new System.Drawing.Size(1499, 395);
            this.dgvListPatientInspection.TabIndex = 147;
            this.dgvListPatientInspection.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListPatientInspection_CellDoubleClick);
            // 
            // cmsPatientInspection
            // 
            this.cmsPatientInspection.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsPatientInspection.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDoctorDetailsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.CancelToolStripMenuItem1,
            this.InspectionToolStripMenuItem2});
            this.cmsPatientInspection.Name = "cmsListPeople";
            this.cmsPatientInspection.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.cmsPatientInspection.Size = new System.Drawing.Size(246, 200);
            // 
            // showDoctorDetailsToolStripMenuItem
            // 
            this.showDoctorDetailsToolStripMenuItem.Image = global::HCMS.Properties.Resources.PersonDetails_32;
            this.showDoctorDetailsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showDoctorDetailsToolStripMenuItem.Name = "showDoctorDetailsToolStripMenuItem";
            this.showDoctorDetailsToolStripMenuItem.Size = new System.Drawing.Size(245, 54);
            this.showDoctorDetailsToolStripMenuItem.Text = "&Show Patient Details";
            this.showDoctorDetailsToolStripMenuItem.Click += new System.EventHandler(this.showDoctorDetailsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(242, 6);
            // 
            // CancelToolStripMenuItem1
            // 
            this.CancelToolStripMenuItem1.Image = global::HCMS.Properties.Resources.Delete_32___Copy;
            this.CancelToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.CancelToolStripMenuItem1.Name = "CancelToolStripMenuItem1";
            this.CancelToolStripMenuItem1.Size = new System.Drawing.Size(245, 54);
            this.CancelToolStripMenuItem1.Text = "&Cancel";
            this.CancelToolStripMenuItem1.Click += new System.EventHandler(this.CancelToolStripMenuItem1_Click);
            // 
            // InspectionToolStripMenuItem2
            // 
            this.InspectionToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.makeMedicalRecordToolStripMenuItem});
            this.InspectionToolStripMenuItem2.Image = global::HCMS.Properties.Resources.doctor_48;
            this.InspectionToolStripMenuItem2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.InspectionToolStripMenuItem2.Name = "InspectionToolStripMenuItem2";
            this.InspectionToolStripMenuItem2.Size = new System.Drawing.Size(245, 54);
            this.InspectionToolStripMenuItem2.Text = "&Inspection Patient";
            this.InspectionToolStripMenuItem2.Click += new System.EventHandler(this.InspectionToolStripMenuItem2_Click);
            // 
            // txtFindBy
            // 
            this.txtFindBy.BackColor = System.Drawing.Color.White;
            this.txtFindBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFindBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFindBy.Location = new System.Drawing.Point(388, 287);
            this.txtFindBy.Name = "txtFindBy";
            this.txtFindBy.Size = new System.Drawing.Size(208, 30);
            this.txtFindBy.TabIndex = 156;
            this.txtFindBy.TextChanged += new System.EventHandler(this.txtFindBy_TextChanged);
            this.txtFindBy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFindBy_KeyPress);
            // 
            // cbFindBy
            // 
            this.cbFindBy.BackColor = System.Drawing.Color.White;
            this.cbFindBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFindBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFindBy.FormattingEnabled = true;
            this.cbFindBy.Items.AddRange(new object[] {
            "None",
            "Appointment ID",
            "Patient ID",
            "Patient Name",
            "Appointment Status"});
            this.cbFindBy.Location = new System.Drawing.Point(137, 285);
            this.cbFindBy.Name = "cbFindBy";
            this.cbFindBy.Size = new System.Drawing.Size(230, 33);
            this.cbFindBy.TabIndex = 155;
            this.cbFindBy.SelectedIndexChanged += new System.EventHandler(this.cbFindBy_SelectedIndexChanged);
            // 
            // lblFindBy
            // 
            this.lblFindBy.AutoSize = true;
            this.lblFindBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFindBy.Location = new System.Drawing.Point(36, 288);
            this.lblFindBy.Name = "lblFindBy";
            this.lblFindBy.Size = new System.Drawing.Size(92, 25);
            this.lblFindBy.TabIndex = 154;
            this.lblFindBy.Text = "Find By:";
            // 
            // lblRecordsCount
            // 
            this.lblRecordsCount.AutoSize = true;
            this.lblRecordsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsCount.Location = new System.Drawing.Point(146, 752);
            this.lblRecordsCount.Name = "lblRecordsCount";
            this.lblRecordsCount.Size = new System.Drawing.Size(74, 25);
            this.lblRecordsCount.TabIndex = 152;
            this.lblRecordsCount.Text = "[????]";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(36, 752);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(104, 25);
            this.label22.TabIndex = 151;
            this.label22.Text = "Records: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(666, 227);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(355, 46);
            this.label1.TabIndex = 148;
            this.label1.Text = "Patient Inspection";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::HCMS.Properties.Resources.Close_32___Copy;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1395, 752);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(145, 44);
            this.btnClose.TabIndex = 153;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::HCMS.Properties.Resources.doctor_patient_512;
            this.pictureBox1.Location = new System.Drawing.Point(720, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(251, 201);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 149;
            this.pictureBox1.TabStop = false;
            // 
            // makeMedicalRecordToolStripMenuItem
            // 
            this.makeMedicalRecordToolStripMenuItem.Name = "makeMedicalRecordToolStripMenuItem";
            this.makeMedicalRecordToolStripMenuItem.Size = new System.Drawing.Size(236, 26);
            this.makeMedicalRecordToolStripMenuItem.Text = "Make Medical Record";
            this.makeMedicalRecordToolStripMenuItem.Click += new System.EventHandler(this.makeMedicalRecordToolStripMenuItem_Click);
            // 
            // FrmPatientsInspection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1591, 807);
            this.Controls.Add(this.dgvListPatientInspection);
            this.Controls.Add(this.txtFindBy);
            this.Controls.Add(this.cbFindBy);
            this.Controls.Add(this.lblFindBy);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblRecordsCount);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmPatientsInspection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Patients Inspection";
            this.Load += new System.EventHandler(this.FrmPatientsInspection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListPatientInspection)).EndInit();
            this.cmsPatientInspection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvListPatientInspection;
        private System.Windows.Forms.ContextMenuStrip cmsPatientInspection;
        private System.Windows.Forms.ToolStripMenuItem showDoctorDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem CancelToolStripMenuItem1;
        private System.Windows.Forms.TextBox txtFindBy;
        private System.Windows.Forms.ComboBox cbFindBy;
        private System.Windows.Forms.Label lblFindBy;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblRecordsCount;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem InspectionToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem makeMedicalRecordToolStripMenuItem;
    }
}