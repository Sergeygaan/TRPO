using MyPaint.CORE;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MyPaint.Actions
{
    class DrawActoins : IActoins
    {
        /// <summary>
        /// Переменная, хранящая список точек для построения фигур.
        /// </summary>
        private List<PointF> _points = new List<PointF>();


        public List<PointF> MouseMove(object sender, MouseEventArgs e, MainForm.FigureType Currentfigure, SelectDraw SelectClass, MainForm.Actions CurrentActions, List<IFigureBuild> FiguresBuild)
        {
            switch (Currentfigure)
            {

                case MainForm.FigureType.Line:
                case MainForm.FigureType.Ellipse:
                case MainForm.FigureType.Rectangle:

                    if (_points.Count != 0)
                    {
                        _points[1] = new PointF(e.Location.X, e.Location.Y);
                    }
                    break;
            }

            return _points;
        }

        public void MouseUp(object sender, MouseEventArgs e, MainForm.FigureType Currentfigure, SelectDraw SelectClass, DrawPaint DrawClass, List<IFigureBuild> FiguresBuild)
        {

            if (e.Button == MouseButtons.Left)              //если нажата левая кнопка мыши
            {

                switch (Currentfigure)
                {

                    case MainForm.FigureType.Line:
                    case MainForm.FigureType.Ellipse:
                    case MainForm.FigureType.Rectangle:

                    if (_points.Count != 0)
                    {

                        _points[1] = new PointF(e.Location.X, e.Location.Y);
                        DrawClass.MouseUp(Currentfigure, _points, FiguresBuild);
                        _points.Clear();
                    }
                    break;
                }
            }
        }

        public void MouseDown(object sender, MouseEventArgs e, MainForm.FigureType Currentfigure, SelectDraw SelectClass, DrawPaint DrawClass, List<IFigureBuild> FiguresBuild)
        {

            if (e.Button == MouseButtons.Left)              //если нажата левая кнопка мыши
            {
                switch (Currentfigure)
                {

                    case MainForm.FigureType.Line:
                    case MainForm.FigureType.Ellipse:
                    case MainForm.FigureType.Rectangle:

                        _points.Add(new PointF(e.Location.X, e.Location.Y));
                        _points.Add(new PointF(e.Location.X, e.Location.Y));

                        break;

                    case MainForm.FigureType.PoliLine:
                    case MainForm.FigureType.Polygon:

                        _points.Add(new PointF(e.Location.X, e.Location.Y));

                        break;

                }
            }
            else
            {
                switch (Currentfigure)
                {
                    case MainForm.FigureType.Polygon:
                    case MainForm.FigureType.PoliLine:

                        if (_points.Count != 0)
                        {
                            DrawClass.MouseUp(Currentfigure, _points, FiguresBuild);
                            _points.Clear();

                        }

                break;
                }
            }

        }

    }
}
