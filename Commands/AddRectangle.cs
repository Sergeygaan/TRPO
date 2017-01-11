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
        private List<Object> _figures;

        public void PaintFigure(PaintEventArgs e, List<PointF> _points, Pen _penFigure)
        {
            e.Graphics.DrawRectangle(_penFigure, _ellipse.ShowRectangle(_points[0], _points[1]));
        }

        public void AddFigure(Object DrawObject, List<PointF> Points, List<Object> Figures)
        {
            _drawObject = DrawObject;
            _points = Points;
            _figures = Figures;
            _drawObject.Path.AddRectangle(_ellipse.ShowRectangle(_points[0], _points[1]));
        }

        public void Execute()
        {
            _figures.Insert(_drawObject.IdFigure, _drawObject);
        }

        public void Undo()
        {
            _figures.RemoveAt(_drawObject.IdFigure);
        }

        public Object Output()
        {
            return _drawObject;
        }

    }
}
