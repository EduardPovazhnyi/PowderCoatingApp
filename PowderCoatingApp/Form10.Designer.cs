namespace PowderCoatingApp
{
    partial class CustomerDashboardForm
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
            this.lblDateTime = new System.Windows.Forms.Label();
            this.timerDateTime = new System.Windows.Forms.Timer(this.components);
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.lblAppName = new System.Windows.Forms.Label();
            this.logo1 = new System.Windows.Forms.PictureBox();
            this.lblCustomerDashboard = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnBackToHome = new System.Windows.Forms.Button();
            this.btnAddOrder = new System.Windows.Forms.Button();
            this.btnEditProfile = new System.Windows.Forms.Button();
            this.btnViewOrders = new System.Windows.Forms.Button();
            this.btnNotifications = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.logo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
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
            this.lblDateTime.TabIndex = 8;
            this.lblDateTime.Text = "Date Time";
            // 
            // timerDateTime
            // 
            this.timerDateTime.Interval = 1000;
            this.timerDateTime.Tick += new System.EventHandler(this.timerDateTime_Tick);
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
            this.lblCompanyName.TabIndex = 9;
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
            this.lblAppName.TabIndex = 10;
            this.lblAppName.Text = "Powder Coating Service App";
            // 
            // logo1
            // 
            this.logo1.BackColor = System.Drawing.Color.Transparent;
            this.logo1.Image = global::PowderCoatingApp.Properties.Resources.Logo2;
            this.logo1.Location = new System.Drawing.Point(12, 12);
            this.logo1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.logo1.Name = "logo1";
            this.logo1.Size = new System.Drawing.Size(187, 174);
            this.logo1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logo1.TabIndex = 11;
            this.logo1.TabStop = false;
            // 
            // lblCustomerDashboard
            // 
            this.lblCustomerDashboard.AutoSize = true;
            this.lblCustomerDashboard.BackColor = System.Drawing.Color.Transparent;
            this.lblCustomerDashboard.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerDashboard.ForeColor = System.Drawing.Color.White;
            this.lblCustomerDashboard.Location = new System.Drawing.Point(233, 78);
            this.lblCustomerDashboard.Name = "lblCustomerDashboard";
            this.lblCustomerDashboard.Size = new System.Drawing.Size(239, 31);
            this.lblCustomerDashboard.TabIndex = 19;
            this.lblCustomerDashboard.Text = "Customer Dashboard";
            this.lblCustomerDashboard.Click += new System.EventHandler(this.lblCustomerDashboard_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.BackColor = System.Drawing.Color.Transparent;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.White;
            this.lblWelcome.Location = new System.Drawing.Point(234, 109);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(291, 28);
            this.lblWelcome.TabIndex = 20;
            this.lblWelcome.Text = "Welcome, [CustomerName]!";
            this.lblWelcome.Click += new System.EventHandler(this.lblWelcome_Click);
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
            this.btnBackToHome.Location = new System.Drawing.Point(12, 513);
            this.btnBackToHome.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBackToHome.Name = "btnBackToHome";
            this.btnBackToHome.Size = new System.Drawing.Size(255, 74);
            this.btnBackToHome.TabIndex = 21;
            this.btnBackToHome.Text = "Back";
            this.btnBackToHome.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBackToHome.UseVisualStyleBackColor = true;
            this.btnBackToHome.Click += new System.EventHandler(this.btnBackToHome_Click);
            // 
            // btnAddOrder
            // 
            this.btnAddOrder.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.btnAddOrder.FlatAppearance.BorderSize = 5;
            this.btnAddOrder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.btnAddOrder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnAddOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddOrder.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnAddOrder.Location = new System.Drawing.Point(12, 199);
            this.btnAddOrder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAddOrder.Name = "btnAddOrder";
            this.btnAddOrder.Size = new System.Drawing.Size(255, 74);
            this.btnAddOrder.TabIndex = 22;
            this.btnAddOrder.Text = "Add Order";
            this.btnAddOrder.UseVisualStyleBackColor = true;
            this.btnAddOrder.Click += new System.EventHandler(this.btnAddOrder_Click);
            // 
            // btnEditProfile
            // 
            this.btnEditProfile.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.btnEditProfile.FlatAppearance.BorderSize = 5;
            this.btnEditProfile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.btnEditProfile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnEditProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditProfile.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditProfile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnEditProfile.Location = new System.Drawing.Point(12, 277);
            this.btnEditProfile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEditProfile.Name = "btnEditProfile";
            this.btnEditProfile.Size = new System.Drawing.Size(255, 74);
            this.btnEditProfile.TabIndex = 23;
            this.btnEditProfile.Text = "Edit Profile";
            this.btnEditProfile.UseVisualStyleBackColor = true;
            this.btnEditProfile.Click += new System.EventHandler(this.btnEditProfile_Click);
            // 
            // btnViewOrders
            // 
            this.btnViewOrders.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.btnViewOrders.FlatAppearance.BorderSize = 5;
            this.btnViewOrders.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.btnViewOrders.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnViewOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewOrders.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewOrders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnViewOrders.Location = new System.Drawing.Point(275, 139);
            this.btnViewOrders.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnViewOrders.Name = "btnViewOrders";
            this.btnViewOrders.Size = new System.Drawing.Size(219, 49);
            this.btnViewOrders.TabIndex = 24;
            this.btnViewOrders.Text = "Orders";
            this.btnViewOrders.UseVisualStyleBackColor = true;
            this.btnViewOrders.Click += new System.EventHandler(this.btnViewOrders_Click);
            // 
            // btnNotifications
            // 
            this.btnNotifications.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.btnNotifications.FlatAppearance.BorderSize = 5;
            this.btnNotifications.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.btnNotifications.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnNotifications.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNotifications.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNotifications.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnNotifications.Location = new System.Drawing.Point(510, 139);
            this.btnNotifications.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNotifications.Name = "btnNotifications";
            this.btnNotifications.Size = new System.Drawing.Size(219, 49);
            this.btnNotifications.TabIndex = 27;
            this.btnNotifications.Text = "Notifications";
            this.btnNotifications.UseVisualStyleBackColor = true;
            this.btnNotifications.Click += new System.EventHandler(this.btnNotifications_Click);
            // 
            // dgvData
            // 
            this.dgvData.BackgroundColor = System.Drawing.Color.White;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(275, 199);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersWidth = 51;
            this.dgvData.RowTemplate.Height = 80;
            this.dgvData.Size = new System.Drawing.Size(680, 322);
            this.dgvData.TabIndex = 28;
            // 
            // CustomerDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PowderCoatingApp.Properties.Resources.BackGroundImage2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(981, 618);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.btnNotifications);
            this.Controls.Add(this.btnViewOrders);
            this.Controls.Add(this.btnEditProfile);
            this.Controls.Add(this.btnAddOrder);
            this.Controls.Add(this.btnBackToHome);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.lblCustomerDashboard);
            this.Controls.Add(this.logo1);
            this.Controls.Add(this.lblAppName);
            this.Controls.Add(this.lblCompanyName);
            this.Controls.Add(this.lblDateTime);
            this.Name = "CustomerDashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Dashboard Powder Coating Service PCS Povazhna Sereda";
            this.Load += new System.EventHandler(this.CustomerDashboardForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Timer timerDateTime;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.PictureBox logo1;
        private System.Windows.Forms.Label lblCustomerDashboard;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnBackToHome;
        private System.Windows.Forms.Button btnAddOrder;
        private System.Windows.Forms.Button btnEditProfile;
        private System.Windows.Forms.Button btnViewOrders;
        private System.Windows.Forms.Button btnNotifications;
        private System.Windows.Forms.DataGridView dgvData;
    }
}