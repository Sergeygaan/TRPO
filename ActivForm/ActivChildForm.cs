using MyPaint.Actions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MyPaint.Core;
using MyPaint.Build;
using MyPaint.Command;
using MyPaint.ObjectType;
using Core;
using Microsoft.Practices.Unity;
using Unity;

namespace ActivForm
{
    public class ActivChildForm
    {
        /// <summary>
        /// Переменная, хранящая класс с действиями над фигурами.
        /// </summary>
        private EditObject _edipParametr = new EditObject();

        /// <summary>
        /// Переменная, хранящая список точек для построения фигур.
        /// </summary>
        private List<PointF> _points = new List<PointF>();

        /// <summary>
        /// Переменная, хранящая класс для отрисовки и сохранения фигур.
        /// </summary>
        private DrawPaint _drawClass;

        /// <summary>
        /// Переменная, хранящая класс для выделения.
        /// </summary>
        private SelectDraw _selectClass;

        /// <summary>
        /// Переменная, хранящая список классов для построения различных фигур.
        /// </summary>
        private List<IFigureBuild> _figuresBuild = new List<IFigureBuild>();

        /// <summary>
        /// Переменная, хранящая список с фигурами при загрузке старого проекта.
        /// </summary>
        private List<ObjectFugure> _figuresLoad = new List<ObjectFugure>();


        /// <summary>
        /// Переменная, хранящая список действий для построения различных фигур.
        /// </summary>
        private List<IActoins> _actionsBuild = new List<IActoins>();

        /// <summary>
        /// Переменная, хранящая класс с параметрами.
        /// </summary>
        private ParameterChanges _parameterChangesClass;

        /// <summary>
        /// Переменная, хранящая класс unity.
        /// </summary>
        UnityContainer UnityContainerInit = new UnityContainer();

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

        public ActivChildForm(SelectDraw selectClass, DrawPaint drawClass, ParameterChanges parameterChangesClass, List<IFigureBuild> figuresBuild, List<IActoins> actionsBuild)
        {
           
            _selectClass = selectClass;

            _drawClass = drawClass;

            _parameterChangesClass = parameterChangesClass;

            _figuresBuild = figuresBuild.GetRange(0, figuresBuild.Count);

            _actionsBuild = actionsBuild.GetRange(0, actionsBuild.Count);

        }

        public void Child_Paint(PaintEventArgs e, int currentfigure, Color linecolor, int thickness, DashStyle dashstyle, Color linecolorSupport)
        {
            _drawClass.Paint(e, currentfigure, _points, _figuresBuild, linecolor, thickness, dashstyle);
            _drawClass.Paint(e, currentfigure, _points, _figuresBuild, linecolor, thickness, dashstyle);

            if (_selectClass.SeleckResult() != null)
            {
                _drawClass.SupportPoint(_selectClass.SeleckResult(), _figuresBuild, linecolorSupport);
            }
        }

        public void Child_MouseMove( MouseEventArgs e, int currentfigure, int currentActions)
        {
            _points = _actionsBuild[currentActions].MouseMove(e, currentfigure, currentActions);
        }


        /// <summary>
        /// Метод, выполняемый при нажатии мыши на рабочей области
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая координаты мыщи</para>
        public void Child_MouseUp(MouseEventArgs e, int _currentfigure, int _currentActions, Color linecolor, int thickness, DashStyle dashstyle, Color brushcolor, bool fill)    // Нажата клавиша 
        {
            _actionsBuild[_currentActions].MouseUp(e, _currentfigure, linecolor, thickness, dashstyle, brushcolor, fill);

        }

        /// <summary>
        /// Метод, выполняемый при отпускании кнопки мыши на рабочей области
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая координаты мыщи</para>
        public void Child1_MouseDown(MouseEventArgs e, int currentfigure, int currentActions)  // Нажата отпущена 
        {
            _actionsBuild[currentActions].MouseDown(e, currentfigure);
        }
        /// <summary>
        /// Метод, выполняющий обновление рабочей области.
        /// </summary>
        public void RefreshBitmap()
        {
            _drawClass.RefreshBitmap();
        }

        /// <summary>
        /// Метод, выполняющий удаление фигур.
        /// </summary>
        public void DeleteFigure()
        {
            if (_points.Count != 0)
            {
                _points.Clear();
            }
            _selectClass.MouseUp();
            _cleanFigure = UnityContainerInit.Resolve<CleanFigure>(new OrderedParametersOverride(new object[] { _drawClass.FiguresList, _figuresLoad }));
            _parameterChangesClass.Clear(_cleanFigure);
        }

        /// <summary>
        /// Метод, выполняющий удаление опорных точек.
        /// </summary>
        public void DeleteSupportFigure()
        {
            _selectClass.MouseUp();
           
        }

        /// <summary>
        /// Метод, выполняющий копирование выделенных фигур.
        /// </summary>
        public void СopyFigure()
        {
            if ((_selectClass.SeleckResult() != null) && (_selectClass.SeleckResult().Count != 0))
            {
                _replicationFigure = UnityContainerInit.Resolve<ReplicationFigure>(new OrderedParametersOverride(new object[] { _selectClass.SeleckResult(), _drawClass.FiguresList }));
                _parameterChangesClass.ReplicationFigure(_selectClass.SeleckResult(), _replicationFigure);
            }
        }

        /// <summary>
        /// Метод, выполняющий удаление выделенных фигур.
        /// </summary>
        public void DeleteSelectFigure()
        {
            if ((_selectClass.SeleckResult() != null) && (_selectClass.SeleckResult().Count != 0))
            {
                _deleteFigure = UnityContainerInit.Resolve<DeleteFigure>(new OrderedParametersOverride(new object[] { _selectClass.SeleckResult(), _drawClass.FiguresList }));
                _parameterChangesClass.DeleteFigure(_selectClass.SeleckResult(), _deleteFigure);
            }
            //ChangeActions(LastActions);
            _selectClass.MouseUp();
        }

        /// <summary>
        /// Метод, выполняющий изменение заливки у выбранных фигур.
        /// </summary>
        public void СhangeBackgroundFigure(Color ColorBlakgroung)
        {
            if (_selectClass.SeleckResult().Count != 0)
            {
                _brushColor = UnityContainerInit.Resolve<СhangeBackgroundFigure>(new OrderedParametersOverride(new object[] { _selectClass.SeleckResult(), ColorBlakgroung }));
                _parameterChangesClass.СhangeBackgroundFigure(_selectClass.SeleckResult(), _brushColor);
                //ChangeActions(LastActions);
            }
        }

        /// <summary>
        /// Метод, выполняющий удаление заливки у выбранных фигур.
        /// </summary>
        public void DeleteBackgroundFigure()
        {
            if ((_selectClass.SeleckResult() != null) && (_selectClass.SeleckResult().Count != 0))
            {
                _deleteBrush = UnityContainerInit.Resolve<DeleteBackgroundFigure>(new OrderedParametersOverride(new object[] { _selectClass.SeleckResult() }));
                _parameterChangesClass.DeleteBackgroundFigure(_selectClass.SeleckResult(), _deleteBrush);
            }
        }

        /// <summary>
        /// Метод, выполняющий изменение цвета кисти у выбранных фигур.
        /// </summary>
        /// <para name = "ColorPen">Переменная, хранящая новый цвет кисти.</para>
        public void ColorSelectPen(Color ColorPen)
        {
            if (_selectClass.SeleckResult().Count != 0)
            {
                _penColor = UnityContainerInit.Resolve<СhangePenColor>(new OrderedParametersOverride(new object[] { _selectClass.SeleckResult(), ColorPen }));
                _parameterChangesClass.СhangePenColorFigure(_selectClass.SeleckResult(), _penColor);
                //ChangeActions(LastActions);
            }
        }

        /// <summary>
        /// Метод, выполняющий изменение стиля кисти у выбранных фигур.
        /// </summary>
        public void СhangePenStyleFigure(DashStyle dashstyle)
        {
            if (_selectClass.SeleckResult().Count != 0)
            {
                _penStyle = UnityContainerInit.Resolve<СhangePenStyle>(new OrderedParametersOverride(new object[] { _selectClass.SeleckResult(), dashstyle }));
                _parameterChangesClass.СhangePenStyleFigure(_selectClass.SeleckResult(), _penStyle);
                //ChangeActions(LastActions);
            }
        }

        /// <summary>
        /// Метод, выполняющий изменение толщины кисти у выбранных фигур.
        /// </summary>
        public void СhangePenWidthFigure(int thickness)
        {
            if (_selectClass.SeleckResult().Count != 0)
            {
                _penWidth = UnityContainerInit.Resolve<СhangePenWidth>(new OrderedParametersOverride(new object[] { _selectClass.SeleckResult(), thickness }));
                _parameterChangesClass.СhangePenWidthFigure(_selectClass.SeleckResult(), _penWidth);
                //ChangeActions(LastActions);

                int deltaX = 0;
                int deltaY = 0;

                foreach (ObjectFugure SelectObject in _selectClass.SeleckResult())
                {
                    _edipParametr.MoveObjectSupport(SelectObject, deltaX, deltaY);
                }
            }
        }

        /// <summary>
        /// Метод, выполняющий действие "Отменить".
        /// </summary>
        public void UndoFigure()
        {
            if ((_points != null) && (_points.Count != 0))
            {
                _points.Clear();
            }

            _selectClass.MouseUp();
            _drawClass.UndoFigure();
        }
        /// <summary>
        /// Метод, выполняющий действие "Повторить".
        /// </summary>
        public void RedoFigure()
        {
            _drawClass.RedoFigure();
        }

        /// <summary>
        /// Метод, выполняющий изменение цвета кисти у опорных точек.
        /// </summary>
        /// <para name = "NextColor">Переменная, хранящая новый цвет опорных точек.</para>
        public void СhangeSupportPenStyleFigure(Color NextColor)
        {
            if (_selectClass.SeleckResult().Count != 0)
            {
                _supportPenColor = UnityContainerInit.Resolve<СhangeSupportPenColor>(new OrderedParametersOverride(new object[] { NextColor, _selectClass.SeleckResult() }));
                _parameterChangesClass.СhangeSupportPenStyleFigure( _supportPenColor);
            }
        }

        /// <summary>
        /// Метод, выполняющий экспортирование рисунка.
        /// </summary>
        //public PictureBox SaveProject()
        //{
        //    //_drawClass.SaveProject(DrawForm);
        //    //return DrawForm;

        //}

        /// <summary>
        /// Метод, выполняющий отчистку после экспорта.
        /// </summary>
        public void ClearProject()
        {
            //_drawClass.ClearProject(DrawForm);
           
        }

        /// <summary>
        /// Метод, выполняющий возвращающий индекс комманды.
        /// </summary>
        public int IndexCommand
        {
            get
            {
                if ((_points != null) && (_points.Count != 0))
                {
                    _points.Clear();
                }
                return _drawClass.IndexCommand;
            }
            set
            {
                _drawClass.IndexCommand = value;
            }
        }

        /// <summary>
        /// Метод, возвращающий список выделенных фигур.
        /// </summary>
        public bool SelectFigure()
        {
            
            bool ListFigure = false;
            if (_selectClass.SeleckResult().Count != 0)
            {
                ListFigure = true;
            }

            return ListFigure;
        }

    }
}
