namespace HCMS.Appointment_Inspection.Doctors.Controls
{
    partial class clsMedicalRecordPrescriptionsCard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvMedicalRecordPrescriptions = new System.Windows.Forms.DataGridView();
            this.lblRecordsCount = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicalRecordPrescriptions)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMedicalRecordPrescriptions
            // 
            this.dgvMedicalRecordPrescriptions.AllowUserToAddRows = false;
            this.dgvMedicalRecordPrescriptions.AllowUserToDeleteRows = false;
            this.dgvMedicalRecordPrescriptions.BackgroundColor = System.Drawing.Color.White;
            this.dgvMedicalRecordPrescriptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMedicalRecordPrescriptions.GridColor = System.Drawing.Color.Black;
            this.dgvMedicalRecordPrescriptions.Location = new System.Drawing.Point(0, 21);
            this.dgvMedicalRecordPrescriptions.Name = "dgvMedicalRecordPrescriptions";
            this.dgvMedicalRecordPrescriptions.ReadOnly = true;
            this.dgvMedicalRecordPrescriptions.RowHeadersWidth = 51;
            this.dgvMedicalRecordPrescriptions.RowTemplate.Height = 24;
            this.dgvMedicalRecordPrescriptions.Size = new System.Drawing.Size(1352, 340);
            this.dgvMedicalRecordPrescriptions.TabIndex = 0;
            // 
            // lblRecordsCount
            // 
            this.lblRecordsCount.AutoSize = true;
            this.lblRecordsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsCount.Location = new System.Drawing.Point(113, 374);
            this.lblRecordsCount.Name = "lblRecordsCount";
            this.lblRecordsCount.Size = new System.Drawing.Size(74, 25);
            this.lblRecordsCount.TabIndex = 154;
            this.lblRecordsCount.Text = "[????]";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(3, 374);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(104, 25);
            this.label22.TabIndex = 153;
            this.label22.Text = "Records: ";
            // 
            // clsMedicalRecordPrescriptionsCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblRecordsCount);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.dgvMedicalRecordPrescriptions);
            this.Name = "clsMedicalRecordPrescriptionsCard";
            this.Size = new System.Drawing.Size(1355, 422);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicalRecordPrescriptions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMedicalRecordPrescriptions;
        private System.Windows.Forms.Label lblRecordsCount;
        private System.Windows.Forms.Label label22;
    }
}
