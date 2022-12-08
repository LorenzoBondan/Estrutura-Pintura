using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Concatenar
{
    public partial class frmWelcome : Form
    {
        public frmWelcome()
        {
            InitializeComponent();
        }

        private void frmWelcome_Load(object sender, EventArgs e)
        {
            string usuario = Environment.UserName;
            int ponto = usuario.IndexOf('.');
            if (ponto > 0)
            {
                string primeironome = usuario.Substring(0, ponto);
                primeironome = char.ToUpper(primeironome[0]) + primeironome.Substring(1); // primeira letra maiuscula
                label1.Text = "Bem-vindo(a) " + primeironome + "!";
            }
            else
            {
                label1.Text = "Bem-vindo(a) " + usuario + "!";
            }

            //pictureBox1.Parent = label1;

            this.Opacity = 0.0;
            timer1.Start();
            
            label1.ForeColor = Color.FromArgb(this.BackColor.R, this.BackColor.G, this.BackColor.B);
        }

        int[] targetColor = { 0, 0, 0 }; //black
        int[] fadeRGB = new int[3];

        int counter = 0;

        void fadeIn()
        {
            fadeRGB[0] = label1.ForeColor.R;
            fadeRGB[1] = label1.ForeColor.G;
            fadeRGB[2] = label1.ForeColor.B;

            if (fadeRGB[0] > targetColor[0])
                fadeRGB[0]--;
            else if (fadeRGB[0] < targetColor[0])
                fadeRGB[0]++;
            if (fadeRGB[1] > targetColor[1])
                fadeRGB[1]--;
            else if (fadeRGB[1] < targetColor[1])
                fadeRGB[1]++;
            if (fadeRGB[2] > targetColor[2])
                fadeRGB[2]--;
            else if (fadeRGB[2] < targetColor[2])
                fadeRGB[2]++;
            if (fadeRGB[0] == targetColor[0] && fadeRGB[1] == targetColor[1] && fadeRGB[2] == targetColor[2])
                timer2.Stop();

            label1.ForeColor = Color.FromArgb(fadeRGB[0], fadeRGB[1], fadeRGB[2]);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.03;
            if (this.Opacity == 1)
            {
                timer1.Stop();

                this.Hide();
                var form2 = new Form1();
                form2.Closed += (s, args) => this.Close();
                form2.Show();

            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            counter++;
            if (counter > 30)
            {
                fadeIn();
            }
            
        }
    }
}
