namespace PowderCoatingApp
{
    partial class AddOrderForm
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
            this.timerDateTime = new System.Windows.Forms.Timer(this.components);
            this.lblDateTime = new System.Windows.Forms.Label();
            this.btnBackToHome = new System.Windows.Forms.Button();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.lblAppName = new System.Windows.Forms.Label();
            this.logo1 = new System.Windows.Forms.PictureBox();
            this.lblAddOrder = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.lblProductType = new System.Windows.Forms.Label();
            this.txtProductType = new System.Windows.Forms.TextBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.lblSpecifications = new System.Windows.Forms.Label();
            this.txtSpecifications = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblDelivery = new System.Windows.Forms.Label();
            this.cmbDelivery = new System.Windows.Forms.ComboBox();
            this.lblPhotos = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.btnAddPhotos = new System.Windows.Forms.Button();
            this.lblPayment = new System.Windows.Forms.Label();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.btnSubmitOrder = new System.Windows.Forms.Button();
            this.chkMain1 = new System.Windows.Forms.CheckBox();
            this.chkMain2 = new System.Windows.Forms.CheckBox();
            this.chkMain3 = new System.Windows.Forms.CheckBox();
            this.chkMain4 = new System.Windows.Forms.CheckBox();
            this.chkMain5 = new System.Windows.Forms.CheckBox();
            this.txtCustomPaymentMethod = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.logo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // timerDateTime
            // 
            this.timerDateTime.Interval = 1000;
            this.timerDateTime.Tick += new System.EventHandler(this.timerDateTime_Tick);
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
            this.lblDateTime.TabIndex = 18;
            this.lblDateTime.Text = "Date Time";
            this.lblDateTime.Click += new System.EventHandler(this.lblDateTime_Click);
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
            this.btnBackToHome.Location = new System.Drawing.Point(169, 536);
            this.btnBackToHome.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBackToHome.Name = "btnBackToHome";
            this.btnBackToHome.Size = new System.Drawing.Size(263, 74);
            this.btnBackToHome.TabIndex = 19;
            this.btnBackToHome.Text = "Back";
            this.btnBackToHome.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBackToHome.UseVisualStyleBackColor = true;
            this.btnBackToHome.Click += new System.EventHandler(this.btnBackToHome_Click);
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
            this.lblCompanyName.TabIndex = 20;
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
            this.lblAppName.TabIndex = 21;
            this.lblAppName.Text = "Powder Coating Service App";
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
            this.logo1.TabIndex = 22;
            this.logo1.TabStop = false;
            // 
            // lblAddOrder
            // 
            this.lblAddOrder.AutoSize = true;
            this.lblAddOrder.BackColor = System.Drawing.Color.Transparent;
            this.lblAddOrder.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddOrder.ForeColor = System.Drawing.Color.White;
            this.lblAddOrder.Location = new System.Drawing.Point(233, 78);
            this.lblAddOrder.Name = "lblAddOrder";
            this.lblAddOrder.Size = new System.Drawing.Size(179, 31);
            this.lblAddOrder.TabIndex = 23;
            this.lblAddOrder.Text = "Add New Order";
            this.lblAddOrder.Click += new System.EventHandler(this.lblAddOrder_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.BackColor = System.Drawing.Color.Transparent;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.White;
            this.lblWelcome.Location = new System.Drawing.Point(234, 109);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(240, 28);
            this.lblWelcome.TabIndex = 24;
            this.lblWelcome.Text = "Welcome, [UserName]!";
            this.lblWelcome.Click += new System.EventHandler(this.lblWelcome_Click);
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(239, 168);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(263, 36);
            this.cmbCustomer.TabIndex = 25;
            this.cmbCustomer.SelectedIndexChanged += new System.EventHandler(this.cmbCustomer_SelectedIndexChanged);
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.BackColor = System.Drawing.Color.Transparent;
            this.lblCustomer.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomer.ForeColor = System.Drawing.Color.White;
            this.lblCustomer.Location = new System.Drawing.Point(234, 137);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(106, 28);
            this.lblCustomer.TabIndex = 26;
            this.lblCustomer.Text = "Customer:\n";
            this.lblCustomer.Click += new System.EventHandler(this.lblCustomer_Click);
            // 
            // lblProductType
            // 
            this.lblProductType.AutoSize = true;
            this.lblProductType.BackColor = System.Drawing.Color.Transparent;
            this.lblProductType.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductType.ForeColor = System.Drawing.Color.White;
            this.lblProductType.Location = new System.Drawing.Point(12, 207);
            this.lblProductType.Name = "lblProductType";
            this.lblProductType.Size = new System.Drawing.Size(137, 28);
            this.lblProductType.TabIndex = 27;
            this.lblProductType.Text = "Product Type:";
            this.lblProductType.Click += new System.EventHandler(this.lblProductType_Click);
            // 
            // txtProductType
            // 
            this.txtProductType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductType.Location = new System.Drawing.Point(12, 238);
            this.txtProductType.Name = "txtProductType";
            this.txtProductType.Size = new System.Drawing.Size(263, 34);
            this.txtProductType.TabIndex = 28;
            this.txtProductType.TextChanged += new System.EventHandler(this.txtProductType_TextChanged);
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.BackColor = System.Drawing.Color.Transparent;
            this.lblColor.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColor.ForeColor = System.Drawing.Color.White;
            this.lblColor.Location = new System.Drawing.Point(12, 275);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(66, 28);
            this.lblColor.TabIndex = 29;
            this.lblColor.Text = "Color:";
            this.lblColor.Click += new System.EventHandler(this.lblColor_Click);
            // 
            // txtColor
            // 
            this.txtColor.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtColor.Location = new System.Drawing.Point(12, 304);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(263, 34);
            this.txtColor.TabIndex = 30;
            this.txtColor.TextChanged += new System.EventHandler(this.txtColor_TextChanged);
            // 
            // lblSpecifications
            // 
            this.lblSpecifications.AutoSize = true;
            this.lblSpecifications.BackColor = System.Drawing.Color.Transparent;
            this.lblSpecifications.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpecifications.ForeColor = System.Drawing.Color.White;
            this.lblSpecifications.Location = new System.Drawing.Point(12, 341);
            this.lblSpecifications.Name = "lblSpecifications";
            this.lblSpecifications.Size = new System.Drawing.Size(223, 28);
            this.lblSpecifications.TabIndex = 31;
            this.lblSpecifications.Text = "Additional information:";
            this.lblSpecifications.Click += new System.EventHandler(this.lblSpecifications_Click);
            // 
            // txtSpecifications
            // 
            this.txtSpecifications.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSpecifications.Location = new System.Drawing.Point(12, 372);
            this.txtSpecifications.Multiline = true;
            this.txtSpecifications.Name = "txtSpecifications";
            this.txtSpecifications.Size = new System.Drawing.Size(263, 147);
            this.txtSpecifications.TabIndex = 32;
            this.txtSpecifications.TextChanged += new System.EventHandler(this.txtSpecifications_TextChanged);
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.BackColor = System.Drawing.Color.Transparent;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.ForeColor = System.Drawing.Color.White;
            this.lblAddress.Location = new System.Drawing.Point(690, 207);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(90, 28);
            this.lblAddress.TabIndex = 33;
            this.lblAddress.Text = "Address:";
            this.lblAddress.Click += new System.EventHandler(this.lblAddress_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(695, 238);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(263, 34);
            this.txtAddress.TabIndex = 34;
            this.txtAddress.TextChanged += new System.EventHandler(this.txtAddress_TextChanged);
            // 
            // lblDelivery
            // 
            this.lblDelivery.AutoSize = true;
            this.lblDelivery.BackColor = System.Drawing.Color.Transparent;
            this.lblDelivery.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDelivery.ForeColor = System.Drawing.Color.White;
            this.lblDelivery.Location = new System.Drawing.Point(690, 275);
            this.lblDelivery.Name = "lblDelivery";
            this.lblDelivery.Size = new System.Drawing.Size(169, 28);
            this.lblDelivery.TabIndex = 35;
            this.lblDelivery.Text = "Delivery Method:";
            this.lblDelivery.Click += new System.EventHandler(this.lblDelivery_Click_1);
            // 
            // cmbDelivery
            // 
            this.cmbDelivery.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDelivery.FormattingEnabled = true;
            this.cmbDelivery.Location = new System.Drawing.Point(695, 302);
            this.cmbDelivery.Name = "cmbDelivery";
            this.cmbDelivery.Size = new System.Drawing.Size(263, 36);
            this.cmbDelivery.TabIndex = 36;
            this.cmbDelivery.SelectedIndexChanged += new System.EventHandler(this.cmbDelivery_SelectedIndexChanged);
            // 
            // lblPhotos
            // 
            this.lblPhotos.AutoSize = true;
            this.lblPhotos.BackColor = System.Drawing.Color.Transparent;
            this.lblPhotos.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhotos.ForeColor = System.Drawing.Color.White;
            this.lblPhotos.Location = new System.Drawing.Point(280, 207);
            this.lblPhotos.Name = "lblPhotos";
            this.lblPhotos.Size = new System.Drawing.Size(152, 28);
            this.lblPhotos.TabIndex = 37;
            this.lblPhotos.Text = "Upload Photos:";
            this.lblPhotos.Click += new System.EventHandler(this.lblPhotos_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(285, 238);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 121);
            this.pictureBox1.TabIndex = 38;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(419, 238);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(128, 121);
            this.pictureBox2.TabIndex = 39;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(553, 238);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(128, 121);
            this.pictureBox3.TabIndex = 40;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(285, 382);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(128, 121);
            this.pictureBox4.TabIndex = 41;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // pictureBox5
            // 
            this.pictureBox5.Location = new System.Drawing.Point(419, 382);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(128, 121);
            this.pictureBox5.TabIndex = 42;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Click += new System.EventHandler(this.pictureBox5_Click);
            // 
            // btnAddPhotos
            // 
            this.btnAddPhotos.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.btnAddPhotos.FlatAppearance.BorderSize = 5;
            this.btnAddPhotos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.btnAddPhotos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnAddPhotos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPhotos.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPhotos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnAddPhotos.Location = new System.Drawing.Point(553, 388);
            this.btnAddPhotos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAddPhotos.Name = "btnAddPhotos";
            this.btnAddPhotos.Size = new System.Drawing.Size(128, 131);
            this.btnAddPhotos.TabIndex = 43;
            this.btnAddPhotos.Text = "Add Photo";
            this.btnAddPhotos.UseVisualStyleBackColor = true;
            this.btnAddPhotos.Click += new System.EventHandler(this.btnAddPhotos_Click);
            // 
            // lblPayment
            // 
            this.lblPayment.AutoSize = true;
            this.lblPayment.BackColor = System.Drawing.Color.Transparent;
            this.lblPayment.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayment.ForeColor = System.Drawing.Color.White;
            this.lblPayment.Location = new System.Drawing.Point(690, 341);
            this.lblPayment.Name = "lblPayment";
            this.lblPayment.Size = new System.Drawing.Size(174, 28);
            this.lblPayment.TabIndex = 44;
            this.lblPayment.Text = "Payment Method:";
            this.lblPayment.Click += new System.EventHandler(this.lblPayment_Click);
            // 
            // cmbPaymentMethod
            // 
            this.cmbPaymentMethod.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPaymentMethod.FormattingEnabled = true;
            this.cmbPaymentMethod.Location = new System.Drawing.Point(695, 372);
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.Size = new System.Drawing.Size(263, 36);
            this.cmbPaymentMethod.TabIndex = 45;
            this.cmbPaymentMethod.SelectedIndexChanged += new System.EventHandler(this.cmbPaymentMethod_SelectedIndexChanged);
            // 
            // btnSubmitOrder
            // 
            this.btnSubmitOrder.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.btnSubmitOrder.FlatAppearance.BorderSize = 5;
            this.btnSubmitOrder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.btnSubmitOrder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSubmitOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmitOrder.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmitOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnSubmitOrder.Location = new System.Drawing.Point(695, 445);
            this.btnSubmitOrder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSubmitOrder.Name = "btnSubmitOrder";
            this.btnSubmitOrder.Size = new System.Drawing.Size(263, 74);
            this.btnSubmitOrder.TabIndex = 46;
            this.btnSubmitOrder.Text = "Add Order";
            this.btnSubmitOrder.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSubmitOrder.UseVisualStyleBackColor = true;
            this.btnSubmitOrder.Click += new System.EventHandler(this.btnSubmitOrder_Click);
            // 
            // chkMain1
            // 
            this.chkMain1.AutoSize = true;
            this.chkMain1.BackColor = System.Drawing.Color.Transparent;
            this.chkMain1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMain1.ForeColor = System.Drawing.Color.White;
            this.chkMain1.Location = new System.Drawing.Point(285, 361);
            this.chkMain1.Name = "chkMain1";
            this.chkMain1.Size = new System.Drawing.Size(59, 21);
            this.chkMain1.TabIndex = 47;
            this.chkMain1.Text = "Main";
            this.chkMain1.UseVisualStyleBackColor = false;
            this.chkMain1.CheckedChanged += new System.EventHandler(this.chkMain1_CheckedChanged);
            // 
            // chkMain2
            // 
            this.chkMain2.AutoSize = true;
            this.chkMain2.BackColor = System.Drawing.Color.Transparent;
            this.chkMain2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMain2.ForeColor = System.Drawing.Color.White;
            this.chkMain2.Location = new System.Drawing.Point(419, 361);
            this.chkMain2.Name = "chkMain2";
            this.chkMain2.Size = new System.Drawing.Size(59, 21);
            this.chkMain2.TabIndex = 48;
            this.chkMain2.Text = "Main";
            this.chkMain2.UseVisualStyleBackColor = false;
            this.chkMain2.CheckedChanged += new System.EventHandler(this.chkMain2_CheckedChanged);
            // 
            // chkMain3
            // 
            this.chkMain3.AutoSize = true;
            this.chkMain3.BackColor = System.Drawing.Color.Transparent;
            this.chkMain3.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMain3.ForeColor = System.Drawing.Color.White;
            this.chkMain3.Location = new System.Drawing.Point(553, 361);
            this.chkMain3.Name = "chkMain3";
            this.chkMain3.Size = new System.Drawing.Size(59, 21);
            this.chkMain3.TabIndex = 49;
            this.chkMain3.Text = "Main";
            this.chkMain3.UseVisualStyleBackColor = false;
            this.chkMain3.CheckedChanged += new System.EventHandler(this.chkMain3_CheckedChanged);
            // 
            // chkMain4
            // 
            this.chkMain4.AutoSize = true;
            this.chkMain4.BackColor = System.Drawing.Color.Transparent;
            this.chkMain4.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMain4.ForeColor = System.Drawing.Color.White;
            this.chkMain4.Location = new System.Drawing.Point(285, 506);
            this.chkMain4.Name = "chkMain4";
            this.chkMain4.Size = new System.Drawing.Size(59, 21);
            this.chkMain4.TabIndex = 50;
            this.chkMain4.Text = "Main";
            this.chkMain4.UseVisualStyleBackColor = false;
            this.chkMain4.CheckedChanged += new System.EventHandler(this.chkMain4_CheckedChanged);
            // 
            // chkMain5
            // 
            this.chkMain5.AutoSize = true;
            this.chkMain5.BackColor = System.Drawing.Color.Transparent;
            this.chkMain5.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMain5.ForeColor = System.Drawing.Color.White;
            this.chkMain5.Location = new System.Drawing.Point(419, 506);
            this.chkMain5.Name = "chkMain5";
            this.chkMain5.Size = new System.Drawing.Size(59, 21);
            this.chkMain5.TabIndex = 51;
            this.chkMain5.Text = "Main";
            this.chkMain5.UseVisualStyleBackColor = false;
            this.chkMain5.CheckedChanged += new System.EventHandler(this.chkMain5_CheckedChanged);
            // 
            // txtCustomPaymentMethod
            // 
            this.txtCustomPaymentMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomPaymentMethod.Location = new System.Drawing.Point(695, 414);
            this.txtCustomPaymentMethod.Name = "txtCustomPaymentMethod";
            this.txtCustomPaymentMethod.Size = new System.Drawing.Size(263, 28);
            this.txtCustomPaymentMethod.TabIndex = 52;
            this.txtCustomPaymentMethod.Visible = false;
            this.txtCustomPaymentMethod.TextChanged += new System.EventHandler(this.txtCustomPaymentMethod_TextChanged);
            // 
            // AddOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PowderCoatingApp.Properties.Resources.BackGroundImage2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(981, 618);
            this.Controls.Add(this.txtCustomPaymentMethod);
            this.Controls.Add(this.chkMain5);
            this.Controls.Add(this.chkMain4);
            this.Controls.Add(this.chkMain3);
            this.Controls.Add(this.chkMain2);
            this.Controls.Add(this.chkMain1);
            this.Controls.Add(this.btnSubmitOrder);
            this.Controls.Add(this.cmbPaymentMethod);
            this.Controls.Add(this.lblPayment);
            this.Controls.Add(this.btnAddPhotos);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblPhotos);
            this.Controls.Add(this.cmbDelivery);
            this.Controls.Add(this.lblDelivery);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.txtSpecifications);
            this.Controls.Add(this.lblSpecifications);
            this.Controls.Add(this.txtColor);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.txtProductType);
            this.Controls.Add(this.lblProductType);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.cmbCustomer);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.lblAddOrder);
            this.Controls.Add(this.logo1);
            this.Controls.Add(this.lblAppName);
            this.Controls.Add(this.lblCompanyName);
            this.Controls.Add(this.btnBackToHome);
            this.Controls.Add(this.lblDateTime);
            this.Name = "AddOrderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Order Form Powder Coating Service PCS Povazhna Sereda";
            this.Load += new System.EventHandler(this.AddOrderForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerDateTime;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Button btnBackToHome;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.PictureBox logo1;
        private System.Windows.Forms.Label lblAddOrder;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Label lblProductType;
        private System.Windows.Forms.TextBox txtProductType;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Label lblSpecifications;
        private System.Windows.Forms.TextBox txtSpecifications;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblDelivery;
        private System.Windows.Forms.ComboBox cmbDelivery;
        private System.Windows.Forms.Label lblPhotos;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Button btnAddPhotos;
        private System.Windows.Forms.Label lblPayment;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private System.Windows.Forms.Button btnSubmitOrder;
        private System.Windows.Forms.CheckBox chkMain1;
        private System.Windows.Forms.CheckBox chkMain2;
        private System.Windows.Forms.CheckBox chkMain3;
        private System.Windows.Forms.CheckBox chkMain4;
        private System.Windows.Forms.CheckBox chkMain5;
        private System.Windows.Forms.TextBox txtCustomPaymentMethod;
    }
}