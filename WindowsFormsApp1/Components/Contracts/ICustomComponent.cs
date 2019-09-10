using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1.Components.Contracts
{
    interface ICustomComponent
    {
        Size Size { get; }
        Point Location { get; set; }
        IEnumerable<Control> Controls { get; }
        bool IsValid { get; }
    }
}
