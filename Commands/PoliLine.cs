using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    class PoliLine : IFigureBuild
    {
        private AddPoliLine _addFigurePoliLine;
        private СonstructionFigure _ellipse = new СonstructionFigure();
        private SupportObject _drawSupportObject;

        public void PaintFigure(PaintEventArgs e, List<PointF> _points, Pen _penFigure)
        {
            if (_points.Count > 1)
            {
                PointF[] PointPoliLine = _points.ToArray();

                e.Graphics.DrawLines(_penFigure, PointPoliLine);
            }
        }

        public void AddFigure(Object DrawObject, List<PointF> _points, List<IFigureCommand> _figuresBuild)
        {

            _addFigurePoliLine = new AddPoliLine();
            _addFigurePoliLine.AddFigure(DrawObject, _points);
            _addFigurePoliLine.Execute();

            _addFigurePoliLine.Output().FigureStart = _points[0];
            _addFigurePoliLine.Output().FigureEnd = _points[1];
            _addFigurePoliLine.Output().IdFigure = _figuresBuild.Count;


            _figuresBuild.Add(_addFigurePoliLine);

        }

        public void AddSupportPoint(IFigureCommand SelectObject)
        {
            for (int i = 0; i < SelectObject.Output().PointSelect.Length; i++)
            {
                _drawSupportObject = new SupportObject(new Pen(MainForm.FigurePropertiesSupport.linecolor, 1), new GraphicsPath());
                _drawSupportObject.Path.AddEllipse(_ellipse.SelectFigure(SelectObject.Output().PointSelect[i], SelectObject.Output().Pen.Width));
                _drawSupportObject.IdFigure = SelectObject.Output().IdFigure;
                _drawSupportObject.ControlPointF = i;

                SelectObject.Output().AddListFigure(_drawSupportObject);
            }
        }

        public void ScaleSelectFigure(Object SelectObject, SupportObject SupportObj, int DeltaX, int DeltaY, EditObject EdipParametr)
        {
            if ((SelectObject.PointSelect[0].X - SelectObject.PointSelect[1].X != 0) && (SelectObject.PointSelect[0].Y - SelectObject.PointSelect[1].Y != 0))
            {
                SelectObject.PointSelect = SelectObject.Path.PathPoints;
            }
            EdipParametr.EditObjectPoliLine(SelectObject, SupportObj, DeltaX, DeltaY);

        }

        public void ScaleFigure(MouseEventArgs e, IFigureCommand DrawObject, List<IFigureCommand> SelectedFigures)
        {
            
            for (int i = 1; i < DrawObject.Output().Path.PathPoints.Length; i++)
            {
                float PoliLineX, PoliLineY;

                PoliLineY = (-(DrawObject.Output().Path.PathPoints[i - 1].X * DrawObject.Output().Path.PathPoints[i].Y - DrawObject.Output().Path.PathPoints[i].X * DrawObject.Output().Path.PathPoints[i - 1].Y) - ((DrawObject.Output().Path.PathPoints[i - 1].Y - DrawObject.Output().Path.PathPoints[i].Y) * e.Location.X)) / (DrawObject.Output().Path.PathPoints[i].X - DrawObject.Output().Path.PathPoints[i - 1].X);

                PoliLineX = (-(DrawObject.Output().Path.PathPoints[i - 1].X * DrawObject.Output().Path.PathPoints[i].Y - DrawObject.Output().Path.PathPoints[i].X * DrawObject.Output().Path.PathPoints[i - 1].Y) - ((DrawObject.Output().Path.PathPoints[i].X - DrawObject.Output().Path.PathPoints[i - 1].X) * e.Location.Y)) / (DrawObject.Output().Path.PathPoints[i - 1].Y - DrawObject.Output().Path.PathPoints[i].Y);

                if ((e.Location.Y >= PoliLineY - DrawObject.Output().Pen.Width - 2) && (e.Location.Y <= PoliLineY + DrawObject.Output().Pen.Width + 2) || (e.Location.X >= PoliLineX - DrawObject.Output().Pen.Width - 2) && (e.Location.X <= PoliLineX + DrawObject.Output().Pen.Width + 2))
                {
                    DrawObject.Output().PointSelect = DrawObject.Output().Path.PathPoints;
                    DrawObject.Output().SelectFigure = true;
                    //DrawObject.Pen.Width += 1;
                    SelectedFigures.Add(DrawObject);
                }
            }
        }


    }
}
