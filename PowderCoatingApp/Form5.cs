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
        private int userId;

        private byte[] avatarBytes;

        public AddAdminForm(int userId)
        {
            InitializeComponent();
            this.userId = userId;
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
            AdminManagementForm adminManagement = new AdminManagementForm((int)userId);
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

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim().ToLower(); // That to normalize email
            string phone = txtPhone.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            // Basic validation
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword) ||
                avatarBytes == null)
            {
                MessageBox.Show("Please fill all fields and select an avatar.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Email format check
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") || email.Count(c => c == '@') != 1)
            {
                MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Phone number check - only digits, and length between 8 and 15
            if (!System.Text.RegularExpressions.Regex.IsMatch(phone, @"^\d{8,15}$"))
            {
                MessageBox.Show("Phone number must contain only digits and be between 8 to 15 digits long.", "Invalid Phone", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string passwordHash = HashPassword(password);
            string connectionString = "server=localhost;port=3306;user=root;password=;database=PowderCoatingDB;";
            //string connectionString = "server=localhost;port=3306;user=root;password=qwerty;database=PowderCoatingDB;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if email already exists
                    string checkEmailQuery = "SELECT COUNT(*) FROM users WHERE LOWER(email) = @checkEmail";
                    MySqlCommand checkCmd = new MySqlCommand(checkEmailQuery, conn);
                    checkCmd.Parameters.AddWithValue("@checkEmail", email);
                    long count = (long)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("An account with this email already exists.", "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Check if MySQL user already exists
                    string checkUserQuery = "SELECT COUNT(*) FROM mysql.user WHERE user = @user";
                    using (MySqlCommand checkUserCmd = new MySqlCommand(checkUserQuery, conn))
                    {
                        checkUserCmd.Parameters.AddWithValue("@user", name);
                        long userExists = (long)checkUserCmd.ExecuteScalar();
                        if (userExists > 0)
                        {
                            MessageBox.Show("MySQL user already exists!", "Duplicate User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Create MySQL user
                    string createUserQuery = $"CREATE USER '{name}'@'localhost' IDENTIFIED BY '{password}';";
                    MySqlCommand createUserCmd = new MySqlCommand(createUserQuery, conn);
                    createUserCmd.ExecuteNonQuery();

                    // Grant basic permissions
                    //string grantQuery = $"GRANT SELECT, INSERT, UPDATE ON PowderCoatingDB.* TO '{name}'@'localhost';";
                    //MySqlCommand grantCmd = new MySqlCommand(grantQuery, conn);
                    //grantCmd.ExecuteNonQuery();

                    // Grant limited permissions suitable for a Service Manager
                    string grantPermissions = @"GRANT SELECT, INSERT, UPDATE, DELETE ON PowderCoatingDB.users TO '" + name + @"'@'localhost';
                                        GRANT SELECT, INSERT, UPDATE, DELETE ON PowderCoatingDB.orders TO '" + name + @"'@'localhost';";
                    MySqlCommand grantCmd = new MySqlCommand(grantPermissions, conn);
                    grantCmd.ExecuteNonQuery();


                    // Insert into users table (with automatic timestamp for creationDate)
                    string userQuery = @"INSERT INTO users (name, email, passwordHash, phoneNumber, role, avatar)
                                 VALUES (@name, @email, @password, @phone, 'ServiceManager', @avatar)";
                    MySqlCommand cmdUser = new MySqlCommand(userQuery, conn);
                    cmdUser.Parameters.AddWithValue("@name", name);
                    cmdUser.Parameters.AddWithValue("@email", email);
                    cmdUser.Parameters.AddWithValue("@password", passwordHash);
                    cmdUser.Parameters.AddWithValue("@phone", phone);
                    cmdUser.Parameters.AddWithValue("@avatar", avatarBytes);
                    cmdUser.ExecuteNonQuery();

                    // Log the query for debugging
                    //MessageBox.Show($"Executing query: {cmdUser.CommandText}");
                    //cmdUser.ExecuteNonQuery();

                    // Get the userId of the inserted user
                    long userId = cmdUser.LastInsertedId;

                    // Insert into servicemanagers table (only for Service Manager-specific data)
                    string managerQuery = "INSERT INTO servicemanagers (employeeNumber, userID, name, creationDate) VALUES (@empNum, @userId, @name, @creationDate)";
                    MySqlCommand cmdManager = new MySqlCommand(managerQuery, conn);
                    cmdManager.Parameters.AddWithValue("@empNum", Guid.NewGuid().ToString().Substring(0, 8));
                    cmdManager.Parameters.AddWithValue("@userId", userId);
                    cmdManager.Parameters.AddWithValue("@name", name); // Assuming 'name' is already captured from user input
                    //cmdManager.Parameters.AddWithValue("@creationDate", DateTime.Now); // Or just use CURRENT_TIMESTAMP 
                    cmdManager.ExecuteNonQuery();



                }

                MessageBox.Show("Service Manager registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.Close();
                ClearFormFields();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                pictureBoxAvatar.Image = new Bitmap(filePath);
                avatarBytes = File.ReadAllBytes(filePath); // Save image to the database
            }
        }

        private void ClearFormFields()
        {
            txtName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();
            avatarBytes = null;
            pictureBoxAvatar.Image = null;
        }


        private void pictureBoxAvatar_Click(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblDateTime_Click(object sender, EventArgs e)
        {

        }
    }
}
