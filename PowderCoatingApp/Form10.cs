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
        public CustomerDashboardForm(int userId)
        {
            InitializeComponent();
            loggedInUserId = userId;
        }

        private void CustomerDashboardForm_Load(object sender, EventArgs e)
        {
            // Use the image from Resources
            this.BackgroundImage = Properties.Resources.BackGroundImage2;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            timerDateTime.Start();
            LoadUserName();
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
