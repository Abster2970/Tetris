using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public class NotSelectableButton : Button
    {
        public NotSelectableButton()
        {
            this.SetStyle(ControlStyles.Selectable, false);
        }
    }
}
