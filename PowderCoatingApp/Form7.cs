// Name: Eduard Povazhnyi
//Class: H48W35-HNDCOMSD-F242A-L
//Project description: App for Powder Coatings Services
//Date: 05/2025
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PowderCoatingApp
{
    public partial class AddOrderForm : Form
    {
        private int loggedInUserId;
        private string userRole;// Сustomer, Service Manager, Chief Admin
        private List<string> photoPathsBefore = new List<string>(); // Relative paths "Before" photos
        private List<string> photoPathsAfter = new List<string>();  // "After" photos. Not used in this form - this list will be needed in another form, Form6
        private readonly string connectionString = "server=localhost;user=root;password=;database=powdercoatingdb;";


        public AddOrderForm(int userId, string role)
        {
            InitializeComponent();
            loggedInUserId = userId;
            userRole = role;
            LoadUserName(); // Show user name in lblWelcome
        }

        private void LoadUserName()
        {
            using (var conn = new MySqlConnection("server=localhost;user=root;password=;database=powdercoatingdb;"))
            {
                string query = "SELECT name FROM users WHERE userID = @userID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userID", loggedInUserId);
                conn.Open();
                var name = cmd.ExecuteScalar();
                if (name != null)
                    lblWelcome.Text = "Welcome, " + name.ToString() + "!";
            }
        }


        private void AddOrderForm_Load(object sender, EventArgs e)
        {
            // Use the image from Resources
            this.BackgroundImage = Properties.Resources.BackGroundImage2;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            timerDateTime.Start();

            // Fill delivery combo box
            cmbDelivery.Items.AddRange(new string[] { "pickup", "delivery", "pickup/delivery", "no" });
            cmbDelivery.SelectedItem = "no";

            // all standard payment options
            cmbPaymentMethod.Items.Clear();
            cmbPaymentMethod.Items.AddRange(new string[] { "Cash", "Credit Card", "Bank Transfer", "PayPal", "Other" });

            if (userRole == "customer")
            {
                
                cmbCustomer.Visible = false;
                lblCustomer.Visible = false;// the drop-down list is not displayed — but the customerId is still needed
                // (Customer ID will be taken from loggedInUserId)
            }
            else
            {
                LoadCustomers();
            }



            // Hide Back button for admin roles
            if (userRole == "ChiefAdmin" || userRole == "ServiceManager")
            {
                btnBackToHome.Visible = false;
            }
        }

        private void LoadCustomers()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT c.customerID, u.name FROM customers c JOIN users u ON c.userID = u.userID";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbCustomer.Items.Add(new ComboBoxItem(reader.GetString("name"), reader.GetInt32("customerID")));
                    }
                }
            }
        }

        
        // method for saving Orders to the database
        private int SaveOrderToDatabase()
        {
            int newOrderId = -1;

            using (var conn = new MySqlConnection("server=localhost;user=root;password=;database=powdercoatingdb;"))
            {
                conn.Open();

                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Get selected customer ID
                        int customerId = (userRole == "customer")
                            ? loggedInUserId
                            : ((ComboBoxItem)cmbCustomer.SelectedItem).Value;

                        string productType = txtProductType.Text;
                        string color = txtColor.Text;
                        string specifications = txtSpecifications.Text;
                        string address = txtAddress.Text;
                        string delivery = cmbDelivery.SelectedItem?.ToString() ?? "no";
                        // Get the payment method from the combobox
                        string paymentMethod = cmbPaymentMethod.SelectedItem?.ToString();
                        if (paymentMethod == "Other" && string.IsNullOrWhiteSpace(txtCustomPaymentMethod.Text))
                        {
                            MessageBox.Show("Please specify the custom payment method.");
                            return -1;
                        }

                        // Update paymentInfo in the customers table
                        string updateCustomerPaymentQuery = @"UPDATE customers SET paymentInfo = @paymentInfo WHERE customerID = @customerId";
                        MySqlCommand updatePaymentCmd = new MySqlCommand(updateCustomerPaymentQuery, conn, transaction);
                        updatePaymentCmd.Parameters.AddWithValue("@paymentInfo", paymentMethod);
                        updatePaymentCmd.Parameters.AddWithValue("@customerId", customerId);
                        updatePaymentCmd.ExecuteNonQuery();


                        // Insert order
                        string insertOrderQuery = @"INSERT INTO orders (customerId, productType, color, specifications, address, delivery, createdBy)
                                            VALUES (@customerId, @productType, @color, @specifications, @address, @delivery, @createdBy);
                                            SELECT LAST_INSERT_ID();";

                        MySqlCommand cmd = new MySqlCommand(insertOrderQuery, conn, transaction);
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        cmd.Parameters.AddWithValue("@productType", productType);
                        cmd.Parameters.AddWithValue("@color", color);
                        cmd.Parameters.AddWithValue("@specifications", specifications);
                        cmd.Parameters.AddWithValue("@address", address);
                        cmd.Parameters.AddWithValue("@delivery", delivery);
                        cmd.Parameters.AddWithValue("@createdBy", loggedInUserId);
                        // cmd.Parameters.AddWithValue("@createdBy", loggedInUserId); 
                        // If store paymentMethod for each order, need to add a field to the database and use this parameter.

                        // Get the new order ID
                        newOrderId = Convert.ToInt32(cmd.ExecuteScalar());

                        // Save Before photos to DB
                        string photosDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Photos", "Before", $"Order_{newOrderId}");

                        if (!Directory.Exists(photosDir))
                            Directory.CreateDirectory(photosDir);

                        // Determine which photo is Main
                        int mainBeforePhotoIndex = -1;
                        if (chkMain1.Checked) mainBeforePhotoIndex = 0;
                        else if (chkMain2.Checked) mainBeforePhotoIndex = 1;
                        else if (chkMain3.Checked) mainBeforePhotoIndex = 2;
                        else if (chkMain4.Checked) mainBeforePhotoIndex = 3;
                        else if (chkMain5.Checked) mainBeforePhotoIndex = 4;


                        // Copy each photo to a subfolder and save the path
                        for (int i = 0; i < photoPathsBefore.Count; i++)
                        {
                            string originalPath = photoPathsBefore[i];
                            string fileName = Path.GetFileNameWithoutExtension(originalPath);
                            string extension = Path.GetExtension(originalPath);
                            //string relativePath = Path.Combine("Photos", "Before", fileName);

                            
                            // Avoiding file name conflicts (adding a timestamp)
                            string uniqueFileName = $"{fileName}_{DateTime.Now.Ticks}{extension}";
                            string destinationPath = Path.Combine(photosDir, uniqueFileName);
                            File.Copy(originalPath, destinationPath, true); // overwrite = true

                            // Relative path to save to database
                            string relativePath = Path.Combine("Photos", "Before", $"Order_{newOrderId}", uniqueFileName);

                            bool isMain = (i == mainBeforePhotoIndex);

                            string insertPhotoQuery = @"INSERT INTO photos (orderId, imagePath, photoType, isMain)
                                                        VALUES (@orderId, @path, 'before', @isMain)";
                            MySqlCommand photoCmd = new MySqlCommand(insertPhotoQuery, conn, transaction);
                            photoCmd.Parameters.AddWithValue("@orderId", newOrderId);
                            photoCmd.Parameters.AddWithValue("@path", relativePath);
                            photoCmd.Parameters.AddWithValue("@isMain", isMain);
                            photoCmd.ExecuteNonQuery();
                        }

                        transaction.Commit(); // save the transaction
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // rollback on error
                        MessageBox.Show("Error while saving order: " + ex.Message);
                    }
                }
            }

            return newOrderId;
        }

        // This method can be removed
        private bool IsMainPhoto(string filePath)
        {
            string fileName = Path.GetFileName(filePath);

            if (chkMain1.Checked && photoPathsBefore.ElementAtOrDefault(0)?.EndsWith(fileName) == true)
                return true;
            if (chkMain2.Checked && photoPathsBefore.ElementAtOrDefault(1)?.EndsWith(fileName) == true)
                return true;
            if (chkMain3.Checked && photoPathsBefore.ElementAtOrDefault(2)?.EndsWith(fileName) == true)
                return true;
            if (chkMain4.Checked && photoPathsBefore.ElementAtOrDefault(3)?.EndsWith(fileName) == true)
                return true;
            if (chkMain5.Checked && photoPathsBefore.ElementAtOrDefault(4)?.EndsWith(fileName) == true)
                return true;

            return false;
        }


        private void timerDateTime_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private void lblDateTime_Click(object sender, EventArgs e)
        {

        }

        // Back button: Open Customer Dashboard and close this form to avoid multiple windows
        private async void btnBackToHome_Click(object sender, EventArgs e)
        {
            //ServiceManagerDashboardForm serviceManagerDashboardForm = new ServiceManagerDashboardForm(loggedInUserId);
            await Animator.FadeOut(this);
            this.Close(); // Close current AddOrderForm

        }
        // Autofill address when selecting a client
        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (cmbCustomer.SelectedItem == null) return; // check if (cmbCustomer.SelectedItem == null) return to avoid NullReferenceException
                                                        // if the SelectedIndexChanged event fires when clearing the Form or initializing.

            int customerId = ((ComboBoxItem)cmbCustomer.SelectedItem).Value;

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT address, paymentInfo FROM customers WHERE customerID = @customerID", conn);
                cmd.Parameters.AddWithValue("@customerID", customerId);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtAddress.Text = reader["address"].ToString();
                        // display other payment method
                        string methodFromDb = reader["paymentInfo"].ToString();

                        if (!cmbPaymentMethod.Items.Contains(methodFromDb))
                        {
                            cmbPaymentMethod.Items.Add(methodFromDb); // додай, якщо нестандартний варіант
                        }
                        cmbPaymentMethod.SelectedItem = methodFromDb;
                    }
                }
            }
        }
        // to store ID + Name
        public class ComboBoxItem
        {
            public string Text { get; set; }
            public int Value { get; set; }

            public ComboBoxItem(string text, int value)
            {
                Text = text;
                Value = value;
            }

            public override string ToString()
            {
                return Text;
            }
        }


        private void lblCustomer_Click(object sender, EventArgs e)
        {

        }

        private void lblProductType_Click(object sender, EventArgs e)
        {

        }

        private void txtProductType_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblColor_Click(object sender, EventArgs e)
        {

        }

        private void lblAddress_Click(object sender, EventArgs e)
        {

        }

        private void lblSpecifications_Click(object sender, EventArgs e)
        {

        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblDelivery_Click_1(object sender, EventArgs e)
        {

        }

        private void btnSubmitOrder_Click(object sender, EventArgs e)
        {
            // Basic validation
            if (userRole != "customer" && cmbCustomer.SelectedItem == null)
            {
                MessageBox.Show("Please select a customer.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtProductType.Text) || string.IsNullOrWhiteSpace(txtColor.Text))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            if (photoPathsBefore.Count == 0)
            {
                MessageBox.Show("Please add at least one photo (Before).");
                return;
            }

            int orderId = SaveOrderToDatabase();

            if (orderId > 0)
            {
                MessageBox.Show("Order saved successfully! Order ID: " + orderId);
                ClearForm(); // clear Form
            }
        }

        private void ClearForm()
        {
            txtProductType.Clear();
            txtColor.Clear();
            txtSpecifications.Clear();
            txtAddress.Clear();
            cmbDelivery.SelectedIndex = -1;
            cmbPaymentMethod.SelectedIndex = -1;


            // If the role is not customer — reset the selected customer
            // Якщо роль не customer — скинути обраного клієнта
            if (userRole != "customer")
            {
                cmbCustomer.SelectedIndex = -1;
            }

            // Clear photo
            photoPathsBefore.Clear();

            // Clean up images
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox4.Image = null;
            pictureBox5.Image = null;

            // Reset all checkboxes Main
            chkMain1.Checked = false;
            chkMain2.Checked = false;
            chkMain3.Checked = false;
            chkMain4.Checked = false;
            chkMain5.Checked = false;
        }


        private void btnAddPhotos_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //string photosDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Photos", "Before");

                //if (!Directory.Exists(photosDir))
                    //Directory.CreateDirectory(photosDir);

                foreach (string file in ofd.FileNames)
                {
                    if (photoPathsBefore.Count >= 5)
                    {
                        MessageBox.Show("Maximum 5 photos allowed.");
                        break;
                    }
                    try
                    {
                        // Copy the photo to a local folder Копіюємо фото в локальну папку
                        //string fileName = Path.GetFileName(file);
                        // Store the full path to the copied file in a list, and generate a unique name
                        //string uniqueFileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{DateTime.Now.Ticks}{Path.GetExtension(fileName)}";
                        //string destPath = Path.Combine(photosDir, uniqueFileName);

                        //File.Copy(file, destPath, true);

                        // Storing a relative path in a list
                        //string relativePath = Path.Combine("Photos", "Before", fileName);
                        
                        // Store the full path
                        photoPathsBefore.Add(file);

                        // To show photos Показуємо фото
                        DisplayPhoto(file); // pass the full path to the copy тут передаємо повний шлях до копії
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to add photo: " + ex.Message);
                    }

                }
            }
        }

        private void DisplayPhoto(string filePath)
        {
            PictureBox[] boxes = { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5 };

            for (int i = 0; i < boxes.Length; i++)
            {
                if (boxes[i].Image == null)
                {
                    boxes[i].Image = Image.FromFile(filePath);
                    boxes[i].SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                }
            }
        }

        private void cmbDelivery_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblPayment_Click(object sender, EventArgs e)
        {

        }

        private void cmbPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPaymentMethod.SelectedItem?.ToString() == "Other")
            {
                txtCustomPaymentMethod.Visible = true;
                txtCustomPaymentMethod.Focus();
            }
            else
            {
                txtCustomPaymentMethod.Visible = false;
            }
        }

        private void lblPhotos_Click(object sender, EventArgs e)
        {

        }

        private void txtColor_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSpecifications_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }

        private void lblAddOrder_Click(object sender, EventArgs e)
        {

        }

        private void chkMain3_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMain3.Checked)
            {
                if (pictureBox3.Image == null)
                {
                    chkMain3.Checked = false;
                    MessageBox.Show("Cannot set as Main. No photo in this slot.");
                    return;
                }

                chkMain2.Checked = false;
                chkMain1.Checked = false;
                chkMain4.Checked = false;
                chkMain5.Checked = false;
            }
        }
        // only one photo can be the Main
        private void chkMain1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMain1.Checked)
            {
                if (pictureBox1.Image == null)
                {
                    chkMain1.Checked = false;
                    MessageBox.Show("Cannot set as Main. No photo in this slot.");
                    return;
                }

                chkMain2.Checked = false;
                chkMain3.Checked = false;
                chkMain4.Checked = false;
                chkMain5.Checked = false;
            }
        }

        private void chkMain2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMain2.Checked)
            {
                if (pictureBox2.Image == null)
                {
                    chkMain2.Checked = false;
                    MessageBox.Show("Cannot set as Main. No photo in this slot.");
                    return;
                }

                chkMain1.Checked = false;
                chkMain3.Checked = false;
                chkMain4.Checked = false;
                chkMain5.Checked = false;
            }
        }

        private void chkMain4_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMain4.Checked)
            {
                if (pictureBox4.Image == null)
                {
                    chkMain4.Checked = false;
                    MessageBox.Show("Cannot set as Main. No photo in this slot.");
                    return;
                }

                chkMain2.Checked = false;
                chkMain3.Checked = false;
                chkMain1.Checked = false;
                chkMain5.Checked = false;
            }
        }

        private void chkMain5_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMain5.Checked)
            {
                if (pictureBox5.Image == null)
                {
                    chkMain5.Checked = false;
                    MessageBox.Show("Cannot set as Main. No photo in this slot.");
                    return;
                }

                chkMain2.Checked = false;
                chkMain3.Checked = false;
                chkMain4.Checked = false;
                chkMain1.Checked = false;
            }
        }

        private void txtCustomPaymentMethod_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
