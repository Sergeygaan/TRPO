using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    [Serializable]
    class Polygon : IFigureBuild
    {
        private AddPolygon _addFigureAddPolygon;
        private СonstructionFigure _ellipse = new СonstructionFigure();
        private SupportObject _drawSupportObject;

        public void PaintFigure(PaintEventArgs e, List<PointF> Points, Pen PenFigure)
        {
            if (Points.Count > 1)
            {
                PointF[] PointPolygon = Points.ToArray();

                e.Graphics.DrawLines(PenFigure, PointPolygon);
            }
        }

        public void AddFigure(Object DrawObject, List<PointF> Points, List<IFigureCommand> FiguresBuild, List<Object> Figures)
        {
            _addFigureAddPolygon = new AddPolygon();
            _addFigureAddPolygon.AddFigure(DrawObject, Points, Figures);
            
            _addFigureAddPolygon.Output().FigureStart = Points[0];
            _addFigureAddPolygon.Output().FigureEnd = Points[1];
            _addFigureAddPolygon.Output().IdFigure = Figures.Count;

            Figures.Add(_addFigureAddPolygon.Output());
            FiguresBuild.Add(_addFigureAddPolygon);
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
            EdipParametr.EditObjectPoliLine(SelectObject, SupportObj, DeltaX, DeltaY);

        }

        public void ScaleFigure(MouseEventArgs e, Object DrawObject, List<Object> SelectedFigures)
        {
            DrawObject.PointSelect = DrawObject.Path.PathPoints;
            DrawObject.SelectFigure = true;
            //DrawObject.Pen.Width += 1;
            SelectedFigures.Add(DrawObject);
        }

    }
}
