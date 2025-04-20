using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowderCoatingApp
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
            timerDateTime.Start();
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            // Use the image from Resources
            // Set background
            this.BackgroundImage = Properties.Resources.BackGroundImage2;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            timerDateTime.Start();

            // Add items to Payment Method ComboBox
            cmbPaymentMethod.Items.Add("Cash");
            cmbPaymentMethod.Items.Add("Credit Card");
            cmbPaymentMethod.Items.Add("Bank Transfer");
            cmbPaymentMethod.Items.Add("PayPal");
            cmbPaymentMethod.SelectedIndex = 0;
        }

        private void lblDateTime_Click(object sender, EventArgs e)
        {

        }

        private void timerDateTime_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private async void btnBackToHome_Click(object sender, EventArgs e)
        {
            HomeForm homeForm = new HomeForm();
            await Animator.FadeOut(this);
            await Animator.FadeIn(homeForm);
        }


        // Add the Password Hashing Method
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void cmbPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // 1. Validate input fields
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            // 2. Hash password
            string hashedPassword = HashPassword(txtPassword.Text);

            // 3. Get selected payment method from ComboBox
            string selectedPaymentMethod = cmbPaymentMethod.SelectedItem.ToString();

            // 4. Connect to MySQL
            string connectionString = "server=localhost;user=root;password=your_password;database=PowderCoatingDB;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // 5. Insert into Users table
                    string queryUser = @"INSERT INTO Users (name, email, password, phoneNumber, role) 
                                 VALUES (@name, @email, @passwordHash, @phone, 'Customer')";
                    MySqlCommand cmdUser = new MySqlCommand(queryUser, conn);
                    cmdUser.Parameters.AddWithValue("@name", txtName.Text);
                    cmdUser.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmdUser.Parameters.AddWithValue("@passwordHash", hashedPassword);
                    cmdUser.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cmdUser.ExecuteNonQuery();

                    int userId = (int)cmdUser.LastInsertedId;

                    // 6. Insert into Customers table
                    string queryCustomer = @"INSERT INTO Customers (address, paymentInfo, userID) 
                                     VALUES (@address, @paymentInfo, @userID)";
                    MySqlCommand cmdCustomer = new MySqlCommand(queryCustomer, conn);
                    cmdCustomer.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmdCustomer.Parameters.AddWithValue("@paymentInfo", selectedPaymentMethod);
                    cmdCustomer.Parameters.AddWithValue("@userID", userId);
                    cmdCustomer.ExecuteNonQuery();

                    // 7. Show success and redirect
                    MessageBox.Show("Registration successful! Please log in.");
                    this.Hide();
                    HomeForm home = new HomeForm();
                    home.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error during registration: " + ex.Message);
                }
            }
        }

        private void lblCompanyName_Click(object sender, EventArgs e)
        {

        }

        private void btnUploadAvatar_Click(object sender, EventArgs e)
        {

        }        
    }
}
