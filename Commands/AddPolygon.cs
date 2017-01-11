using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    [Serializable]
    class AddPolygon : IFigureCommand
    {
        private List<PointF> _points;
        private Object _drawObject;
        private List<Object> _figures;
        private string _operatorValue;
        public void PaintFigure(PaintEventArgs e, List<PointF> _points, Pen _penFigure)
        {
            if (_points.Count > 1)
            {
                PointF[] PointPolygon = _points.ToArray();

                e.Graphics.DrawLines(_penFigure, PointPolygon);
            }
        }

        public void AddFigure(Object DrawObject, List<PointF> Points, List<Object> Figures)
        {
            _drawObject = DrawObject;
            _points = Points;
            _figures = Figures;

            PointF[] PointPolygon = _points.ToArray();

            _drawObject.Path.AddLines(PointPolygon);

            _drawObject.Path.CloseFigure();
            _operatorValue = "Добавление многоугольника";
        }

        public void Execute()
        {
            _figures.Insert(_drawObject.IdFigure, _drawObject);
            _operatorValue = "Добавление многоугольника";
        }

        public void Undo()
        {
            _figures.RemoveAt(_drawObject.IdFigure);
            _operatorValue = "Удаление многоугольника";
        }

        public string Operation()
        {
            return _operatorValue;
        }

        public Object Output()
        {
            return _drawObject;
        }

    }
}
