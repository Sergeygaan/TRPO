using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MyPaint.Build;
using MyPaint.Core;
using System.Drawing.Drawing2D;
using Core;
using MyPaint.Command;
using Unity;
using Microsoft.Practices.Unity;

namespace MyPaint.Actions
{
    /// <summary>
    /// Класс, выполнящий выделение объектов областью.
    /// </summary>
    public class SelectRegionActions : IActoins
    {
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
        /// Переменная, хранящая класс unity.
        /// </summary>
        UnityContainer UnityContainerInit = new UnityContainer();

        /// <summary>
        /// Переменная, хранящая список классов для построения различных фигур.
        /// </summary>
        private List<IFigureBuild> _figuresBuild = new List<IFigureBuild>();

        private ParameterChanges _parameterChangesClass;

        private СhangeMove _penMove;

        public SelectRegionActions(List<IFigureBuild> FiguresBuild, SelectDraw SelectClass, DrawPaint DrawClass, ParameterChanges ParameterChangesClass)
        {
            _figuresBuild = FiguresBuild;
            _selectClass = SelectClass;
            _drawClass = DrawClass;
            _parameterChangesClass = ParameterChangesClass;
        }

        /// <summary>
        /// Метод, выполняющий действие при перемещении мыши.
        /// </summary>
        /// <para name = "e">Объект хранящий данные о мыши</para>
        /// <para name = "sender">Объект хранящий данные об объекте</para>
        /// <para name = "Currentfigure">Объект хранящий данные о выбранной фигуре</para>
        /// <para name = "SelectClass">Объект хранящий данные о выбранных фигурах</para>
        /// <para name = "DrawClass">Объект хранящий данные о классе используемом для отрисовки фигур</para>
        /// <para name = "FiguresBuild">Объект хранящий о классах построения</para>
        public List<PointF> MouseMove(MouseEventArgs e, int Currentfigure, int CurrentActions)
        {
            if (_selectClass.SeleckResult().Count == 0)
            {
                if ((e.Button == MouseButtons.Left) && (_points.Count != 0))
                {
                    _points[1] = new PointF(e.Location.X, e.Location.Y);
                }
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                {
                    _selectClass.MouseMove(e, CurrentActions, _figuresBuild);
                }
            }

            return _points;
        }

        /// <summary>
        /// Метод, выполняющий действие при отпускании мыши.
        /// </summary>
        /// <para name = "e">Объект хранящий данные о мыши</para>
        /// <para name = "sender">Объект хранящий данные об объекте</para>
        /// <para name = "Currentfigure">Объект хранящий данные о выбранной фигуре</para>
        /// <para name = "SelectClass">Объект хранящий данные о выбранных фигурах</para>
        /// <para name = "DrawClass">Объект хранящий данные о классе используемом для отрисовки фигур</para>
        /// <para name = "FiguresBuild">Объект хранящий о классах построения</para>
        public void MouseUp(MouseEventArgs e, int Currentfigure, Color linecolor, int thickness, DashStyle dashstyle, Color brushcolor, bool fill)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_selectClass.SeleckResult().Count == 0)
                {
                    _selectClass.MouseDown(e, _drawClass.SeparationZone(), _drawClass.FiguresList, 2, _figuresBuild);
                    _points.Clear();
                }
                else
                {
                    _selectClass.MouseUpSupport();
                    _penMove = UnityContainerInit.Resolve<СhangeMove>(new OrderedParametersOverride(new object[] { _selectClass.SeleckResult() }));
                    _parameterChangesClass.СhangeMoveFigure(_selectClass.SeleckResult(), "MouseUp", _penMove);
                }

            }
           else
            {
                if (_selectClass.SeleckResult().Count == 0)
                {
                    _selectClass.MouseUp();

                }
                else
                {
                    _selectClass.MouseUp();
                }
            }
        }

        /// <summary>
        /// Метод, выполняющий действие при нажатии мыши.
        /// </summary>
        /// <para name = "e">Объект хранящий данные о мыши</para>
        /// <para name = "sender">Объект хранящий данные об объекте</para>
        /// <para name = "Currentfigure">Объект хранящий данные о выбранной фигуре</para>
        /// <para name = "SelectClass">Объект хранящий данные о выбранных фигурах</para>
        /// <para name = "DrawClass">Объект хранящий данные о классе используемом для отрисовки фигур</para>
        /// <para name = "FiguresBuild">Объект хранящий о классах построения</para>
        public void MouseDown(MouseEventArgs e, int Currentfigure)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_selectClass.SeleckResult().Count == 0)
                {
                    _selectClass.MouseUp();

                    _points.Add(new PointF(e.Location.X, e.Location.Y));
                    _points.Add(new PointF(e.Location.X, e.Location.Y));
                }
                else
                {
                    _penMove = UnityContainerInit.Resolve<СhangeMove>(new OrderedParametersOverride(new object[] { _selectClass.SeleckResult() }));
                    _parameterChangesClass.СhangeMoveFigure(_selectClass.SeleckResult(), "Down", _penMove);
                    _selectClass.SavePoint(e);
                }

            }

        }

    }

}
