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
    public partial class RandSeedBox : Form
    {
        public RandSeedBox(int seed)
        {
            InitializeComponent();

            seedUD.Value = seed; //sets the value of the up down to the seed parameter
        }

        public int GetSeed()
        {
            return (int)seedUD.Value;
        }
    }
}
