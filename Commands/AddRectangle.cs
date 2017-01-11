using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    class AddRectangle : IFigureCommand
    {
        private СonstructionFigure _ellipse = new СonstructionFigure();
        private List<PointF> _points;
        private Object _drawObject;

        public void PaintFigure(PaintEventArgs e, List<PointF> _points, Pen _penFigure)
        {
            e.Graphics.DrawRectangle(_penFigure, _ellipse.ShowRectangle(_points[0], _points[1]));
        }

        public void AddFigure(Object DrawObject, List<PointF> Points)
        {
            _drawObject = DrawObject;
            _points = Points;
        }

        public void Execute()
        {
            _drawObject.Path.AddRectangle(_ellipse.ShowRectangle(_points[0], _points[1]));
        }

        public void Undo()
        {

        }

        public Object Output()
        {
            return _drawObject;
        }

    }
}
