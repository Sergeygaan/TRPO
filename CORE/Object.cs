using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PaintedObjectsMoving {
	class Object : ICloneable
    {
		private GraphicsPath _path;
        private Pen _pen;
        private PointF[] _pointSelect;
        private PointF _figureStart = new Point();                          //стартовая точка фигуры
        private PointF _figureEnd = new Point();                            //конечная точка фигуры
        private bool _selectFigure = false;
        private int _idFigure;
        private SolidBrush _brush = null;

        private MainForm.FigureType _currentfigure;

        List<SupportObject> _supportFigures = new List<SupportObject>();

        public int IdFigure
        {
            get { return _idFigure; }
            set { _idFigure = value; }
        }

        public GraphicsPath Path {
			get { return _path; }
			set { _path = value; }
        }

        //Добавить опорную фигуру в список
        public void AddListFigure(SupportObject AddFigure)
        {
            _supportFigures.Add(AddFigure);
        }

        public void EditListFigure(int index,Rectangle Rectangles)
        {
            _supportFigures[index].Path.Reset();

            _supportFigures[index].Path.AddEllipse(Rectangles);
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

        public PointF FigureStart
        {
            get { return _figureStart; }
            set { _figureStart = value; }
        }

        public PointF FigureEnd
        {
            get { return _figureEnd; }
            set { _figureEnd = value; }
        }

        public SolidBrush Brush
        {
            get { return _brush; }
            set { _brush = value; }
        }
        public Pen @Pen {
			get { return _pen; }
			set { _pen = value; }
		}
		public Object(Pen pen, GraphicsPath path, SolidBrush brush, MainForm.FigureType CurrentFigure) {
            _path = path;
            _pen = pen;
            _brush = brush;
            _currentfigure = CurrentFigure;

        }

		#region ICloneable Members

		public object Clone() {

			return new Object(this.Pen, this.Path.Clone() as GraphicsPath, _brush, _currentfigure);
		}

        public Object CloneObject()
        {
            //Pen CopiPen = new Pen(_pen.Color);
            return new Object(this.Pen, this.Path.Clone() as GraphicsPath, _brush, _currentfigure);
        }

        #endregion
    }
}
