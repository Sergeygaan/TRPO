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
    class Ellipses : IFigureBuild
    {
        private AddEllipse _addFigureEllipse;
        private СonstructionFigure _ellipse = new СonstructionFigure();
        private SupportObject _drawSupportObject;

        public void PaintFigure(PaintEventArgs e, List<PointF> Points, Pen PenFigure)
        {
            e.Graphics.DrawEllipse(PenFigure, _ellipse.ShowRectangle(Points[0], Points[1]));
        }

        public void AddFigure(Object DrawObject, List<PointF> Points, List<IFigureCommand> FiguresBuild, List<Object> Figures)
        {
            _addFigureEllipse = new AddEllipse();
            _addFigureEllipse.AddFigure(DrawObject, Points, Figures);
          
            _addFigureEllipse.Output().FigureStart = Points[0];
            _addFigureEllipse.Output().FigureEnd = Points[1];
            _addFigureEllipse.Output().IdFigure = Figures.Count;

            Figures.Add(_addFigureEllipse.Output());
            FiguresBuild.Add(_addFigureEllipse);
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
