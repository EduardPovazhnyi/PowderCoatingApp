namespace PowderCoatingApp
{
    partial class AdminManagementForm
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
            this.logo1 = new System.Windows.Forms.PictureBox();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.lblAppName = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.timerDateTime = new System.Windows.Forms.Timer(this.components);
            this.btnBackToHome = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnAddAdmin = new System.Windows.Forms.Button();
            this.btnServiceDashboard = new System.Windows.Forms.Button();
            this.dgvAdmins = new System.Windows.Forms.DataGridView();
            this.lblWelcome = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdmins)).BeginInit();
            this.SuspendLayout();
            // 
            // logo1
            // 
            this.logo1.BackColor = System.Drawing.Color.Transparent;
            this.logo1.Image = global::PowderCoatingApp.Properties.Resources.Logo2;
            this.logo1.Location = new System.Drawing.Point(12, 11);
            this.logo1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.logo1.Name = "logo1";
            this.logo1.Size = new System.Drawing.Size(187, 174);
            this.logo1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logo1.TabIndex = 7;
            this.logo1.TabStop = false;
            this.logo1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.AutoSize = true;
            this.lblCompanyName.BackColor = System.Drawing.Color.Transparent;
            this.lblCompanyName.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyName.ForeColor = System.Drawing.Color.White;
            this.lblCompanyName.Location = new System.Drawing.Point(232, 11);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(659, 41);
            this.lblCompanyName.TabIndex = 8;
            this.lblCompanyName.Text = "Powder Coating Service PCS Povazhna Sereda";
            // 
            // lblAppName
            // 
            this.lblAppName.AutoSize = true;
            this.lblAppName.BackColor = System.Drawing.Color.Transparent;
            this.lblAppName.ForeColor = System.Drawing.Color.White;
            this.lblAppName.Location = new System.Drawing.Point(272, 50);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(179, 16);
            this.lblAppName.TabIndex = 9;
            this.lblAppName.Text = "Powder Coating Service App";
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.BackColor = System.Drawing.Color.Transparent;
            this.lblDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTime.ForeColor = System.Drawing.Color.Firebrick;
            this.lblDateTime.Location = new System.Drawing.Point(505, 558);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(134, 29);
            this.lblDateTime.TabIndex = 13;
            this.lblDateTime.Text = "Date Time";
            this.lblDateTime.Click += new System.EventHandler(this.lblDateTime_Click);
            // 
            // timerDateTime
            // 
            this.timerDateTime.Interval = 1000;
            this.timerDateTime.Tick += new System.EventHandler(this.timerDateTime_Tick);
            // 
            // btnBackToHome
            // 
            this.btnBackToHome.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.btnBackToHome.FlatAppearance.BorderSize = 5;
            this.btnBackToHome.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.btnBackToHome.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnBackToHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackToHome.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackToHome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnBackToHome.Location = new System.Drawing.Point(171, 515);
            this.btnBackToHome.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBackToHome.Name = "btnBackToHome";
            this.btnBackToHome.Size = new System.Drawing.Size(255, 74);
            this.btnBackToHome.TabIndex = 14;
            this.btnBackToHome.Text = "Back";
            this.btnBackToHome.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBackToHome.UseVisualStyleBackColor = true;
            this.btnBackToHome.Click += new System.EventHandler(this.btnBackToHome_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(232, 81);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(367, 38);
            this.lblTitle.TabIndex = 15;
            this.lblTitle.Text = "Admin Management Panel";
            // 
            // btnAddAdmin
            // 
            this.btnAddAdmin.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.btnAddAdmin.FlatAppearance.BorderSize = 5;
            this.btnAddAdmin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.btnAddAdmin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnAddAdmin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddAdmin.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAdmin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnAddAdmin.Location = new System.Drawing.Point(12, 189);
            this.btnAddAdmin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAddAdmin.Name = "btnAddAdmin";
            this.btnAddAdmin.Size = new System.Drawing.Size(271, 74);
            this.btnAddAdmin.TabIndex = 16;
            this.btnAddAdmin.Text = "Add Manager";
            this.btnAddAdmin.UseVisualStyleBackColor = true;
            this.btnAddAdmin.Click += new System.EventHandler(this.btnAddAdmin_Click);
            // 
            // btnServiceDashboard
            // 
            this.btnServiceDashboard.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.btnServiceDashboard.FlatAppearance.BorderSize = 5;
            this.btnServiceDashboard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.btnServiceDashboard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnServiceDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnServiceDashboard.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnServiceDashboard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnServiceDashboard.Location = new System.Drawing.Point(12, 277);
            this.btnServiceDashboard.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnServiceDashboard.Name = "btnServiceDashboard";
            this.btnServiceDashboard.Size = new System.Drawing.Size(271, 88);
            this.btnServiceDashboard.TabIndex = 19;
            this.btnServiceDashboard.Text = "Service Manager \r\nDashboard";
            this.btnServiceDashboard.UseVisualStyleBackColor = true;
            this.btnServiceDashboard.Click += new System.EventHandler(this.btnServiceDashboard_Click);
            // 
            // dgvAdmins
            // 
            this.dgvAdmins.BackgroundColor = System.Drawing.Color.White;
            this.dgvAdmins.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdmins.Location = new System.Drawing.Point(289, 189);
            this.dgvAdmins.Name = "dgvAdmins";
            this.dgvAdmins.RowHeadersWidth = 51;
            this.dgvAdmins.RowTemplate.Height = 80;
            this.dgvAdmins.Size = new System.Drawing.Size(680, 322);
            this.dgvAdmins.TabIndex = 20;
            this.dgvAdmins.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAdmins_CellContentClick);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.BackColor = System.Drawing.Color.Transparent;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.White;
            this.lblWelcome.Location = new System.Drawing.Point(234, 133);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(283, 28);
            this.lblWelcome.TabIndex = 21;
            this.lblWelcome.Text = "Welcome, [ManagerName]!";
            this.lblWelcome.Click += new System.EventHandler(this.lblWelcome_Click);
            // 
            // AdminManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::PowderCoatingApp.Properties.Resources.BackGroundImage2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(981, 618);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.dgvAdmins);
            this.Controls.Add(this.btnServiceDashboard);
            this.Controls.Add(this.btnAddAdmin);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnBackToHome);
            this.Controls.Add(this.lblDateTime);
            this.Controls.Add(this.lblAppName);
            this.Controls.Add(this.lblCompanyName);
            this.Controls.Add(this.logo1);
            this.Name = "AdminManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin Management Form Powder Coating Service PCS Povazhna Sereda";
            this.Load += new System.EventHandler(this.AdminManagementForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdmins)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox logo1;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Timer timerDateTime;
        private System.Windows.Forms.Button btnBackToHome;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnAddAdmin;
        private System.Windows.Forms.Button btnServiceDashboard;
        private System.Windows.Forms.DataGridView dgvAdmins;
        private System.Windows.Forms.Label lblWelcome;
    }
}