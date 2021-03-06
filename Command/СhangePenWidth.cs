﻿using System;
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
    /// Класс, выполняющий изменение толщины линий у выбранных фигур
    /// </summary>
    public class СhangePenWidth : IFigureCommand
    {
        /// <summary>
        /// Переменная, хранящая скопированый список выделенных фигур.
        /// </summary>
        private List<ObjectFugure> _seleckResult;

        /// <summary>
        /// Переменная, хранящая новую толщину линий.
        /// </summary>
        public int _currentThickness;

        /// <summary>
        /// Переменная, хранящая строку с текущим действием.
        /// </summary>
        private string _operatorValue;

        /// <summary>
        /// Переменная, хранящая кисть для рисования.
        /// </summary>
        private Pen[] _pen;

        public void AddFigure(ObjectFugure DrawObject, List<PointF> Points, List<ObjectFugure> Figures) { }

        /// <summary>
        /// Метод, выполняющий изменения толщины линий у выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая список выделенных фигур</para>
        /// <para name = "CurrentThickness">Переменная, хранящая новую толщину линий.</para>
        public СhangePenWidth(List<ObjectFugure> SeleckResult, int CurrentThickness)
        {
            _currentThickness = CurrentThickness;

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
                Pen CurrentPen = new Pen(SelectObject.Pen.Color);
                CurrentPen.Width = _currentThickness;
                CurrentPen.DashStyle = SelectObject.Pen.DashStyle;

                SelectObject.Pen = CurrentPen;
            }
            _operatorValue = "Изменение толщины линии";
        }

        /// <summary>
        /// Метод, выполняющий действие "Повторить".
        /// </summary>
        public void Redo()
        {
            foreach (ObjectFugure SelectObject in _seleckResult)
            {
                Pen CurrentPen = new Pen(SelectObject.Pen.Color);
                CurrentPen.Width = _currentThickness;
                CurrentPen.DashStyle = SelectObject.Pen.DashStyle;

                SelectObject.Pen = CurrentPen;
            }
            _operatorValue = "Изменение толщины линии";
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
            _operatorValue = "Отмена изменения толщины линии";
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
