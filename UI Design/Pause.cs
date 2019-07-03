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
    public partial class Pause : UserControl
    {
        public Pause()
        {
            InitializeComponent();
            
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void Pause_Load(object sender, EventArgs e)
        {
           
        }
    }
}
