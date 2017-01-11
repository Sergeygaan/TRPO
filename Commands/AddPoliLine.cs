using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    class AddPoliLine : IFigureCommand
    {
        private List<PointF> _points;
        private Object _drawObject;
        private List<Object> _figures;
        public void PaintFigure(PaintEventArgs e, List<PointF> _points, Pen _penFigure)
        {
            if (_points.Count > 1)
            {
                PointF[] PointPoliLine = _points.ToArray();

                e.Graphics.DrawLines(_penFigure, PointPoliLine);
            }
        }

        public void AddFigure(Object DrawObject, List<PointF> Points, List<Object> Figures)
        {
            _drawObject = DrawObject;
            _points = Points;
            _figures = Figures;

            PointF[] PointPoliLine = _points.ToArray();

            _drawObject.Path.AddLines(PointPoliLine);
        }

        public void Execute()
        {
            _figures.Add(_drawObject);
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
            _figures.Remove(_drawObject);
        }

    }
}
