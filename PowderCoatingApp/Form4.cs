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
    public partial class AdminManagementForm : Form
    {
        string connectionString = "server=localhost;user=root;password=your_password;database=PowderCoatingDB;";
        public AdminManagementForm()
        {
            InitializeComponent();
            timerDateTime.Start();
        }

        private void AdminManagementForm_Load(object sender, EventArgs e)
        {
            // Use the image from Resources
            this.BackgroundImage = Properties.Resources.BackGroundImage2;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            timerDateTime.Start();

            LoadManagers(); // loads the Managers into the DataGridView
        }

        private void timerDateTime_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private void LoadManagers()
        {
            string connectionString = "server=localhost;user=root;password=your_password;database=PowderCoatingDB;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT userID, name, email, phoneNumber FROM Users WHERE role = 'ServiceManager'";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dgvAdmins.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading managers: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAddAdmin_Click(object sender, EventArgs e)
        {
            AddAdminForm addAdmin = new AddAdminForm(); // create this next
            addAdmin.ShowDialog();
        }

        private void btnRemoveAdmin_Click(object sender, EventArgs e)
        {
            RemoveAdminForm removeAdmin = new RemoveAdminForm(); // will create this too
            removeAdmin.ShowDialog();
        }

        private void btnViewAdmins_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT userID, name, email, phoneNumber FROM Users WHERE role = 'ServiceManager'";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dgvAdmins.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading admins: " + ex.Message);
                }
            }
        }

        private void btnServiceDashboard_Click(object sender, EventArgs e)
        {
            ServiceManagerDashboard dashboard = new ServiceManagerDashboard(); // I’ll create this later
            dashboard.Show();
            this.Hide();
        }

        private async void btnBackToHome_Click(object sender, EventArgs e)
        {
            ChooseRoleForm chooseRole = new ChooseRoleForm();
            await Animator.FadeOut(this);
            await Animator.FadeIn(chooseRole);
            
        }
    }
}
