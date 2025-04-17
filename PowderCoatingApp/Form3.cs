using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowderCoatingApp
{
    public partial class ChooseRoleForm : Form
    {
        public ChooseRoleForm()
        {
            InitializeComponent();
        }

        private void btnRegisterCustomer_Click(object sender, EventArgs e)
        {
            RegistrationForm customerForm = new RegistrationForm();
            customerForm.Show();
            this.Hide();
        }

        private void btnRegisterManager_Click(object sender, EventArgs e)
        {
            AdminRegistrationForm managerForm = new AdminRegistrationForm();
            managerForm.Show();
            this.Hide();
        }

        private void ChooseRoleForm_Load(object sender, EventArgs e)
        {
            // Use the image from Resources
            this.BackgroundImage = Properties.Resources.BackGroundImage2;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            timerDateTime.Start();
        }

        private void btnBackToHome_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }
    }
}
