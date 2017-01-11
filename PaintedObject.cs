using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PaintedObjectsMoving {
	class PaintedObject : ICloneable
    {
		private GraphicsPath path;
        private Pen pen;
        private PointF[] _pointSelect;
        private Point _figureStart = new Point();                          //стартовая точка фигуры
        private Point _figureEnd = new Point();                            //конечная точка фигуры
        private bool _selectFigure = false;

        private MainForm.FigureType _currentfigure;

        List<SupportObject> _supportFigures = new List<SupportObject>();

        public GraphicsPath Path {
			get { return path; }
			set { path = value; }
        }

        //Добавить опорную фигуру в список
        public void AddListFigure(SupportObject AddFigure)
        {
            _supportFigures.Add(AddFigure);
        }

        //Отчистить список опорных фигур
        public void ClearListFigure()
        {
            _supportFigures.Clear();
        }

        //Вернуть список опорных фигур
        public List<SupportObject> SelectListFigure()
        {
            return _supportFigures;
        }

        //Вернуть фигуры
        public MainForm.FigureType CurrentFigure
        {
            get { return _currentfigure; }
            set { _currentfigure = value; }
        }

        //Вернуть координаты
        public PointF[] PointSelect
        {
            get { return _pointSelect; }
            set { _pointSelect = value; }
        }

        public bool SelectFigure
        {
            get { return _selectFigure; }
            set { _selectFigure = value; }
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
