using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOL_HappelChristopher
{
    public class VisualSettings
    {
        public static Color ChangeColor(Color color) //parameter used to tell dialog box what the default color should be on.
        {
            ColorDialog dlg = new ColorDialog(); //Instantiates ColorDialog to be displayed to the user.  
            
            dlg.Color = color; // sets the Color of dlg to the parameter so it is on the correct default color
            
            dlg.ShowDialog(); //Displays dlg

            return dlg.Color;
        }
    }
}
