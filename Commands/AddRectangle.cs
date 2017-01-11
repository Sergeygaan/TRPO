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
    class AddRectangle : IFigureCommand
    {
        private СonstructionFigure _ellipse = new СonstructionFigure();
        private List<PointF> _points;
        private Object _drawObject;
        private List<Object> _figures;
        private string _operatorValue;

        public void PaintFigure(PaintEventArgs e, List<PointF> Points, Pen PenFigure)
        {
            e.Graphics.DrawRectangle(PenFigure, _ellipse.ShowRectangle(Points[0], Points[1]));
        }

        public void AddFigure(Object DrawObject, List<PointF> Points, List<Object> Figures)
        {
            _drawObject = DrawObject;
            _points = Points;
            _figures = Figures;
            _drawObject.Path.AddRectangle(_ellipse.ShowRectangle(_points[0], _points[1]));
            _operatorValue = "Добавление прямоугольника";
        }

        public void Execute()
        {
            _figures.Insert(_drawObject.IdFigure, _drawObject);
            _operatorValue = "Добавление прямоугольника";
        }

        public void Undo()
        {
            _figures.RemoveAt(_drawObject.IdFigure);
            _operatorValue = "Удаление прямоугольника";
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
