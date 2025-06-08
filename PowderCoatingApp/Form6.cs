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
    public partial class ServiceManagerDashboardForm : Form
    {
        private int loggedInUserId;
        private string userRole; //user role
        private List<string> columnOrder = new List<string>(); // Preserve column order for Grid
        // photo playback for this order and type before/after
        private readonly string connectionString = "server=localhost;user=root;password=;database=powdercoatingdb;";
        private enum GridMode
        {
            Orders,
            Customers,
            Notifications
        }

        private GridMode currentGridMode = GridMode.Orders;

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
            currentGridMode = GridMode.Orders;
            // Bind oder handler
            dgvData.CellClick -= dgvData_CellClick_Orders;
            // clear previous data and colums
            dgvData.CellClick -= dgvData_CellClick;
            dgvData.DataSource = null;
            dgvData.Columns.Clear();
            dgvData.Rows.Clear();
            //Avoid Grid bug after adding Photo After
            dgvData.AutoGenerateColumns = true;

            // Bind oder handler
            //dgvData.CellClick += dgvData_CellClick_Orders;

            // Load SQL data
            string query = @"SELECT
            c.customerID,
            u.name AS Name,
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
            u2.name AS createdBy
            FROM orders o
            JOIN customers c ON o.customerID = c.customerID
            JOIN users u ON c.userID = u.userID
            LEFT JOIN users u2 ON o.createdBy = u2.userID";

            // Load into Grid
            LoadDataIntoGrid(query);
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
            // Convert photo paths into images
            //foreach (DataGridViewRow row in dgvData.Rows)
            //{
            //string beforePath = row.Cells["Before"].Value?.ToString();
            //string afterPath = row.Cells["After"].Value?.ToString();

            //row.Cells["Before"].Value = LoadImageOrNull(beforePath);
            //row.Cells["After"].Value = LoadImageOrNull(afterPath);
            //}

            // Replace text columns with image columns
            //ReplaceWithImageColumn("Before");
            //ReplaceWithImageColumn("After");

            // Check and Remove any duplicate Before/After columns
            //while (dgvData.Columns.Cast<DataGridViewColumn>().Count(c => c.Name == "Before") > 1)
            //dgvData.Columns.Remove(dgvData.Columns["Before"]);

            //while (dgvData.Columns.Cast<DataGridViewColumn>().Count(c => c.Name == "After") > 1)
            //dgvData.Columns.Remove(dgvData.Columns["After"]);

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

                        // Clean invisible symbols (just in case)
                        rawPath = rawPath.Trim().Replace("\n", "").Replace("\r", "").Replace("\t", "");

                        // If the path is already correct (contains "Order_"), just use it
                        string resolvedPath = rawPath;
                        if (!rawPath.Contains("Order_"))
                        {
                            // If path is not in correct format, reconstruct it
                            string orderId = row.Cells["orderID"].Value?.ToString();
                            if (string.IsNullOrWhiteSpace(orderId)) continue; // Skip if orderID is invalid

                            // Detect whether it's "Before" or "After"
                            string folder = photoCol == "Before" ? "Before" : "After";

                            // Extract file name
                            string fileName = Path.GetFileName(rawPath);

                            // Construct correct relative path
                            resolvedPath = Path.Combine("Photos", folder, $"Order_{orderId}", fileName);
                        }

                        Console.WriteLine($"Loading image: {resolvedPath}");

                        row.Cells[photoCol].Value = LoadImageOrNull(resolvedPath);
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

            // Add Edit/Delete buttons only if missing
            if (!dgvData.Columns.Contains("Edit")) AddButtonColumn("Edit", "Edit", Color.LightGreen, Color.DarkGreen);
            if (!dgvData.Columns.Contains("Delete")) AddButtonColumn("Delete", "Delete", Color.LightCoral, Color.DarkRed);

            // Hook CellClick for Edit/Delete logic
            // Unbind to avoid duplication
            dgvData.CellClick -= dgvData_CellClick_Orders;
            // Then connect the appropriate handler
            dgvData.CellClick += dgvData_CellClick_Orders;
            // Disable adding new blank row
            dgvData.AllowUserToAddRows = false;
            
        }


        // Insert Main Photo in columns Before/After
        //private void ReplaceWithImageColumn(string colName)
        //{
        //if (!dgvData.Columns.Contains(colName)) return;

        //int colIndex = dgvData.Columns[colName].Index;
        //dgvData.Columns.Remove(colName);

        //DataGridViewImageColumn imgCol = new DataGridViewImageColumn
        //{
        //Name = colName,
        //HeaderText = colName,
        //ImageLayout = DataGridViewImageCellLayout.Zoom
        //};
        //dgvData.Columns.Insert(colIndex, imgCol);
        //}

        private void AddButtonColumn(string name, string text, Color bgColor, Color selectionColor)
        {
            if (dgvData.Columns.Contains(name)) return;

            DataGridViewButtonColumn button = new DataGridViewButtonColumn
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
                    SelectionBackColor = selectionColor,
                    SelectionForeColor = Color.White,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold)
                }
            };
            dgvData.Columns.Add(button);
        }


        // Helper: load image from file or return null
        private Image LoadImageOrNull(string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path)) return null;

                string fullPath = Path.Combine(Application.StartupPath, path);

                // DEBUG: Show the exact path
                //MessageBox.Show($"DB path: '{path}'\nFull: '{fullPath}'\nExists: {File.Exists(fullPath)}", "DEBUG: Image path");

                if (File.Exists(fullPath))
                {
                    // Avoid file locking issues
                    using (var bmpTemp = new Bitmap(fullPath))
                    {
                        return new Bitmap(bmpTemp);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("LoadImageOrNull ERROR: " + ex.Message);
                return null;
            }
        }

        // processing Edit/Delete and Before//After buttons for Orders
        private void dgvData_CellClick_Orders(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            DataGridViewRow row = dgvData.Rows[e.RowIndex];
            string colName = dgvData.Columns[e.ColumnIndex].Name;

            // Adding photos to After Only ChiefAdmin and ServiceManager can add photos
            if (colName == "After" && (userRole == "ChiefAdmin" || userRole == "ServiceManager"))
            {
                int orderId = Convert.ToInt32(row.Cells["orderID"].Value);
                string photoType = "after";
                var paths = GetPhotoPaths(orderId, photoType);

                // Adding photos to After no more than 5
                if (paths.Count < 5)
                {
                    // Select and add photo
                    using (OpenFileDialog dlg = new OpenFileDialog())
                    {
                        dlg.Title = "Add AFTER photo";
                        dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            string srcFile = dlg.FileName;
                            string fileName = Path.GetFileName(srcFile);
                            string destFolder = Path.Combine("Photos", "After", $"Order_{orderId}");
                            if (!Directory.Exists(destFolder))
                                Directory.CreateDirectory(destFolder);
                            string destFile = Path.Combine(destFolder, fileName);

                            // Avoid overwrite
                            int count = 1;
                            string baseFileName = Path.GetFileNameWithoutExtension(fileName);
                            string ext = Path.GetExtension(fileName);
                            while (File.Exists(destFile))
                            {
                                destFile = Path.Combine(destFolder, $"{baseFileName}_{count}{ext}");
                                count++;
                            }

                            File.Copy(srcFile, destFile);

                            // Save relative path
                            string relPath = Path.Combine("Photos", "After", $"Order_{orderId}", Path.GetFileName(destFile));
                            AddPhotoToDb(orderId, relPath, "after");
                            MessageBox.Show("Photo added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadOrders(); // Refresh grid
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You can only add up to 5 AFTER photos.", "Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                

                // If photos exist, open gallery
                //if (paths.Count > 0)
                //{
                    //int startIndex = 0; // I can find main photo here
                    //var gallery = new ImageGalleryForm(paths, startIndex);
                    //gallery.ShowDialog();
                //}
                //return; // Avoid Edit/Delete logic
            }

            // opens a large image and switches photos on click (works in a circle)
            if (colName == "Before" || colName == "After")
            {
                int orderId = Convert.ToInt32(row.Cells["orderID"].Value);
                string photoType = colName.ToLower(); // Before or After
                var paths = GetPhotoPaths(orderId, photoType);

                if (paths.Count > 0)
                {
                    // For simplicity, startIndex = 0
                    int startIndex = 0;
                    var gallery = new ImageGalleryForm(paths, startIndex);
                    gallery.ShowDialog();
                    
                }
                return; // To avoid duplicate Edit/Delete logic
            }

            // Existing logic for Edit/Delete:
            if (colName == "Edit")
            {
                int orderId = Convert.ToInt32(row.Cells["orderID"].Value);
                UpdateOrder(row, orderId);
            }
            else if (colName == "Delete")
            {
                int orderId = Convert.ToInt32(row.Cells["orderID"].Value);
                DeleteOrder(orderId);
            }
        }

        // Method adding photos to the database
        private void AddPhotoToDb(int orderId, string imagePath, string photoType)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                // Make sure only one isMain=1 per order/photoType
                string checkMainSql = "SELECT COUNT(*) FROM photos WHERE orderID=@oid AND photoType=@ptype AND isMain=1";
                using (var checkCmd = new MySqlCommand(checkMainSql, conn))
                {
                    checkCmd.Parameters.AddWithValue("@oid", orderId);
                    checkCmd.Parameters.AddWithValue("@ptype", photoType);
                    int hasMain = Convert.ToInt32(checkCmd.ExecuteScalar());

                    string insertSql = @"INSERT INTO photos (orderID, imagePath, photoType, isMain)
                                 VALUES (@oid, @path, @ptype, @main)";
                    using (var cmd = new MySqlCommand(insertSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@oid", orderId);
                        cmd.Parameters.AddWithValue("@path", imagePath.Replace("\\", "\\\\")); // For DB, double-backslashes are safer
                        cmd.Parameters.AddWithValue("@ptype", photoType);
                        cmd.Parameters.AddWithValue("@main", hasMain == 0 ? 1 : 0); // First photo = main
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }


        // Method for editing orders
        private void UpdateOrder(DataGridViewRow row, int orderId)
        {
            string productType = row.Cells["productType"].Value?.ToString();
            string color = row.Cells["color"].Value?.ToString();
            string specifications = row.Cells["specifications"].Value?.ToString();
            string status = row.Cells["status"].Value?.ToString();
            string address = row.Cells["address"].Value?.ToString();
            string delivery = row.Cells["delivery"].Value?.ToString();

            using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;password=;database=powdercoatingdb;"))
            {
                string query = @"
            UPDATE orders 
            SET productType = @productType, color = @color, specifications = @specs,
                status = @status, address = @address, delivery = @delivery,
                updatedDate = CURRENT_TIMESTAMP
            WHERE orderID = @orderID";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@productType", productType);
                cmd.Parameters.AddWithValue("@color", color);
                cmd.Parameters.AddWithValue("@specs", specifications);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@delivery", delivery);
                cmd.Parameters.AddWithValue("@orderID", orderId);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Order updated successfully.");
                    LoadOrders();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to update order: " + ex.Message);
                }
            }
        }

        // Order deletion method
        private void DeleteOrder(int orderId)
        {
            var confirm = MessageBox.Show("Are you sure you want to delete this order?", "Delete", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;password=;database=powdercoatingdb;"))
                {
                    string query = "DELETE FROM orders WHERE orderID = @orderID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@orderID", orderId);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Order deleted successfully.");
                        LoadOrders();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to delete order: " + ex.Message);
                    }
                }
            }
        }
        // returns a List<string> with paths to photos for this order and type before/after
        private List<string> GetPhotoPaths(int orderId, string photoType)
        {
            var paths = new List<string>();
            string sql = "SELECT imagePath FROM photos WHERE orderID = @oid AND photoType = @ptype ORDER BY photoID";
            using (var conn = new MySqlConnection("server=localhost;user=root;password=;database=powdercoatingdb;")) // тут явно вкажи рядок
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@oid", orderId);
                cmd.Parameters.AddWithValue("@ptype", photoType);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        paths.Add((reader["imagePath"]?.ToString() ?? "").Trim());
                    }
                }
            }
            return paths;
        }


        // Loads all Customers into DataGridView
        private void LoadCustomers()
        {
            currentGridMode = GridMode.Customers;
            // clear previous data
            dgvData.DataSource = null;
            dgvData.Columns.Clear();

            // Unbind all click handlers
            dgvData.CellClick -= dgvData_CellClick_Orders;
            dgvData.CellClick -= dgvData_CellClick;

            // Bind customer handler
            dgvData.CellClick += dgvData_CellClick;

            dgvData.RowHeadersWidth = 22;
            if (dgvData.Columns.Contains("customerID"))
            {
                dgvData.Columns["customerID"].Width = 27;
                dgvData.Columns["customerID"].Resizable = DataGridViewTriState.False;
            }

            string query = @"SELECT c.customerID, u.name, u.email, u.phoneNumber, c.address, c.paymentInfo, u.avatar, u.creationDate   
                         FROM customers c 
                         JOIN users u ON c.userID = u.userID";

            dgvData.AutoGenerateColumns = true;
            LoadDataIntoGrid(query);

            if (dgvData.Columns.Contains("customerID"))
            {
                dgvData.Columns["customerID"].Width = 27;
                dgvData.Columns["customerID"].Resizable = DataGridViewTriState.False;
            }

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

            dgvData.AllowUserToAddRows = false;
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


        // dropdown list for paymentInfo and status Allow Typing for "Other" Option
        private void dgvData_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is ComboBox cb)
            //if (dgvData.CurrentCell.ColumnIndex == dgvData.Columns["paymentInfo"].Index && e.Control is ComboBox cb)
            {
                string colName = dgvData.Columns[dgvData.CurrentCell.ColumnIndex].Name;

                //cb.DropDownStyle = ComboBoxStyle.DropDown;

                // Remove existing handler to prevent duplication
                cb.SelectedIndexChanged -= PaymentInfoComboBox_SelectedIndexChanged;
                //cb.SelectedIndexChanged += PaymentInfoComboBox_SelectedIndexChanged;

                // Prevent editing predefined items
                cb.KeyPress -= PaymentInfoComboBox_KeyPress;
                //cb.KeyPress += PaymentInfoComboBox_KeyPress;

                // Allow 'Other: ...' values without exceptions
                //string value = dgvData.CurrentCell.Value?.ToString();
                //if (!string.IsNullOrWhiteSpace(value) && value.StartsWith("Other:") && !cb.Items.Contains(value))
                //
                //cb.Items.Add(value);
                //cb.SelectedItem = value;
                //}

                // Add current value if it's a valid custom Other: value
                //string currentVal = dgvData.CurrentCell.Value?.ToString();
                //if (!string.IsNullOrWhiteSpace(currentVal) && currentVal.StartsWith("Other:") && !cb.Items.Contains(currentVal))
                //{
                //cb.Items.Add(currentVal);
                //cb.SelectedItem = currentVal;
                //}

                if (colName == "paymentInfo")
                {
                    cb.DropDownStyle = ComboBoxStyle.DropDown;

                    cb.SelectedIndexChanged += PaymentInfoComboBox_SelectedIndexChanged;
                    cb.KeyPress += PaymentInfoComboBox_KeyPress;

                    string value = dgvData.CurrentCell.Value?.ToString();
                    if (!string.IsNullOrWhiteSpace(value) && value.StartsWith("Other:") && !cb.Items.Contains(value))
                    {
                        cb.Items.Add(value);
                        cb.SelectedItem = value;
                    }
                }
                else if (colName == "status")
                {
                    cb.DropDownStyle = ComboBoxStyle.DropDownList;
                }
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
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var colName = dgvData.Columns[e.ColumnIndex].Name;
            var row = dgvData.Rows[e.RowIndex];

            if (colName == "Edit")
            {
                switch (currentGridMode)
                {
                    case GridMode.Orders:
                        int orderId = Convert.ToInt32(row.Cells["orderID"].Value);
                        UpdateOrder(row, orderId);
                        break;
                    case GridMode.Customers:
                        int customerId = Convert.ToInt32(row.Cells["customerID"].Value);
                        EditCustomer(customerId);
                        break;
                    default:
                        MessageBox.Show("Unknown grid mode.");
                        break;
                }
            }
            else if (colName == "Delete")
            {
                switch (currentGridMode)
                {
                    case GridMode.Orders:
                        int orderId = Convert.ToInt32(row.Cells["orderID"].Value);
                        DeleteOrder(orderId);
                        break;
                    case GridMode.Customers:
                        int customerId = Convert.ToInt32(row.Cells["customerID"].Value);
                        DeleteCustomer(customerId);
                        break;
                    default:
                        MessageBox.Show("Unknown grid mode.");
                        break;
                }
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
            currentGridMode = GridMode.Notifications;
            // clear previous data
            dgvData.DataSource = null;
            dgvData.Columns.Clear();

            string query = @"SELECT n.notificationID, u.name AS SenderName, o.orderID, n.message, n.sentDate 
                     FROM notifications n
                     LEFT JOIN users u ON n.userID = u.userID
                     LEFT JOIN orders o ON n.orderID = o.orderID
                     ORDER BY n.sentDate DESC";

            LoadDataIntoGrid(query);
            dgvData.AutoGenerateColumns = true;
        }


        private void LoadDataIntoGrid(string query)
        {
            // Always disable the “new row” placeholder
            dgvData.AllowUserToAddRows = false; // here
            // Unhook any attached events that might have been wired previously
            dgvData.CellClick -= dgvData_CellClick;
            dgvData.CellClick -= dgvData_CellClick_Orders;
            dgvData.CellValidating -= dgvData_CellValidating;
            dgvData.DataError -= dgvData_DataError;
            dgvData.EditingControlShowing -= dgvData_EditingControlShowing;
            // Wipe out any previous binding or columns
            dgvData.DataSource = null;
            dgvData.Columns.Clear();

            // Fill a DataTable from the database
            DataTable dt = new DataTable();
            using (var conn = new MySqlConnection("server=localhost;user=root;password=;database=powdercoatingdb;"))
            using (var adapter = new MySqlDataAdapter(query, conn))
            {
                adapter.Fill(dt);
            }
            
            // Bind the fresh data
            dgvData.DataSource = dt;

            // Set general styling for Grid
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
            //dgvData.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            //dgvData.EnableHeadersVisualStyles = false;

            // Re‑attach common validation and error handlers
            dgvData.CellValidating += dgvData_CellValidating;
            dgvData.DataError += dgvData_DataError;
            dgvData.EditingControlShowing += dgvData_EditingControlShowing;

        }

        private void timerDateTime_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private async void btnBackToHome_Click(object sender, EventArgs e)
        {
            if (userRole == "ChiefAdmin")
            {
                // Only ChiefAdmin goes to AdminManagementForm
                AdminManagementForm adminManagementForm = new AdminManagementForm(loggedInUserId);
                await Animator.FadeOut(this);
                await Animator.FadeIn(adminManagementForm);
            }
            else if (userRole == "ServiceManager")
            {
                // ServiceManager goes to HomeForm (замінити HomeForm на твою домашню форму)
                HomeForm homeForm = new HomeForm();
                await Animator.FadeOut(this);
                await Animator.FadeIn(homeForm);
            }
            else
            {
                // For other roles: show error or redirect somewhere else
                MessageBox.Show("You do not have access to this section.");
            }
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
            var addOrderForm = new AddOrderForm(loggedInUserId, userRole);
            addOrderForm.ShowDialog();
            LoadOrders();
        }

        private void btnViewCustomers_Click(object sender, EventArgs e)
        {
            if (currentGridMode != GridMode.Customers)
            {
                LoadCustomers();
                currentGridMode = GridMode.Customers;
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnViewOrders_Click(object sender, EventArgs e)
        {
            if (currentGridMode != GridMode.Orders)
            {
                LoadOrders();
                currentGridMode = GridMode.Orders;
            }
        }

        private void btnNotifications_Click(object sender, EventArgs e)
        {
            if (currentGridMode != GridMode.Notifications)
            {
                LoadNotifications();
                currentGridMode = GridMode.Notifications;
            }
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
