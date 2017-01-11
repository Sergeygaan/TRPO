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

    /// <summary>
    /// Класс, содержащий комманды для построения полилинии.
    /// </summary>
    class AddPoliLine : IFigureCommand
    {
        /// <summary>
        /// Переменная, хранящая опорные точки для построения полилинии.
        /// </summary>
        private List<PointF> _points;

        /// <summary>
        /// Переменная, хранящая ссылку на построенный объект.
        /// </summary>
        private Object _drawObject;

        /// <summary>
        /// Переменная, хранящая ссылку на список всех фигур.
        /// </summary>
        private List<Object> _figures;

        /// <summary>
        /// Переменная, хранящая строку с текущим действием.
        /// </summary>
        private string _operatorValue;


        /// <summary>
        /// Метод, выполняющий построение полилинии.
        /// </summary>
        /// <para name = "DrawObject">Переменная для сохранения созданного объекта</para>
        /// <para name = "Points">Точки для построения полилинии</para>
        /// <para name = "Figures">Переменная хранащая список всех фигур</para>
        public void AddFigure(Object DrawObject, List<PointF> Points, List<Object> Figures)
        {
            _drawObject = DrawObject;
            _points = Points;
            _figures = Figures;

            PointF[] PointPoliLine = _points.ToArray();

            _drawObject.Path.AddLines(PointPoliLine);
            _operatorValue = "Добавление полилинии";

        }

        /// <summary>
        /// Метод, выполняющий действие "Повторить".
        /// </summary>
        public void Redo()
        {
            _figures.Insert(_drawObject.IdFigure, _drawObject);
            _operatorValue = "Добавление полилинии";
        }

        /// <summary>
        /// Метод, выполняющий действие "Отменить".
        /// </summary>
        public void Undo()
        {
            _figures.RemoveAt(_drawObject.IdFigure);
            _operatorValue = "Удаление полилинии";
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
        public Object Output()
        {
            return _drawObject;
        }


    }
}
