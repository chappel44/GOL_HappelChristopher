using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOL_HappelChristopher
{
    public partial class ModalSimSpeed : Form
    {
        public ModalSimSpeed(int speed)
        {
            InitializeComponent();
            
            simSpeed_UpDown.Value = speed; //When the box is opened it gets the default simulation speed
        }

        public int GetUDSpeed()
        {
            return (int)simSpeed_UpDown.Value; //return the value of the the up down
        }

    }
}
