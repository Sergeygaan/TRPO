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
    /// Класс, выполняющий изменение цвета отрисовки линий у выбранных фигур
    /// </summary>
    public class СhangePenColor : IFigureCommand
    {
        /// <summary>
        /// Переменная, хранящая скопированый список выделенных фигур.
        /// </summary>
        private List<ObjectFugure> _seleckResult;

        /// <summary>
        /// Переменная, хранящая новый цвет линий.
        /// </summary>
        public Color _currentColor;

        /// <summary>
        /// Переменная, хранящая цвет линий до их изменения.
        /// </summary>
        private Color [] _penColor;

        /// <summary>
        /// Переменная, хранящая кисть для рисования.
        /// </summary>
        private Pen [] _pen;

        /// <summary>
        /// Переменная, хранящая строку с текущим действием.
        /// </summary>
        private string _operatorValue;

        public void AddFigure(ObjectFugure DrawObject, List<PointF> Points, List<ObjectFugure> Figures) { }

        /// <summary>
        /// Метод, выполняющий изменения цвета линий у выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая список выделенных фигур</para>
        /// <para name = "CurrentColor">Переменная, хранящая новый цвет линий фигур.</para>
        public СhangePenColor(List<ObjectFugure> SeleckResult, Color CurrentColor)
        {
            _currentColor = CurrentColor;

            _penColor = new Color[SeleckResult.Count];
            _pen = new Pen[SeleckResult.Count];

            int i = 0;
            foreach (ObjectFugure SelectObject in SeleckResult)
            {
                _pen[i] = SelectObject.Pen;
                i++;
            }

            _seleckResult = SeleckResult.GetRange(0, SeleckResult.Count);

            foreach (ObjectFugure SelectObject in SeleckResult)
            {
                Pen CurrentPen = new Pen(_currentColor);
                CurrentPen.Width = SelectObject.Pen.Width;
                CurrentPen.DashStyle = SelectObject.Pen.DashStyle;

                SelectObject.Pen = CurrentPen;
            }
            _operatorValue = "Изменение цвета линии";
        }

        /// <summary>
        /// Метод, выполняющий действие "Повторить".
        /// </summary>
        public void Redo()
        {
            foreach (ObjectFugure SelectObject in _seleckResult)
            {
                Pen CurrentPen = new Pen(_currentColor);
                CurrentPen.Width = SelectObject.Pen.Width;
                CurrentPen.DashStyle = SelectObject.Pen.DashStyle;

                SelectObject.Pen = CurrentPen;
            }
            _operatorValue = "Изменение цвета линии";
        }

        /// <summary>
        /// Метод, выполняющий действие "Отменить".
        /// </summary>
        public void Undo()
        {
            int i = 0;
            foreach (ObjectFugure SelectObject in _seleckResult)
            {
                SelectObject.Pen = _pen[i];
                i++;
            }

            _operatorValue = "Отмена изменения цвета линии";
        }

        /// <summary>
        /// Метод, возвращающий строку с текущим действием.
        /// </summary>
        public string Operation()
        {
            return _operatorValue;
        }

        public ObjectFugure Output()
        {
            return null;
        }

    }
}
