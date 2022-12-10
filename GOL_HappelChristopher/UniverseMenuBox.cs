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
    public partial class ModalDialog : Form
    {
        public ModalDialog(int xMax, int yMax)
        {
            InitializeComponent();

            cellRow_UpDown2.Value = xMax;
            cellColumn_UpDown1.Value = yMax;
        }

        //UD stands for up down
        public int GetUDColumn()
        {
            return (int)cellColumn_UpDown1.Value;
        }

        public int GetUDRow()
        {
            return (int)cellRow_UpDown2.Value;
        }
    }
}
