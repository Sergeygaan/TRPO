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
    /// Класс, выполняющий изменение цвета отрисовки линий опорных точек у выбранных фигур
    /// </summary>
    public class СhangeSupportPenColor : IFigureCommand
    {
        /// <summary>
        /// Переменная, хранящая скопированый список выделенных фигур.
        /// </summary>
        private List<ObjectFugure> _seleckResult;

        /// <summary>
        /// Переменная, хранящая новый цвет линий опорных точек.
        /// </summary>
        private Color _nextColor;

        /// <summary>
        /// Переменная, хранящая цвет линий опорных точек до их изменения.
        /// </summary>
        private Color[] _penColor;

        /// <summary>
        /// Переменная, хранящая строку с текущим действием.
        /// </summary>
        private string _operatorValue;

        public void AddFigure(ObjectFugure DrawObject, List<PointF> Points, List<ObjectFugure> Figures) { }

        /// <summary>
        /// Метод, выполняющий изменения цвета линий у выбранных опорных точек.
        /// </summary>
        /// <para name = "NextColor">Переменная, хранящая новый цвет линий опорных точек.</para>
        /// <para name = "SeleckResult">Переменная, хранящая список выделенных фигур</para>
        public СhangeSupportPenColor(Color NextColor, List<ObjectFugure> SeleckResult)
        {
            _nextColor = NextColor;

            int SummFigure = 0;

            foreach (ObjectFugure SelectObject in SeleckResult)
            {
                SummFigure += SelectObject.SelectListFigure().Count;
            }

            _penColor = new Color[SummFigure];

            _seleckResult = SeleckResult.GetRange(0, SeleckResult.Count);

            int i = 0;
            foreach (ObjectFugure SelectObject in _seleckResult)
            {
                foreach (SupportObjectFugure SuppportObject in SelectObject.SelectListFigure())
                {
                    _penColor[i] = SuppportObject.Pen.Color;
                    i++;
                }
     
            }

            foreach (ObjectFugure SelectObject in _seleckResult)
            {
                foreach (SupportObjectFugure SuppportObject in SelectObject.SelectListFigure())
                {
                    SuppportObject.Pen.Color = _nextColor;

                }

            }

            _operatorValue = "Изменение цвета линии опорных точек";
        }

        /// <summary>
        /// Метод, выполняющий действие "Повторить".
        /// </summary>
        public void Redo()
        {
            foreach (ObjectFugure SelectObject in _seleckResult)
            {
                foreach (SupportObjectFugure SuppportObject in SelectObject.SelectListFigure())
                {
                    SuppportObject.Pen.Color = _nextColor;
                    
                }

            }

            _operatorValue = "Изменение цвета линии опорных точек";
        }

        /// <summary>
        /// Метод, выполняющий действие "Отменить".
        /// </summary>
        public void Undo()
        {
            int i = 0;
            foreach (ObjectFugure SelectObject in _seleckResult)
            {
                foreach (SupportObjectFugure SuppportObject in SelectObject.SelectListFigure())
                {
                    SuppportObject.Pen.Color = _penColor[i];
                    i++;
                }

            }

            _operatorValue = "Отмена изменения цвета линии опорных точек";
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
