using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    [Serializable]
    class RectangleSelect : IFigureBuild
    {
        private СonstructionFigure _ellipse = new СonstructionFigure();
        private Pen _penFigureSelect = new Pen(Color.Black, 1);


        public void PaintFigure(PaintEventArgs e, List<PointF> Points, Pen PenFigure)
        {
            e.Graphics.DrawRectangle(PenFigure, _ellipse.ShowRectangle(Points[0], Points[1]));
        }

        public void AddFigure(Object DrawObject, List<PointF> Points, List<IFigureCommand> FiguresBuild, List<Object> Figures) { }

        public void AddSupportPoint(Object SelectObject) { }

        public void ScaleSelectFigure(Object SelectObject, SupportObject SupportObj, int DeltaX, int DeltaY, EditObject EdipParametr) { }

        public void ScaleFigure(MouseEventArgs e, Object DrawObject, List<Object> SelectedFigures) { }

    }
}
