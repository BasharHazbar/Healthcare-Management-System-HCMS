namespace HCMS.Appointment_Inspection.Doctors
{
    partial class frmMedicalRecoedDetails
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrlMedicalRecoredCard1 = new HCMS.Appointment_Inspection.Doctors.Controls.ctrlMedicalRecoredCard();
            this.clsMedicalRecordPrescriptionsCard1 = new HCMS.Appointment_Inspection.Doctors.Controls.clsMedicalRecordPrescriptionsCard();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::HCMS.Properties.Resources.doctor_patient_512;
            this.pictureBox1.Location = new System.Drawing.Point(60, 92);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(251, 235);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 150;
            this.pictureBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::HCMS.Properties.Resources.Close_32___Copy;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1218, 787);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(145, 44);
            this.btnClose.TabIndex = 154;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrlMedicalRecoredCard1
            // 
            this.ctrlMedicalRecoredCard1.BackColor = System.Drawing.Color.White;
            this.ctrlMedicalRecoredCard1.Location = new System.Drawing.Point(351, 36);
            this.ctrlMedicalRecoredCard1.Name = "ctrlMedicalRecoredCard1";
            this.ctrlMedicalRecoredCard1.Size = new System.Drawing.Size(1012, 391);
            this.ctrlMedicalRecoredCard1.TabIndex = 1;
            // 
            // clsMedicalRecordPrescriptionsCard1
            // 
            this.clsMedicalRecordPrescriptionsCard1.BackColor = System.Drawing.Color.White;
            this.clsMedicalRecordPrescriptionsCard1.Location = new System.Drawing.Point(12, 417);
            this.clsMedicalRecordPrescriptionsCard1.Name = "clsMedicalRecordPrescriptionsCard1";
            this.clsMedicalRecordPrescriptionsCard1.Size = new System.Drawing.Size(1396, 414);
            this.clsMedicalRecordPrescriptionsCard1.TabIndex = 0;
            // 
            // frmMedicalRecoedDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1427, 899);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ctrlMedicalRecoredCard1);
            this.Controls.Add(this.clsMedicalRecordPrescriptionsCard1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmMedicalRecoedDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Medical Recoed Details";
            this.Load += new System.EventHandler(this.frmMedicalRecoedDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.clsMedicalRecordPrescriptionsCard clsMedicalRecordPrescriptionsCard1;
        private Controls.ctrlMedicalRecoredCard ctrlMedicalRecoredCard1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnClose;
    }
}