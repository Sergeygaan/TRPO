using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using MyPaint.Build;
using MyPaint.Command;
using MyPaint.ObjectType;

namespace MyPaint.Command
{
    /// <summary>
    /// Класс, содержащий комманды для построения прямоугольника.
    /// </summary>
    public class AddBuildFigure : IFigureCommand
    {

        /// <summary>
        /// Переменная, хранящая класс для построения структуры прямоугольника.
        /// </summary>
        private СonstructionFigure _сonstructionFigure = new СonstructionFigure();

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
                _drawObject.Path.AddRectangle(_сonstructionFigure.ShowRectangle(_points[0], _points[1]));

                _TypeFigure = "прямоугольник";
            }
            if (DrawObject.CurrentFigure == 1)
            {
                _drawObject.Path.AddEllipse(_сonstructionFigure.ShowRectangle(_points[0], _points[1]));

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
