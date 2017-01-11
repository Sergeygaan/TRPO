using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace PaintedObjectsMoving
{
    [Serializable]
    class SupportObject : ICloneable
    {
        private GraphicsPath _path;
        private Pen _pen;
        private int _controlPointF;
        private int _idFigure;

        public int IdFigure
        {
            get { return _idFigure; }
            set { _idFigure = value; }
        }

        public GraphicsPath Path
        {
            get { return _path; }
            set { _path = value; }
        }

        public int ControlPointF
        {
            get { return _controlPointF; }
            set { _controlPointF = value; }
        }

        public Pen @Pen
        {
            get { return _pen; }
            set { _pen = value; }
        }
        public SupportObject(Pen pen, GraphicsPath path)
        {
            _path = path;
            _pen = pen;
        }

        #region ICloneable Members

        public object Clone()
        {
            return new SupportObject(this.Pen, this.Path.Clone() as GraphicsPath);
        }

        #endregion
    }
}
