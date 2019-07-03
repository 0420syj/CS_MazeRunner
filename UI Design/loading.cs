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
    public partial class loading : UserControl
    {
        Timer timeback = new Timer();

        public loading()
        {
            InitializeComponent();

            timeback.Tick += new EventHandler(bar_loading);

            defaul_t();
        }
        void defaul_t()
        {
            loading_bar.Height = 4; loading_bar.Width = 0; loading_bar.Left = 350;

            timeback.Interval = 30;

            timeback.Start();
        }

        int load_per = 0;
        void bar_loading(object sender, EventArgs e)
        {
            loading_bar.Width += load_per;

            if(loading_bar.Width < 1)
            {
                load_per = 16;
            }
            if(loading_bar.Width > 720)
            {
                loading_bar.Width = 0;
            }
        }
    }
}
