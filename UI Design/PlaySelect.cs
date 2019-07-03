using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_Design
{
    public partial class PlaySelect : UserControl
    {
        public int wall_type;
        public int level = 1;
        public int sound_on;
        public int char_type;
        


        public PlaySelect()
        {
            InitializeComponent();
            levelBox.Text = "초급";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (levelBox.Text == "중급")
                level = 2;
            else if (levelBox.Text == "고급")
                level = 3;
            else
                level = 1;

            //FindForm().Visible = false;
            if (!String.IsNullOrEmpty(txtNickname.Text)) 
            {
                
                Forms Form2 = new Forms(txtNickname.Text, wall_type, level, sound_on, char_type);
                Form2.Show();
            }
            else
            {
                MessageBox.Show("닉네임을 입력하세요.");
            }
            
        }
        

        private void txtNickname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsWhiteSpace(e.KeyChar))
            {
                MessageBox.Show("공백문자는 입력 불가합니다.");
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            //FindForm().Visible = false;
            if (!String.IsNullOrEmpty(txtNickname.Text) && !string.IsNullOrEmpty(txtIp.Text))
            {
                MultiForm multiForm = new MultiForm(txtNickname.Text, txtIp.Text);
                multiForm.Show();
            }
            else
            {
                MessageBox.Show("닉네임과 IP를 정확히 입력하세요.");
            }
        }
    }
}
