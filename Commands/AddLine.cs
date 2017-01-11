using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    class AddLine : IFigureCommand
    {
        private List<PointF> _points;
        private Object _drawObject;

        public void PaintFigure(PaintEventArgs e, List<PointF> _points, Pen _penFigure)
        {
            e.Graphics.DrawLine(_penFigure, _points[0], _points[1]);
        }

        public void AddFigure(Object DrawObject, List<PointF> Points)
        {
            _drawObject = DrawObject;
            _points = Points;
        }

        public void Execute()
        {
            _drawObject.Path.AddLine(_points[0], _points[1]);
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
