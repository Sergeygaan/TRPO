using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    class Ellipses : IFigureBuild
    {
        private AddEllipse _addFigureEllipse;
        private СonstructionFigure _ellipse = new СonstructionFigure();
        private SupportObject _drawSupportObject;

        public void PaintFigure(PaintEventArgs e, List<PointF> _points, Pen _penFigure)
        {
            e.Graphics.DrawEllipse(_penFigure, _ellipse.ShowRectangle(_points[0], _points[1]));
        }

        public void AddFigure(Object DrawObject, List<PointF> _points, List<IFigureCommand> _figuresBuild)
        {
            _addFigureEllipse = new AddEllipse();
            _addFigureEllipse.AddFigure(DrawObject, _points);
            _addFigureEllipse.Execute();

            _addFigureEllipse.Output().FigureStart = _points[0];
            _addFigureEllipse.Output().FigureEnd = _points[1];
            _addFigureEllipse.Output().IdFigure = _figuresBuild.Count;


            _figuresBuild.Add(_addFigureEllipse);
        }

        public void AddSupportPoint(Object SelectObject)
        {
            for (int i = 0; i < SelectObject.PointSelect.Length; i += 3)
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
            EdipParametr.EditObjectEllepse(SelectObject, SupportObj, DeltaX, DeltaY);

            SelectObject.PointSelect = SelectObject.Path.PathPoints;
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
