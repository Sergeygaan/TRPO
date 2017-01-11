using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    class Polygon : IFigureBuild
    {
        private AddPolygon _addFigureAddPolygon;
        private СonstructionFigure _ellipse = new СonstructionFigure();
        private SupportObject _drawSupportObject;

        public void PaintFigure(PaintEventArgs e, List<PointF> _points, Pen _penFigure)
        {
            if (_points.Count > 1)
            {
                PointF[] PointPolygon = _points.ToArray();

                e.Graphics.DrawLines(_penFigure, PointPolygon);
            }
        }

        public void AddFigure(Object DrawObject, List<PointF> _points, List<IFigureCommand> _figuresBuild)
        {
            _addFigureAddPolygon = new AddPolygon();
            _addFigureAddPolygon.AddFigure(DrawObject, _points);
            _addFigureAddPolygon.Execute();

            _addFigureAddPolygon.Output().FigureStart = _points[0];
            _addFigureAddPolygon.Output().FigureEnd = _points[1];
            _addFigureAddPolygon.Output().IdFigure = _figuresBuild.Count;


            _figuresBuild.Add(_addFigureAddPolygon);
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
            DrawObject.Output().PointSelect = DrawObject.Output().Path.PathPoints;
            DrawObject.Output().SelectFigure = true;
            //DrawObject.Pen.Width += 1;
            SelectedFigures.Add(DrawObject);
        }

    }
}
