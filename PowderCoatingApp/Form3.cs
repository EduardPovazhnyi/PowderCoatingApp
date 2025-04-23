using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PowderCoatingApp
{
    public partial class ChooseRoleForm : Form
    {
        public ChooseRoleForm()
        {
            InitializeComponent();
            timerDateTime.Start();
        }

        private void btnRegisterCustomer_Click(object sender, EventArgs e)
        {
            RegistrationForm customerForm = new RegistrationForm(); // for Users
            customerForm.Show();
            this.Hide();
        }

        private void btnRegisterManager_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Unauthorized. Only the Chief Manager can register new Managers.", "Access Restricted", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            // Show Chief Login Fields
            lblChiefLogin.Visible = true;
            lblChiefName.Visible = true;
            txtChiefName.Visible = true;
            lblChiefPassword.Visible = true;
            txtChiefPassword.Visible = true;
            btnConfirmChiefLogin.Visible = true;
        }

        private void ChooseRoleForm_Load(object sender, EventArgs e)
        {
            // Use the image from Resources
            this.BackgroundImage = Properties.Resources.BackGroundImage2;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            timerDateTime.Start();
        }

        private async void btnBackToHome_Click(object sender, EventArgs e)
        {
            HomeForm homeForm = new HomeForm();
            await Animator.FadeOut(this);
            await Animator.FadeIn(homeForm);            
        }

        private void lblChiefLogin_Click(object sender, EventArgs e)
        {

        }

        private void txtChiefName_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblChiefPassword_Click(object sender, EventArgs e)
        {

        }

        private void btnConfirmChiefLogin_Click(object sender, EventArgs e)
        {
            string chiefName = txtChiefName.Text;
            string chiefPassword = txtChiefPassword.Text;
            string hashedPassword = HashPassword(chiefPassword);

            string connectionString = "server=localhost;port=3300;user=root;password=qwerty;database=PowderCoatingDB;";
            

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Connection opened successfully."); // Cheking line
                    string query = "SELECT * FROM users WHERE name = @name AND passwordHash = @password AND role = 'ChiefAdmin'";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", chiefName);
                    cmd.Parameters.AddWithValue("@password", hashedPassword);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Success: open AdminManagementForm
                        this.Hide();
                        AdminManagementForm adminForm = new AdminManagementForm();
                        adminForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Chief Manager credentials.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                }
            }


        }

        // Password hashing function
        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
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

        private void timerDateTime_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private void lblDateTime_Click(object sender, EventArgs e)
        {

        }
    }


}
