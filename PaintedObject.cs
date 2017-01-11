using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PaintedObjectsMoving {
	class PaintedObject : ICloneable
    {
		private GraphicsPath path;
        private Pen pen;

        private Point _figureStart = new Point();                          //стартовая точка фигуры
        private Point _figureEnd = new Point();                            //конечная точка фигуры

        public GraphicsPath Path {
			get { return path; }
			set { path = value; }
		}

        public Point FigureStart
        {
            get { return _figureStart; }
            set { _figureStart = value; }
        }

        public Point FigureEnd
        {
            get { return _figureEnd; }
            set { _figureEnd = value; }
        }

        public Pen @Pen {
			get { return pen; }
			set { pen = value; }
		}
		public PaintedObject(Pen pen, GraphicsPath path) {
			this.path = path;
			this.pen = pen;
		}

		#region ICloneable Members

		public object Clone() {
			return new PaintedObject(this.Pen, this.Path.Clone() as GraphicsPath);
		}

		#endregion
	}
}
