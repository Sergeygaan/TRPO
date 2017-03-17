﻿using MyPaint.Command;
using MyPaint.Core;
using MyPaint.ObjectType;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Core
{
    public class ParameterChanges
    {
        /// <summary>
        /// Переменная, хранящая класс для отрисовки и сохранения фигур.
        /// </summary>
        private DrawPaint _drawClass;

        //Классы комманд
        /// <summary>
        /// Переменная, хранящая класс с командой для изменения размера кисти.
        /// </summary>
        private СhangePenWidth _penWidth;

        /// <summary>
        /// Переменная, хранящая класс с командой для изменения цвета кисти.
        /// </summary>
        private СhangePenColor _penColor;

        /// <summary>
        /// Переменная, хранящая класс с командой для изменения стиля кисти.
        /// </summary>
        private СhangePenStyle _penStyle;

        /// <summary>
        /// Переменная, хранящая класс с командой для изменения перемещения фигур.
        /// </summary>
        private СhangeMove _penMove;

        /// <summary>
        /// Переменная, хранящая класс с командой для изменения цвета заливки.
        /// </summary>
        private СhangeBackgroundFigure _brushColor;

        /// <summary>
        /// Переменная, хранящая класс с командой для удаления заливки.
        /// </summary>
        private DeleteBackgroundFigure _deleteBrush;

        /// <summary>
        /// Переменная, хранящая класс с командой для удаления фигур.
        /// </summary>
        private DeleteFigure _deleteFigure;

        /// <summary>
        /// Переменная, хранящая список с фигурами при загрузке старого проекта.
        /// </summary>
        private List<ObjectFugure> _figuresLoad = new List<ObjectFugure>();

        /// <summary>
        /// Переменная, хранящая класс с командой для копирования фигур.
        /// </summary>
        private ReplicationFigure _replicationFigure;

        /// <summary>
        /// Переменная, хранящая класс с командой для изменения цвета опорных точек.
        /// </summary>
        private СhangeSupportPenColor _supportPenColor;

        /// <summary>
        /// Переменная, хранящая класс с командой для отчистки рабочей области.
        /// </summary>
        private CleanFigure _cleanFigure;

        private UndoRedo _commandClass;

        /// <summary>
        /// Переменная, хранящая список команд.
        /// </summary>
        private List<IFigureCommand> _iFigureCommandBuild = new List<IFigureCommand>();

        public ParameterChanges(DrawPaint DrawClass, UndoRedo CommandClass)
        {
            _drawClass = DrawClass;
            _commandClass = CommandClass;
            _iFigureCommandBuild.Add(_cleanFigure);
        }

        /// <summary>
        /// Метод, выполняющий копирование выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая  список выделенных фигур.</para>
        public void ReplicationFigure(List<ObjectFugure> SeleckResult)
        {
            if (SeleckResult.Count != 0)
            {
                _drawClass.EditFigure();

                _replicationFigure = new ReplicationFigure(SeleckResult, _drawClass.FiguresList);

                _iFigureCommandBuild[0] = _replicationFigure;
                _commandClass.AddCommand(_iFigureCommandBuild);

            }

        }

        /// <summary>
        /// Метод, выполняющий удаление выбранных фигуры.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая  список выделенных фигур.</para>
        public void DeleteFigure(List<ObjectFugure> SeleckResult)
        {
            if (SeleckResult.Count != 0)
            {
                _drawClass.EditFigure();

                _deleteFigure = new DeleteFigure(SeleckResult, _drawClass.FiguresList);

                _iFigureCommandBuild[0] = _deleteFigure;
                _commandClass.AddCommand(_iFigureCommandBuild);

            }

        }

        /// <summary>
        /// Метод, выполняющий удаление фона у выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая  список выделенных фигур.</para>
        public void DeleteBackgroundFigure(List<ObjectFugure> SeleckResult)
        {

            if (SeleckResult.Count != 0)
            {
                _drawClass.EditFigure();

                _deleteBrush = new DeleteBackgroundFigure(SeleckResult);

                _iFigureCommandBuild[0] = _deleteBrush;
                _commandClass.AddCommand(_iFigureCommandBuild);

            }
        }

        /// <summary>
        /// Метод, выполняющий изменение цвета фона у выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая  список выделенных фигур.</para>
        /// <para name = "ColorСhangeBackground">Переменная, хранящая новый цвет фона.</para>
        public void СhangeBackgroundFigure(List<ObjectFugure> SeleckResult, Color ColorСhangeBackground)
        {
            if (SeleckResult.Count != 0)
            {
                _drawClass.EditFigure();

                _brushColor = new СhangeBackgroundFigure(SeleckResult, ColorСhangeBackground);

                _iFigureCommandBuild[0] = _brushColor;
                _commandClass.AddCommand(_iFigureCommandBuild);

            }

        }

        /// <summary>
        /// Метод, выполняющий изменение цвета кисти у выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая  список выделенных фигур.</para>
        /// <para name = "PenColor">Переменная, хранящая новый цвет кисти.</para>
        public void СhangePenColorFigure(List<ObjectFugure> SeleckResult, Color PenColor)
        {
            if (SeleckResult.Count != 0)
            {
                _drawClass.EditFigure();

                _penColor = new СhangePenColor(SeleckResult, PenColor);
                _iFigureCommandBuild[0] = _penColor;
                _commandClass.AddCommand(_iFigureCommandBuild);
            }

        }

        /// <summary>
        /// Метод, выполняющий изменение толщины пера у выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая список выделенных фигур.</para>
        public void СhangePenWidthFigure(List<ObjectFugure> SeleckResult, int thickness)
        {
            if (SeleckResult.Count != 0)
            {
                _drawClass.EditFigure();

                _penWidth = new СhangePenWidth(SeleckResult, thickness);

                _iFigureCommandBuild[0] = _penWidth;
                _commandClass.AddCommand(_iFigureCommandBuild);

            }

        }

        /// <summary>
        /// Метод, выполняющий изменение положения фигуры при перемещении.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая список выделенных фигур.</para>
        /// <para name = "Boot">Переменная, хранящая текущее действие над фигурой.</para>
        public void СhangeMoveFigure(List<ObjectFugure> SeleckResult, string Boot)
        {
            if (SeleckResult.Count != 0)
            {
                if (Boot == "Down")
                {
                    _drawClass.EditFigure();

                    _penMove = new СhangeMove(SeleckResult);
                }
                else
                {
                    _penMove.СhangeMoveEnd(SeleckResult);

                    _iFigureCommandBuild[0] = _penMove;
                    _commandClass.AddCommand(_iFigureCommandBuild);

                }

            }

        }

        /// <summary>
        /// Метод, выполняющий изменения стиля линий у выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая список выделенных фигур.</para>
        public void СhangePenStyleFigure(List<ObjectFugure> SeleckResult, DashStyle dashstyle)
        {
            if (SeleckResult.Count != 0)
            {
                _drawClass.EditFigure();

                _penStyle = new СhangePenStyle(SeleckResult, dashstyle);
                _iFigureCommandBuild[0] = _penStyle;
                _commandClass.AddCommand(_iFigureCommandBuild);

            }

        }

        /// <summary>
        /// Метод, выполняющий изменения цвета линий у опорных точек.
        /// </summary>
        /// <para name = "NextColor">Переменная, хранящая новый цвет опорных точек.</para>
        /// <para name = "SeleckResult">Переменная, хранящая список выделенных фигур.</para>
        public void СhangeSupportPenStyleFigure(Color NextColor, List<ObjectFugure> SeleckResult)
        {
            _drawClass.EditFigure();

            _supportPenColor = new СhangeSupportPenColor(NextColor, SeleckResult);

            _iFigureCommandBuild[0] = _supportPenColor;
            _commandClass.AddCommand(_iFigureCommandBuild);

        }

        /// <summary>
        /// Метод, выполняющий отчищает список с фигурами.
        /// </summary>
        public void Clear()
        {
            if (_drawClass.FiguresList.Count != 0)
            {
                _drawClass.EditFigure();

                _cleanFigure = new CleanFigure(_drawClass.FiguresList, _figuresLoad);

                _iFigureCommandBuild[0] = _cleanFigure;
                _commandClass.AddCommand(_iFigureCommandBuild);
            }
        }
    }
}