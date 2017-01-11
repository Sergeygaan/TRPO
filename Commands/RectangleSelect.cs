using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    class RectangleSelect : IFigureBuild
    {
        private СonstructionFigure _ellipse = new СonstructionFigure();
        private Pen _penFigureSelect = new Pen(Color.Black, 1);


        public void PaintFigure(PaintEventArgs e, List<PointF> _points, Pen _penFigure)
        {
            e.Graphics.DrawRectangle(_penFigureSelect, _ellipse.ShowRectangle(_points[0], _points[1]));
        }

        public void AddFigure(Object DrawObject, List<PointF> _points, List<IFigureCommand> _figuresBuild) { }

        public void AddSupportPoint(IFigureCommand SelectObject) { }

        public void ScaleSelectFigure(Object SelectObject, SupportObject SupportObj, int DeltaX, int DeltaY, EditObject EdipParametr) { }

        public void ScaleFigure(MouseEventArgs e, IFigureCommand DrawObject, List<IFigureCommand> SelectedFigures) { }

    }
}
