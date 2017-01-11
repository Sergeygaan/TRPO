using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPaint.CORE;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MyPaint.Actions
{
    interface IActoins
    {
        List<PointF> MouseMove(object sender, MouseEventArgs e, MainForm.FigureType Currentfigure, SelectDraw SelectClass, MainForm.Actions CurrentActions, List<IFigureBuild> FiguresBuild);

        void MouseUp(object sender, MouseEventArgs e, MainForm.FigureType Currentfigure, SelectDraw SelectClass, DrawPaint DrawClass, List<IFigureBuild> FiguresBuild);

        void MouseDown(object sender, MouseEventArgs e, MainForm.FigureType Currentfigure, SelectDraw SelectClass, DrawPaint DrawClass, List<IFigureBuild> FiguresBuild);
    }
}
