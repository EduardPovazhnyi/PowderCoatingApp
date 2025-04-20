using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
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
    public partial class AddAdminForm : Form
    {
        private byte[] avatarBytes;

        public AddAdminForm()
        {
            InitializeComponent();
            timerDateTime.Start();
        }

        private void timerDateTime_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private void AddAdminForm_Load(object sender, EventArgs e)
        {
            // Use the image from Resources
            this.BackgroundImage = Properties.Resources.BackGroundImage2;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            timerDateTime.Start();
        }

        private async void btnBackToHome_Click(object sender, EventArgs e)
        {
            AdminManagementForm adminManagement = new AdminManagementForm();
            await Animator.FadeOut(this);
            await Animator.FadeIn(adminManagement);            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblEmail_Click(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword) ||
                avatarBytes == null)
            {
                MessageBox.Show("Please fill all fields and select an avatar.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string passwordHash = HashPassword(password);
            string connectionString = "server=localhost;user=root;database=powdercoatingdb;port=3306;password=your_password;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // 1. Insert into users table
                string userQuery = @"INSERT INTO users (name, email, passwordHash, phoneNumber, role, avatar)
                             VALUES (@name, @email, @password, @phone, 'ServiceManager', @avatar)";
                MySqlCommand cmdUser = new MySqlCommand(userQuery, conn);
                cmdUser.Parameters.AddWithValue("@name", name);
                cmdUser.Parameters.AddWithValue("@email", email);
                cmdUser.Parameters.AddWithValue("@password", passwordHash);
                cmdUser.Parameters.AddWithValue("@phone", phone);
                cmdUser.Parameters.AddWithValue("@avatar", avatarBytes);
                cmdUser.ExecuteNonQuery();

                long userId = cmdUser.LastInsertedId;

                // 2. Insert into servicemanagers table
                string managerQuery = "INSERT INTO servicemanagers (employeeNumber, userID) VALUES (@empNum, @userId)";
                MySqlCommand cmdManager = new MySqlCommand(managerQuery, conn);
                cmdManager.Parameters.AddWithValue("@empNum", Guid.NewGuid().ToString().Substring(0, 8));
                cmdManager.Parameters.AddWithValue("@userId", userId);
                cmdManager.ExecuteNonQuery();
            }

            MessageBox.Show("Service Manager registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.png)|*.jpg;*.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                pictureBoxAvatar.Image = new Bitmap(filePath);
                avatarBytes = File.ReadAllBytes(filePath); // Save image to the database
            }
        }

        private void pictureBoxAvatar_Click(object sender, EventArgs e)
        {

        }
    }
}
