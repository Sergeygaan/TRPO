using Microsoft.Practices.Unity;
using MyPaint.Command;
using MyPaint.Core;
using MyPaint.ObjectType;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using Unity;

namespace Core
{
    public class ParameterChanges
    {
        /// <summary>
        /// Переменная, хранящая класс для отрисовки и сохранения фигур.
        /// </summary>
        private DrawPaint _drawClass;

        /// <summary>
        /// Переменная, хранящая класс с командой для отчистки рабочей области.
        /// </summary>
        private CleanFigure _cleanFigure;

        private Сommands _commandClass;


        /// <summary>
        /// Переменная, хранящая список команд.
        /// </summary>
        private List<IFigureCommand> _iFigureCommandBuild = new List<IFigureCommand>();

        public ParameterChanges(DrawPaint DrawClass, Сommands CommandClass)
        {
            _drawClass = DrawClass;
            _commandClass = CommandClass;
            _iFigureCommandBuild.Add(_cleanFigure);
            
        }

        /// <summary>
        /// Метод, выполняющий копирование выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая  список выделенных фигур.</para>
        public void ReplicationFigure(List<ObjectFugure> SeleckResult, ReplicationFigure _replicationFigure)
        {
            if (SeleckResult.Count != 0)
            {
                _drawClass.EditFigure();

                _iFigureCommandBuild[0] = _replicationFigure;
                _commandClass.AddCommand(_iFigureCommandBuild);

            }

        }

        /// <summary>
        /// Метод, выполняющий удаление выбранных фигуры.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая  список выделенных фигур.</para>
        public void DeleteFigure(List<ObjectFugure> SeleckResult, DeleteFigure _deleteFigure)
        {
            if (SeleckResult.Count != 0)
            {
                _drawClass.EditFigure();

                _iFigureCommandBuild[0] = _deleteFigure;
                _commandClass.AddCommand(_iFigureCommandBuild);

            }

        }

        /// <summary>
        /// Метод, выполняющий удаление фона у выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая  список выделенных фигур.</para>
        public void DeleteBackgroundFigure(List<ObjectFugure> SeleckResult, DeleteBackgroundFigure _deleteBrush)
        {

            if (SeleckResult.Count != 0)
            {
                _drawClass.EditFigure();

                _iFigureCommandBuild[0] = _deleteBrush;
                _commandClass.AddCommand(_iFigureCommandBuild);

            }
        }

        /// <summary>
        /// Метод, выполняющий изменение цвета фона у выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая  список выделенных фигур.</para>
        /// <para name = "ColorСhangeBackground">Переменная, хранящая новый цвет фона.</para>
        public void СhangeBackgroundFigure(List<ObjectFugure> SeleckResult, СhangeBackgroundFigure _brushColor)
        {
            if (SeleckResult.Count != 0)
            {
                _drawClass.EditFigure();

                _iFigureCommandBuild[0] = _brushColor;
                _commandClass.AddCommand(_iFigureCommandBuild);

            }

        }

        /// <summary>
        /// Метод, выполняющий изменение цвета кисти у выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая  список выделенных фигур.</para>
        /// <para name = "PenColor">Переменная, хранящая новый цвет кисти.</para>
        public void СhangePenColorFigure(List<ObjectFugure> SeleckResult, СhangePenColor _penColor)
        {
            if (SeleckResult.Count != 0)
            {
                _drawClass.EditFigure();

                _iFigureCommandBuild[0] = _penColor;
                _commandClass.AddCommand(_iFigureCommandBuild);
            }

        }

        /// <summary>
        /// Метод, выполняющий изменение толщины пера у выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая список выделенных фигур.</para>
        public void СhangePenWidthFigure(List<ObjectFugure> SeleckResult, СhangePenWidth _penWidth)
        {
            if (SeleckResult.Count != 0)
            {
                _drawClass.EditFigure();

                _iFigureCommandBuild[0] = _penWidth;
                _commandClass.AddCommand(_iFigureCommandBuild);

            }

        }

        /// <summary>
        /// Метод, выполняющий изменение положения фигуры при перемещении.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая список выделенных фигур.</para>
        /// <para name = "Boot">Переменная, хранящая текущее действие над фигурой.</para>
        public void СhangeMoveFigure(List<ObjectFugure> SeleckResult, string Boot, СhangeMove _penMove)
        {
            if (SeleckResult.Count != 0)
            {
                if (Boot == "Down")
                {
                    _drawClass.EditFigure();

                    var UnityContainerInit = new UnityContainer();

                    _penMove = UnityContainerInit.Resolve<СhangeMove>(new OrderedParametersOverride(new object[] { SeleckResult }));

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
        public void СhangePenStyleFigure(List<ObjectFugure> SeleckResult, СhangePenStyle _penStyle)
        {
            if (SeleckResult.Count != 0)
            {
                _drawClass.EditFigure();

                _iFigureCommandBuild[0] = _penStyle;
                _commandClass.AddCommand(_iFigureCommandBuild);

            }

        }

        /// <summary>
        /// Метод, выполняющий изменения цвета линий у опорных точек.
        /// </summary>
        /// <para name = "NextColor">Переменная, хранящая новый цвет опорных точек.</para>
        /// <para name = "SeleckResult">Переменная, хранящая список выделенных фигур.</para>
        public void СhangeSupportPenStyleFigure(СhangeSupportPenColor _supportPenColor)
        {
            _drawClass.EditFigure();

            _iFigureCommandBuild[0] = _supportPenColor;
            _commandClass.AddCommand(_iFigureCommandBuild);

        }

        /// <summary>
        /// Метод, выполняющий отчищает список с фигурами.
        /// </summary>
        public void Clear(CleanFigure _cleanFigure)
        {
            if (_drawClass.FiguresList.Count != 0)
            {
                _drawClass.EditFigure();

                _iFigureCommandBuild[0] = _cleanFigure;
                _commandClass.AddCommand(_iFigureCommandBuild);
            }
        }
    }
}
