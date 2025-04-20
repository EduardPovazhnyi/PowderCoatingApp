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
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();
            timerDateTime.Start();
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            // Use the image from Resources
            this.BackgroundImage = Properties.Resources.BackGroundImage2;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            timerDateTime.Start();

        }

        private void lblDateTime_Click(object sender, EventArgs e)
        {

        }

        private void timerDateTime_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            //ChooseRoleForm chooseRoleForm = new ChooseRoleForm();
            //chooseRoleForm.Show();
            //this.Hide();
            ChooseRoleForm chooseRoleForm = new ChooseRoleForm();
            await Animator.FadeOut(this);
            await Animator.FadeIn(chooseRoleForm);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

        }
    }
}
