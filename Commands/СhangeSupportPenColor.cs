using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyPaint.CORE
{
    /// <summary>
    /// Класс, выполняющий изменение цвета отрисовки линий опорных точек у выбранных фигур
    /// </summary>
    class СhangeSupportPenColor : IFigureCommand
    {
        /// <summary>
        /// Переменная, хранящая скопированый список выделенных фигур.
        /// </summary>
        private List<Object> _seleckResult;

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

        /// <summary>
        /// Метод, выполняющий изменения цвета линий у выбранных фигур.
        /// </summary>
        /// <para name = "NextColor">Переменная, хранящая новый цвет линий опорных точек.</para>
        /// <para name = "SeleckResult">Переменная, хранящая список выделенных фигур</para>
        public СhangeSupportPenColor(Color NextColor, List<Object> SeleckResult)
        {
            _nextColor = NextColor;


            _penColor = new Color[SeleckResult.Count * 4];

            _seleckResult = SeleckResult.GetRange(0, SeleckResult.Count);

            int i = 0;
            foreach (Object SelectObject in _seleckResult)
            {
                foreach (SupportObject SuppportObject in SelectObject.SelectListFigure())
                {
                    _penColor[i] = SuppportObject.Pen.Color;
                    i++;
                }
     
            }

            _operatorValue = "Изменение цвета линии опорных точек";
        }

        /// <summary>
        /// Метод, выполняющий действие "Повторить".
        /// </summary>
        public void Redo()
        {
            foreach (Object SelectObject in _seleckResult)
            {
                foreach (SupportObject SuppportObject in SelectObject.SelectListFigure())
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
            foreach (Object SelectObject in _seleckResult)
            {
                foreach (SupportObject SuppportObject in SelectObject.SelectListFigure())
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

    }
}
