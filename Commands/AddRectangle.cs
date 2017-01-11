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
    /// Класс, содержащий комманды для построения прямоугольника.
    /// </summary>
    class AddRectangle : IFigureCommand
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
        /// Метод, выполняющий построение прямоугольника.
        /// </summary>
        /// <para name = "DrawObject">Переменная для сохранения созданного объекта</para>
        /// <para name = "Points">Точки для построения прямоугольника</para>
        /// <para name = "Figures">Переменная хранащая список всех фигур</para>
        public void AddFigure(Object DrawObject, List<PointF> Points, List<Object> Figures)
        {
            _drawObject = DrawObject;
            _points = Points;
            _figures = Figures;
            _drawObject.Path.AddRectangle(_сonstructionFigure.ShowRectangle(_points[0], _points[1]));
            _operatorValue = "Добавление прямоугольника";
        }

        /// <summary>
        /// Метод, выполняющий действие "Повторить".
        /// </summary>
        public void Redo()
        {
            _figures.Insert(_drawObject.IdFigure, _drawObject);
            _operatorValue = "Добавление прямоугольника";
        }

        /// <summary>
        /// Метод, выполняющий действие "Отменить".
        /// </summary>
        public void Undo()
        {
            _figures.RemoveAt(_drawObject.IdFigure);
            _operatorValue = "Удаление прямоугольника";
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
