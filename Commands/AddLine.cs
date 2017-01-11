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
    class AddLine : IFigureCommand
    {
        private List<PointF> _points;
        private Object _drawObject;
        private List<Object> _figures;
        private string _operatorValue;

        public void AddFigure(Object DrawObject, List<PointF> Points, List<Object> Figures)
        {
            _drawObject = DrawObject;
            _points = Points;
            _figures = Figures;
            _drawObject.Path.AddLine(_points[0], _points[1]);

            _operatorValue = "Добавление линии";
        }

        public void Execute()
        {
            _figures.Add(null);
            _figures[_drawObject.IdFigure] = _drawObject;
            //MessageBox.Show(_drawObject.Path.PathPoints[1].X.ToString());
            _operatorValue = "Добавление линии";
        }

        public void Undo()
        {
            _figures.RemoveAt(_drawObject.IdFigure);
            _operatorValue = "Удаление линии";
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
