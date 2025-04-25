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
using System.Windows.Forms.VisualStyles;

namespace PowderCoatingApp
{
    public partial class AdminManagementForm : Form
    {
        string connectionString = "server=localhost;port=3306;user=root;password=;database=PowderCoatingDB;";
        //string connectionString = "server=localhost;port=3306;user=root;password=qwerty;database=PowderCoatingDB;";
        
        public AdminManagementForm()
        {
            InitializeComponent();
            timerDateTime.Start();
            dgvAdmins.CellPainting += dgvAdmins_CellPainting;
            dgvAdmins.CellContentClick += dgvAdmins_CellContentClick;
            dgvAdmins.CellClick += dgvAdmins_CellClick;


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
            string connectionString = "server=localhost;port=3306;user=root;password=;database=PowderCoatingDB;";             
            //string connectionString = "server=localhost;port=3306;user=root;password=qwerty;database=PowderCoatingDB;";            

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
                    // Set general DataGridView styles
                    dgvAdmins.EnableHeadersVisualStyles = false; // To allow my styling

                    dgvAdmins.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
                    dgvAdmins.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    dgvAdmins.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    dgvAdmins.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvAdmins.RowHeadersDefaultCellStyle.BackColor = Color.Black;
                    dgvAdmins.RowHeadersDefaultCellStyle.ForeColor = Color.White;

                    dgvAdmins.BackgroundColor = Color.Black;
                    dgvAdmins.DefaultCellStyle.BackColor = Color.Black;
                    dgvAdmins.DefaultCellStyle.ForeColor = Color.White;
                    dgvAdmins.DefaultCellStyle.SelectionBackColor = Color.DarkSlateGray;
                    dgvAdmins.DefaultCellStyle.SelectionForeColor = Color.White;
                    dgvAdmins.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);


                    // This line for dgvAdmins
                    dgvAdmins.AutoGenerateColumns = false;
                    dgvAdmins.RowTemplate.Height = 80;
                    dgvAdmins.RowHeadersWidth = 24;

                    // Define columns
                    dgvAdmins.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "ID",
                        DataPropertyName = "userID",
                        Name = "userID",  // <-- ADD THIS
                        Width = 26
                    });

                    dgvAdmins.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Name",
                        DataPropertyName = "name",
                        Name = "name",    // <-- ADD THIS
                        Width = 50
                    });

                    dgvAdmins.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Email",
                        DataPropertyName = "email",
                        Name = "email",   // <-- ADD THIS
                        Width = 115
                    });

                    dgvAdmins.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Phone",
                        DataPropertyName = "phoneNumber",
                        Name = "phoneNumber",   // <-- ADD THIS
                        Width = 75
                    });


                    // Make all columns editable except Avatar, Edit, and Delete
                    foreach (DataGridViewColumn column in dgvAdmins.Columns)
                    {
                        if (column.HeaderText != "Avatar" && column.HeaderText != "" && column.HeaderText != "Edit" && column.HeaderText != "Delete")
                        {
                            column.ReadOnly = false;
                        }
                        else
                        {
                            column.ReadOnly = true;
                        }
                    }

                    // Avatar column
                    var avatarColumn = new DataGridViewImageColumn
                    {
                        HeaderText = "Avatar",
                        Name = "Avatar",
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

                    // Edit and Delete button columns 
                    if (dgvAdmins.Columns["EditColumn"] == null && dgvAdmins.Columns["DeleteColumn"] == null)
                    {
                        // Edit button
                        DataGridViewButtonColumn editButton = new DataGridViewButtonColumn
                        {
                            Name = "EditColumn",
                            HeaderText = "",
                            Text = "Edit",
                            UseColumnTextForButtonValue = true,
                            Width = 60
                        };
                        dgvAdmins.Columns.Add(editButton);
                        dgvAdmins.EnableHeadersVisualStyles = false;


                        // Delete button
                        DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn
                        {
                            Name = "DeleteColumn",
                            HeaderText = "",
                            Text = "Delete",
                            UseColumnTextForButtonValue = true,
                            Width = 60
                        };
                        dgvAdmins.Columns.Add(deleteButton);
                        dgvAdmins.EnableHeadersVisualStyles = false;

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

        private void dgvAdmins_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 &&
                (e.ColumnIndex == dgvAdmins.Columns["EditColumn"].Index ||
                 e.ColumnIndex == dgvAdmins.Columns["DeleteColumn"].Index))
            {
                e.Handled = true;

                // Fill button background
                using (Brush brush = new SolidBrush(
                    dgvAdmins.Columns[e.ColumnIndex].Name == "EditColumn" ? Color.LightGreen : Color.LightCoral))
                {
                    e.Graphics.FillRectangle(brush, e.CellBounds);
                }

                // Draw black border around the button
                using (Pen pen = new Pen(Color.Black, 1))
                {
                    Rectangle rect = e.CellBounds;
                    rect.Width -= 1; // optional: remove right edge clipping
                    rect.Height -= 1; // optional: remove bottom edge clipping
                    e.Graphics.DrawRectangle(pen, rect);
                }

                // Draw button text
                TextRenderer.DrawText(
                    e.Graphics,
                    dgvAdmins.Columns[e.ColumnIndex].Name == "EditColumn" ? "Edit" : "Delete",
                    dgvAdmins.Font,
                    e.CellBounds,
                    Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }                  


        private void dgvAdmins_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string columnName = dgvAdmins.Columns[e.ColumnIndex].Name;

                // "Edit" column index
                if (columnName == "EditColumn")
                {
                    // Get values from row
                    int userId = Convert.ToInt32(dgvAdmins.Rows[e.RowIndex].Cells["userID"].Value);
                    string name = dgvAdmins.Rows[e.RowIndex].Cells["name"].Value.ToString();
                    string email = dgvAdmins.Rows[e.RowIndex].Cells["email"].Value.ToString();
                    string phone = dgvAdmins.Rows[e.RowIndex].Cells["phoneNumber"].Value.ToString();

                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            string updateQuery = @"UPDATE users 
                                           SET name = @name, email = @email, phoneNumber = @phone 
                                           WHERE userID = @userID";

                            MySqlCommand cmd = new MySqlCommand(updateQuery, conn);
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@email", email);
                            cmd.Parameters.AddWithValue("@phone", phone);
                            cmd.Parameters.AddWithValue("@userID", userId);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("User updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadManagers(); // Refresh the grid
                            }
                            else
                            {
                                MessageBox.Show("Update failed. No changes made.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error updating user: " + ex.Message);
                        }
                    }
                }

                // "Delete" column index
                else if (columnName == "DeleteColumn")
                {
                    var confirm = MessageBox.Show("Are you sure you want to delete this Service Manager?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirm == DialogResult.Yes)
                    {
                        int userId = Convert.ToInt32(dgvAdmins.Rows[e.RowIndex].Cells["userID"].Value);

                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            conn.Open();

                            // First delete from servicemanagers
                            MySqlCommand cmd1 = new MySqlCommand("DELETE FROM servicemanagers WHERE userID = @userId", conn);
                            cmd1.Parameters.AddWithValue("@userId", userId);
                            cmd1.ExecuteNonQuery();

                            // Then delete from users
                            MySqlCommand cmd2 = new MySqlCommand("DELETE FROM users WHERE userID = @userId", conn);
                            cmd2.Parameters.AddWithValue("@userId", userId);
                            cmd2.ExecuteNonQuery();

                            MessageBox.Show("Service Manager deleted successfully.");
                            LoadManagers(); // Refresh grid
                        }
                    }
                }
            }
        }

        private void dgvAdmins_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvAdmins.Columns[e.ColumnIndex].HeaderText == "Avatar")
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Title = "Choose Avatar Image";
                    ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            // Load image and set it in the grid
                            Image newAvatar = Image.FromFile(ofd.FileName);
                            dgvAdmins.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = newAvatar;

                            // Convert image to byte[]
                            byte[] imageBytes = File.ReadAllBytes(ofd.FileName);

                            int userId = Convert.ToInt32(dgvAdmins.Rows[e.RowIndex].Cells["userID"].Value);

                            using (MySqlConnection conn = new MySqlConnection(connectionString))
                            {
                                conn.Open();
                                string query = "UPDATE users SET avatar = @avatar WHERE userID = @userId";
                                MySqlCommand cmd = new MySqlCommand(query, conn);
                                cmd.Parameters.AddWithValue("@avatar", imageBytes);
                                cmd.Parameters.AddWithValue("@userId", userId);
                                cmd.ExecuteNonQuery();
                            }

                            MessageBox.Show("Avatar updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error updating avatar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }


        private async void btnAddAdmin_Click(object sender, EventArgs e)
        {
            AddAdminForm addAdmin = new AddAdminForm();// create this next - done
            await Animator.FadeOut(this);
            await Animator.FadeIn(addAdmin);
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
