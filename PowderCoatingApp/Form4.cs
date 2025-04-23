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
using System.IO;

namespace PowderCoatingApp
{
    public partial class AdminManagementForm : Form
    {
        string connectionString = "server=localhost;port=3300;user=root;password=qwerty;database=PowderCoatingDB;";
        
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
            string connectionString = "server=localhost;port=3300;user=root;password=qwerty;database=PowderCoatingDB;";            

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT userID, name, email, phoneNumber, avatar FROM users WHERE role = 'ServiceManager'";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    // Reset DataGridView
                    dgvAdmins.DataSource = null;
                    dgvAdmins.Rows.Clear();
                    dgvAdmins.Columns.Clear();

                    // This line for dgvAdmins
                    dgvAdmins.AutoGenerateColumns = false;
                    dgvAdmins.RowTemplate.Height = 80;

                    // Define columns
                    dgvAdmins.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "userID" });
                    dgvAdmins.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Name", DataPropertyName = "name" });
                    dgvAdmins.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Email", DataPropertyName = "email" });
                    dgvAdmins.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Phone", DataPropertyName = "phoneNumber" });

                    // Avatar column
                    var avatarColumn = new DataGridViewImageColumn
                    {
                        HeaderText = "Avatar",
                        ImageLayout = DataGridViewImageCellLayout.Zoom,
                        Width = 80
                    };
                    dgvAdmins.Columns.Add(avatarColumn);

                    // Add rows
                    foreach (DataRow row in table.Rows)
                    {
                        byte[] avatarData = row["avatar"] == DBNull.Value ? null : (byte[])row["avatar"];
                        Image avatarImage = avatarData != null ? ByteArrayToImage(avatarData) : Properties.Resources.defaultAvatar;

                        dgvAdmins.Rows.Add(
                            row["userID"],
                            row["name"],
                            row["email"],
                            row["phoneNumber"],
                            avatarImage
                        );
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading managers: " + ex.Message);
                }
            }
        }

        private Image ByteArrayToImage(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void btnAddAdmin_Click(object sender, EventArgs e)
        {
            AddAdminForm addAdmin = new AddAdminForm();// create this next - done
            await Animator.FadeOut(this);
            await Animator.FadeIn(addAdmin);
        }

        private void btnRemoveAdmin_Click(object sender, EventArgs e)
        {
            //RemoveAdminForm removeAdmin = new RemoveAdminForm(); // will create this too
            //removeAdmin.ShowDialog();
        }

        private void btnViewAdmins_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT userID, name, email, phoneNumber, avatar FROM Users WHERE role = 'ServiceManager'";
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
            //ServiceManagerDashboard dashboard = new ServiceManagerDashboard(); // I’ll create this later
            //dashboard.Show();
            //this.Hide();
        }

        private async void btnBackToHome_Click(object sender, EventArgs e)
        {
            ChooseRoleForm chooseRole = new ChooseRoleForm();
            await Animator.FadeOut(this);
            await Animator.FadeIn(chooseRole);
            
        }

        private void lblDateTime_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
