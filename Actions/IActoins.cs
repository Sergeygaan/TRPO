using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPaint.CORE;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MyPaint.Actions
{
    interface IActoins
    {
        public void MouseMove(object sender, MouseEventArgs e, MainForm.FigureType Currentfigure);

        public void MouseUp(object sender, MouseEventArgs e, MainForm.FigureType Currentfigure);

        public void MouseDown(object sender, MouseEventArgs e, MainForm.FigureType Currentfigure);
    }
}
