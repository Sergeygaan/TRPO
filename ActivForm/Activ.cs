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
    public class Activ
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
        /// Переменная, хранящая список действий для построения различных фигур.
        /// </summary>
        private List<IActoins> _actionsBuild = new List<IActoins>();

        private UndoRedo _commandClass;

        private ParameterChanges _parameterChangesClass;

        public Activ(int Width, int Height)
        {
            var UnityContainerInit = new UnityContainer();

            _commandClass = UnityContainerInit.Resolve<UndoRedo>();

            _selectClass = UnityContainerInit.Resolve<SelectDraw>();

            _drawClass = UnityContainerInit.Resolve<DrawPaint>(new OrderedParametersOverride(new object[] { Width, Height, _commandClass }));

            _parameterChangesClass = UnityContainerInit.Resolve<ParameterChanges>(new OrderedParametersOverride(new object[] { _drawClass, _commandClass }));

            //UnityContainerInit.RegisterType<IFigureBuild, Rectangles>();

            UnityContainerInit.RegisterType<IFigureBuild, Rectangles>();

            _figuresBuild.Add(UnityContainerInit.Resolve<Rectangles>());
            _figuresBuild.Add(UnityContainerInit.Resolve<Ellipses>());
            _figuresBuild.Add(UnityContainerInit.Resolve<Line>());
            _figuresBuild.Add(UnityContainerInit.Resolve<PoliLine>());
            _figuresBuild.Add(UnityContainerInit.Resolve<Polygon>());
            _figuresBuild.Add(UnityContainerInit.Resolve<RectangleSelect>());

            _actionsBuild.Add(UnityContainerInit.Resolve<DrawActoins>(new OrderedParametersOverride(new object[] { _figuresBuild, _selectClass, _drawClass })));
            _actionsBuild.Add(UnityContainerInit.Resolve<SelectRegionActions>(new OrderedParametersOverride(new object[] { _figuresBuild, _selectClass, _drawClass, _parameterChangesClass })));
            _actionsBuild.Add(UnityContainerInit.Resolve<SelectPointActions>(new OrderedParametersOverride(new object[] { _figuresBuild, _selectClass, _drawClass, _parameterChangesClass })));
        }

        public void Child_Paint(PaintEventArgs e, int _currentfigure, Color linecolor, int thickness, DashStyle dashstyle, Color linecolorSupport)
        {
            _drawClass.Paint(e, _currentfigure, _points, _figuresBuild, linecolor, thickness, dashstyle);

            if (_selectClass.SeleckResult() != null)
            {
                _drawClass.SupportPoint(_selectClass.SeleckResult(), _figuresBuild, linecolorSupport);
            }
        }

        public void Child_MouseMove( MouseEventArgs e, int _currentfigure, int _currentActions)
        {
            _points = _actionsBuild[_currentActions].MouseMove(e, _currentfigure, _currentActions);
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
        public void Child1_MouseDown(MouseEventArgs e, int _currentfigure, int _currentActions)  // Нажата отпущена 
        {
            _actionsBuild[_currentActions].MouseDown(e, _currentfigure);
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
            _parameterChangesClass.Clear();
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
            if (_selectClass.SeleckResult() != null)
            {
                _parameterChangesClass.ReplicationFigure(_selectClass.SeleckResult());
            }
        }

        /// <summary>
        /// Метод, выполняющий удаление выделенных фигур.
        /// </summary>
        public void DeleteSelectFigure()
        {
            if (_selectClass.SeleckResult() != null)
            {
                _parameterChangesClass.DeleteFigure(_selectClass.SeleckResult());
            }
            //ChangeActions(LastActions);
            _selectClass.MouseUp();
        }

        /// <summary>
        /// Метод, выполняющий изменение заливки у выбранных фигур.
        /// </summary>
        public void СhangeBackgroundFigure(Color ColorBlakgroung)
        {
            _parameterChangesClass.СhangeBackgroundFigure(_selectClass.SeleckResult(), ColorBlakgroung);
            //ChangeActions(LastActions);
        }

        /// <summary>
        /// Метод, выполняющий удаление заливки у выбранных фигур.
        /// </summary>
        public void DeleteBackgroundFigure()
        {
            if (_selectClass.SeleckResult() != null)
            {
                _parameterChangesClass.DeleteBackgroundFigure(_selectClass.SeleckResult());
            }
        }

        /// <summary>
        /// Метод, выполняющий изменение цвета кисти у выбранных фигур.
        /// </summary>
        /// <para name = "ColorPen">Переменная, хранящая новый цвет кисти.</para>
        public void ColorSelectPen(Color ColorPen)
        {
            _parameterChangesClass.СhangePenColorFigure(_selectClass.SeleckResult(), ColorPen);
            //ChangeActions(LastActions);
        }

        /// <summary>
        /// Метод, выполняющий изменение стиля кисти у выбранных фигур.
        /// </summary>
        public void СhangePenStyleFigure(DashStyle dashstyle)
        {
            _parameterChangesClass.СhangePenStyleFigure(_selectClass.SeleckResult(), dashstyle);
            //ChangeActions(LastActions);
        }

        /// <summary>
        /// Метод, выполняющий изменение толщины кисти у выбранных фигур.
        /// </summary>
        public void СhangePenWidthFigure(int thickness)
        {
            _parameterChangesClass.СhangePenWidthFigure(_selectClass.SeleckResult(), thickness);
            //ChangeActions(LastActions);

            int deltaX = 0;
            int deltaY = 0;

            foreach (ObjectFugure SelectObject in _selectClass.SeleckResult())
            {
                _edipParametr.MoveObjectSupport(SelectObject, deltaX, deltaY);
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
            _parameterChangesClass.СhangeSupportPenStyleFigure(NextColor, _selectClass.SeleckResult());
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
