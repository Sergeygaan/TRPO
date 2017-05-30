using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MyPaint.Core;
using MyPaint.Build;

namespace MyPaint.Actions
{
    /// <summary>
    /// Класс, выполнящий ротрисовку объектов.
    /// </summary>
    public class DrawActoins : IActoins
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
        /// Переменная, хранящая список классов для построения различных фигур.
        /// </summary>
        private List<IFigureBuild> _figuresBuild = new List<IFigureBuild>();


        public DrawActoins(List<IFigureBuild> FiguresBuild, SelectDraw SelectClass, DrawPaint DrawClass)
        {
            _figuresBuild = FiguresBuild;
            _selectClass = SelectClass;
            _drawClass = DrawClass;
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
            //_points = (IMouseEvent)_figuresBuild[Currentfigure].MouseMove(_points, e);
            IMouseEvent MouseEvent = (IMouseEvent)_figuresBuild[Currentfigure];
           
            _points = MouseEvent.MouseMove(_points, e);

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
            _drawClass.MouseUp(Currentfigure, _points, e, linecolor, thickness, dashstyle, brushcolor, fill);
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
        public void MouseDown(MouseEventArgs e, int currentfigure)
        {
            IMouseEvent mouseEvent = (IMouseEvent)_figuresBuild[currentfigure];
            mouseEvent.MouseDown(_points, e, currentfigure, _figuresBuild);
        }

    }
}
