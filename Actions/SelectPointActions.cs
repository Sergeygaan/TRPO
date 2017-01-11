using System.Collections.Generic;
using MyPaint.CORE;
using System.Drawing;
using System.Windows.Forms;

namespace MyPaint.Actions
{
    class SelectPointActions : IActoins
    {
        /// <summary>
        /// Переменная, хранящая список точек для построения фигур.
        /// </summary>
        private List<PointF> _points = new List<PointF>();

        public List<PointF> MouseMove(object sender, MouseEventArgs e, MainForm.FigureType Currentfigure, SelectDraw SelectClass, MainForm.Actions CurrentActions, List<IFigureBuild> FiguresBuild)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (SelectClass.SeleckResult().Count != 0)
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
                    SelectClass.MouseDown(e, DrawClass.SeparationZone(), DrawClass.FiguresList, MainForm.Actions.SelectPoint, FiguresBuild);

                }
                else
                {
                    SelectClass.MouseUpSupport();
                    DrawClass.СhangeMoveFigure(SelectClass.SeleckResult(), "MouseUp");
                }
            }

            if (e.Button == MouseButtons.Right)
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
                    //LastActions = MainForm.Actions.SelectPoint;
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
