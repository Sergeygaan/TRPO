using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace PaintedObjectsMoving
{
    class SupportObject : ICloneable
    {
        private GraphicsPath path;
        private Pen pen;
        private int _controlPointF;

        public GraphicsPath Path
        {
            get { return path; }
            set { path = value; }
        }

        public int ControlPointF
        {
            get { return _controlPointF; }
            set { _controlPointF = value; }
        }

        public Pen @Pen
        {
            get { return pen; }
            set { pen = value; }
        }
        public SupportObject(Pen pen, GraphicsPath path)
        {
            this.path = path;
            this.pen = pen;
        }

        #region ICloneable Members

        public object Clone()
        {
            return new PaintedObject(this.Pen, this.Path.Clone() as GraphicsPath);
        }

        #endregion
    }
}
