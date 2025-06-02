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
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using PowderCoatingApp;

namespace PowderCoatingApp
{
    
    public partial class LoginForm : Form
    {
        
        public LoginForm()
        {
            InitializeComponent();
            timerDateTime.Start();
        }

        private readonly string connectionString = "server=localhost;user=root;password=;database=powdercoatingdb;";

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Use the image from Resources
            this.BackgroundImage = Properties.Resources.BackGroundImage2;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            timerDateTime.Start();

            // Show login controls
            lblLogin.Visible = true;
            lblUserName.Visible = true;
            txtUserName.Visible = true;
            lblUserPassword.Visible = true;
            txtUserPassword.Visible = true;
            btnLogin.Visible = true;
            lblError.Text = "";
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

        //System Login Logic
        private void btnLogin_Click(object sender, EventArgs e)
        {
            lblError.Text = ""; // clear previous error
            string username = txtUserName.Text.Trim();
            string password = txtUserPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblError.Text = "Please enter both username and password.";
                return;
            }

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT userID, passwordHash, role FROM users WHERE name = @username LIMIT 1";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                lblError.Text = "User not found.";
                                return;
                            }

                            int userId = Convert.ToInt32(reader["userID"]);
                            string hashFromDb = reader["passwordHash"].ToString();
                            string userRole = reader["role"].ToString();

                            // Check hashed password
                            if (!VerifyPasswordHash(password, hashFromDb))
                            {
                                lblError.Text = "Incorrect password.";
                                return;
                            }

                            // Depending on the role, open the required Dashboard:
                            this.Hide(); // Hide login form (можна закрити .Close() якщо треба)

                            if (userRole == "ChiefAdmin")
                            {
                                var adminForm = new AdminManagementForm(userId);
                                adminForm.Show();
                            }
                            else if (userRole == "ServiceManager")
                            {
                                var serviceForm = new ServiceManagerDashboardForm(userId);
                                serviceForm.Show();
                            }
                            else if (userRole == "Customer")
                            {
                                //MessageBox.Show("Customer Dashboard is not implemented yet.", "Info");
                                var customerForm = new CustomerDashboardForm(userId);
                                customerForm.Show();
                            }
                            else
                            {
                                lblError.Text = "Unknown user role!";
                                this.Show(); // Повертаємо LoginForm якщо щось не так
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Login failed: " + ex.Message;
            }
        }

        // SHA256 password check (adapt if you use another hashing algorithm!)
        private bool VerifyPasswordHash(string password, string hashFromDb)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                string hash = BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
                return hash == hashFromDb.ToLowerInvariant();
            }
        }

        private void txtUserPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblError_Click(object sender, EventArgs e)
        {

        }

        private void lblLogin_Click(object sender, EventArgs e)
        {

        }

        private void lblUserName_Click(object sender, EventArgs e)
        {

        }

        private void lblUserPassword_Click(object sender, EventArgs e)
        {

        }
    }
}
