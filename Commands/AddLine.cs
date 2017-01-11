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
    /// Класс, содержащий комманды для построения линии.
    /// </summary>
    class AddLine : IFigureCommand
    {
        /// <summary>
        /// Переменная, хранящая опорные точки для построения линии.
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
        /// Метод, выполняющий построение линии.
        /// </summary>
        /// <para name = "DrawObject">Переменная для сохранения созданного объекта</para>
        /// <para name = "Points">Точки для построения линии</para>
        /// <para name = "Figures">Переменная хранащая список всех фигур</para>
        public void AddFigure(Object DrawObject, List<PointF> Points, List<Object> Figures)
        {
            _drawObject = DrawObject;
            _points = Points;
            _figures = Figures;
            _drawObject.Path.AddLine(_points[0], _points[1]);

            _operatorValue = "Добавление линии";
        }

        /// <summary>
        /// Метод, выполняющий действие "Повторить".
        /// </summary>
        public void Redo()
        {
            _figures.Add(null);
            _figures[_drawObject.IdFigure] = _drawObject;
            //MessageBox.Show(_drawObject.Path.PathPoints[1].X.ToString());
            _operatorValue = "Добавление линии";
        }

        /// <summary>
        /// Метод, выполняющий действие "Отменить".
        /// </summary>
        public void Undo()
        {
            _figures.RemoveAt(_drawObject.IdFigure);
            _operatorValue = "Удаление линии";
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
