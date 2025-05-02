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
    public partial class ServiceManagerDashboardForm : Form
    {
        private int loggedInUserId;

        public ServiceManagerDashboardForm(int userId)
        {
            InitializeComponent();
            loggedInUserId = userId;
            LoadOrders();
            LoadUserName(); // Load and display user's name. Show "Welcome, {name}"
            timerDateTime.Start();
        }

        private string GetUserRole(int userId)
        {
            string role = string.Empty;
            using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;password=;database=powdercoatingdb;"))
            {
                string query = "SELECT role FROM users WHERE userID = @userID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userID", userId);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        role = result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load user role: " + ex.Message);
                }
            }
            return role;
        }

        private void LoadUserName()
        {
            using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;password=;database=powdercoatingdb;"))
            {
                string query = "SELECT name FROM users WHERE userID = @userID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userID", loggedInUserId);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        string name = result.ToString();
                        lblWelcome.Text = $"Welcome, {name}!";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load user name: " + ex.Message);
                }
            }
        }


        private void LoadOrders()
        {
            string query = @"SELECT o.orderID, o.productType, o.color, o.status, o.createdDate, c.customerID 
                         FROM orders o 
                         LEFT JOIN customers c ON o.customerID = c.customerID";

            LoadDataIntoGrid(query);
        }

        private void LoadCustomers()
        {
            string query = @"SELECT u.userID, u.name, u.email, c.customerID, c.address, c.paymentInfo 
                         FROM customers c 
                         JOIN users u ON c.userID = u.userID";

            LoadDataIntoGrid(query);
        }

        private void LoadDataIntoGrid(string query)
        {
            using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;password=;database=powdercoatingdb;"))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvData.DataSource = dt;
            }
        }

        private void timerDateTime_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private async void btnBackToHome_Click(object sender, EventArgs e)
        {
            AdminManagementForm adminManagementForm = new AdminManagementForm(loggedInUserId);
            await Animator.FadeOut(this);
            await Animator.FadeIn(adminManagementForm);
        }

        private void ServiceManagerDashboard_Load(object sender, EventArgs e)
        {
            // Use the image from Resources
            this.BackgroundImage = Properties.Resources.BackGroundImage2;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            timerDateTime.Start();

            // Get user role
            string userRole = GetUserRole(loggedInUserId);

            // If the user is Chief Admin, hide the Edit Profile button
            if (userRole == "Chief Admin")
            {
                btnEditProfile.Visible = false; // Hide the button
            }
        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            var registrationForm = new RegistrationForm(RegistrationMode.ManagerAddCustomer);
            registrationForm.ShowDialog();
            LoadCustomers(); // Reload the Customers list 

        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            var addOrderForm = new AddOrderForm();
            addOrderForm.ShowDialog();
            LoadOrders();
        }

        private void btnViewCustomers_Click(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnViewOrders_Click(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void btnEditProfile_Click(object sender, EventArgs e)
        {
            var editProfileForm = new EditProfileForm(loggedInUserId);
            editProfileForm.ShowDialog();
        }
    }
}
