using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    class Line : IFigureBuild
    {
        private СonstructionFigure _ellipse = new СonstructionFigure();
        private SupportObject _drawSupportObject;
        private AddLine _addFigureLine;

        public void PaintFigure(PaintEventArgs e, List<PointF> _points, Pen _penFigure)
        {
            e.Graphics.DrawLine(_penFigure, _points[0], _points[1]);
        }

        public void AddFigure(Object DrawObject, List<PointF> _points, List<IFigureCommand> _figuresCommand, List<Object> Figures)
        {
            _addFigureLine = new AddLine();
            _addFigureLine.AddFigure(DrawObject, _points, Figures);
          
            _addFigureLine.Output().FigureStart = _points[0];
            _addFigureLine.Output().FigureEnd = _points[1];
            _addFigureLine.Output().IdFigure = Figures.Count;

            Figures.Add(_addFigureLine.Output());

            _figuresCommand.Add(_addFigureLine);
        }


        public void AddSupportPoint(Object SelectObject)
        {
            for (int i = 0; i < SelectObject.PointSelect.Length; i++)
            {
                _drawSupportObject = new SupportObject(new Pen(MainForm.FigurePropertiesSupport.linecolor, 1), new GraphicsPath());
                _drawSupportObject.Path.AddEllipse(_ellipse.SelectFigure(SelectObject.PointSelect[i], SelectObject.Pen.Width));
                _drawSupportObject.IdFigure = SelectObject.IdFigure;
                _drawSupportObject.ControlPointF = i;

                SelectObject.AddListFigure(_drawSupportObject);
            }
        }

        public void ScaleSelectFigure(Object SelectObject, SupportObject SupportObj, int DeltaX, int DeltaY, EditObject EdipParametr)
        {
            if ((SelectObject.PointSelect[0].X - SelectObject.PointSelect[1].X != 0) && (SelectObject.PointSelect[0].Y - SelectObject.PointSelect[1].Y != 0))
            {
                SelectObject.PointSelect = SelectObject.Path.PathPoints;
            }
            EdipParametr.EditObjectLine(SelectObject, SupportObj, DeltaX, DeltaY);
        }


        public void ScaleFigure(MouseEventArgs e, Object DrawObject, List<Object> SelectedFigures)
        {
            float LineX, LineY;

            LineY = (-(DrawObject.Path.PathPoints[1].X * DrawObject.Path.PathPoints[0].Y - DrawObject.Path.PathPoints[0].X * DrawObject.Path.PathPoints[1].Y) - ((DrawObject.Path.PathPoints[1].Y - DrawObject.Path.PathPoints[0].Y) * e.Location.X)) / (DrawObject.Path.PathPoints[0].X - DrawObject.Path.PathPoints[1].X);

            LineX = (-(DrawObject.Path.PathPoints[1].X * DrawObject.Path.PathPoints[0].Y - DrawObject.Path.PathPoints[0].X * DrawObject.Path.PathPoints[1].Y) - ((DrawObject.Path.PathPoints[0].X - DrawObject.Path.PathPoints[1].X) * e.Location.Y)) / (DrawObject.Path.PathPoints[1].Y - DrawObject.Path.PathPoints[0].Y);

            if ((e.Location.Y >= LineY - DrawObject.Pen.Width - 2) && (e.Location.Y <= LineY + DrawObject.Pen.Width + 2) || (e.Location.X >= LineX - DrawObject.Pen.Width - 2) && (e.Location.X <= LineX + DrawObject.Pen.Width + 2))
            {
                DrawObject.PointSelect = DrawObject.Path.PathPoints;
                DrawObject.SelectFigure = true;
                //DrawObject.Pen.Width += 1;
                SelectedFigures.Add(DrawObject);
            }

        }

    }
}
