using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    class AddEllipse : IFigureCommand
    {
        private СonstructionFigure _ellipse = new СonstructionFigure();
        private List<PointF> _points;
        private Object _drawObject;
        private Object _drawObjectClone;
        private List<Object> _figures;

        public void PaintFigure(PaintEventArgs e, List<PointF> _points, Pen _penFigure)
        {
            e.Graphics.DrawEllipse(_penFigure, _ellipse.ShowRectangle(_points[0], _points[1]));
        }

        public void AddFigure(Object DrawObject, List<PointF> Points, List<Object> Figures)
        {
            _drawObject = DrawObject;
            _points = Points;
            _figures = Figures;

            _drawObject.Path.AddEllipse(_ellipse.ShowRectangle(_points[0], _points[1]));
            _drawObjectClone = DrawObject.CloneObject();
            _drawObjectClone.IdFigure = DrawObject.IdFigure;
        }

        public void Execute()
        {
            _figures.Insert(_drawObjectClone.IdFigure, _drawObjectClone);
        }

        public void Undo()
        {
            UndoFigure();
        }

        public Object Output()
        {
            return _drawObject;
        }
        public void UndoFigure()
        {
            _figures.RemoveAt(_drawObject.IdFigure);
        }
    }
}
