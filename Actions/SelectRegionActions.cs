using System.Collections.Generic;
using MyPaint.CORE;
using System.Drawing;
using System.Windows.Forms;


namespace MyPaint.Actions
{
    class SelectRegionActions : IActoins
    {
        /// <summary>
        /// Переменная, хранящая список точек для построения фигур.
        /// </summary>
        private List<PointF> _points = new List<PointF>();

        public List<PointF> MouseMove(object sender, MouseEventArgs e, MainForm.FigureType Currentfigure, SelectDraw SelectClass, MainForm.Actions CurrentActions, List<IFigureBuild> FiguresBuild)
        {
            if (SelectClass.SeleckResult().Count == 0)
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
                    SelectClass.MouseMove(e, CurrentActions, FiguresBuild);
                }
            }

            return _points;
        }

        public void MouseUp(object sender, MouseEventArgs e, MainForm.FigureType Currentfigure, SelectDraw SelectClass, DrawPaint DrawClass, List<IFigureBuild> FiguresBuild)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (SelectClass.SeleckResult().Count == 0)
                {
                    SelectClass.MouseDown(e, DrawClass.SeparationZone(), DrawClass.FiguresList, MainForm.Actions.SelectRegion, FiguresBuild);
                    _points.Clear();
                }
                else
                {
                    SelectClass.MouseUpSupport();
                    DrawClass.СhangeMoveFigure(SelectClass.SeleckResult(), "MouseUp");

                }

            }
           else
            {
                if (SelectClass.SeleckResult().Count == 0)
                {
                    SelectClass.MouseUp();

                }
                else
                {
                    SelectClass.MouseUp();
                    //ChangeActions(LastActions);
                }
            }
        }

        public void MouseDown(object sender, MouseEventArgs e, MainForm.FigureType Currentfigure, SelectDraw SelectClass, DrawPaint DrawClass, List<IFigureBuild> FiguresBuild)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (SelectClass.SeleckResult().Count == 0)
                {
                    SelectClass.MouseUp();

                    _points.Add(new PointF(e.Location.X, e.Location.Y));
                    _points.Add(new PointF(e.Location.X, e.Location.Y));
                    //LastActions = MainForm.Actions.SelectRegion;

                }
                else
                {
                    DrawClass.СhangeMoveFigure(SelectClass.SeleckResult(), "Down");
                    SelectClass.SavePoint(e);
                }

            }

        }

    }

}
