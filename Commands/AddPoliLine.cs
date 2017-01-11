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
    class AddPoliLine : IFigureCommand
    {
        private List<PointF> _points;
        private Object _drawObject;
        private List<Object> _figures;
        private string _operatorValue;

        public void PaintFigure(PaintEventArgs e, List<PointF> Points, Pen PenFigure)
        {
            if (_points.Count > 1)
            {
                PointF[] PointPoliLine = Points.ToArray();

                e.Graphics.DrawLines(PenFigure, PointPoliLine);
            }
        }

        public void AddFigure(Object DrawObject, List<PointF> Points, List<Object> Figures)
        {
            _drawObject = DrawObject;
            _points = Points;
            _figures = Figures;

            PointF[] PointPoliLine = _points.ToArray();

            _drawObject.Path.AddLines(PointPoliLine);
            _operatorValue = "Добавление полилинии";

        }

        public void Execute()
        {
            _figures.Insert(_drawObject.IdFigure, _drawObject);
            _operatorValue = "Добавление полилинии";
        }

        public void Undo()
        {
            _figures.RemoveAt(_drawObject.IdFigure);
            _operatorValue = "Удаление полилинии";
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
