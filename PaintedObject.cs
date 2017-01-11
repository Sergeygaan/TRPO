using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PaintedObjectsMoving {
	class PaintedObject : ICloneable {
		private GraphicsPath path;

		public GraphicsPath Path {
			get { return path; }
			set { path = value; }
		}
		private Pen pen;

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
