
using System.Collections.Generic;
using System.Drawing;
using MyPaint.ObjectType;

namespace MyPaint.Command
{
    /// <summary>
    /// Класс, содержащий комманды для построения прямоугольника.
    /// </summary>
    public class AddBuildFigure : IFigureCommand
    {

        /// <summary>
        /// Переменная, хранящая опорные точки для построения прямоугольника.
        /// </summary>
        private List<PointF> _points;

        /// <summary>
        /// Переменная, хранящая ссылку на построенный объект.
        /// </summary>
        private ObjectFugure _drawObject;

        /// <summary>
        /// Переменная, хранящая ссылку на список всех фигур.
        /// </summary>
        private List<ObjectFugure> _figures;

        /// <summary>
        /// Переменная, хранящая строку с текущим действием.
        /// </summary>
        private string _operatorValue;

        private string _TypeFigure;

        /// <summary>
        /// Переменная, хранящая верхнуую координату фигуры.
        /// </summary>
        private int _top;

        /// <summary>
        /// Переменная, хранящая левую координату фигуры.
        /// </summary>       
        private int _left;

        /// <summary>
        /// Переменная, хранящая нижнюю координату фигуры.
        /// </summary>           
        private int _down;

        /// <summary>
        /// Переменная, хранящая правую координату фигуры.
        /// </summary>        
        private int _right;

        /// <summary>
        /// Метод, выполняющий построение прямоугольника.
        /// </summary>
        /// <para name = "DrawObject">Переменная для сохранения созданного объекта</para>
        /// <para name = "Points">Точки для построения прямоугольника</para>
        /// <para name = "Figures">Переменная хранащая список всех фигур</para>
        public void AddFigure(ObjectFugure DrawObject, List<PointF> Points, List<ObjectFugure> Figures)
        {
            _drawObject = DrawObject;
            _points = Points;
            _figures = Figures;
            _drawObject.FigureStart = Points[0];
            _drawObject.FigureEnd = Points[1];
            _drawObject.IdFigure = Figures.Count;

            if (DrawObject.CurrentFigure == 0)
            {
                _drawObject.Path.AddRectangle(ShowRectangle(_points[0], _points[1]));

                _TypeFigure = "прямоугольник";
            }
            if (DrawObject.CurrentFigure == 1)
            {
                _drawObject.Path.AddEllipse(ShowRectangle(_points[0], _points[1]));

                _operatorValue = "эллипс";
            }
            if (DrawObject.CurrentFigure == 2)
            {
                _drawObject.Path.AddLine(_points[0], _points[1]);

                _TypeFigure = "линия";
            }
            if (DrawObject.CurrentFigure == 3)
            {
                PointF[] PointPoliLine = _points.ToArray();

                _drawObject.Path.AddLines(PointPoliLine);
                _TypeFigure = "полилиния";

            }
            if (DrawObject.CurrentFigure == 4)
            {

                PointF[] PointPolygon = _points.ToArray();
                _drawObject.Path.AddLines(PointPolygon);

                _drawObject.Path.CloseFigure();
                _TypeFigure = "многоугольник";
            }
        }

        /// <summary>
        /// Метод, выполняющий пострение структуры фигуры.
        /// </summary>
        /// <para name = "Start">Переменная, хранящая начальную координату фигуры.</para>
        /// <para name = "End">Переменная, хранящая конечную координату фигуры.</para>
        public Rectangle ShowRectangle(PointF Start, PointF End)
        {

            _left = (int)((Start.X - End.X > 0) ? End.X : Start.X);
            _down = (int)((Start.Y - End.Y > 0) ? Start.Y : End.Y);
            _top = (int)((Start.Y - End.Y > 0) ? End.Y : Start.Y);
            _right = (int)((Start.X - End.X > 0) ? Start.X : End.X);

            Rectangle rect = Rectangle.FromLTRB(_left, _top, _right, _down);

            return rect;
        }

        /// <summary>
        /// Метод, выполняющий действие "Повторить".
        /// </summary>
        public void Redo()
        {
            _figures.Insert(_drawObject.IdFigure, _drawObject);
            _operatorValue = "Добавлен" + _TypeFigure;
        }

        /// <summary>
        /// Метод, выполняющий действие "Отменить".
        /// </summary>
        public void Undo()
        {
            _figures.RemoveAt(_drawObject.IdFigure);
            _operatorValue = "Удален " + _TypeFigure;
        }

        /// <summary>
        /// Метод, возвращающий строку с текущим действием.
        /// </summary>
        public string Operation()
        {
            return _operatorValue;
        }

        /// <summary>
        /// Метод, возвращающий фигуру над которой проводятся действия.
        /// </summary>
        public ObjectFugure Output()
        {
            return _drawObject;
        }

    }
}
