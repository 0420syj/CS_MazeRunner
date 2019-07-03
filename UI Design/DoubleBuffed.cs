using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Design
{
    public class DoubleBuffered : System.Windows.Forms.Panel
    {
        public DoubleBuffered()
        {
            this.SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer |
            System.Windows.Forms.ControlStyles.UserPaint |
            System.Windows.Forms.ControlStyles.AllPaintingInWmPaint,
            true);

            this.UpdateStyles();
        }
    }
}
