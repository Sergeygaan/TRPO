﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    /// <summary>
    /// Класс, выполняющий изменение стиля отрисовки линий у выбранных фигур
    /// </summary>
    class СhangePenStyle : IFigureCommand
    {
        /// <summary>
        /// Переменная, хранящая скопированый список выделенных фигур.
        /// </summary>
        private List<Object> _seleckResult;

        /// <summary>
        /// Переменная, хранящая новый стиль линий.
        /// </summary>
        private DashStyle _dashStyle;

        /// <summary>
        /// Переменная, хранящая кисть для рисования.
        /// </summary>
        private Pen[] _pen;

        /// <summary>
        /// Переменная, хранящая строку с текущим действием.
        /// </summary>
        private string _operatorValue;

        /// <summary>
        /// Метод, выполняющий изменения стиля линий у выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая список выделенных фигур</para>
        /// <para name = "DashStyle">Переменная, хранящая новый стиль линий.</para>
        public СhangePenStyle(List<Object> SeleckResult, DashStyle DashStyle)
        {
            _dashStyle = DashStyle;

            _pen = new Pen[SeleckResult.Count];

            int i = 0;
            foreach (Object SelectObject in SeleckResult)
            {
                _pen[i] = SelectObject.Pen;
                i++;
                
            }

            _seleckResult = SeleckResult.GetRange(0, SeleckResult.Count);

            foreach (Object SelectObject in SeleckResult)
            {
                Pen CurrentPen = new Pen(SelectObject.Pen.Color);
                CurrentPen.Width = SelectObject.Pen.Width;
                CurrentPen.DashStyle = _dashStyle;

                SelectObject.Pen = CurrentPen;
            }
            _operatorValue = "Изменение стиля линии";
        }

        /// <summary>
        /// Метод, выполняющий действие "Повторить".
        /// </summary>
        public void Redo()
        {
            foreach (Object SelectObject in _seleckResult)
            {
                Pen CurrentPen = new Pen(SelectObject.Pen.Color);
                CurrentPen.Width = SelectObject.Pen.Width;
                CurrentPen.DashStyle = _dashStyle;

                SelectObject.Pen = CurrentPen;
            }
            _operatorValue = "Изменение стиля линии";
        }

        /// <summary>
        /// Метод, выполняющий действие "Отменить".
        /// </summary>
        public void Undo()
        {
            int i = 0;
            foreach (Object SelectObject in _seleckResult)
            {
                SelectObject.Pen = _pen[i];
                i++;
            }

            _operatorValue = "Отмена измменения стиля линии";
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
