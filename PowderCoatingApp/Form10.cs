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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PowderCoatingApp
{
    public partial class CustomerDashboardForm : Form
    {
        private int loggedInUserId;
        private string userRole = "customer";
        private string connectionString = "server=localhost;user=root;password=;database=powdercoatingdb;";

        private enum GridMode
        {
            Orders,
            Notifications,
            EditProfile
        }
        private GridMode currentGridMode = GridMode.Orders;

        public CustomerDashboardForm(int userId)
        {
            InitializeComponent();
            loggedInUserId = userId;
            this.Load += CustomerDashboardForm_Load;
            
        }

        private void CustomerDashboardForm_Load(object sender, EventArgs e)
        {
            // Use the image from Resources
            this.BackgroundImage = Properties.Resources.BackGroundImage2;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            timerDateTime.Start();
            LoadUserName();
            LoadCustomerOrders();
            LoadCustomerNotifications();

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

        // Loading orders of the current user
        private void LoadCustomerOrders()
        {
            currentGridMode = GridMode.Orders;
            // Bind oder handler
            //dgvData.CellClick -= dgvData_CellClick_Orders;
            // clear previous data and colums
            //dgvData.CellClick -= dgvData_CellClick;
            dgvData.DataSource = null;
            dgvData.Columns.Clear();
            dgvData.Rows.Clear();
            //Avoid Grid bug after adding Photo After
            dgvData.AutoGenerateColumns = true;

            // currentUserId - it is loggedInUserId ONLY orders for the current user
            string query = $@"
        SELECT
            o.orderID,
            o.productType,
            o.color,
            o.specifications,
            (SELECT imagePath FROM photos WHERE orderID = o.orderID AND photoType = 'before' AND isMain = 1 LIMIT 1) AS 'Before',
            (SELECT imagePath FROM photos WHERE orderID = o.orderID AND photoType = 'after' AND isMain = 1 LIMIT 1) AS 'After',
            o.status,
            o.createdDate,
            o.updatedDate,
            o.address,
            o.delivery,
            c.paymentInfo
        FROM orders o
        JOIN customers c ON o.customerID = c.customerID
        WHERE c.userID = @userId
        ORDER BY o.createdDate DESC";

            using (var conn = new MySqlConnection(connectionString))
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@userId", loggedInUserId);
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvData.DataSource = dt;
                }
            }

            // Load into Grid
            LoadDataIntoGrid(query, loggedInUserId);
            // refresh data before manually replacing columns
            dgvData.Refresh();
            Application.DoEvents(); // Ensure rows are updates


            // Remove any duplicated Before/After columns created by DataSource
            foreach (string col in new[] { "Before", "After" })
            {
                bool found = false;
                for (int i = dgvData.Columns.Count - 1; i >= 0; i--)
                {
                    if (dgvData.Columns[i].Name == col)
                    {
                        if (!found)
                            found = true; // keep the first found
                        else
                            dgvData.Columns.RemoveAt(i); // remove all others
                    }
                }
            }

            // ColumnHeaders
            dgvData.RowHeadersWidth = 22;

            if (dgvData.Columns.Contains("customerID"))
            {
                dgvData.Columns["customerID"].Width = 27;
                dgvData.Columns["customerID"].Resizable = DataGridViewTriState.False;
            }
            

            // Convert and Replace Before/After columns to image columns
            foreach (string photoCol in new[] { "Before", "After" })
            {
                if (dgvData.Columns.Contains(photoCol))
                {
                    int colIndex = dgvData.Columns[photoCol].Index;

                    // So at First, extract image paths from the string-based column
                    List<string> imagePaths = dgvData.Rows
                        .Cast<DataGridViewRow>()
                        .Select(row => row.Cells[photoCol].Value?.ToString())
                        .ToList();

                    // Replace column with image column
                    dgvData.Columns.Remove(photoCol);

                    // Insert new image column
                    DataGridViewImageColumn imgCol = new DataGridViewImageColumn
                    {
                        Name = photoCol,
                        HeaderText = photoCol,
                        ImageLayout = DataGridViewImageCellLayout.Zoom
                    };
                    dgvData.Columns.Insert(colIndex, imgCol);

                    // Set image values back to rows
                    for (int i = 0; i < dgvData.Rows.Count; i++)
                    {
                        if (i >= imagePaths.Count) break;

                        var row = dgvData.Rows[i];
                        var rawPath = (imagePaths[i] ?? "").Trim(); // e.g. Photos\Before\OrderID\1605025-1_638830382418877061.jpg

                        // Skip empty or invalid paths
                        if (string.IsNullOrWhiteSpace(rawPath) || !Path.HasExtension(rawPath))
                        {
                            row.Cells[photoCol].Value = null;
                            continue;
                        }

                        
                    }

                }
            }

            dgvData.AutoGenerateColumns = false;

            // Make some columns read-only
            string[] readOnlyCols = { "customerID", "Name", "orderID", "createdDate", "updatedDate", "createdBy" };
            foreach (string colName in readOnlyCols)
            {
                if (dgvData.Columns.Contains(colName))
                    dgvData.Columns[colName].ReadOnly = true;
            }

            // Add ComboBox for status 
            if (dgvData.Columns.Contains("status"))
            {
                int colIndex = dgvData.Columns["status"].Index;
                dgvData.Columns.Remove("status");

                DataGridViewComboBoxColumn statusCol = new DataGridViewComboBoxColumn
                {
                    Name = "status",
                    HeaderText = "Status",
                    DataPropertyName = "status",
                    Items = { "Pending", "Accepted", "InProgress", "Completed", "Cancelled" },
                    FlatStyle = FlatStyle.Flat
                };
                dgvData.Columns.Insert(colIndex, statusCol);
            }

            // Add ComboBox for delivery
            if (dgvData.Columns.Contains("delivery"))
            {
                int colIndex = dgvData.Columns["delivery"].Index;
                dgvData.Columns.Remove("delivery");

                DataGridViewComboBoxColumn deliveryCol = new DataGridViewComboBoxColumn
                {
                    Name = "delivery",
                    HeaderText = "Delivery",
                    DataPropertyName = "delivery",
                    Items = { "pickup", "delivery", "pickup/delivery", "no" },
                    FlatStyle = FlatStyle.Flat
                };
                dgvData.Columns.Insert(colIndex, deliveryCol);
            }

            //??? It worked, now there is no double generation of Grid after adding Photo After
            dgvData.AutoGenerateColumns = false;

            // Add Edit/Cancel buttons only if missing
            if (!dgvData.Columns.Contains("Edit")) AddButtonColumn("Edit", "Edit", Color.LightGreen, Color.DarkGreen);
            if (!dgvData.Columns.Contains("Cancel")) AddButtonColumn("Cancel", "Cancel", Color.LightCoral, Color.DarkRed);



            // Hook CellClick for Edit/Delete logic
            // Unbind to avoid duplication
            dgvData.CellClick -= dgvData_CellClick;
            // Then connect the appropriate handler
            dgvData.CellClick += dgvData_CellClick;
            // Disable adding new blank row
            dgvData.AllowUserToAddRows = false;

            
        }

        // Helper for ComboBox columns
        private void ReplaceWithComboBox(string colName, string[] items)
        {
            if (!dgvData.Columns.Contains(colName)) return;
            int idx = dgvData.Columns[colName].Index;
            dgvData.Columns.Remove(colName);
            var comboCol = new DataGridViewComboBoxColumn
            {
                Name = colName,
                HeaderText = colName,
                DataPropertyName = colName,
                Items = { }
            };
            comboCol.Items.AddRange(items);
            dgvData.Columns.Insert(idx, comboCol);
        }

        // Add Edit/Cancel button columns
        private void AddButtonColumn(string name, string text, Color bgColor, Color selColor)
        {
            if (dgvData.Columns.Contains(name)) return;
            var btn = new DataGridViewButtonColumn
            {
                Name = name,
                Text = text,
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat,
                Width = 60,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = bgColor,
                    ForeColor = Color.White,
                    SelectionBackColor = selColor,
                    SelectionForeColor = Color.White,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold)
                }
            };
            dgvData.Columns.Add(btn);
        }

        // Load image helper 
        private Image LoadImageOrNull(string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path)) return null;
                string fullPath = Path.Combine(Application.StartupPath, path);
                if (!File.Exists(fullPath)) return null;
                using (var bmp = new Bitmap(fullPath))
                    return new Bitmap(bmp);
            }
            catch { return null; }
        }

        // CellClick: Edit/Cancel/Gallery
        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var row = dgvData.Rows[e.RowIndex];
            var colName = dgvData.Columns[e.ColumnIndex].Name;

            if (colName == "Edit")
            {
                int orderId = Convert.ToInt32(row.Cells["orderID"].Value);
                UpdateOrder(row, orderId);
            }
            else if (colName == "Cancel")
            {
                int orderId = Convert.ToInt32(row.Cells["orderID"].Value);
                SetOrderCancelled(orderId);
            }
            else if (colName == "Before" || colName == "After")
            {
                int orderId = Convert.ToInt32(row.Cells["orderID"].Value);
                string photoType = colName.ToLower();
                var paths = GetPhotoPaths(orderId, photoType);
                if (paths.Count > 0)
                {
                    var gallery = new ImageGalleryForm(paths, 0);
                    gallery.ShowDialog();
                }
            }
        }

        // Update order (only address, delivery, paymentInfo)
        private void UpdateOrder(DataGridViewRow row, int orderId)
        {
            string address = row.Cells["address"].Value?.ToString();
            string delivery = row.Cells["delivery"].Value?.ToString();
            string payment = row.Cells["paymentInfo"].Value?.ToString();

            using (var conn = new MySqlConnection(connectionString))
            {
                string query = @"UPDATE orders SET address=@addr, delivery=@del, paymentInfo=@pay, updatedDate=CURRENT_TIMESTAMP WHERE orderID=@oid";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@addr", address);
                cmd.Parameters.AddWithValue("@del", delivery);
                cmd.Parameters.AddWithValue("@pay", payment);
                cmd.Parameters.AddWithValue("@oid", orderId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Order updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadCustomerOrders();
        }

        // Set order to Cancelled (do not delete from the database)
        private void SetOrderCancelled(int orderId)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                string query = @"UPDATE orders SET status='Cancelled', updatedDate=CURRENT_TIMESTAMP WHERE orderID=@oid";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@oid", orderId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Order cancelled.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadCustomerOrders();
        }

        // Get all photo paths for this order and type
        private List<string> GetPhotoPaths(int orderId, string photoType)
        {
            var paths = new List<string>();
            string sql = "SELECT imagePath FROM photos WHERE orderID = @oid AND photoType = @ptype ORDER BY photoID";
            using (var conn = new MySqlConnection(connectionString))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@oid", orderId);
                cmd.Parameters.AddWithValue("@ptype", photoType);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        paths.Add(reader["imagePath"]?.ToString() ?? "");
                }
            }
            return paths;
        }

        private void LoadCustomerNotifications()
        {
            currentGridMode = GridMode.Notifications;
            dgvData.DataSource = null;
            dgvData.Columns.Clear();

            string query = @"
        SELECT n.notificationID, n.message, n.sentDate
        FROM notifications n
        JOIN orders o ON n.orderID = o.orderID
        JOIN customers c ON o.customerID = c.customerID
        WHERE c.userID = @userId
        ORDER BY n.sentDate DESC";
            using (var conn = new MySqlConnection(connectionString))
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@userId", loggedInUserId);
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvData.DataSource = dt;
                }
            }

            dgvData.AutoGenerateColumns = true;
            dgvData.RowHeadersWidth = 22;
            dgvData.AllowUserToAddRows = false;
            dgvData.ReadOnly = true;
        }

        // Universal grid loader for CustomerDashboardForm
        private void LoadDataIntoGrid(string query, int userId)
        {
            dgvData.AllowUserToAddRows = false; // Hide new row
            dgvData.DataSource = null;
            dgvData.Columns.Clear();

            // Connect to DB and fill DataTable
            DataTable dt = new DataTable();
            using (var conn = new MySqlConnection(connectionString))
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
            dgvData.DataSource = dt;

            // Set visual styles, copy from Form6 
            dgvData.BackgroundColor = Color.Black;
            dgvData.GridColor = Color.Gray;
            dgvData.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dgvData.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvData.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvData.EnableHeadersVisualStyles = false;

            dgvData.DefaultCellStyle.BackColor = Color.Black;
            dgvData.DefaultCellStyle.ForeColor = Color.White;
            dgvData.DefaultCellStyle.SelectionBackColor = Color.DarkSlateGray;
            dgvData.DefaultCellStyle.SelectionForeColor = Color.White;
        }



        private void timerDateTime_Tick(object sender, EventArgs e)
        {
           lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private void lblCustomerDashboard_Click(object sender, EventArgs e)
        {

        }

        private async void btnBackToHome_Click(object sender, EventArgs e)
        {
            HomeForm homeForm = new HomeForm();
            await Animator.FadeOut(this);
            await Animator.FadeIn(homeForm);
        }

        // When opening AddOrderForm user's name is shown
        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            AddOrderForm addOrderForm = new AddOrderForm(loggedInUserId, "customer");
            addOrderForm.ShowDialog();
        }

        private void btnEditProfile_Click(object sender, EventArgs e)
        {

        }

        private void btnViewOrders_Click(object sender, EventArgs e)
        {

        }

        private void btnNotifications_Click(object sender, EventArgs e)
        {

        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }
    }
}
