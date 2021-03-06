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
    /// Класс, выполняющий перемещения фигур.
    /// </summary>
    public class СhangeMove : IFigureCommand
    {
        /// <summary>
        /// Переменная, хранящая скопированый список выделенных фигур.ы
        /// </summary>
        private List<ObjectFugure> _seleckResult;

        /// <summary>
        /// Переменная, хранящая положение фигуры до перемещения.
        /// </summary>
        private GraphicsPath [] _pathUndo;

        /// <summary>
        /// Переменная, хранящая положение фигуры после перемещения.
        /// </summary>
        private GraphicsPath [] _pathRedo;

        /// <summary>
        /// Переменная, хранящая строку с текущим действием.
        /// </summary>
        private string _operatorValue;

        public void AddFigure(ObjectFugure DrawObject, List<PointF> Points, List<ObjectFugure> Figures) { }

        /// <summary>
        /// Метод, выполняющий изменения цвета заливки у выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая список выделенных фигур</para>
        public СhangeMove(List<ObjectFugure> SeleckResult)
        {
            _seleckResult = SeleckResult.GetRange(0, SeleckResult.Count);

            _pathRedo = new GraphicsPath[_seleckResult.Count];

            _pathUndo = new GraphicsPath[SeleckResult.Count];

            int i = 0;
            foreach (ObjectFugure SelectObjectResult in _seleckResult)
            {
                _pathUndo[i] = (GraphicsPath)SelectObjectResult.PathClone.Clone();
                i++;
            }

            _operatorValue = "Перемещение объеста";

        }

        /// <summary>
        /// Метод, выполняющий сохранения координат фигуры после перемещения.
        /// </summary>
        public void СhangeMoveEnd(List<ObjectFugure> SeleckResult)
        {

            _pathRedo = new GraphicsPath[SeleckResult.Count];

            int i = 0;
            foreach (ObjectFugure SelectObject in SeleckResult)
            {
                _pathRedo[i] = (GraphicsPath)SelectObject.PathClone.Clone();

                i++;
            }

            _operatorValue = "Перемещение объекта";

        }

        /// <summary>
        /// Метод, выполняющий действие "Повторить".
        /// </summary>
        public void Redo()
        {

            int i = 0;
            foreach (ObjectFugure ObjectRedo in _seleckResult)
            {
                ObjectRedo.Path = (GraphicsPath)_pathRedo[i].Clone();
              
                i++;
            }

            _operatorValue = "Перемещение объекта";

        }
        /// <summary>
        /// Метод, возвращающий строку с текущим действием.
        /// </summary>
        public void Undo()
        {

            int i = 0;
            foreach (ObjectFugure ObjectUndo in _seleckResult)
            {
                ObjectUndo.Path = (GraphicsPath)_pathUndo[i].Clone();
               
                i++;
                _operatorValue = "Отмена перемещение объекта";
            }
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
