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
    public partial class AddOrderForm : Form
    {
        private int loggedInUserId;
        private string userRole;// // Сustomer, Service Manager, Chief Admin
        private List<string> photoPathsBefore = new List<string>();

        public AddOrderForm(int userId)
        {
            InitializeComponent();
            loggedInUserId = userId;
            userRole = role;
        }

        private void AddOrderForm_Load(object sender, EventArgs e)
        {
            // Use the image from Resources
            this.BackgroundImage = Properties.Resources.BackGroundImage2;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            timerDateTime.Start();
            
            cmbDelivery.Items.AddRange(new string[] { "pickup", "delivery", "pickup/delivery", "no" });

            if (userRole == "customer")
            {
                cmbCustomer.Visible = false;
                lblCustomer.Visible = false;// the drop-down list is not displayed — but the customerId is still needed
                
            }
            else
            {
                LoadCustomers();
            }
        }

        private void LoadCustomers()
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT id, name FROM customers", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbCustomer.Items.Add(new ComboBoxItem(reader.GetString("name"), reader.GetInt32("id")));
                    }
                }
            }
        }
        // method for saving Orders to the database
        private int SaveOrderToDatabase()
        {
            int newOrderId = -1;

            using (var conn = Database.GetConnection())
            {
                conn.Open();

                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Get selected customer ID
                        int customerId = (userRole == "customer")
                            ? loggedInUserId
                            : ((ComboBoxItem)cmbCustomer.SelectedItem).Value;

                        string productType = txtProductType.Text;
                        string color = txtColor.Text;
                        string specifications = txtSpecifications.Text;
                        string address = txtAddress.Text;
                        string delivery = cmbDelivery.SelectedItem?.ToString() ?? "no";

                        // Insert order
                        string insertOrderQuery = @"INSERT INTO orders (customerId, productType, color, specifications, address, delivery, createdBy)
                                            VALUES (@customerId, @productType, @color, @specifications, @address, @delivery, @createdBy);
                                            SELECT LAST_INSERT_ID();";

                        MySqlCommand cmd = new MySqlCommand(insertOrderQuery, conn, transaction);
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        cmd.Parameters.AddWithValue("@productType", productType);
                        cmd.Parameters.AddWithValue("@color", color);
                        cmd.Parameters.AddWithValue("@specifications", specifications);
                        cmd.Parameters.AddWithValue("@address", address);
                        cmd.Parameters.AddWithValue("@delivery", delivery);
                        cmd.Parameters.AddWithValue("@createdBy", loggedInUserId);

                        newOrderId = Convert.ToInt32(cmd.ExecuteScalar());

                        // Save photos to DB
                        foreach (var path in photoPathsBefore)
                        {
                            string insertPhotoQuery = @"INSERT INTO photos (orderId, path, photoType)
                                                VALUES (@orderId, @path, 'before')";
                            MySqlCommand photoCmd = new MySqlCommand(insertPhotoQuery, conn, transaction);
                            photoCmd.Parameters.AddWithValue("@orderId", newOrderId);
                            photoCmd.Parameters.AddWithValue("@path", path); // TODO: Save local path or uploaded path

                            photoCmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error while saving order: " + ex.Message);
                    }
                }
            }

            return newOrderId;
        }


        private void timerDateTime_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private void lblDateTime_Click(object sender, EventArgs e)
        {

        }

        private async void btnBackToHome_Click(object sender, EventArgs e)
        {
            ServiceManagerDashboardForm serviceManagerDashboardForm = new ServiceManagerDashboardForm(loggedInUserId);
            await Animator.FadeOut(this);
            await Animator.FadeIn(serviceManagerDashboardForm);
            
        }
        // Autofill address when selecting a client
        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            int customerId = ((ComboBoxItem)cmbCustomer.SelectedItem).Value;

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT address, paymentInfo FROM customers WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@id", customerId);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtAddress.Text = reader["address"].ToString();
                        cmbPaymentMethod.Text = reader["paymentInfo"].ToString(); // Якщо треба
                    }
                }
            }
        }
        // to store ID + Name
        public class ComboBoxItem
        {
            public string Text { get; set; }
            public int Value { get; set; }

            public ComboBoxItem(string text, int value)
            {
                Text = text;
                Value = value;
            }

            public override string ToString()
            {
                return Text;
            }
        }


        private void lblCustomer_Click(object sender, EventArgs e)
        {

        }

        private void lblProductType_Click(object sender, EventArgs e)
        {

        }

        private void txtProductType_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblColor_Click(object sender, EventArgs e)
        {

        }

        private void lblAddress_Click(object sender, EventArgs e)
        {

        }

        private void lblSpecifications_Click(object sender, EventArgs e)
        {

        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblDelivery_Click_1(object sender, EventArgs e)
        {

        }

        private void btnSubmitOrder_Click(object sender, EventArgs e)
        {
            // Basic validation
            if (cmbCustomer.SelectedItem == null)
            {
                MessageBox.Show("Please select a customer.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtProductType.Text) || string.IsNullOrWhiteSpace(txtColor.Text))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            if (photoPathsBefore.Count == 0)
            {
                MessageBox.Show("Please add at least one photo (Before).");
                return;
            }

            int orderId = SaveOrderToDatabase();

            if (orderId > 0)
            {
                MessageBox.Show("Order saved successfully! Order ID: " + orderId);
                this.Close(); // або navigate назад
            }
        }

        private void btnAddPhotos_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in ofd.FileNames)
                {
                    if (photoPathsBefore.Count < 5)
                    {
                        photoPathsBefore.Add(file);
                        DisplayPhoto(file);
                    }
                    else
                    {
                        MessageBox.Show("Maximum 5 photos allowed.");
                        break;
                    }
                }
            }
        }

        private void DisplayPhoto(string filePath)
        {
            PictureBox pb = new PictureBox();
            pb.Image = Image.FromFile(filePath);
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Width = 127;
            pb.Height = 130;
            flowLayoutPanelPhotos.Controls.Add(pb);
        }

        private void cmbDelivery_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblPayment_Click(object sender, EventArgs e)
        {

        }

        private void cmbPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblPhotos_Click(object sender, EventArgs e)
        {

        }

        private void txtColor_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSpecifications_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }

        private void lblAddOrder_Click(object sender, EventArgs e)
        {

        }

        private void chkMain3_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMain3.Checked)
            {
                chkMain2.Checked = false;
                chkMain1.Checked = false;
                chkMain4.Checked = false;
                chkMain5.Checked = false;
            }
        }
        // only one photo can be the Main
        private void chkMain1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMain1.Checked)
            {
                chkMain2.Checked = false;
                chkMain3.Checked = false;
                chkMain4.Checked = false;
                chkMain5.Checked = false;
            }
        }

        private void chkMain2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMain2.Checked)
            {
                chkMain1.Checked = false;
                chkMain3.Checked = false;
                chkMain4.Checked = false;
                chkMain5.Checked = false;
            }
        }

        private void chkMain4_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMain4.Checked)
            {
                chkMain2.Checked = false;
                chkMain3.Checked = false;
                chkMain1.Checked = false;
                chkMain5.Checked = false;
            }
        }

        private void chkMain5_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMain5.Checked)
            {
                chkMain2.Checked = false;
                chkMain3.Checked = false;
                chkMain4.Checked = false;
                chkMain1.Checked = false;
            }
        }
    }
}
