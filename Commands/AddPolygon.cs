using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    class AddPolygon : IFigureCommand
    {
        private List<PointF> _points;
        private Object _drawObject;

        public void PaintFigure(PaintEventArgs e, List<PointF> _points, Pen _penFigure)
        {
            if (_points.Count > 1)
            {
                PointF[] PointPolygon = _points.ToArray();

                e.Graphics.DrawLines(_penFigure, PointPolygon);
            }
        }

        public void AddFigure(Object DrawObject, List<PointF> Points)
        {
            _drawObject = DrawObject;
            _points = Points;
        }

        public void Execute()
        {
            PointF[] PointPolygon = _points.ToArray();

            _drawObject.Path.AddLines(PointPolygon);

            _drawObject.Path.CloseFigure();
        }

        public void Undo()
        {

        }

        public Object Output()
        {
            return _drawObject;
        }

        public void AddSupportPoint(Object SelectObject)
        {
        }

        public void ScaleSelectFigure(Object SelectObject, SupportObject SupportObj, int DeltaX, int DeltaY, EditObject EdipParametr)
        {
          
        }


        public void ScaleFigure(MouseEventArgs e, Object DrawObject, List<Object> SelectedFigures)
        {
           
        }

    }
}
