using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PowderCoatingApp
{
    public partial class ServiceManagerDashboardForm : Form
    {
        private int loggedInUserId;
        private string userRole; //user role
        private List<string> columnOrder = new List<string>(); // // Preserve column order for Grid

        public ServiceManagerDashboardForm(int userId)
        {
            InitializeComponent();
            loggedInUserId = userId;
            this.btnNotifications.Click += new System.EventHandler(this.btnNotifications_Click);
            LoadOrders();
            LoadUserName(); // Load and display user's name. Show "Welcome, {name}"
            this.Load += ServiceManagerDashboard_Load; // Hide Edit Profile button from ChiefAdmin
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

        // Loads all Oders into DataGridView
        private void LoadOrders()
        {
            string query = @"SELECT o.orderID, o.productType, o.color, o.status, o.createdDate, c.customerID 
                         FROM orders o 
                         LEFT JOIN customers c ON o.customerID = c.customerID";

            LoadDataIntoGrid(query);
        }

        // Loads all Customers into DataGridView
        private void LoadCustomers()
        {
            dgvData.RowHeadersWidth = 22;
            if (dgvData.Columns.Contains("customerID"))
            {
                dgvData.Columns["customerID"].Width = 27;
                dgvData.Columns["customerID"].Resizable = DataGridViewTriState.False;
            }

            string query = @"SELECT c.customerID, u.name, u.email, u.phoneNumber, c.address, c.paymentInfo, u.avatar, u.creationDate   
                         FROM customers c 
                         JOIN users u ON c.userID = u.userID";

            LoadDataIntoGrid(query);

            // Display avatar as image in DataGridView
            if (dgvData.Columns.Contains("avatar"))
            {
                DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
                imageColumn = (DataGridViewImageColumn)dgvData.Columns["avatar"];
                imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }

                // Clear existing buttons
            if (!dgvData.Columns.Contains("Edit"))
            {
                DataGridViewButtonColumn editButton = new DataGridViewButtonColumn
                {
                    Name = "Edit",
                    HeaderText = "",
                    Text = "Edit",
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Flat,
                    Width = 60,
                    DefaultCellStyle = new DataGridViewCellStyle 
                      {
                          BackColor = Color.LightGreen,
                          ForeColor = Color.White,
                          SelectionBackColor = Color.DarkGreen,
                          SelectionForeColor = Color.White
                      }

                };
                dgvData.Columns.Add(editButton);
            }

            if (!dgvData.Columns.Contains("Delete"))
            {
                 DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn
                 {
                     Name = "Delete",
                     HeaderText = "",
                     Text = "Delete",
                     UseColumnTextForButtonValue = true,
                     FlatStyle = FlatStyle.Flat,
                     Width = 60,
                     DefaultCellStyle = new DataGridViewCellStyle
                      {
                         BackColor = Color.LightCoral,
                         ForeColor = Color.White,
                         SelectionBackColor = Color.DarkRed,
                         SelectionForeColor = Color.White
                      }
                 };
                  dgvData.Columns.Add(deleteButton);
            }

            // Style Edit and Delete buttons
            dgvData.CellFormatting += (s, e) =>
            {
                if (dgvData.Columns[e.ColumnIndex].Name == "Edit")
                {
                    e.CellStyle.BackColor = Color.LightGreen;
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                }
                else if (dgvData.Columns[e.ColumnIndex].Name == "Delete")
                {
                    e.CellStyle.BackColor = Color.LightCoral;
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                }
            };

            // combo box in DataGridView
            if (dgvData.Columns.Contains("paymentInfo"))
            {
                // Remove the existing column
               int colIndex = dgvData.Columns["paymentInfo"].Index; // Track current position
               dgvData.Columns.Remove("paymentInfo"); // Remove existing column

                 // Create a new combo box column
                 DataGridViewComboBoxColumn paymentColumn = new DataGridViewComboBoxColumn
                 {
                      Name = "paymentInfo",
                      HeaderText = "paymentInfo",
                      DataPropertyName = "paymentInfo", // must match the column in DataSource
                      Items = { "Cash", "Credit Card", "Bank Transfer", "PayPal", "Other" },
                      FlatStyle = FlatStyle.Flat
                 };

                 // Insert it back at the same index
                 dgvData.Columns.Insert(colIndex, paymentColumn); // Insert at original position
            }

            // Handle button clicks
            dgvData.CellClick -= dgvData_CellClick; // Prevent multiple bindings
            dgvData.CellClick += dgvData_CellClick;

            dgvData.DataError -= dgvData_DataError; // avoid multiple binding
            dgvData.DataError += dgvData_DataError;

            
            dgvData.AllowUserToAddRows = false; // Disable new row creation
            // Disable CreationDate and CustomerID column for editing
            if (dgvData.Columns.Contains("creationDate")) 
                dgvData.Columns["creationDate"].ReadOnly = true;
            if (dgvData.Columns.Contains("customerID")) 
                dgvData.Columns["customerID"].ReadOnly = true;

            // Wire up validation event (once)
            dgvData.CellValidating -= dgvData_CellValidating; // avoid double-binding
            dgvData.CellValidating += dgvData_CellValidating;

            // Save the initial column order once
            if (columnOrder.Count == 0)
            {
                foreach (DataGridViewColumn col in dgvData.Columns)
                {
                    columnOrder.Add(col.Name);
                }
                if (!columnOrder.Contains("Edit")) columnOrder.Add("Edit");
                if (!columnOrder.Contains("Delete")) columnOrder.Add("Delete");
            }
            else
            {
                // Restore saved order
                foreach (var colName in columnOrder)
                {
                    if (dgvData.Columns.Contains(colName))
                    {
                        dgvData.Columns[colName].DisplayIndex = columnOrder.IndexOf(colName);
                    }
                }
                if (dgvData.Columns.Contains("Edit"))
                    dgvData.Columns["Edit"].DisplayIndex = dgvData.Columns.Count - 2;
                if (dgvData.Columns.Contains("Delete"))
                    dgvData.Columns["Delete"].DisplayIndex = dgvData.Columns.Count - 1;

                dgvData.RowHeadersWidth = 22;
                if (dgvData.Columns.Contains("customerID"))
                {
                    dgvData.Columns["customerID"].Width = 27; 
                    dgvData.Columns["customerID"].Resizable = DataGridViewTriState.False; 
                }
            }
        }
        // Validation Logic
        private void dgvData_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string columnName = dgvData.Columns[e.ColumnIndex].Name;
            string value = e.FormattedValue?.ToString().Trim() ?? "";

            if (columnName == "email")
            {
                if (!IsValidEmail(value))
                {
                    e.Cancel = true;
                    MessageBox.Show("Invalid email format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (columnName == "phoneNumber")
            {
                if (!Regex.IsMatch(value, @"^\d{10,15}$"))
                {
                    e.Cancel = true;
                    MessageBox.Show("Phone number must be digits only (10–15 characters).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (columnName == "paymentInfo")
            {
                string[] allowed = { "Cash", "Credit Card", "Bank Transfer", "PayPal", "Other" };

                if (value.StartsWith("Other:"))
                {
                    string details = value.Substring(6).Trim();
                    if (string.IsNullOrWhiteSpace(details))
                    {
                        e.Cancel = true;
                        MessageBox.Show("Please provide details after 'Other:'.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (!allowed.Contains(value))
                {
                    e.Cancel = true;
                    MessageBox.Show("Invalid payment method. Allowed: Cash, Credit Card, Bank Transfer, PayPal, Other (with description).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        // Helper Method to Validate Email
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        // To check Other: [value]” is valid & doesn’t throw DataGridViewComboBoxCell error
        private void dgvData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Prevent crash for custom combo values like "Other: Bitcoin"
            if (dgvData.Columns[e.ColumnIndex].Name == "paymentInfo")
            {
                e.ThrowException = false;
            }
        }


        // dropdown list for paymentInfo Allow Typing for "Other" Option
        private void dgvData_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvData.CurrentCell.ColumnIndex == dgvData.Columns["paymentInfo"].Index && e.Control is ComboBox cb)
            {
                cb.DropDownStyle = ComboBoxStyle.DropDown;

                // Remove existing handler to prevent duplication
                cb.SelectedIndexChanged -= PaymentInfoComboBox_SelectedIndexChanged;
                cb.SelectedIndexChanged += PaymentInfoComboBox_SelectedIndexChanged;

                // Prevent editing predefined items
                cb.KeyPress -= PaymentInfoComboBox_KeyPress;
                cb.KeyPress += PaymentInfoComboBox_KeyPress;

                // Allow 'Other: ...' values without exceptions
                string value = dgvData.CurrentCell.Value?.ToString();
                if (!string.IsNullOrWhiteSpace(value) && value.StartsWith("Other:") && !cb.Items.Contains(value))
                {
                    cb.Items.Add(value);
                    cb.SelectedItem = value;
                }

                // Add current value if it's a valid custom Other: value
                //string currentVal = dgvData.CurrentCell.Value?.ToString();
                //if (!string.IsNullOrWhiteSpace(currentVal) && currentVal.StartsWith("Other:") && !cb.Items.Contains(currentVal))
                //{
                //cb.Items.Add(currentVal);
                //cb.SelectedItem = currentVal;
                //}
            }
        }

        private void PaymentInfoComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            // Allow typing ONLY if text starts with "Other:"
            if (!combo.Text.StartsWith("Other:"))
            {
                e.Handled = true;
            }
            
        }


        private void PaymentInfoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox combo)
            {
                if (combo.SelectedItem?.ToString() == "Other")
                {
                    // Allow free typing
                    combo.DropDownStyle = ComboBoxStyle.DropDown;
                    combo.Text = "Other: ";
                    combo.SelectionStart = combo.Text.Length;
                    
                }
                else
                {
                    // Disable free typing for predefined options
                    combo.DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }
        }


        // Handle Edit and Delete Buttons
        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Skip header clicks
            if (e.RowIndex < 0) return;

            if (dgvData.Columns[e.ColumnIndex].Name == "Edit")
            {
                int customerId = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["customerID"].Value);
                EditCustomer(customerId);
            }
            else if (dgvData.Columns[e.ColumnIndex].Name == "Delete")
            {
                int customerId = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["customerID"].Value);
                DeleteCustomer(customerId);
            }
        }


        // Customer Editing directly in row
        private void EditCustomer(int customerId)
        {
            try
            {
                // Get values from the selected row
                DataGridViewRow row = dgvData.CurrentRow;

                string name = row.Cells["name"].Value?.ToString();
                string email = row.Cells["email"].Value?.ToString();
                string phone = row.Cells["phoneNumber"].Value?.ToString();
                string address = row.Cells["address"].Value?.ToString();
                string paymentInfo = row.Cells["paymentInfo"].Value?.ToString();

                using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;password=;database=powdercoatingdb;"))
                {
                    string query = @"UPDATE users u
                             JOIN customers c ON u.userID = c.userID
                             SET u.name = @name, u.email = @email, u.phoneNumber = @phone,
                                 c.address = @address, c.paymentInfo = @payment
                             WHERE c.customerID = @customerID";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@payment", paymentInfo);
                    cmd.Parameters.AddWithValue("@customerID", customerId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Customer updated successfully.");
                LoadCustomers(); // Refresh the DataGridView                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating customer: " + ex.Message);
            }
        }

        private void DeleteCustomer(int customerId)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;password=;database=powdercoatingdb;"))
                {
                    string query = "DELETE FROM customers WHERE customerID = @customerID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@customerID", customerId);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Customer deleted successfully.");
                        LoadCustomers(); // Refresh
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting customer: " + ex.Message);
                    }
                }
            }
        }



        private void LoadNotifications()
        {
            string query = @"SELECT n.notificationID, u.name AS SenderName, o.orderID, n.message, n.sentDate 
                     FROM notifications n
                     LEFT JOIN users u ON n.userID = u.userID
                     LEFT JOIN orders o ON n.orderID = o.orderID
                     ORDER BY n.sentDate DESC";

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

                // Set general styling for Grid
                dgvData.BackgroundColor = Color.Black;
                dgvData.DefaultCellStyle.BackColor = Color.Black;
                dgvData.DefaultCellStyle.ForeColor = Color.White;
                dgvData.DefaultCellStyle.SelectionBackColor = Color.DarkSlateGray;
                dgvData.DefaultCellStyle.SelectionForeColor = Color.White;
                

                dgvData.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
                dgvData.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvData.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvData.EnableHeadersVisualStyles = false;

                dgvData.GridColor = Color.Gray;
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
            dgvData.EditingControlShowing += dgvData_EditingControlShowing; // dropdown list for paymentInfo
            timerDateTime.Start();

            // Get user role
            userRole = GetUserRole(loggedInUserId);

            // If the user is Chief Admin, hide the Edit Profile button
            if (userRole == "ChiefAdmin")
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
            var addOrderForm = new AddOrderForm(loggedInUserId);
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

        private void btnNotifications_Click(object sender, EventArgs e)
        {
            LoadNotifications();
        }

        private void lblDateTime_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnEditProfile_Click(object sender, EventArgs e)
        {

        }


        //private void btnEditProfile_Click(object sender, EventArgs e)
        //{
        //var editProfileForm = new EditProfileForm(loggedInUserId);
        //editProfileForm.ShowDialog();
        //}

    }
}
