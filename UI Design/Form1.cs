using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 

namespace UI_Design
{
    

    public partial class Form1 : Form
    {
        public int wall_type = 1;
        public int sound_on = 1;
        public int char_type = 1;


        public Form1()
        {
            InitializeComponent();
            
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            SidePanel.Top = btnHome.Top;
            home1.BringToFront();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
           
            SidePanel.Top = btnPlay.Top;
            //playSelect1 = new PlaySelect(wall_type);
            playSelect1.wall_type = wall_type;
            playSelect1.sound_on = sound_on;
            playSelect1.char_type = char_type;
            playSelect1.BringToFront();

        }

        private void btnCredit_Click(object sender, EventArgs e)
        {
            SidePanel.Top = btnCredit.Top;
            credit1.BringToFront();
        }

        private void btnRank_Click(object sender, EventArgs e)
        {
            SidePanel.Top = btnRank.Top;
            rank1.BringToFront();
        }

        private void btnOption_Click(object sender, EventArgs e)
        {
            SidePanel.Top = btnOption.Top;
            option1.BringToFront();
            
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            home1.BringToFront();
           

        }

        private void btnThemeBlack_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(41, 39, 40);
            panel2.BackColor = Color.FromArgb(41, 39, 40);
            BackColor = SystemColors.AppWorkspace;
            lbl_themeColor.ForeColor = Color.White;
            lblWall.ForeColor = Color.White;
            label1.ForeColor = Color.White;
            label2.ForeColor = Color.White;
            Point p = new Point(455, 70);
            select1.Location = p;
            
        }

        private void btnThemeBlue_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Orange;
            panel2.BackColor = Color.Orange;
            BackColor = Color.Beige;
            lbl_themeColor.ForeColor = Color.Orange;
            lblWall.ForeColor = Color.Orange;
            label1.ForeColor = Color.Orange;
            label2.ForeColor = Color.Orange;
            Point p = new Point(586, 70);
            select1.Location = p;
        }

        private void btnThemeCap_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(192, 192, 255);
            panel2.BackColor = Color.FromArgb(192, 192, 255);
            BackColor = Color.SeaShell;
            lbl_themeColor.ForeColor = Color.FromArgb(192, 192, 255);
            lblWall.ForeColor = Color.FromArgb(192, 192, 255);
            label1.ForeColor = Color.FromArgb(192, 192, 255);
            label2.ForeColor = Color.FromArgb(192, 192, 255);
            Point p = new Point(520, 70);
            select1.Location = p;
        }

        private void btnThemeBasy_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(255, 224, 192);
            panel2.BackColor = Color.FromArgb(255, 224, 192);
            BackColor = Color.SeaShell;
            lbl_themeColor.ForeColor = Color.FromArgb(255, 224, 192);
            lblWall.ForeColor = Color.FromArgb(255, 224, 192);
            label1.ForeColor = Color.FromArgb(255, 224, 192);
            label2.ForeColor = Color.FromArgb(255, 224, 192);
            Point p = new Point(653, 70);
            select1.Location = p;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            wall_type = 1;
            Point p = new Point(455, 198);
            select2.Location = p;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            wall_type = 2;
            Point p = new Point(520, 198);
            select2.Location = p;
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            wall_type = 3;
            Point p = new Point(586, 198);
            select2.Location = p;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            wall_type = 4;
            Point p = new Point(653, 198);
            select2.Location = p;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            sound_on = 1;
            Point p = new Point(455, 324);
            select3.Location = p;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            sound_on = 2;
            Point p = new Point(586, 324);
            select3.Location = p;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            char_type = 3;
            Point p = new Point(653, 447);
            select4.Location = p;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            char_type = 2;
            Point p = new Point(554, 447);
            select4.Location = p;
        }

        private void CharZ_front_Click(object sender, EventArgs e)
        {
            char_type = 1;
            Point p = new Point(455, 447);
            select4.Location = p;
        }
    }
}
