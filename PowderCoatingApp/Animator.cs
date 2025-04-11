using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowderCoatingApp
{
    public static class Animator
    {
        public static async Task FadeIn(Form form, int interval = 10)
        {
            form.Opacity = 0;
            form.Show();
            while (form.Opacity < 1)
            {
                await Task.Delay(interval);
                form.Opacity += 0.05;
            }
        }

        public static async Task FadeOut(Form form, int interval = 10)
        {
            while (form.Opacity > 0)
            {
                await Task.Delay(interval);
                form.Opacity -= 0.05;
            }
            form.Hide();
        }
    }
}
