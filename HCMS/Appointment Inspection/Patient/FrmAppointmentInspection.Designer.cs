namespace HCMS.Appointment_Inspection
{
    partial class FrmAppointmentInspection
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
            this.dgvListAppointments = new System.Windows.Forms.DataGridView();
            this.cmsAppointments = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showDoctorDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.CancelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRecordsCount = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtFindBy = new System.Windows.Forms.TextBox();
            this.cbFindBy = new System.Windows.Forms.ComboBox();
            this.lblFindBy = new System.Windows.Forms.Label();
            this.btnAddNewInspectionAppoinment = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListAppointments)).BeginInit();
            this.cmsAppointments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvListAppointments
            // 
            this.dgvListAppointments.AllowUserToAddRows = false;
            this.dgvListAppointments.AllowUserToDeleteRows = false;
            this.dgvListAppointments.BackgroundColor = System.Drawing.Color.White;
            this.dgvListAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListAppointments.ContextMenuStrip = this.cmsAppointments;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvListAppointments.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvListAppointments.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvListAppointments.GridColor = System.Drawing.Color.White;
            this.dgvListAppointments.Location = new System.Drawing.Point(32, 340);
            this.dgvListAppointments.MultiSelect = false;
            this.dgvListAppointments.Name = "dgvListAppointments";
            this.dgvListAppointments.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvListAppointments.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvListAppointments.RowHeadersWidth = 51;
            this.dgvListAppointments.RowTemplate.Height = 24;
            this.dgvListAppointments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListAppointments.Size = new System.Drawing.Size(1499, 395);
            this.dgvListAppointments.TabIndex = 137;
            this.dgvListAppointments.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListAppointments_CellDoubleClick);
            // 
            // cmsAppointments
            // 
            this.cmsAppointments.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsAppointments.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDoctorDetailsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.CancelToolStripMenuItem1,
            this.DeleteToolStripMenuItem2});
            this.cmsAppointments.Name = "cmsListPeople";
            this.cmsAppointments.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.cmsAppointments.Size = new System.Drawing.Size(231, 124);
            // 
            // showDoctorDetailsToolStripMenuItem
            // 
            this.showDoctorDetailsToolStripMenuItem.Image = global::HCMS.Properties.Resources.PersonDetails_32;
            this.showDoctorDetailsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showDoctorDetailsToolStripMenuItem.Name = "showDoctorDetailsToolStripMenuItem";
            this.showDoctorDetailsToolStripMenuItem.Size = new System.Drawing.Size(230, 38);
            this.showDoctorDetailsToolStripMenuItem.Text = "&Show Doctor Details";
            this.showDoctorDetailsToolStripMenuItem.Click += new System.EventHandler(this.showDoctorDetailsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(227, 6);
            // 
            // CancelToolStripMenuItem1
            // 
            this.CancelToolStripMenuItem1.Image = global::HCMS.Properties.Resources.Delete_32___Copy;
            this.CancelToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.CancelToolStripMenuItem1.Name = "CancelToolStripMenuItem1";
            this.CancelToolStripMenuItem1.Size = new System.Drawing.Size(230, 38);
            this.CancelToolStripMenuItem1.Text = "&Cancel";
            this.CancelToolStripMenuItem1.Click += new System.EventHandler(this.CancelToolStripMenuItem1_Click);
            // 
            // DeleteToolStripMenuItem2
            // 
            this.DeleteToolStripMenuItem2.BackColor = System.Drawing.SystemColors.Control;
            this.DeleteToolStripMenuItem2.Image = global::HCMS.Properties.Resources.Delete_32_2;
            this.DeleteToolStripMenuItem2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.DeleteToolStripMenuItem2.Name = "DeleteToolStripMenuItem2";
            this.DeleteToolStripMenuItem2.Size = new System.Drawing.Size(230, 38);
            this.DeleteToolStripMenuItem2.Text = "&Delete";
            this.DeleteToolStripMenuItem2.Click += new System.EventHandler(this.DeleteToolStripMenuItem2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(623, 223);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(459, 46);
            this.label1.TabIndex = 138;
            this.label1.Text = "Appointment Inspection";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRecordsCount
            // 
            this.lblRecordsCount.AutoSize = true;
            this.lblRecordsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsCount.Location = new System.Drawing.Point(137, 754);
            this.lblRecordsCount.Name = "lblRecordsCount";
            this.lblRecordsCount.Size = new System.Drawing.Size(74, 25);
            this.lblRecordsCount.TabIndex = 142;
            this.lblRecordsCount.Text = "[????]";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(27, 754);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(104, 25);
            this.label22.TabIndex = 141;
            this.label22.Text = "Records: ";
            // 
            // txtFindBy
            // 
            this.txtFindBy.BackColor = System.Drawing.Color.White;
            this.txtFindBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFindBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFindBy.Location = new System.Drawing.Point(379, 289);
            this.txtFindBy.Name = "txtFindBy";
            this.txtFindBy.Size = new System.Drawing.Size(208, 30);
            this.txtFindBy.TabIndex = 146;
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
            "Doctor ID",
            "Doctor Name",
            "Specialization",
            "Clinic Address",
            "Appointment Status"});
            this.cbFindBy.Location = new System.Drawing.Point(128, 287);
            this.cbFindBy.Name = "cbFindBy";
            this.cbFindBy.Size = new System.Drawing.Size(230, 33);
            this.cbFindBy.TabIndex = 145;
            this.cbFindBy.SelectedIndexChanged += new System.EventHandler(this.cbFindBy_SelectedIndexChanged);
            // 
            // lblFindBy
            // 
            this.lblFindBy.AutoSize = true;
            this.lblFindBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFindBy.Location = new System.Drawing.Point(27, 290);
            this.lblFindBy.Name = "lblFindBy";
            this.lblFindBy.Size = new System.Drawing.Size(92, 25);
            this.lblFindBy.TabIndex = 144;
            this.lblFindBy.Text = "Find By:";
            // 
            // btnAddNewInspectionAppoinment
            // 
            this.btnAddNewInspectionAppoinment.BackColor = System.Drawing.Color.White;
            this.btnAddNewInspectionAppoinment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddNewInspectionAppoinment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddNewInspectionAppoinment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewInspectionAppoinment.Image = global::HCMS.Properties.Resources.AddAppointment_32;
            this.btnAddNewInspectionAppoinment.Location = new System.Drawing.Point(1450, 253);
            this.btnAddNewInspectionAppoinment.Name = "btnAddNewInspectionAppoinment";
            this.btnAddNewInspectionAppoinment.Size = new System.Drawing.Size(81, 66);
            this.btnAddNewInspectionAppoinment.TabIndex = 140;
            this.btnAddNewInspectionAppoinment.UseVisualStyleBackColor = false;
            this.btnAddNewInspectionAppoinment.Click += new System.EventHandler(this.btnAddNewInspectionAppoinment_Click);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::HCMS.Properties.Resources.Close_32___Copy;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1386, 754);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(145, 44);
            this.btnClose.TabIndex = 143;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::HCMS.Properties.Resources.Inspection_Appointment;
            this.pictureBox1.Location = new System.Drawing.Point(711, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(251, 201);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 139;
            this.pictureBox1.TabStop = false;
            // 
            // FrmAppointmentInspection
            // 
            this.AcceptButton = this.btnAddNewInspectionAppoinment;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1566, 812);
            this.Controls.Add(this.txtFindBy);
            this.Controls.Add(this.cbFindBy);
            this.Controls.Add(this.lblFindBy);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblRecordsCount);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.btnAddNewInspectionAppoinment);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvListAppointments);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmAppointmentInspection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Appointment Inspection";
            this.Load += new System.EventHandler(this.FrmAppointmentInspection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListAppointments)).EndInit();
            this.cmsAppointments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvListAppointments;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddNewInspectionAppoinment;
        private System.Windows.Forms.Label lblRecordsCount;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtFindBy;
        private System.Windows.Forms.ComboBox cbFindBy;
        private System.Windows.Forms.Label lblFindBy;
        private System.Windows.Forms.ContextMenuStrip cmsAppointments;
        private System.Windows.Forms.ToolStripMenuItem showDoctorDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CancelToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem2;
    }
}